using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace API_JWT.Identity
{
    public class ApplicationSignInManager : SignInManager<Applicationuser>
    {
        public ApplicationSignInManager(ApplicationUserManager applicationUserManager,
       IHttpContextAccessor httpContextAccessor, IUserClaimsPrincipalFactory<Applicationuser> userClaimsPrincipalFactory,
       IOptions<IdentityOptions> options, ILogger<ApplicationSignInManager> logger,
       IAuthenticationSchemeProvider authenticationSchemeProvider, IUserConfirmation<Applicationuser> userConfirmation
       ) : base(applicationUserManager, httpContextAccessor, userClaimsPrincipalFactory, options, logger, authenticationSchemeProvider, userConfirmation)
        {

        }
    }
}
