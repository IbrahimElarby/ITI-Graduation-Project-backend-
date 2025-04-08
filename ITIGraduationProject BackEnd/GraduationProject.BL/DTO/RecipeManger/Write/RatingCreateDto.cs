using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL.DTO.RecipeManger.Write
{
    public class RatingCreateDto
    {
        public int RecipeID { get; set; }
        public int UserID { get; set; }
        public int Score { get; set; }
    }
}
