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

    public static class Md5HashHelper
    {
        public static string GetMd5Hash(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                // Note: this is the way azure calculates the md5 hash, see http://blogs.msdn.com/b/windowsazurestorage/archive/2011/02/18/windows-azure-blob-md5-overview.aspx
                var fileData = File.ReadAllBytes(fileName);

                md5.TransformFinalBlock(fileData, 0, fileData.Length);

                var hashBytes = md5.Hash;
                var hash = Convert.ToBase64String(hashBytes);

                return hash;
            }
        }
    }
}