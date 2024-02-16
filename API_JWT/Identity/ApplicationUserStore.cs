using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace API_JWT.Identity
{
    public class ApplicationUserStore : UserStore<Applicationuser>
    {
        public ApplicationUserStore(ApplicationDbContext context) : base(context)
        {

        }
    }
}
