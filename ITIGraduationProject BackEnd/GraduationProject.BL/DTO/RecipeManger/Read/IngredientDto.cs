﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL.DTO.RecipeManger.Read
{
    public class RecipeIngredientDTO
    {
        public int IngredientID { get; set; }
        public string ingredientName { get; set; }
        public decimal CaloriesPer100g { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbs { get; set; }
        public decimal Fats { get; set; }
    }
} 
