﻿namespace AzureStorageSync
{
    using Catel;

    public static class StringExtensions
    {
        public static string GetContainerName(this string remoteFileName)
        {
            Argument.IsNotNullOrWhitespace(() => remoteFileName);

            var containerName = remoteFileName.GetCloudStorageCompatibleString();

            var lastSlashIndex = containerName.IndexOf('/', 1);
            if (lastSlashIndex != -1)
            {
                containerName = containerName.Substring(0, lastSlashIndex);
            }

            containerName = containerName.RemoveSurroundingSlashes();

            return containerName.ToLower();
        }

        public static string GetDirectoryName(this string remoteFileName)
        {
            Argument.IsNotNullOrWhitespace(() => remoteFileName);

            var directoryName = remoteFileName.GetCloudStorageCompatibleString();
            directoryName = directoryName.RemoveSurroundingSlashes();
            if (!directoryName.Contains("/"))
            {
                return string.Empty;
            }

            var containerName = remoteFileName.GetContainerName();
            directoryName = directoryName.Substring(containerName.Length + 1);

            var blobName = remoteFileName.GetBlobName();
            directoryName = directoryName.Substring(0, directoryName.Length - blobName.Length);

            directoryName = directoryName.RemoveSurroundingSlashes();

            return directoryName;
        }

        public static string GetBlobName(this string remoteFileName)
        {
            Argument.IsNotNullOrWhitespace(() => remoteFileName);

            var fileName = remoteFileName.GetCloudStorageCompatibleString();

            var lastSlashIndex = fileName.LastIndexOf('/');
            if (lastSlashIndex != -1)
            {
                fileName = fileName.Substring(lastSlashIndex + 1);
            }

            fileName = fileName.RemoveSurroundingSlashes();

            return fileName;
        }

        public static string GetCloudStorageCompatibleString(this string input)
        {
            input = input.Replace("\\", "/");

            if (!input.StartsWith("/"))
            {
                input = string.Format("/{0}", input);
            }

            // Make lowercase: this might break it for other consumers of this app, but it looks like
            // everything for us is lower-case in azure
            input = input.ToLower();

            return input;
        }

        public static string RemoveSurroundingSlashes(this string input)
        {
            if (input.StartsWith("/"))
            {
                input = input.Substring(1);
            }

            if (input.EndsWith("/"))
            {
                input = input.Substring(0, input.Length - 1);
            }

            return input;
        }
    }
}