using AutoMapper;
using Blog.Entity.DTOs.User;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.ViewComponents
{
    public class HeaderUserViewComponent : ViewComponent
    {
        private readonly UserManager<TUser> userManager;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public HeaderUserViewComponent(UserManager<TUser> userManager,IMapper mapper,IUserService userService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loggedUser = await userManager.GetUserAsync(HttpContext.User);
            var user = await userService.GetUserProfileAsync();
            var map = mapper.Map<UserViewModel>(loggedUser);
            map.Image.FileName = user.CurrentImage;
            var role = string.Join("",await userManager.GetRolesAsync(loggedUser));
            map.Role = role;
                
            return View(map);
        }
    }
}
