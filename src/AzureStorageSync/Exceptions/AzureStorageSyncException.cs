// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureStorageSyncException.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


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