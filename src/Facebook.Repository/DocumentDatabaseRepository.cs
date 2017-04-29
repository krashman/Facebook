
using System;
using System.Threading.Tasks;
using Facebook.Domain;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


namespace Facebook.Repository
{
    public interface IDocumentDatabaseRepository<T> where T : class
    {
        Task<T> GetItemAsync(string id);
    }

    public class DocumentDatabaseRepository<T> : IDocumentDatabaseRepository<T> where T : class
    {
        private readonly string _databaseId;
        private readonly string _collectionId;
        private readonly DocumentClient _documentClient;

        public DocumentDatabaseRepository(IOptions<ApplicationSettings> applicationSettingsOptions)
        {
            var applicationSettings = applicationSettingsOptions.Value;
            _databaseId = applicationSettings.DocumentDatabaseName;
            _collectionId = applicationSettings.Collection;
            _documentClient = new DocumentClient(new Uri(applicationSettings.DocumentDatabaseEndpoint), applicationSettings.DocumentDatabaseAuthorizationKey, new ConnectionPolicy
            {
                EnableEndpointDiscovery = false
            });
        }


        public async Task<T> GetItemAsync(string id)
        {
            try
            {
                Document document = await _documentClient.ReadDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, id));
                return (T)(dynamic)document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }
    }
}