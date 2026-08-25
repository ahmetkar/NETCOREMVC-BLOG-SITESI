using Blog.Service.Services.Abstractions;
using Blog.Web.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Editor}")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("yearly-article-counts")]
        public async Task<IActionResult> GetYearlyArticleCount()
        {
            var count = await _dashboardService.GetYearlyArticleCount();
            return Ok(count);
        }

        [HttpGet("total-articles")]
        public async Task<IActionResult> GetTotalArticleCount()
        {
            var count = await _dashboardService.GetTotalArticleCount();
            return Ok(count);
        }

        [HttpGet("total-views")]
        public async Task<IActionResult> GetTotalViewCount()
        {
            var count = await _dashboardService.GetTotalViewCount();
            return Ok(count);
        }
    }
}
