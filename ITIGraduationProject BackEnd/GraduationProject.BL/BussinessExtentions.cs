
using FluentValidation;
using ITIGraduationProject.BL.Manger;
using ITIGraduationProject.BL.Manger.CategoryManger;
using ITIGraduationProject.BL.Manger.SubscriptionManger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL
{
    public static class BussinessExtentions
    {
        public static void AddBusinessServices(
       this IServiceCollection services)
        {
           
            services.AddValidatorsFromAssembly(
           typeof(BussinessExtentions).Assembly);
            services.AddScoped<IBlogPostManger, BlogPostManger>();
            services.AddScoped<IRecipeManger, RecipeManger>();
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<ICategoryManger, CategoryManger>();
            services.AddScoped<ISubscriptionManger,SubscriptionManger>();
            services.AddScoped<IIngredientManger, IngredientManger>();
            services.AddScoped<IBlogPostCategoryManger, BlogPostCategoryManger>();
            services.AddScoped<IRecipeCategoryManger, RecipeCategoryManger>();
            services.AddHttpClient<ExternalRecipeService>();
            services.AddHttpClient<NutritionService>();




        }
    }
}
