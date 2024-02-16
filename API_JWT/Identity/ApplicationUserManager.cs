using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace API_JWT.Identity
{
    public class ApplicationUserManager:UserManager<Applicationuser>
  {
    public ApplicationUserManager(ApplicationUserStore applicationUserStore, IOptions<IdentityOptions> options,
    IPasswordHasher<Applicationuser> passwordHasher, IEnumerable<IUserValidator<Applicationuser>> userValidators,
    IEnumerable<IPasswordValidator<Applicationuser>> passwordValidators, ILookupNormalizer lookupNormalizer,
    IdentityErrorDescriber identityErrorDescriber, IServiceProvider service, ILogger<ApplicationUserManager> logger) : base(
     applicationUserStore, options, passwordHasher, userValidators, passwordValidators, lookupNormalizer, identityErrorDescriber, service, logger)

    {

    }
    
    }
}
