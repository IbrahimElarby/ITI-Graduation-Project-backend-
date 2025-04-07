using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL
{
    public class AuthorNestedDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; } = string.Empty; // From ApplicationUser
         // Or other safe fields
    }
}
