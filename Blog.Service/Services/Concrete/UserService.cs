using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOs.User;
using Blog.Entity.Entities;
using Blog.Entity.Enums;
using Blog.Service.Extensions;
using Blog.Service.Helpers.Images;
using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWorkk unitOfWorkk;
        private readonly Microsoft.AspNetCore.Identity.UserManager<TUser> userManager;
        private readonly RoleManager<TRole> roleManager;
        private readonly IImageHelper imageHelper;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly SignInManager<TUser> signInManager;
        private readonly ClaimsPrincipal _user;

        public UserService(IMapper mapper, IUnitOfWorkk unitOfWorkk, UserManager<TUser> userManager, RoleManager<TRole> roleManager, IImageHelper imageHelper, IHttpContextAccessor contextAccessor,SignInManager<TUser> signInManager)
        {
            this.mapper = mapper;
            this.unitOfWorkk = unitOfWorkk;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.imageHelper = imageHelper;
            this.contextAccessor = contextAccessor;
            this.signInManager = signInManager;
            _user = contextAccessor.HttpContext.User;
        }



     

        public async Task<IdentityResult> CreateUserAsync(UserCreateModel userCreateModel)
        {
            var map = mapper.Map<TUser>(userCreateModel);
            map.UserName = userCreateModel.Email;
            var result = await userManager.CreateAsync(map, string.IsNullOrEmpty(userCreateModel.Password) ? "" : userCreateModel.Password);
            if (result.Succeeded) {
                var findRole = await roleManager.FindByIdAsync(userCreateModel.RoleId.ToString());
                await userManager.AddToRoleAsync(map, findRole.ToString());
            }
            return result;
        }

        public async Task<(IdentityResult result,string email)> DeleteUserAsync(Guid userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null) return (IdentityResult.Failed(new IdentityError { Description="Kullanıcı bilgileri bulunamadı."}),"-");
            return (await userManager.DeleteAsync(user),user.Email);


        }

        public async Task<List<TRole>> GetAllRolesAsync()
        {
            return await roleManager.Roles.ToListAsync();

        }

        public async Task<List<UserViewModel>> GetAllUserWithRoleAsync()
        {
            var users = await userManager.Users.ToListAsync();
            if (users.Count > 0)
            {
                var map = mapper.Map<List<UserViewModel>>(users);
                
                    foreach (var user in map)
                    {
                        var findUser = await userManager.FindByIdAsync(user.Id.ToString());
                        if (findUser != null)
                        {
                            var findRole = await userManager.GetRolesAsync(findUser);
                            if (findRole != null)
                            {
                                var role = string.Join("", findRole);
                                user.Role = role;
                            }
                        }

                    }
                    return map;
                
                
            }
            return null;
        }

        public async Task<TUser> GetUserByIdAsync(Guid userId)
        {
            return await userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<TUser> GetUserByNameAsync(string authorName)
        {
            return await unitOfWorkk.GetRepository<TUser>().GetAsync(x=>x.FirstName == authorName,x=>x.Image);
        }

        public async Task<string> GetUserRoleAsync(TUser user)
        {
            if (user != null)
            {
                var role = await userManager.GetRolesAsync(user);
                if (role.Count > 0)
                {
                    return string.Join("", role);
                }
            }
            return string.Empty;

        }

       
        public async Task<IdentityResult> UpdateUserAsync(UserUpdateModel userUpdate)
        {
            var user = await GetUserByIdAsync(userUpdate.Id);
            if (user != null)
            {
                var userRole = await GetUserRoleAsync(user);
                if (!string.IsNullOrEmpty(userRole))
                {
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var res = await userManager.RemoveFromRoleAsync(user, userRole);
                        if (res.Succeeded)
                        {
                            var findRole = await roleManager.FindByIdAsync(userUpdate.RoleId.ToString());
                            if (findRole != null)
                            {
                                var r = await userManager.AddToRoleAsync(user, findRole.Name);
                                if (r.Succeeded)
                                {
                                    return result;
                                }
                            }
                        }
                    }
                    
                }
            }   
            return IdentityResult.Failed(new IdentityError { Description = "Kullanıcı bilgileri güncellenirken sorun oluştu." });
        }

        public async Task<Guid> ImageUpload(string? oldImage, string Title, IFormFile imageFile, ImageType type, string userEmail)
        {
            if(oldImage != null)imageHelper.Delete(oldImage);

            var imageUpload = await imageHelper.Upload(Title, imageFile, type);
            if (imageUpload != null)
            {
                Image image = new Image(imageUpload.FullName, imageFile.ContentType, userEmail);
                await unitOfWorkk.GetRepository<Image>().AddAsync(image);
                return image.Id;
                
            }
            return Guid.Empty;
        }

        public async Task<UserProfileViewModel> GetUserProfileAsync()
        {
            var userId = _user.GetLoggedInUserId();
            if (!userId.Equals(Guid.Empty))
            {
                var getUserWithImage = await unitOfWorkk.GetRepository<TUser>().GetAsync(a => a.Id == userId, x => x.Image);
                if (getUserWithImage != null)
                {
                    var map = mapper.Map<UserProfileViewModel>(getUserWithImage);
                    map.CurrentImage = getUserWithImage.Image.FileName;
                    return map;
                }
            }
            return null;
        }

        //Modelstateler dönmüyor
        public async Task<bool> UserProfileUpdateAsync(UserProfileViewModel userProfile)
        {
            if (userProfile.CurrentPassword == null) return false;
            var userId = _user.GetLoggedInUserId();
            if (!userId.Equals(Guid.Empty)) { 
            var user = await GetUserByIdAsync(userId);
            if (user != null) { 
                var isVerified = await userManager.CheckPasswordAsync(user, userProfile.CurrentPassword);
                if (isVerified && userProfile.NewPassword != null)
                {
                    var result = await userManager.ChangePasswordAsync(user, userProfile.CurrentPassword, userProfile.NewPassword);
                    if (result.Succeeded)
                    {
                        await userManager.UpdateSecurityStampAsync(user);
                        await signInManager.SignOutAsync();
                        await signInManager.PasswordSignInAsync(user, userProfile.NewPassword, true, false);

                        mapper.Map(userProfile, user);

                        if (userProfile.Photo != null)
                        {
                            user.ImageId = await ImageUpload(userProfile.CurrentImage, userProfile.FirstName, userProfile.Photo, ImageType.User, userProfile.Email);
                        }

                        await userManager.UpdateAsync(user);
                        await unitOfWorkk.SaveAsync();

                        return true;
                    }
                    else return false;
                }
                else if (isVerified)
                {
                    await userManager.UpdateSecurityStampAsync(user);
                    mapper.Map(userProfile, user);

                    if (userProfile.Photo != null)
                    {
                        user.ImageId = await ImageUpload(userProfile.CurrentImage, userProfile.FirstName, userProfile.Photo, ImageType.User, userProfile.Email);
                    }

                    await userManager.UpdateAsync(user);
                    await unitOfWorkk.SaveAsync();
                    return true;
                }
           
            }
            }
            return false;
        }
    }
}
