
using System;
using System.Runtime.CompilerServices;
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
        Task<Document> CreateItemAsync(T item);
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
            Initialize();


        }

        private async void Initialize()
        {
            await CreateDatabaseIfNotExistsAsync();
            await CreateCollectionIfNotExistsAsync();
        }

        public async Task<Document> CreateItemAsync(T item)
        {
            return await _documentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId), item);
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


        //TODO: Prob move somewhere in the pipeline where everything is really setup, so no need to call these checks
        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await _documentClient.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(_databaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await _documentClient.CreateDatabaseAsync(new Database { Id = _databaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await _documentClient.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await _documentClient.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(_databaseId),
                        new DocumentCollection { Id = _collectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }
    }
}