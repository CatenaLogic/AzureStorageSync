// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileInfo.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace AzureStorageSync
{
    using System.Diagnostics;

    [DebuggerDisplay("{LocalFileName} => {RemoteFileName}")]
    public class FileDescriptor
    {
        public FileDescriptor(string localFileName, string remoteFileName, FileAction action)
        {
            LocalFileName = localFileName;
            RemoteFileName = remoteFileName;
            Action = action;
        }

        public string LocalFileName { get; private set; }

        public string RemoteFileName { get; private set; }

        public FileAction Action { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} => {1}", LocalFileName, RemoteFileName);
        }
    }
}