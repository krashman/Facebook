using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Facebook.Repository
{
    public class IdentityUserClaimRepository : Repository<IdentityUserClaim<string>>, IIdentityUserClaimRepository
    {
        public IdentityUserClaimRepository(FacebookDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public IdentityUserClaim<string> GetClaimBy(string identityId, string claimType)
        {
            return entities.FirstOrDefault(x => x.UserId == identityId && x.ClaimType == claimType);
        }

        public void UpdateClaimGiven(string identityId, string claimType, string newValue)
        {
            var claim = GetClaimBy(identityId, claimType);
            claim.ClaimValue = newValue;
            Update(claim);
        }
    }
}