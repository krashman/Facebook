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
    public interface IUserProfileRepository : IDocumentDatabaseRepository<UserProfile>
    {
        Task UploadFile(Stream fileStream, string fileName, string formFileContentType, string identityId);
    }

    public class UserProfileRepository : DocumentDatabaseRepository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(IOptions<ApplicationSettings> applicationSettingsOptions) : base(applicationSettingsOptions)
        {
        }

        public async Task UploadFile(Stream fileStream, string fileName, string formFileContentType, string identityId)
        {
            // TODO: Move to appsettings.json
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.DevelopmentStorageAccount;

                // Create a blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Get a reference to a container named "profile-pictures."
                CloudBlobContainer container = blobClient.GetContainerReference("profile-pictures");
                

                await container.CreateIfNotExistsAsync();
                if (container.Properties.PublicAccess != BlobContainerPublicAccessType.Blob)
                {
                    await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                }

                // Get a reference to a blob named "profile-pictures".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
                blockBlob.Properties.ContentType = formFileContentType;
                blockBlob.Metadata.Add("UserId", identityId);
                await blockBlob.UploadFromStreamAsync(fileStream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected override string CollectionId { get; } = nameof(UserProfile);
    }
}
