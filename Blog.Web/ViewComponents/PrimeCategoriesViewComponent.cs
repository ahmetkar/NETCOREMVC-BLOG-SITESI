using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.ViewComponents
{
    public class PrimeCategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;

        public PrimeCategoriesViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await categoryService.GetPrimeCategoriesAsync();
            return View(categories);
        }
    }
}
