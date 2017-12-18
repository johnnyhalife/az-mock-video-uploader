using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzMockVideoUploader
{
    class Program
    {
        private static readonly string MediaFile = @"Media\BigBuckBunny.mp4";

        private static readonly string AzStorageConnectionString = 
                ConfigurationManager.AppSettings["AzStorageConnectionString"];

        private static readonly string AzStorageContainerName =
                ConfigurationManager.AppSettings["AzStorageContainerName"];

        public static void Main(string[] args)
        {
            // Get the Az Storge Account
            var storageAccount = CloudStorageAccount.Parse(AzStorageConnectionString);

            // Create the blob client to interact with Storage Account
            var blobClient = storageAccount.CreateCloudBlobClient();

            // Get a container reference 
            var container = blobClient.GetContainerReference(AzStorageContainerName);

            // Ensure that the container exists
            container.CreateIfNotExists();

            // Retrieve reference to a blob named "BigBuckBunny".
            var blockBlob = container.GetBlockBlobReference("BigBuckBunny");

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(MediaFile))
            {
                blockBlob.UploadFromStream(fileStream);
            }
        }
    }
}
