namespace AzureStorageSync
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Azure;
    using Catel;
    using Catel.Logging;
    using Microsoft.WindowsAzure.Storage;

    public static class Synchronizer
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public static async Task<int> SyncAsync(Context context)
        {
            ArgumentNullException.ThrowIfNull(context);

            var storageAccount = CloudStorageAccount.Parse(context.ConnectionString);

            Log.Info("Calculating differences");

            var differenceCalculator = new DifferenceCalculator(context, storageAccount);
            var fileDescriptors = await differenceCalculator.GetFileDescriptorsAsync();
            if (fileDescriptors.Count == 0)
            {
                Log.Info("Found no differences, synchronization is not required");
                return 0;
            }

            Log.Info($"Found {fileDescriptors.Count} differences, starting synchronization");

            var uploader = new Uploader(storageAccount);
            var downloader = new Downloader(storageAccount);

            var tasks = new List<Task>();

            foreach (var fileDescriptor in fileDescriptors)
            {
                switch (fileDescriptor.Action)
                {
                    case FileAction.Upload:
                        tasks.Add(uploader.UploadAsync(fileDescriptor));
                        break;

                    case FileAction.Download:
                        tasks.Add(downloader.DownloadAsync(fileDescriptor));
                        break;

                    case FileAction.Ignore:
                        Log.Debug($"Ignoring file '{fileDescriptor}'");
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            await Task.WhenAll(tasks);

            return 0;
        }
    }
}
