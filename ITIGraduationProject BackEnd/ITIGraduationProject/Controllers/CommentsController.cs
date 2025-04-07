using ITIGraduationProject.BL.DTO;
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
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("comments")]
        public async Task<IActionResult> CreateComment([FromBody] CommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var recipe = await _context.Recipes.FindAsync(commentDto.RecipeId);

            if (recipe == null)
                return NotFound("Recipe not found");

            var comment = new Comment
            {
                Text = commentDto.Content,
                UserID = int.Parse(userId),
                RecipeID = commentDto.RecipeId
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCommentsByRecipe), new { recipeId = comment.RecipeID }, comment);
        }
        [HttpGet("recipes/{id}/comments")]
        public async Task<IActionResult> GetCommentsByRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
                return NotFound("Recipe not found");

            var comments = await _context.Comments
                .Where(c => c.RecipeID == id)
                .ToListAsync();

            return Ok(comments);
        }
    }
}

