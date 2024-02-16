using API_JWT.Identity;
using API_JWT.Model.Viewemodel;
using API_JWT.Servicecontract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_JWT.Services
{
    public class UserServices : IUserService
    {
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly ApplicationSignInManager _applicationSignInManager;
        private readonly Appsettings _appsettings;
        public UserServices(ApplicationSignInManager applicationSignInManager, ApplicationUserManager applicationUserManager,IOptions<Appsettings>appSetting)
        {
                _applicationSignInManager= applicationSignInManager;
            _applicationUserManager= applicationUserManager;
            _appsettings= appSetting.Value;
        }
        public async Task<Applicationuser> Authenticate(LoginViewmodel loginViewmodel)
        {
            var re = await _applicationSignInManager.PasswordSignInAsync(loginViewmodel.UserName, loginViewmodel.Password, false, false);
            if(re.Succeeded)
            {
                var applicationUser = await _applicationUserManager.FindByNameAsync(loginViewmodel.UserName);
                applicationUser.PasswordHash = "";
                //JWT
                if (await _applicationUserManager.IsInRoleAsync(applicationUser, SD.Role_Admin))
                    applicationUser.Role = SD.Role_Admin;
                if (await _applicationUserManager.IsInRoleAsync(applicationUser, SD.Role_Employee))
                    applicationUser.Role = SD.Role_Employee;
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appsettings.secret);
                var tokenDescritor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name,applicationUser.Id),
                    new Claim(ClaimTypes.Email,applicationUser.Email),
                    new Claim(ClaimTypes.Role,applicationUser.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescritor);
                applicationUser.Token = tokenHandler.WriteToken(token);


                return applicationUser;
            }
            else
            {
                return null;
            }
        }
    }
}
