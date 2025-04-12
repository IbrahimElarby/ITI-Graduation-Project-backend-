using ITIGraduationProject.BL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ITIGraduationProject
{
    [ApiController]
    [Route("api/recipes/{recipeId}/categories")]
    public class RecipeCategoryController : ControllerBase
    {
        private readonly IRecipeCategoryManger RecipeCategoryManager;

        public RecipeCategoryController(IRecipeCategoryManger _RecipeCategoryManager)
        {
            RecipeCategoryManager = _RecipeCategoryManager;
        }


        [HttpPost("{categoryId}")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> AssignCategory(int recipeId, int categoryId)
        {
            var result = await RecipeCategoryManager.AssignCategory(recipeId, categoryId);
            return result.Success ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }

        // ---------------- Unassign Category from Blog Post ----------------
        [HttpDelete("{categoryId}")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> UnassignCategory(int recipeId, int categoryId)
        {
            var result = await RecipeCategoryManager.UnAssignCategory(recipeId, categoryId);
            return result.Success ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}

