// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Downloader.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace AzureStorageSync.Azure
{
    using Catel;
    using Catel.Logging;
    using Microsoft.WindowsAzure.Storage;

    public class Downloader
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly CloudStorageAccount _storageAccount;

        public Downloader(CloudStorageAccount storageAccount)
        {
            Argument.IsNotNull(() => storageAccount);

            _storageAccount = storageAccount;
        }

        public void Download(FileDescriptor fileDescriptor)
        {
            Argument.IsNotNull(() => fileDescriptor);

            Log.Info("Downloading '{0}'", fileDescriptor);


            Log.Warning("Downloading is not yet implemented, feel free to PR");
        }
    }
}