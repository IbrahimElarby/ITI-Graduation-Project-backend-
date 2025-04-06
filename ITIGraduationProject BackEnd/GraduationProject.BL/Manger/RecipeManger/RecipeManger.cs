using ITIGraduationProject.BL.DTO.RecipeManger;
using ITIGraduationProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL.Manger
{
    public class RecipeManger : IRecipeManger
    {
        private readonly IUnitOfWork unitOfWork;

        public RecipeManger(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<GeneralResult> AddAsync(RecipeDetailsDTO item)
        {
            //var creator = new ApplicationUser()
            //{
            //    UserName = item.Author.UserName,
            //    Email = item.Author.Email,
            //};

            //var recipe = new Recipe()
            //{
            //    Creator = creator,
            //    Title = item.Title,
            //    Instructions = item.Instructions,
            //    PrepTime = item.PrepTime,
            //    Description = item.Description,
            //    CookingTime = item.CookingTime,
            //    CuisineType = item.CuisineType,
            //    CreatedAt = DateTime.Now,

            //    Comments = new List<Comment>(),
            //    Ratings = new List<Rating>(),
            //    RecipeIngredients = new List<RecipeIngredient>(),
            //};

            //unitOfWork.RecipeRepository.Add(recipe);
            //await unitOfWork.SaveChangesAsync();

            
            throw new NotImplementedException();

        }

        public Task<GeneralResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<RecipeDetailsDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<RecipeDetailsDTO>> GetByCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RecipeDetailsDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RecipeDetailsDTO> GetByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResult> UpdateAsync(RecipeDetailsDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
