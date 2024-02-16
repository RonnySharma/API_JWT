using API_JWT.Identity;
using API_JWT.Model.Viewemodel;

namespace API_JWT.Servicecontract
{
    public interface IUserService
    {
        Task<Applicationuser> Authenticate(LoginViewmodel loginViewmodel);
    }
}
