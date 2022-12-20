namespace AzureStorageSync.Azure
{
    using System;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Logging;
    using MethodTimer;
    using Microsoft.WindowsAzure.Storage;

    public class Downloader
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly CloudStorageAccount _storageAccount;

        public Downloader(CloudStorageAccount storageAccount)
        {
            ArgumentNullException.ThrowIfNull(storageAccount);

            _storageAccount = storageAccount;
        }

        [Time("File: {fileDescriptor}")]
        public async Task DownloadAsync(FileDescriptor fileDescriptor)
        {
            ArgumentNullException.ThrowIfNull(fileDescriptor);

            Log.Info("Downloading '{0}'", fileDescriptor);


            Log.Warning("Downloading is not yet implemented, feel free to PR");
        }
    }
}
