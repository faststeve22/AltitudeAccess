using AltitudeAccess.PresentationLayer.DTOs;
using AltitudeAccess.ServiceLayer.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace AltitudeAccess.PresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;

        }

        [HttpPost]
        public ActionResult<string> UserLogin([FromBody]LoginRequestDTO login)
        {
            return Ok(_loginService.UserLogin(login));
        }
    }
}