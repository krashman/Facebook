
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Facebook.Domain;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Extensions.Options;


namespace Facebook.Repository
{
    public interface IDocumentDatabaseRepository<T> where T : class
    {
        Task<T> GetItemAsync(string id);
        Task<Document> CreateItemAsync(T item);
        Task<Document> DeleteItemAsync(string id);
        Task<Document> UpdateItemAsync(string id, Post value);
        Task<IEnumerable<T>> GetItemsWhereAsync(Expression<Func<T, bool>> wherePredicate);
        Task<IEnumerable<T>> GetAllItemsAsync();
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


        //TODO: Prob move somewhere in the pipeline wherePredicate everything is really setup, so no need to call these checks
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

        public async Task<Document> DeleteItemAsync(string id)
        {
            return await _documentClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, id));
        }

        public async Task<Document> UpdateItemAsync(string id, Post value)
        {
            return
                await _documentClient.ReplaceDocumentAsync(
                    UriFactory.CreateDocumentUri(_databaseId, _collectionId, id), value);
        }

        public async Task<IEnumerable<T>> GetAllItemsAsync()
        {
            return await CreateDocumentQuery().ExecuteQuery<T>();

        }
        
        public async Task<IEnumerable<T>> GetItemsWhereAsync(Expression<Func<T, bool>> wherePredicate)
        {
            return await CreateDocumentQuery().Where(wherePredicate).ExecuteQuery();
        }

        private IOrderedQueryable<T> CreateDocumentQuery()
        {
            return
                _documentClient.CreateDocumentQuery<T>(
                    UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId),
                    new FeedOptions() { MaxItemCount = -1 });
        }
    }

    public static class DocumentQueryExtensions
    {
        public static async Task<List<T>> ExecuteQuery<T>(this IQueryable<T> query) where T : class
        {
            var documentQuery = query.AsDocumentQuery();
            var results = new List<T>();
            while (documentQuery.HasMoreResults)
            {
                results.AddRange(await documentQuery.ExecuteNextAsync<T>());
            }
            return results;
        }

    }
}