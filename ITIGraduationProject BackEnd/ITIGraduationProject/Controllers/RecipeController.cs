using ITIGraduationProject.BL;
using ITIGraduationProject.BL.DTO.RecipeManger.Input;
using ITIGraduationProject.BL.DTO.RecipeManger.Output;
using ITIGraduationProject.BL.Manger;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ITIGraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeManger recipeManager;
        public RecipeController(IRecipeManger _recipeManger)
        {
            recipeManager = _recipeManger;
        }

        [HttpGet]
        public async Task<ActionResult<List<RecipeDetailsDTO>>> GetAll()
        {
            var recipes = await recipeManager.GetAll();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDetailsDTO>> GetById(int id)
        {
            var result = await recipeManager.GetById(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("title/{title}")]
        public async Task<ActionResult<List<RecipeDetailsDTO>>> GetByTitle(string title)
        {
            var result = await recipeManager.GetByTitle(title);
            return Ok(result);
        }

        [HttpGet("category/{id}")]
        public async Task<ActionResult<List<RecipeDetailsDTO>>> GetByCategory(int id)
        {
            var result = await recipeManager.GetByCategory(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> Add(RecipeCreateDto recipe)
        {
            var result = await recipeManager.AddAsync(recipe);
            if (!result.Success)
                return TypedResults.BadRequest(result);
            return TypedResults.Ok(result);
        }
        [HttpPut]

        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> Update(RecipeDetailsDTO recipe)
        {

            var result = await recipeManager.UpdateAsync(recipe);
            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await recipeManager.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}