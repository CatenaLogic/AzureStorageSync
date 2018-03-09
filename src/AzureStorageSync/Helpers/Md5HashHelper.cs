// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Md5HashHelper.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace AzureStorageSync
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    public static class Md5HashHelper
    {
#if DEBUG
        //[Time("File: {fileName}")]
#endif
        public static async Task<string> GetMd5HashAsync(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                // Note: this is the way azure calculates the md5 hash, see http://blogs.msdn.com/b/windowsazurestorage/archive/2011/02/18/windows-azure-blob-md5-overview.aspx
                using (var stream = File.OpenRead(fileName))
                {
                    var buffer = new byte[8192];
                    int read;

                    while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) == buffer.Length)
                    {
                        md5.TransformBlock(buffer, 0, read, buffer, 0);
                    }

                    md5.TransformFinalBlock(buffer, 0, read);

                    var hashBytes = md5.Hash;
                    var hash = Convert.ToBase64String(hashBytes);
                    return hash;
                }
            }
        }
    }
}