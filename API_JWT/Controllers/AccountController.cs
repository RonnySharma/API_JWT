using API_JWT.Model.Viewemodel;
using API_JWT.Servicecontract;
using Microsoft.AspNetCore.Mvc;

namespace API_JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
                _userService= userService;
        }
        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]LoginViewmodel loginViewmodel)
        {
            var user = await _userService.Authenticate(loginViewmodel);
            if (user == null) return BadRequest(new { message = "Wrong User/Pwd" });
            return Ok(user);
        }
       
    }
}
