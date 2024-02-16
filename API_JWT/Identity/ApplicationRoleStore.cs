using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace API_JWT.Identity
{
    public class ApplicationRoleStore: RoleStore<ApplicationRole, ApplicationDbContext>
    {
        public ApplicationRoleStore(ApplicationDbContext context, IdentityErrorDescriber errorDescriber) : base(context, errorDescriber)
        {

        }
    
    }
}
