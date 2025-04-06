using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class RestaurantMeal
    {
        public int MealID { get; set; }
        public int RestaurantID { get; set; }
        public Restaurant? Restaurant { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Calories { get; set; }
    }
}
