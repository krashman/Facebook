using Facebook.Domain;
using Microsoft.Extensions.Options;

namespace Facebook.Repository
{
    public class PostRepository : DocumentDatabaseRepository<Post>
    {
        public PostRepository(IOptions<ApplicationSettings> applicationSettingsOptions) : base(applicationSettingsOptions)
        {

        }

        protected override string CollectionId { get; } = nameof(Post);
    }
}