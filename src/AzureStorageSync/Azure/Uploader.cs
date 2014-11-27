// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Uploader.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace AzureStorageSync.Azure
{
    using Catel;
    using Catel.Logging;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class Uploader
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly CloudStorageAccount _storageAccount;

        public Uploader(CloudStorageAccount storageAccount)
        {
            Argument.IsNotNull(() => storageAccount);

            _storageAccount = storageAccount;
        }

        public void Upload(FileDescriptor fileDescriptor)
        {
            Argument.IsNotNull(() => fileDescriptor);

            Log.Info("Uploading '{0}'", fileDescriptor);

            var blob = _storageAccount.GetBlob(fileDescriptor.RemoteFileName);

            using (var fileStream = System.IO.File.OpenRead(fileDescriptor.LocalFileName))
            {
                blob.Properties.ContentMD5 = fileDescriptor.LocalFileHash;
                blob.UploadFromStream(fileStream, options: new BlobRequestOptions());
            }
        }
    }
}