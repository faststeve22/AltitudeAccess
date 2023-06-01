using AltitudeAccess.PresentationLayer.DTOs;
using AltitudeAccess.ServiceLayer.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace AltitudeAccess.PresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }


        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegistrationRequestDTO request)
        {
            return Ok(_registrationService.RegisterNewUser(request));
        }

    }
}

