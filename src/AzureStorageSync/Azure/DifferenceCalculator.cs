// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DifferenceCalculator.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace AzureStorageSync.Azure
{
    using System.Collections.Generic;
    using System.IO;
    using Catel;
    using Microsoft.WindowsAzure.Storage;

    public class DifferenceCalculator
    {
        private readonly Context _context;
        private readonly CloudStorageAccount _storageAccount;

        public DifferenceCalculator(Context context, CloudStorageAccount storageAccount)
        {
            Argument.IsNotNull(() => context);
            Argument.IsNotNull(() => storageAccount);

            _context = context;
            _storageAccount = storageAccount;
        }

        public List<FileDescriptor> GetFileDescriptors()
        {
            var descriptors = new List<FileDescriptor>();

            // Local => Remote
            var files = Directory.GetFiles(_context.LocalDirectory, "*.*", SearchOption.AllDirectories);
            foreach (var fileName in files)
            {
                // Note that this method makes the path lower-case, we might need to fix that in the future
                var relativePath = Catel.IO.Path.GetRelativePath(fileName, _context.LocalDirectory);
                var remoteFileName = Path.Combine(_context.RemoteDirectory, relativePath).GetCloudStorageCompatibleString();

                if (!RemoteFileExists(remoteFileName))
                {
                    descriptors.Add(new FileDescriptor(fileName, remoteFileName, FileAction.Upload));
                }
            }

            // Remote => Local
            // TODO: Not yet implemented

            return descriptors;
        }

        private bool RemoteFileExists(string remoteFileName)
        {
            var blob = _storageAccount.GetBlob(remoteFileName);
            if (!blob.Exists())
            {
                return false;
            }

            // As long as we cannot calculate the md5 hash, we need to return false every time
            return false;

            //blob.FetchAttributes();
            //return true;
        }
    }
}