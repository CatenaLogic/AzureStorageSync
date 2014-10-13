// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelpWriter.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


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
            writer("================");
            writer(string.Empty);
        }

        public static void WriteHelp(Action<string> writer)
        {
            const string message = @"AzureStorageSync let's users synchronize a local directory with an Azure storage container via the command line.

AzureStorageSync [localPath] [remotePath] -c

    solutionPath       The directory containing the solution with the pdb files.
    -u [url]           Url to remote git repository.
    -c [config]        Name of the configuration, default value is 'Release'.
    -b [branch]        Name of the branch to use on the remote repository.
    -l [file]          The log file to write to.
    -s [shaHash]       The SHA-1 hash of the commit.
";
            writer(message);
        }
    }
}