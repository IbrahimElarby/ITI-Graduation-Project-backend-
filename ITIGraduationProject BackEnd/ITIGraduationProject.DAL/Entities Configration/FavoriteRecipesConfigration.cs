using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class FavoriteRecipesConfigration : IEntityTypeConfiguration<FavoriteRecipe>
    {
        public void Configure(EntityTypeBuilder<FavoriteRecipe> builder)
        {
            builder
            .HasKey(fr => new { fr.RecipeID, fr.UserID });

            builder
                .HasOne(fr => fr.User)
                .WithMany(u => u.FavoriteRecipes)
                .HasForeignKey(fr => fr.UserID);

            builder
                .HasOne(fr => fr.Recipe)
                .WithMany(r => r.FavoritedBy)
                .HasForeignKey(fr => fr.RecipeID);
        }
    }
}
