using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }

        public string? ProfileImageUrl { get; set; } 
        public string? Bio { get; set; } 

        // Navigation properties
        public ICollection<Recipe>? Recipes { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<MealSuggestion>? MealSuggestions { get; set; }
        public ICollection<Subscription>? Subscriptions { get; set; }
    }
}
