using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL.DTO.RecipeManger.Write
{
    public class IngredientCreateDto
    {
        public string Name { get; set; }

        public decimal CaloriesPer100g { get; set; }

        public decimal Protein { get; set; }

        public decimal Carbs { get; set; }

        public decimal Fats { get; set; }
    }
}
