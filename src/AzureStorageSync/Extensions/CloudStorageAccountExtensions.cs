// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CloudStorageAccountExtensions.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace AzureStorageSync
{
    using Catel;
    using Catel.Logging;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public static class CloudStorageAccountExtensions
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public static CloudBlockBlob GetBlob(this CloudStorageAccount storageAccount, string path)
        {
            Argument.IsNotNull(() => storageAccount);
            Argument.IsNotNullOrWhitespace(() => path);

            var blobClient = storageAccount.CreateCloudBlobClient();

            var containerName = path.GetContainerName();
            var directoryName = path.GetDirectoryName();
            var blobName = path.GetBlobName();

            var container = blobClient.GetContainerReference(containerName);
            if (!container.Exists())
            {
                Log.Info("Creating container '{0}'", container);

                container.CreateIfNotExists(BlobContainerPublicAccessType.Container);
            }

            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            var directory = container.GetDirectoryReference(directoryName);
            //foreach (var subDirectory in directoryName.Split(new[] { '/' }))
            //{
            //    directory = directory.GetDirectoryReference(subDirectory);
            //    if (directory)
            //}

            var blockBlob = directory.GetBlockBlobReference(blobName);
            return blockBlob;
        }
    }
}