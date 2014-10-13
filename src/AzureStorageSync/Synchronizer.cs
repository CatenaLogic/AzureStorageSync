// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Synchronizer.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace AzureStorageSync
{
    using System;
    using Azure;
    using Catel;
    using Catel.Logging;
    using Microsoft.WindowsAzure.Storage;

    public static class Synchronizer
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public static int Sync(Context context)
        {
            Argument.IsNotNull(() => context);

            var storageAccount = CloudStorageAccount.Parse(context.ConnectionString);

            var differenceCalculator = new DifferenceCalculator(context, storageAccount);
            var fileDescriptors = differenceCalculator.GetFileDescriptors();
            if (fileDescriptors.Count == 0)
            {
                Log.Info("Found no differences, synchronization is not required");
                return 0;
            }

            Log.Info("Found {0} differences, starting synchronization", fileDescriptors.Count);

            var uploader = new Uploader(storageAccount);
            var downloader = new Downloader(storageAccount);

            foreach (var fileDescriptor in fileDescriptors)
            {
                switch (fileDescriptor.Action)
                {
                    case FileAction.Upload:
                        uploader.Upload(fileDescriptor);
                        break;

                    case FileAction.Download:
                        downloader.Download(fileDescriptor);
                        break;

                    case FileAction.Ignore:
                        Log.Debug("Ignoring file '{0}'", fileDescriptor);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return 0;
        }
    }
}