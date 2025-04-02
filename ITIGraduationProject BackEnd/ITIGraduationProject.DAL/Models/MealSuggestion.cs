using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class MealSuggestion
    {
        public int SuggestionID { get; set; }
        public int UserID { get; set; }
        public ApplicationUser? User { get; set; }
        public int RestaurantID { get; set; }
        public Restaurant? Restaurant { get; set; }
        public decimal Budget { get; set; }
    }
}
