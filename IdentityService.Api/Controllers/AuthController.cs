using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("signout")]
        public async Task<IActionResult> SignOut() => Ok();

        [HttpGet("signin")]
        public async Task<ActionResult> SignIn() => Ok();

        [HttpGet("refresh-token")]
        public async Task<ActionResult> RefreshToken() => Ok();

        [HttpGet("reset-password")]
        public async Task<ActionResult> ResetPassword() => Ok();

        [HttpGet("change-password")]
        public async Task<ActionResult> ChangePassword() => Ok();

        [HttpGet("profile")]
        public async Task<ActionResult> Profile() => Ok();
    }
}
