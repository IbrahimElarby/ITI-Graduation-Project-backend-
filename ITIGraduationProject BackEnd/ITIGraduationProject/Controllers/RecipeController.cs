using ITIGraduationProject.BL.DTO.RecipeManger.Output;
using ITIGraduationProject.BL.Manger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITIGraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeManger _recipeManager;
        public RecipeController(IRecipeManger recipeManger)
        {
            _recipeManager = recipeManger;
        }

        [HttpGet]
        public async Task<ActionResult<List<RecipeDetailsDTO>>> GetAll()
        {
            var recipes = await _recipeManager.GetAll();
            return Ok(recipes);
        }
    }
}
