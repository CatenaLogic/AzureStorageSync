namespace AzureStorageSync.Tests
{
    using NUnit.Framework;

    public class ContextFacts
    {
        [TestFixture]
        public class TheDefaultValues
        {
            [TestCase]
            public void SetsRightDefaultValues()
            {
                var context = new Context();

                //Assert.AreEqual("Release", context.ConfigurationName);
                Assert.IsFalse(context.IsHelp);
            }
        }

        [TestFixture]
        public class TheValidateContextMethod
        {
            [TestCase]
            public void ThrowsExceptionForMissingLocalDirectory()
            {
                var context = new Context();

                Assert.Throws<AzureStorageSyncException>(() => context.ValidateContext());
            }

            [TestCase]
            public void ThrowsExceptionForMissingRemoteDirectory()
            {
                var context = new Context()
                {
                    LocalDirectory = @"c:\source\",
                };

                Assert.Throws<AzureStorageSyncException>(() => context.ValidateContext());
            }

            [TestCase]
            public void ThrowsExceptionForMissingConnectionString()
            {
                var context = new Context()
                {
                    LocalDirectory = @"c:\source\",
                    RemoteDirectory = @"/remote/mydir"
                };

                Assert.Throws<AzureStorageSyncException>(() => context.ValidateContext());
            }

            [TestCase]
            public void SucceedsForValidContext()
            {
                var context = new Context()
                {
                    LocalDirectory = @"c:\source\",
                    RemoteDirectory = @"/remote/mydir",
                    ConnectionString = "someconnectionstring"
                };

                // should not throw
                context.ValidateContext();
            }
        }
    }
}
