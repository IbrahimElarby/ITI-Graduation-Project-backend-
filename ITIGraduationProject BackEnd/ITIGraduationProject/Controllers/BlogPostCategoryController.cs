using ITIGraduationProject.BL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ITIGraduationProject
{
    [ApiController]
    [Route("api/posts/{postId}/categories")]
    public class BlogPostCategoryController : ControllerBase
    {
        private readonly IBlogPostCategoryManger blogPostCategoryManager;

        public BlogPostCategoryController(IBlogPostCategoryManger blogPostManager)
        {
            blogPostCategoryManager = blogPostManager;
        }

       
        [HttpPost("{categoryId}")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> AssignCategory(int postId, int categoryId)
        {
            var result = await blogPostCategoryManager.AssignCategory(postId, categoryId);
            return result.Success ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }

        // ---------------- Unassign Category from Blog Post ----------------
        [HttpDelete("{categoryId}")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> UnassignCategory(int postId, int categoryId)
        {
            var result = await blogPostCategoryManager.UnAssignCategory(postId, categoryId);
            return result.Success ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}
