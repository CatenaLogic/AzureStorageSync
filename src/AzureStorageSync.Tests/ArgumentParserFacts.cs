// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentParserFacts.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace AzureStorageSync.Test
{
    using Catel.Test;
    using NUnit.Framework;

    [TestFixture]
    public class ArgumentParserFacts
    {
        [TestCase]
        public void ThrowsExceptionForEmptyParameters()
        {
            ExceptionTester.CallMethodAndExpectException<AzureStorageSyncException>(() => ArgumentParser.ParseArguments(string.Empty));
        }

        [TestCase]
        public void CorrectlyParsesLogFilePath()
        {
            var context = ArgumentParser.ParseArguments("localDirectory remoteDirectory -c connectionString -l logFilePath");

            Assert.AreEqual("localDirectory", context.LocalDirectory);
            Assert.AreEqual("logFilePath", context.LogFile);
        }

        [TestCase]
        public void CorrectlyParsesHelp()
        {
            var context = ArgumentParser.ParseArguments("-h");

            Assert.IsTrue(context.IsHelp);
        }

        [TestCase]
        public void CorrectlyParsesLocalDirectory()
        {
            var context = ArgumentParser.ParseArguments("localDirectory remoteDirectory -c connectionString");

            Assert.AreEqual("localDirectory", context.LocalDirectory);
        }

        [TestCase]
        public void CorrectlyParsesRemoteDirectory()
        {
            var context = ArgumentParser.ParseArguments("localDirectory remoteDirectory -c connectionString");

            Assert.AreEqual("remoteDirectory", context.RemoteDirectory);
        }

        [TestCase]
        public void CorrectlyParsesConnectionString()
        {
            var context = ArgumentParser.ParseArguments("localDirectory remoteDirectory -c connectionString");

            Assert.AreEqual("connectionString", context.ConnectionString);
        }

        [TestCase]
        public void ThrowsExceptionForInvalidNumberOfArguments()
        {
            ExceptionTester.CallMethodAndExpectException<AzureStorageSyncException>(() => ArgumentParser.ParseArguments("localDirectory -l logFilePath extraArg"));
        }

        [TestCase]
        public void ThrowsExceptionForUnknownArgument()
        {
            ExceptionTester.CallMethodAndExpectException<AzureStorageSyncException>(() => ArgumentParser.ParseArguments("localDirectory -x logFilePath"));
        }
    }
}