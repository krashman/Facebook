using Facebook.Domain;
using Microsoft.Extensions.Options;

namespace Facebook.Repository
{
    public interface IProfilePictureUrlRepository : IDocumentDatabaseRepository<UserProfile>
    {
        
    }

    public class ProfilePictureUrlRepository : DocumentDatabaseRepository<UserProfile>, IProfilePictureUrlRepository
    {
        public ProfilePictureUrlRepository(IOptions<ApplicationSettings> applicationSettingsOptions) : base(applicationSettingsOptions)
        {
        }

        protected override string CollectionId { get; } = nameof(UserProfile);
    }
}
