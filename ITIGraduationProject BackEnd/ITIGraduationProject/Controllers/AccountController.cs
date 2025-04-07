using ITIGraduationProject.BL.DTO;
using ITIGraduationProject.BL.Manger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITIGraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto userFromRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountManager.RegisterAsync(userFromRequest);
                if (result.Succeeded)
                {
                    return Ok("User created successfully");
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto userFromRequest)
        {
            if (ModelState.IsValid)
            {
                var token = await _accountManager.LoginAsync(userFromRequest);
                if (token != null)
                {
                    return Ok(new { token, expiration = DateTime.Now.AddHours(1) });
                }

                ModelState.AddModelError("Username", "Invalid Username or Password");
            }
            return BadRequest(ModelState);
        }
    }
}
