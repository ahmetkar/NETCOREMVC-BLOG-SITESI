using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOs.Category;
using Blog.Entity.Entities;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWorkk unitOfWorkk;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal user;

        public CategoryService(IUnitOfWorkk unitOfWorkk,IMapper mapper, IHttpContextAccessor httpContextAccessor) {
            this.unitOfWorkk = unitOfWorkk;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            user = httpContextAccessor.HttpContext.User;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {   
            
            var categories = await unitOfWorkk.GetRepository<Category>().GetAllAsync(x=>!x.IsDeleted);
            var map = mapper.Map<List<CategoryViewModel>>(categories);
            return map;

        }

        public async Task<List<CategoryViewModel>> GetPrimeCategoriesAsync()
        {
            var primecategories = await unitOfWorkk.GetRepository<Category>().GetAllAsync(x => !x.IsDeleted && x.isPrimeCategory);
            var map = mapper.Map<List<CategoryViewModel>>(primecategories);
            return map;
        }

        public async Task<List<MenuCategoryViewModel>> GetMenuCategoriesAndArticlesAsync()
        {
            var menucategories = await unitOfWorkk.GetRepository<Category>().GetAllAndSelectAsync(s=> new Category{
                Name = s.Name,
                Articles = s.Articles.Where(c=>!c.IsDeleted && c.CategoryID == s.Id).Take(4).ToList()
            },x=>!x.IsDeleted,i=>i.Articles);
            List<MenuCategoryViewModel> map = mapper.Map<List<MenuCategoryViewModel>>(menucategories);
            return map;
        }

        public async Task<List<CategoryViewModel>> GetPopularCategoryAsync()
        {
            var categories = await unitOfWorkk.GetRepository<Category>().GetAllAsync(x => !x.IsDeleted,x=>x.Articles);
            List<Category> popularcategories = categories.OrderByDescending(x => x.Articles.Count).Take(5).ToList();
            var map = mapper.Map<List<CategoryViewModel>>(popularcategories);
            return map;
        }

        public async Task AddCategoryAsync(CategoryCreateModel createModel) {

            string userEmail = user.GetLoggedInEmail();

            Category cat = new(createModel.Name,userEmail,DateTime.Now);
            await unitOfWorkk.GetRepository<Category>().AddAsync(cat);
            await unitOfWorkk.SaveAsync();

        }

        public async Task<Category> GetCategoryByGuid(Guid id)
        {
            var category = await unitOfWorkk.GetRepository<Category>().GetByGuidAsync(id);
            return category;
        }

        public async Task<string> SafeDeleteCategoryAsync(Guid categoryId)
        {
            var userEmail = user.GetLoggedInEmail();
            var cat = await unitOfWorkk.GetRepository<Category>().GetByGuidAsync(categoryId);
            cat.IsDeleted = true;
            cat.DeleteDate = DateTime.Now;
            cat.DeletedBy = userEmail;
            await unitOfWorkk.GetRepository<Category>().UpdateAsync(cat);

            await unitOfWorkk.SaveAsync();

            return cat.Name;
        }

        public async Task<string> UpdateCategoryAsync(CategoryUpdateModel updateModel)
        {

            string userEmail = user.GetLoggedInEmail();

            var category = await unitOfWorkk.GetRepository<Category>().GetAsync(x => x.IsDeleted == false && x.Id == updateModel.Id);
            category.Name = updateModel.Name;
            category.ModifiedBy = userEmail;
            category.ModifiedDate = DateTime.Now;
            await unitOfWorkk.GetRepository<Category>().UpdateAsync(category);
            await unitOfWorkk.SaveAsync();

            return category.Name;
        }

        public async Task<List<CategoryViewModel>> GetAllDeletedCategoriesAsync()
        {
            var categories = await unitOfWorkk.GetRepository<Category>().GetAllAsync(x => x.IsDeleted);
            var map = mapper.Map<List<CategoryViewModel>>(categories);
            return map;
        }

        public async Task<string> UndoDeleteCategoryAsync(Guid categoryId)
        {
            var cat = await unitOfWorkk.GetRepository<Category>().GetByGuidAsync(categoryId);
            cat.IsDeleted = false;
            cat.DeleteDate = null;
            cat.DeletedBy = null;
            await unitOfWorkk.GetRepository<Category>().UpdateAsync(cat);

            await unitOfWorkk.SaveAsync();

            return cat.Name;

        }

        public async Task<string> ForceDeleteCategoryAsync(Guid categoryId)
        {
            var cat = await unitOfWorkk.GetRepository<Category>().GetByGuidAsync(categoryId);
            await unitOfWorkk.GetRepository<Category>().DeleteAsync(cat);
            await unitOfWorkk.SaveAsync();
            return cat.Name;
        }

    }
}
