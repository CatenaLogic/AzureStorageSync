﻿namespace AzureStorageSync
{
    using System.Diagnostics;

    [DebuggerDisplay("{LocalFileName} => {RemoteFileName}")]
    public class FileDescriptor
    {
        public FileDescriptor(string localFileName, string localFileHash, string remoteFileName, FileAction action)
        {
            LocalFileName = localFileName;
            LocalFileHash = localFileHash;
            RemoteFileName = remoteFileName;
            Action = action;
        }

        public string LocalFileName { get; private set; }

        public string LocalFileHash { get; private set; }

        public string RemoteFileName { get; private set; }

        public FileAction Action { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} => {1}", LocalFileName, RemoteFileName);
        }
    }
}