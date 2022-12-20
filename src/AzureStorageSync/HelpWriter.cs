namespace AzureStorageSync
{
    using System;
    using Catel.Reflection;

    public static class HelpWriter
    {
        public static void WriteAppHeader(Action<string> writer)
        {
            var assembly = typeof (HelpWriter).Assembly;

            writer(string.Format("{0} v{1}", assembly.Title(), assembly.Version()));
            writer("=========================");
            writer(string.Empty);
        }

        public static void WriteHelp(Action<string> writer)
        {
            const string message = @"AzureStorageSync let's users synchronize a local directory with an Azure storage container via the command line.

AzureStorageSync [localPath] [remotePath] -c

    localPath              The local base directory.
    remotePath             The remote base directory where the first 'directory' will be treated as blob container name.
    -u [url]               Url to remote git repository.
    -c [connectionString]  The Azure storage connection string.
    -l [file]              The log file to write to.
";
            writer(message);
        }
    }
}