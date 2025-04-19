using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ITIGraduationProject.BL.DTO;
using ITIGraduationProject.DAL.Repository.Account;
using ITIGraduationProject.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ITIGraduationProject.BL.Manger.MailServiceManger;
using ITIGraduationProject.BL.DTO.Account;

namespace ITIGraduationProject.BL.Manger
{

    public class AccountManager : IAccountManager
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _config;
        private readonly IEMailService _mailService;


        public AccountManager(IAccountRepository accountRepository, IConfiguration config, IEMailService mailService)
        {
            _accountRepository = accountRepository;
            _config = config;
            _mailService = mailService;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto registerDto)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };
            var result = await _accountRepository.RegisterUserAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                // Generate confirmation token
                var token = await _accountRepository.GenerateEmailConfirmationTokenAsync(user);
                 var encodedToken = System.Net.WebUtility.UrlEncode(token);

                // Confirmation URL
                var confirmationUrl = $"https://localhost:7157/api/Account/ConfirmEmail?userId={user.Id}&token={encodedToken}";

                // Send confirmation email
                await _mailService.SendEmailAsync(registerDto.Email, "Confirm Your Account",
                    $"<h2>Welcome {registerDto.UserName} and thank " +
                    $"you for joining our cook&crave </h2><p>Thank you for registering." +
                    $" Please confirm your email by clicking " +
                    $"<a href='{confirmationUrl}'>here</a>.</p>");
            }

            return result;

        }
        public Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return _accountRepository.GetUserByIdAsync(userId);
        }

        public Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
        {
            return _accountRepository.ConfirmEmailAsync(user, token);
        }
        public async Task<bool> SendPasswordResetEmailAsync(string email)
        {
            var user = await _accountRepository.GetUserByEmailAsync(email);
            if (user == null) return false;

            var token = await _accountRepository.GeneratePasswordResetTokenAsync(user);
            var encodedToken = System.Web.HttpUtility.UrlEncode(token);

            var resetUrl = $"https://localhost:7157/api/Account/ResetPassword?email={email}&token={encodedToken}";

            await _mailService.SendEmailAsync(email, "Reset Your Password",
                $"<h2>Welcome {user.UserName}" +
                $"<h2>Forgot your password?</h2><p>Please reset your password by clicking " +
                $"<a href='{resetUrl}'>here</a> to Join again cook&crave </p>");

            return true;
        }

        public async Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _accountRepository.GetUserByNameAsync(email);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            return await _accountRepository.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            ApplicationUser userFromDb = await _accountRepository.GetUserByNameAsync(loginDto.UserName);
            if (userFromDb != null && await _accountRepository.CheckPasswordAsync(userFromDb, loginDto.Password))
            {
                // Generate JWT Token
                List<Claim> userClaims = new List<Claim>
                {
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim(ClaimTypes.NameIdentifier, userFromDb.Id.ToString()),
                 new Claim(ClaimTypes.Name, userFromDb.UserName)

                };
                var userRoles = await _accountRepository.GetUserRolesAsync(userFromDb);
                foreach (var role in userRoles)
                {
                    userClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecritKey"]));
                var signingCredentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256);

                var jwtToken = new JwtSecurityToken(
                    audience: _config["JWT:AudienceIP"],
                    issuer: _config["JWT:IssuerIP"],
                    expires: DateTime.Now.AddHours(1),
                    claims: userClaims,
                    signingCredentials: signingCredentials
                );

                return new JwtSecurityTokenHandler().WriteToken(jwtToken);
            }

            return null;
        }
        public async Task<getUserDto?> GetBasicUserInfoByIdAsync(string userId)
        {
            var user = await _accountRepository.GetUserByIdAsync(userId);
            if (user == null) return null;

            return new getUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl
            };
        }
    }
}
