using ITIGraduationProject.BL.DTO;
using ITIGraduationProject.BL.Manger;
using ITIGraduationProject.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ITIGraduationProject.BL.DTO.Account;

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
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _accountManager.GetUserByIdAsync(userId);
            if (user == null) return BadRequest("User not found");

            var decodedToken = System.Net.WebUtility.UrlDecode(token);


            var result = await _accountManager.ConfirmEmailAsync(user, decodedToken);
            if (result.Succeeded)
            {
                return Ok("Email confirmed successfully");
            }

            return BadRequest("Invalid token or confirmation failed");
        }

        [HttpPost("SendPasswordResetEmail")]
        public async Task<IActionResult> SendPasswordResetEmail([FromBody] string email)
        {
            var result = await _accountManager.SendPasswordResetEmailAsync(email);
            if (!result)
            {
                return BadRequest("Email not found");
            }
            return Ok("Password reset link sent");
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetDto resetDto)
        {
            var result = await _accountManager.ResetPasswordAsync(resetDto.Email, resetDto.Token, resetDto.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest("Failed to reset password");
            }
            return Ok("Password reset successfully");
        }
        [HttpPut("update-profile/{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] UserDto updatedUser)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound("User not found");

            if (!string.IsNullOrWhiteSpace(updatedUser.UserName))
                user.UserName = updatedUser.UserName;

            if (!string.IsNullOrWhiteSpace(updatedUser.ProfileImageUrl))
                user.ProfileImageUrl = updatedUser.ProfileImageUrl;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok("User updated successfully");
        }
        [HttpPost("ProfileImage")]
        public async Task<IActionResult> UpdateProfileImage([FromBody] UserDto dto)
        {
            var user = await userManager.FindByIdAsync(dto.Id.ToString());
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.ProfileImageUrl = dto.ProfileImageUrl;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest("Failed to update profile image");
            }

            return Ok(new { message = "Profile image updated", imageUrl = user.ProfileImageUrl });
        }

        [HttpGet("RedirectToProfileImage")]
        public async Task<IActionResult> RedirectToProfileImage([FromQuery] string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null || string.IsNullOrEmpty(user.ProfileImageUrl))
            {
                return NotFound("User not found or profile image not set");
            }

            return Redirect(user.ProfileImageUrl);
        }

    }
}
