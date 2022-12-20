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
                throw Log.ErrorAndCreateException<AzureStorageSyncException>("Local directory is missing");
            }

            if (string.IsNullOrEmpty(RemoteDirectory))
            {
                throw Log.ErrorAndCreateException<AzureStorageSyncException>("Remote directory is missing");
            }

            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw Log.ErrorAndCreateException<AzureStorageSyncException>("Connection string name is missing");
            }
        }
    }
}