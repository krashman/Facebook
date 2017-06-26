using Facebook.Domain;
using Microsoft.Extensions.Options;

namespace Facebook.Repository
{
    public interface IProfilePictureUrlRepository : IDocumentDatabaseRepository<ProfilePictureUrl>
    {
        
    }

    public class ProfilePictureUrlRepository : DocumentDatabaseRepository<ProfilePictureUrl>, IProfilePictureUrlRepository
    {
        public ProfilePictureUrlRepository(IOptions<ApplicationSettings> applicationSettingsOptions) : base(applicationSettingsOptions)
        {
        }

        protected override string CollectionId { get; } = nameof(ProfilePictureUrl);
    }
}
