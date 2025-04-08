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

namespace ITIGraduationProject.BL.Manger
{
    public class AccountManager : IAccountManager
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _config;

        public AccountManager(IAccountRepository accountRepository, IConfiguration config)
        {
            _accountRepository = accountRepository;
            _config = config;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto registerDto)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };
            return await _accountRepository.RegisterUserAsync(user, registerDto.Password);
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
    }
}
