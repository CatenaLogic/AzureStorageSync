namespace AzureStorageSync.Azure
{
    using System;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Logging;
    using MethodTimer;
    using Microsoft.WindowsAzure.Storage;

    public class Uploader
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly CloudStorageAccount _storageAccount;

        public Uploader(CloudStorageAccount storageAccount)
        {
            ArgumentNullException.ThrowIfNull(storageAccount);

            _storageAccount = storageAccount;
        }

        [Time("File: {fileDescriptor}")]
        public async Task UploadAsync(FileDescriptor fileDescriptor)
        {
            ArgumentNullException.ThrowIfNull(fileDescriptor);

            Log.Info("Uploading '{0}'", fileDescriptor);

            var blob = await _storageAccount.GetBlobAsync(fileDescriptor.RemoteFileName);

            using (var fileStream = System.IO.File.OpenRead(fileDescriptor.LocalFileName))
            {
                blob.Properties.ContentMD5 = fileDescriptor.LocalFileHash;

                await blob.UploadFromStreamAsync(fileStream);
            }
        }
    }
}
