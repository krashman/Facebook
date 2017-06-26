﻿using System;
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
        Task UploadFile(Stream fileStream, string fileName, string formFileContentType);
    }

    public class ProfilePictureRepository : IProfilePictureRepository
    {
        private readonly IOptions<ApplicationSettings> _applicationSettingsOptions;

        public ProfilePictureRepository(IOptions<ApplicationSettings> applicationSettingsOptions)
        {
            _applicationSettingsOptions = applicationSettingsOptions;
        }

        public async Task UploadFile(Stream fileStream, string fileName, string formFileContentType)
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
