﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL
{
    public class RatingDto
    {
        public decimal Score { get; set; } 
        public int RecipeId { get; set; }

        public int UserId { get; set; }
    }
}
