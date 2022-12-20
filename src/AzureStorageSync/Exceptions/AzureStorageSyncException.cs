namespace AzureStorageSync
{
    using System;

    public class AzureStorageSyncException : Exception
    {
        public AzureStorageSyncException(string message)
            : base(message)
        {
        }
    }
}