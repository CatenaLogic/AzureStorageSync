namespace AzureStorageSync.Tests
{
    using NUnit.Framework;

    public static class StringExtensions
    {
        [TestFixture]
        public class TheGetContainerNameMethod
        {
            [TestCase("MyContainer/myfile.txt", "mycontainer")]
            [TestCase("MyContainer/mydirectory/myfile.txt", "mycontainer")]
            public void ReturnsValidContainer(string input, string expectedOutput)
            {
                var containerName = input.GetContainerName();

                Assert.AreEqual(expectedOutput, containerName);
            }
        }

        [TestFixture]
        public class TheGetDirectoryNameMethod
        {
            [TestCase("mycontainer/myfile.txt", "")]
            [TestCase("mycontainer/mydirectory/myfile.txt", "mydirectory")]
            [TestCase("mycontainer/mydirectory/mysubdirectory/myfile.txt", "mydirectory/mysubdirectory")]
            public void ReturnsValidContainer(string input, string expectedOutput)
            {
                var containerName = input.GetDirectoryName();

                Assert.AreEqual(expectedOutput, containerName);
            }
        }

        [TestFixture]
        public class TheGetBlobNameMethod
        {
            [TestCase("/mycontainer/myfile.txt", "myfile.txt")]
            [TestCase("/mycontainer/mydirectory/myfile.txt", "myfile.txt")]
            public void ReturnsValidBlobName(string input, string expectedOutput)
            {
                var blobName = input.GetBlobName();

                Assert.AreEqual(expectedOutput, blobName);
            }
        }

        [TestFixture]
        public class TheGetCloudStorageCompatibleStringMethod
        {
            [TestCase("mycontainer/myfile.txt", "/mycontainer/myfile.txt")]
            [TestCase("/mycontainer/myfile.txt", "/mycontainer/myfile.txt")]
            [TestCase("\\mycontainer\\mydirectory/myfile.txt", "/mycontainer/mydirectory/myfile.txt")]
            public void ReturnsCompatibleString(string input, string expectedOutput)
            {
                var compatibleString = input.GetCloudStorageCompatibleString();

                Assert.AreEqual(expectedOutput, compatibleString);
            }
        }
    }
}