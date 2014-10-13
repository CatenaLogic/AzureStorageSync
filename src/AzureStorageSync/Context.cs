// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Context.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace AzureStorageSync
{
    using Catel.Logging;

    public class Context
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public Context()
        {

        }

        public bool IsHelp { get; set; }

        public string LogFile { get; set; }

        public string LocalDirectory { get; set; }

        public string RemoteDirectory { get; set; }

        public string ConnectionString { get; set; }

        public void ValidateContext()
        {
            if (string.IsNullOrEmpty(LocalDirectory))
            {
                Log.ErrorAndThrowException<AzureStorageSyncException>("Local directory is missing");
            }

            if (string.IsNullOrEmpty(RemoteDirectory))
            {
                Log.ErrorAndThrowException<AzureStorageSyncException>("Remote directory is missing");
            }

            if (string.IsNullOrEmpty(ConnectionString))
            {
                Log.ErrorAndThrowException<AzureStorageSyncException>("Connection string name is missing");
            }
        }
    }
}