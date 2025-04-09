using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOs.User;
using Blog.Entity.Entities;
using Blog.Entity.Enums;
using Blog.Service.Extensions;
using Blog.Service.Helpers.Images;
using Blog.Service.Services.Abstractions;
using Blog.Web.Consts;
using Blog.Web.ResultMessages;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Security.Claims;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IToastNotification toast;
        private readonly IValidator<TUser> validator;
      


        public UserController(IUserService userService,
            IMapper mapper,
            IToastNotification toast,
            IValidator<TUser> validator
           )
        {
            this.userService = userService;
            this.mapper = mapper;
            this.toast = toast;
            this.validator = validator;
  
        }

        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Index()
        {
            var result = await userService.GetAllUserWithRoleAsync();

           
          
            return View(result);
        }

        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Add()
        {
            var roles = await userService.GetAllRolesAsync();
            
            if (!User.IsInRole($"{RoleConsts.SuperAdmin}")) {                
                var roletoremove = roles.Find(x=>x.Name == "Superadmin");
                if (roletoremove != null)
                {
                    roles.Remove(roletoremove);
                }
            }

            return View(new UserCreateModel { Roles = roles});
        }

        [HttpPost]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Add(UserCreateModel userCreateModel)
        {
            var map = mapper.Map<TUser>(userCreateModel);
            var validation = await validator.ValidateAsync(map);
            if (validation.IsValid) {
                if (ModelState.IsValid)
                {
                    var result = await userService.CreateUserAsync(userCreateModel);
                    if (result.Succeeded)
                    {
                        toast.AddSuccessToastMessage(UserMessages.Add(userCreateModel.Email), new ToastrOptions { Title = "Başarılı" });
                        return RedirectToAction("Index","User",new {Area = "Admin"});
                    }else
                    {
                        result.AddIdentityModelState(this.ModelState);
                        validation.AddModelState(this.ModelState);
                    }
                }
            }else
            {
                validation.AddModelState(this.ModelState);

            }

            var roles = await userService.GetAllRolesAsync();
            if (!User.IsInRole($"{RoleConsts.SuperAdmin}"))
            {
                var roletoremove = roles.Find(x => x.Name == "Superadmin");
                if (roletoremove != null)
                {
                    roles.Remove(roletoremove);
                }
            }
            
            return View(new UserCreateModel { Roles = roles });
        }


        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Update(Guid userId)
        {
            var user = await userService.GetUserByIdAsync(userId);
            var roles = await userService.GetAllRolesAsync();

            var map = mapper.Map<UserUpdateModel>(user);
            
           
            if (!User.IsInRole($"{RoleConsts.SuperAdmin}"))
            {
                var roletoremove = roles.Find(x => x.Name == "Superadmin");
                if (roletoremove != null)
                {
                    roles.Remove(roletoremove);
                }
            }
            map.Roles = roles;
            return View(map);
        }

        [HttpPost]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Update(UserUpdateModel userUpdate)
        {
            var user = await userService.GetUserByIdAsync(userUpdate.Id);
           
           
            if (ModelState.IsValid)
            {
                var userRole = await userService.GetUserRoleAsync(user);
                var map = mapper.Map(userUpdate,user);
                var validation = await validator.ValidateAsync(map);

                if (validation.IsValid) { 
                    user.UserName = userUpdate.Email;
                    user.SecurityStamp = Guid.NewGuid().ToString();

                    var result = await userService.UpdateUserAsync(userUpdate);

                    if (result.Succeeded)
                    {
                        toast.AddSuccessToastMessage(UserMessages.Update(userUpdate.Email), new ToastrOptions { Title = "Başarılı" });
                        return RedirectToAction("Index", "User", new { Area = "Admin" });
                    }else
                    {
                        result.AddIdentityModelState(this.ModelState);
                        validation.AddModelState(this.ModelState);
                    }
                }else
                {
                    validation.AddModelState(this.ModelState);
                }
            }

            var roles = await userService.GetAllRolesAsync();
            if (!User.IsInRole($"{RoleConsts.SuperAdmin}"))
            {
                var roletoremove = roles.Find(x => x.Name == "Superadmin");
                if (roletoremove != null)
                {
                    roles.Remove(roletoremove);
                }
            }

            return View(new UserUpdateModel { Roles = roles});
        }


        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
           
                var result = await userService.DeleteUserAsync(userId);
                if (result.result.Succeeded)
                {
                    toast.AddSuccessToastMessage(UserMessages.Delete(result.email), new ToastrOptions { Title = "Başarılı" });

                }else
                {
                    result.result.AddIdentityModelState(this.ModelState);

                }
            

            return RedirectToAction("Index", "User", new { Area = "Admin" });

        }
        

        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin},{RoleConsts.User}")]
        public async Task<IActionResult> Profil()
        {
            var profile = await userService.GetUserProfileAsync();
            return View(profile);
        }

      
        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Admin},{RoleConsts.User}")]
        public async Task<IActionResult> Profil(UserProfileViewModel userProfile)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.UserProfileUpdateAsync(userProfile);

                if (result)
                {
                    toast.AddSuccessToastMessage("Bilgileriniz başarıyla başarıyla güncellendi.");
                
                    return RedirectToAction("Profil", "User", new { Area = "Admin" });
                }
                else
                {
                    toast.AddErrorToastMessage("Bilgileriniz güncellenirken hata oluştu.");
                    var profile = await userService.GetUserProfileAsync();
                    return View(profile);

                    //result.AddIdentityModelState(ModelState);
                }
            }
            return NotFound();

            
        }
        



    }
}
