﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            // Properties
            builder.Property(r => r.Title)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(r => r.Description)
                .HasMaxLength(2000);

            builder.Property(r => r.CookingTime)
                .IsRequired();

            builder.Property(r => r.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(r => r.Calories)
                .HasPrecision(10, 2);
            builder.Property(r => r.Carbs)
                .HasPrecision(10, 2);
            builder.Property(r => r.Protein)
               .HasPrecision(10, 2);
            builder.Property(r => r.Fats)
                .HasPrecision(10, 2);

            // Relationships
            builder.HasOne(r => r.Creator)
                .WithMany(u => u.Recipes)
                .HasForeignKey(r => r.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
