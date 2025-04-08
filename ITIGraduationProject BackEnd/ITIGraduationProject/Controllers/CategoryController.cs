using ITIGraduationProject.BL;
using ITIGraduationProject.BL.DTO.Category;
using ITIGraduationProject.BL.Manger.CategoryManger;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ITIGraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManger categoryManger;

        public CategoryController(ICategoryManger _categoryManger)
        {
            categoryManger = _categoryManger;
        }
        [HttpGet]
        public async Task<Ok<List<CategoryDetailsDTO>>> GetALL()
        {
            return TypedResults.Ok(await categoryManger.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<Results<Ok<CategoryDetailsDTO>, NotFound>> GetById(int id)
        {
            var category = await categoryManger.GetById(id);
            if (category == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(category);
        }
        [HttpGet("category/{name}")]
        public async Task<Results<Ok<CategoryDetailsDTO>, NotFound>> GetByCategory(string name)
        {
            var category = await categoryManger.GetByName(name);
            if (category == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(category);
        }

        [HttpPost]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> Add(CategoryAddDTO category)
        {
            var result = await categoryManger.AddAsync(category);
            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }

        [HttpPut]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> Update(CategoryAddDTO category)
        {
            {
                var result = await categoryManger.UpdateAsync(category);
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
            var result = await categoryManger.DeleteAsync(id);
            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }
    }
}
