using ITIGraduationProject.BL;
using ITIGraduationProject.DAL;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITIGraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentManger _commentManager;

        public CommentsController(ICommentManger commentManager)
        {
            _commentManager = commentManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CommentDto dto)
        {
            

            var result = await _commentManager.AddCommentAsync(dto);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("recipe/{recipeId}")]
        public async Task<IActionResult> GetCommentsForRecipe(int recipeId)
        {
            var result = await _commentManager.GetCommentsForRecipeAsync(recipeId);
            return Ok(result);
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> UpdateComment(int commentId, [FromBody] CommentDto dto)
        {
          

            var result = await _commentManager.UpdateCommentAsync(commentId, dto.Content, dto.UserId);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId, [FromBody] CommentDto dto)
        {
           

            var result = await _commentManager.DeleteCommentAsync(commentId, dto.UserId);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}

