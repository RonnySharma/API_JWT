using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_JWT.Identity
{
    public class Applicationuser: IdentityUser
    {
        [NotMapped]
        public string Token { get; set; }
        [NotMapped]
        public string Role { get; set; }
    }
}
