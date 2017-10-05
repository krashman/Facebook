using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Facebook.Repository
{
    public interface IIdentityUserClaimRepository : IRepository<IdentityUserClaim<string>>
    {
        IdentityUserClaim<string> GetClaimBy(string identityId, string claimType);
        void UpdateClaimGiven(string identityId, string claimType, string newValue);
    }
}