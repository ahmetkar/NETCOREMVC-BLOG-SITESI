using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;

        public CategoriesViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
           
        }
        

        public async Task<IViewComponentResult> InvokeAsync()
        {
          
            var categories = await categoryService.GetMenuCategoriesAndArticlesAsync();
            return View(categories);
        }
    }
}
