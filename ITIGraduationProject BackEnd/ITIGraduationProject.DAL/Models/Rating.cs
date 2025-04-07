using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class Rating
    {
        public int RatingID { get; set; }
        public int RecipeID { get; set; }
        public Recipe? Recipe { get; set; }
        public int UserID { get; set; }
        public ApplicationUser? User { get; set; }
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
