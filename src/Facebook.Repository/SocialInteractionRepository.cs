using System;
using System.Linq;
using System.Threading.Tasks;
using Facebook.Domain;
using Microsoft.Extensions.Options;

namespace Facebook.Repository
{
    public interface ISocialInteractionsRepository : IDocumentDatabaseRepository<SocialInteractions>
    {
        Task<SocialInteractions> GetItemByPostId(Guid postId);
    }

    public class SocialInteractionRepository : DocumentDatabaseRepository<SocialInteractions>, ISocialInteractionsRepository
    {
        public SocialInteractionRepository(IOptions<ApplicationSettings> applicationSettingsOptions) : base(applicationSettingsOptions)
        {
            Initialize();
        }

        public async Task<SocialInteractions> GetItemByPostId(Guid postId)
        {
            var socialInteractions = (await GetItemsWhereAsync(x => x.PostId == postId)).First();
            return socialInteractions;
        }

        protected override string CollectionId { get; } = nameof(SocialInteractions);
    }
}
