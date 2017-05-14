
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
        Task<Document> UpdateItemAsync(string id, T value);
        Task<IEnumerable<T>> GetItemsWhereAsync(Expression<Func<T, bool>> wherePredicate);
        Task<IEnumerable<T>> GetAllItemsAsync();
    }

    public abstract class DocumentDatabaseRepository<T> : IDocumentDatabaseRepository<T> where T : class
    {
        protected readonly string DatabaseId;
        protected abstract string CollectionId { get; }
        protected readonly DocumentClient DocumentClient;

        protected DocumentDatabaseRepository(IOptions<ApplicationSettings> applicationSettingsOptions)
        {
            var applicationSettings = applicationSettingsOptions.Value;
            DatabaseId = applicationSettings.DocumentDatabaseName;
            DocumentClient = new DocumentClient(new Uri(applicationSettings.DocumentDatabaseEndpoint), applicationSettings.DocumentDatabaseAuthorizationKey, new ConnectionPolicy
            {
                EnableEndpointDiscovery = false
            });
            Initialize();
        }

        protected async void Initialize()
        {
            await CreateDatabaseIfNotExistsAsync();
            await CreateCollectionIfNotExistsAsync();
        }

        public virtual async Task<Document> CreateItemAsync(T item)
        {
            return await DocumentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), item);
        }


        public virtual async Task<T> GetItemAsync(string id)
        {
            try
            {
                Document document = await DocumentClient.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
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
                await DocumentClient.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await DocumentClient.CreateDatabaseAsync(new Database { Id = DatabaseId });
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
                await DocumentClient.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await DocumentClient.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(DatabaseId),
                        new DocumentCollection { Id = CollectionId },
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
            return await DocumentClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
        }

        public async Task<Document> UpdateItemAsync(string id, T value)
        {
            return
                await DocumentClient.ReplaceDocumentAsync(
                    UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id), value);
        }

        public async Task<IEnumerable<T>> GetAllItemsAsync()
        {
            return await CreateDocumentQuery().ExecuteQuery<T>();

        }

        public virtual async Task<IEnumerable<T>> GetItemsWhereAsync(Expression<Func<T, bool>> wherePredicate)
        {
            return await CreateDocumentQuery().Where(wherePredicate).ExecuteQuery();
        }

        private IOrderedQueryable<T> CreateDocumentQuery()
        {
            return
                DocumentClient.CreateDocumentQuery<T>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
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