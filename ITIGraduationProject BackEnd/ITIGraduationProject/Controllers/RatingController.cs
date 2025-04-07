using System.Security.Claims;
using ITIGraduationProject.BL.DTO;
using ITIGraduationProject.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITIGraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RatingController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("ratings")]
        public async Task<IActionResult> CreateRating([FromBody] RatingDto ratingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var recipe = await _context.Recipes.FindAsync(ratingDto.RecipeId);

            if (recipe == null)
                return NotFound("Recipe not found");

            var rating = new Rating
            {
                Score = ratingDto.Score,
                UserID = int.Parse(userId),
                RecipeID = ratingDto.RecipeId
            };

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRatingsByRecipe), new { recipeId = rating.RecipeID }, rating);
        }
        [HttpGet("recipes/{id}/ratings")]
        public async Task<IActionResult> GetRatingsByRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
                return NotFound("Recipe not found");

            var ratings = await _context.Ratings
                .Where(r => r.RecipeID == id)
                .ToListAsync();

            return Ok(ratings);
        }

    }
}
