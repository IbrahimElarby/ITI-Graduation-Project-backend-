using ITIGraduationProject.BL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ITIGraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostManger blogPostmanger;

        public BlogPostController(IBlogPostManger _blogPost)
        {
            blogPostmanger = _blogPost;
        }
        [HttpGet]
        public async Task<Ok<List<BlogPostDetailsDTO>>> GetALL()
        {
            return TypedResults.Ok(await blogPostmanger.GetAll());
        }
        [HttpGet("post/{id}")]
        public async Task<Results<Ok<BlogPostDetailsDTO>, NotFound>> GetById(int id)
        {
            var post = await blogPostmanger.GetById(id);
            if (post == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(post);
        }
        [HttpGet("category/{id}")]
        public async Task<Results<Ok<List<BlogPostDetailsDTO>>, NotFound>> GetByCategory(int id)
        {
            var post = await blogPostmanger.GetByCategory(id);
            if (post == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(post);
        }

        [HttpPost]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> Add(BlogPostAddDTO blogpost)
        {
            var result = await blogPostmanger.AddAsync(blogpost);
            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }

        [HttpPut]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> Update (BlogPostUpdateDTO blogpost)
        {
            {
                var result = await blogPostmanger.UpdateAsync(blogpost);
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
            var result = await blogPostmanger.DeleteAsync(id);
            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }
    }
}
