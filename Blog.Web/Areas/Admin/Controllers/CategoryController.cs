using AutoMapper;
using Blog.Entity.DTOs.Category;
using Blog.Entity.Entities;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstractions;
using Blog.Service.Services.Concrete;
using Blog.Web.Consts;
using Blog.Web.ResultMessages;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IValidator<Category> validator;
        private readonly IMapper mapper;
        private readonly IToastNotification toast;

        public CategoryController(ICategoryService categoryService,IValidator<Category> validator,IMapper mapper,IToastNotification toast)
        {
            this.categoryService = categoryService;
            this.validator = validator;
            this.mapper = mapper;
            this.toast = toast;
        }

        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAllCategoriesAsync();
            
            return View(categories);
        }

        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public IActionResult Add() { 
            

            return View();
        }

        [HttpPost]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Add(CategoryCreateModel categoryModel)
        {
            var map = mapper.Map<Category>(categoryModel);
            var result = await validator.ValidateAsync(map);
            if (result.IsValid)
            {
                await categoryService.AddCategoryAsync(categoryModel);
                toast.AddSuccessToastMessage(CategoryMessages.Add(categoryModel.Name),new ToastrOptions { Title="Başarılı"});
                return RedirectToAction("Index","Category",new {Area = "Admin"});

            }
            result.AddModelState(this.ModelState);
            return View();
        }

        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Update(Guid categoryId)
        {
            var category = await categoryService.GetCategoryByGuid(categoryId);
            var map = mapper.Map<Category, CategoryUpdateModel>(category);
            return View(map);
        }

        [HttpPost]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Update(CategoryUpdateModel updateModel)
        {
            var map = mapper.Map<Category>(updateModel);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid) { 
                var name = await categoryService.UpdateCategoryAsync(updateModel);
                toast.AddSuccessToastMessage(CategoryMessages.Update(updateModel.Name), new ToastrOptions { Title = "Başarılı" });
                return RedirectToAction("Index", "Category", new { Area = "Admin" });
            }
            result.AddModelState(this.ModelState);
            return View();
        }

        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Delete(Guid categoryId)
        {
            var title = await categoryService.SafeDeleteCategoryAsync(categoryId);
            toast.AddSuccessToastMessage(CategoryMessages.Delete(title), new ToastrOptions { Title = "Başarılı" });
            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }

        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> ForceDelete(Guid categoryId)
        {
            var title = await categoryService.ForceDeleteCategoryAsync(categoryId);
            toast.AddSuccessToastMessage(CategoryMessages.Delete(title), new ToastrOptions { Title = "Başarılı" });
            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }

        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> UndoDelete(Guid categoryId)
        {
            var title = await categoryService.UndoDeleteCategoryAsync(categoryId);
            toast.AddSuccessToastMessage(CategoryMessages.UndoDelete(title), new ToastrOptions { Title = "Başarılı" });
            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }


        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> DeletedCategories()
        {
            var categories = await categoryService.GetAllDeletedCategoriesAsync();
            
            return View(categories);

        }

        }
}
