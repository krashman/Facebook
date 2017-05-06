using Facebook.Domain;
using Microsoft.Extensions.Options;

namespace Facebook.Repository
{
    public class SocialInteractionRepository : DocumentDatabaseRepository<SocialInteraction>
    {
        public SocialInteractionRepository(IOptions<ApplicationSettings> applicationSettingsOptions) : base(applicationSettingsOptions)
        {
            Initialize();
        }

        protected override string CollectionId { get; } = nameof(SocialInteraction);
    }
}
