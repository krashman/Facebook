using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Facebook.Domain;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
namespace Facebook.Repository
{
    public interface IProfilePictureRepository
    {
        Task UploadFile(string fileName, Stream fileStream);
    }

    public class ProfilePictureRepository : IProfilePictureRepository
    {
        private readonly IOptions<ApplicationSettings> _applicationSettingsOptions;

        public ProfilePictureRepository(IOptions<ApplicationSettings> applicationSettingsOptions)
        {
            _applicationSettingsOptions = applicationSettingsOptions;
        }

        public async Task UploadFile(string fileName, Stream fileStream)
        {
            // TODO: Move to appsettings.json
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.DevelopmentStorageAccount;

                // Create a blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Get a reference to a container named "mycontainer."
                CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

                await container.CreateIfNotExistsAsync();

                // Get a reference to a blob named "myblob".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
                await blockBlob.UploadFromStreamAsync(fileStream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
