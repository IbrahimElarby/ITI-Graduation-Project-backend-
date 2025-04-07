using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL.DTO.BlogPostManger
{
    public class AuthorNestedDTO
    {
        public string UserName { get; set; } = string.Empty; // From ApplicationUser
        public string Email { get; set; } = string.Empty; // Or other safe fields
    }
}
