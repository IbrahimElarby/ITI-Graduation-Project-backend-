﻿using ITIGraduationProject.BL;
using ITIGraduationProject.BL.DTO.RecipeManger.Input;
using ITIGraduationProject.BL.DTO.RecipeManger.Output;
using ITIGraduationProject.BL.Manger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ITIGraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeManger recipeManager;
        private readonly IFavoriteRecipeManger favoriteRecipeManger;

        public RecipeController(IRecipeManger _recipeManger , IFavoriteRecipeManger _favoriteRecipeManger)
        {
            recipeManager = _recipeManger;
            favoriteRecipeManger = _favoriteRecipeManger;
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
        [HttpPost("generate-ai")]
        [ProducesResponseType(typeof(GeneralResult<RecipeDetailsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GeneralResult<RecipeDetailsDTO>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GeneralResult<RecipeDetailsDTO>>> GenerateFromAI([FromBody] AiRecipeRequest input)
        {
            var result = await recipeManager.ImportAndSaveRecipe(input);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("favorite")]
        [ProducesResponseType(typeof(GeneralResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GeneralResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddToFavorites([FromBody] FavoriteRecipeCreateDto dto)
        {
            var result = await favoriteRecipeManger.AddToFavoritesAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("favoriteRecipesUser/{userId}")]
        
        public async Task<ActionResult<GeneralResult<List<FavoriteRecipeDto>>>> GetFavorites(int userId)
        {
            var result = await favoriteRecipeManger.GetFavoritesForUser(userId);
            return Ok(result);
        }


        [HttpDelete("{userId}/{recipeId}")]
        
        public async Task<ActionResult<GeneralResult>> RemoveFavorite(int userId, int recipeId)
        {
            var result = await favoriteRecipeManger.RemoveFromFavoritesAsync(userId, recipeId);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }

}
