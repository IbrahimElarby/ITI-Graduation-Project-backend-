using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITIGraduationProject.BL.DTO;
using ITIGraduationProject.BL.DTO.Account;
using ITIGraduationProject.DAL;
using Microsoft.AspNetCore.Identity;

namespace ITIGraduationProject.BL.Manger
{
    public interface IAccountManager
    {
        Task<IdentityResult> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);

        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token);
        Task<bool> SendPasswordResetEmailAsync(string email);
        public Task<getUserDto?> GetBasicUserInfoByIdAsync(string userId);
        Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword);
    }
}
