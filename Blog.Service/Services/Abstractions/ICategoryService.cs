using Blog.Entity.DTOs.Category;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Abstractions
{
    public interface ICategoryService
    {
        public Task<List<CategoryViewModel>> GetAllCategoriesAsync();
        public Task<List<CategoryViewModel>> GetAllDeletedCategoriesAsync();
        public Task AddCategoryAsync(CategoryCreateModel createModel);
        public  Task<Category> GetCategoryByGuid(Guid id);
        public Task<string> UpdateCategoryAsync(CategoryUpdateModel updateModel);
        public Task<string> SafeDeleteCategoryAsync(Guid categoryId);
        public Task<string> UndoDeleteCategoryAsync(Guid categoryId);
        Task<List<CategoryViewModel>> GetPrimeCategoriesAsync();
        Task<List<MenuCategoryViewModel>> GetMenuCategoriesAndArticlesAsync();
        Task<List<CategoryViewModel>> GetPopularCategoryAsync();
        Task<string> ForceDeleteCategoryAsync(Guid categoryId);
    }
}
