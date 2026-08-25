using Blog.Entity.DTOs.Category;
using Blog.Service.Services.Abstractions;
using Blog.Web.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Authorize(Policy = "CategoryViewPolicy")]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpPost]
        [Authorize(Policy = "CategoryViewPolicy")]
        public async Task<IActionResult> Add([FromBody] CategoryCreateModel categoryCreateModel)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddCategoryAsync(categoryCreateModel);
                return Ok(new { success = true, message = "Kategori başarıyla eklendi." });
            }
            return BadRequest(new { success = false, message = "Geçersiz kategori bilgileri." });
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "CategoryViewPolicy")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var category = await _categoryService.GetCategoryByGuid(id);
            if (category != null)
            {
                return Ok(category);
            }
            return NotFound(new { message = "Kategori bulunamadı." });
        }

        [HttpPut]
        [Authorize(Policy = "CategoryViewPolicy")]
        public async Task<IActionResult> Update([FromBody] CategoryUpdateModel categoryUpdateModel)
        {
            if (ModelState.IsValid)
            {
                var title = await _categoryService.UpdateCategoryAsync(categoryUpdateModel);
                return Ok(new { success = true, message = $"'{title}' kategorisi başarıyla güncellendi." });
            }
            return BadRequest(new { success = false, message = "Geçersiz kategori bilgileri." });
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "CategoryViewPolicy")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var title = await _categoryService.SafeDeleteCategoryAsync(id);
            return Ok(new { success = true, message = $"'{title}' kategorisi çöp kutusuna taşındı." });
        }

        [HttpGet("deleted")]
        [Authorize(Policy = "CategoryViewPolicy")]
        public async Task<IActionResult> DeletedCategories()
        {
            var categories = await _categoryService.GetAllDeletedCategoriesAsync();
            return Ok(categories);
        }

        [HttpPut("undo/{id}")]
        [Authorize(Policy = "CategoryViewPolicy")]
        public async Task<IActionResult> UndoDelete(Guid id)
        {
            var title = await _categoryService.UndoDeleteCategoryAsync(id);
            return Ok(new { success = true, message = $"'{title}' kategorisi geri getirildi." });
        }

        [HttpDelete("force/{id}")]
        [Authorize(Policy = "CategoryViewPolicy")]
        public async Task<IActionResult> ForceDelete(Guid id)
        {
            var title = await _categoryService.ForceDeleteCategoryAsync(id);
            return Ok(new { success = true, message = $"'{title}' kategorisi kalıcı olarak silindi." });
        }
    }
}
