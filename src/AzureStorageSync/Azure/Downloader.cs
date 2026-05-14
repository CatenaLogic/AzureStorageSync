namespace AzureStorageSync.Azure
{
    using System;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Logging;
    using MethodTimer;
    using Microsoft.Extensions.Logging;
    using Microsoft.WindowsAzure.Storage;

    public class Downloader
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(Downloader));

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

            Logger.LogInformation("Downloading '{0}'", fileDescriptor);


            Logger.LogWarning("Downloading is not yet implemented, feel free to PR");
        }
    }
}
