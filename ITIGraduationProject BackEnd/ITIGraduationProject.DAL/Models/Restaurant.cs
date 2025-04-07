using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class Restaurant
    {
        public int RestaurantID { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? CuisineType { get; set; }

        public ICollection<RestaurantMeal>? RestaurantMeals { get; set; }
        public ICollection<MealSuggestion>? MealSuggestions { get; set; }
    }
}
