using System.Security.Claims;
using ITIGraduationProject.BL;
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
        private readonly IRatingManger _ratingManager;

        public RatingController(IRatingManger ratingManager)
        {
            _ratingManager = ratingManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRating([FromBody] RatingDto dto)
        {
            
            var result = await _ratingManager.AddRatingAsync(dto, dto.UserId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("recipe/{recipeId}")]
        public async Task<IActionResult> GetRatingsByRecipe(int recipeId)
        {
            var result = await _ratingManager.GetRatingsByRecipeAsync(recipeId);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("top")]
        public async Task<IActionResult> GetTopRatedRecipes()
        {
            var result = await _ratingManager.GetTopRatedRecipesAsync();
            return Ok(result);
        }
    }
}
