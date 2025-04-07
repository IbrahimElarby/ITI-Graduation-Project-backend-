using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITIGraduationProject.BL.DTO;
using Microsoft.AspNetCore.Identity;

namespace ITIGraduationProject.BL.Manger
{
     public interface IAccountManager
    {
        Task<IdentityResult> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);
    }
}
