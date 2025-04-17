using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL.DTO.Account
{
    public class ImageUpdateDto
    {
        public int Id { get; set; }

        public string ProfileImageUrl { get; set; } = string.Empty;
    }

}
