// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DifferenceCalculator.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace AzureStorageSync.Azure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Collections;
    using Catel.Logging;
    using Catel.Threading;
    using MethodTimer;
    using Microsoft.WindowsAzure.Storage;

    public class DifferenceCalculator
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly Context _context;
        private readonly CloudStorageAccount _storageAccount;

        public DifferenceCalculator(Context context, CloudStorageAccount storageAccount)
        {
            Argument.IsNotNull(() => context);
            Argument.IsNotNull(() => storageAccount);

            _context = context;
            _storageAccount = storageAccount;
        }

        [Time]
        public async Task<List<FileDescriptor>> GetFileDescriptorsAsync()
        {
            var descriptors = new List<FileDescriptor>();

            //var taskCreators = new List<Func<Task<FileDescriptor>>>();
            //var tasks = new List<Task<FileDescriptor>>();

            // Local => Remote
            var files = Directory.GetFiles(_context.LocalDirectory, "*.*", SearchOption.AllDirectories);
            foreach (var fileName in files)
            {
                var fileDescriptor = await CalculateMd5AndCompareWithRemoteAsync(fileName);
                if (fileDescriptor != null)
                {
                    descriptors.Add(fileDescriptor);
                }

                //taskCreators.Add(() => CalculateMd5AndCompareWithRemoteAsync(fileName));
            }

            // Remote => Local
            // TODO: Not yet implemented

            // Multithread (do we want a max number of threads?, see progress of https://github.com/Catel/Catel/issues/1168)
            //foreach (var taskCreator in taskCreators)
            //{
            //    var task = TaskHelper.Run(taskCreator);
            //    tasks.Add(task);
            //}

            //await Task.WhenAll(tasks);

            //foreach (var task in tasks)
            //{
            //    var fileDescription = task.Result;
            //    if (fileDescription != null)
            //    {
            //        descriptors.Add(fileDescription);
            //    }
            //}

            return descriptors;
        }

#if DEBUG
        [Time("File: {fileName}")]
#endif
        private async Task<FileDescriptor> CalculateMd5AndCompareWithRemoteAsync(string fileName)
        {
            // Note that this method makes the path lower-case, we might need to fix that in the future
            var relativePath = Catel.IO.Path.GetRelativePath(fileName, _context.LocalDirectory);
            var remoteFileName = Path.Combine(_context.RemoteDirectory, relativePath).GetCloudStorageCompatibleString();

            var localHash = await Md5HashHelper.GetMd5HashAsync(fileName);

            var fileDescriptor = await GetFileDescriptionAsync(fileName, remoteFileName, localHash);
            return fileDescriptor;
        }

#if DEBUG
        //[Time("File: {fileName}")]
#endif
        private async Task<FileDescriptor> GetFileDescriptionAsync(string fileName, string remoteFileName, string localHash)
        {
            if (!await RemoteFileExistsAsync(remoteFileName, localHash))
            {
                var fileDescriptor = new FileDescriptor(fileName, localHash, remoteFileName, FileAction.Upload);
                return fileDescriptor;
            }

            return null;
        }

        private async Task<bool> RemoteFileExistsAsync(string remoteFileName, string localHash)
        {
            var blob = _storageAccount.GetBlob(remoteFileName);
            if (!blob.Exists())
            {
                return false;
            }

            try
            {
                await blob.FetchAttributesAsync();

                var remoteHash = blob.Properties.ContentMD5;

                var exists = string.Equals(localHash, remoteHash, StringComparison.OrdinalIgnoreCase);
                return exists;
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Failed to fetch attributes, assuming file '{0}' does not exist", remoteFileName);
                return false;
            }
        }
    }
}