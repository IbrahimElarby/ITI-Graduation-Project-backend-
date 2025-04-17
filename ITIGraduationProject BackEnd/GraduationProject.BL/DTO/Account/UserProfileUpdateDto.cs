using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL.DTO.Account
{
    public class UserProfileUpdateDto
    {
        public string? UserName { get; set; }

        public string? ProfileImageUrl { get; set; }

        public string? NewPassword { get; set; }

        public string? CurrentPassword { get; set; } 
    }

}
