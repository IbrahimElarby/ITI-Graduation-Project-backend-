using ITIGraduationProject.BL.DTO;
using ITIGraduationProject.BL.Manger;
using ITIGraduationProject.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITIGraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager _accountManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(IAccountManager accountManager, UserManager<ApplicationUser> _userManager)
        {
            _accountManager = accountManager;
            userManager = _userManager;
        }
        [HttpPost("assign-role")]
        public async Task<Results<Ok<string>, NotFound<string>>> AssignRole(string userName, string role)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
                return TypedResults.NotFound("User not found");

            var claim = new Claim(ClaimTypes.Role, role);
            await userManager.AddClaimAsync(user, claim);

            return TypedResults.Ok($"Role '{role}' added to user '{userName}'");
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
