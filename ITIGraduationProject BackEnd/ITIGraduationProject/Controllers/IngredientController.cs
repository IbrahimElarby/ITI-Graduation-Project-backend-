using ITIGraduationProject.BL;
using ITIGraduationProject.BL.DTO.Category;
using ITIGraduationProject.BL.DTO.RecipeManger.Read;
using ITIGraduationProject.BL.Manger.CategoryManger;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ITIGraduationProject
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientManger ingredientManger;

        public IngredientController(IIngredientManger _ingredientManger)
        {
            ingredientManger = _ingredientManger;
        }

        [HttpGet]
        public async Task<Ok<List<RecipeIngredientDTO>>> GetALL()
        {
            return TypedResults.Ok(await ingredientManger.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<Results<Ok<RecipeIngredientDTO>, NotFound>> GetById(int id)
        {
            var ingredient = await ingredientManger.GetById(id);
            if (ingredient == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(ingredient);
        }


        [HttpPost]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> Add(RecipeIngredientDTO ingredient)
        {
            var result = await ingredientManger.AddAsync(ingredient);
            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }

        [HttpPut]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> Update(RecipeIngredientDTO ingredient)
        {
            {
                var result = await ingredientManger.UpdateAsync(ingredient);
                if (result.Success)
                {
                    return TypedResults.Ok(result);
                }
                return TypedResults.BadRequest(result);
            }
        }
        [HttpDelete]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> Delete(int id)
        {
            var result = await ingredientManger.DeleteAsync(id);
            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }
    }
}
