namespace AzureStorageSync
{
    using Catel.Logging;
    using Microsoft.Extensions.Logging;

    public class Context
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(Context));

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
                throw Logger.LogErrorAndCreateException<AzureStorageSyncException>("Local directory is missing");
            }

            if (string.IsNullOrEmpty(RemoteDirectory))
            {
                throw Logger.LogErrorAndCreateException<AzureStorageSyncException>("Remote directory is missing");
            }

            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw Logger.LogErrorAndCreateException<AzureStorageSyncException>("Connection string name is missing");
            }
        }
    }
}
