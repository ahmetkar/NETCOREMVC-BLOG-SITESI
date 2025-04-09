using Blog.Entity.DTOs.User;
using Blog.Entity.Entities;
using Blog.Entity.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Abstractions
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetAllUserWithRoleAsync();
        Task<List<TRole>> GetAllRolesAsync();
        Task<IdentityResult> CreateUserAsync(UserCreateModel userCreateModel);
        Task<TUser> GetUserByIdAsync(Guid userId);
        Task<string> GetUserRoleAsync(TUser user);
        Task<IdentityResult> UpdateUserAsync(UserUpdateModel userUpdate);
        Task<(IdentityResult result ,string email)> DeleteUserAsync(Guid userId);
        Task<Guid> ImageUpload(string oldImage, string Title, IFormFile imageFile, ImageType type, string userEmail);
        Task<UserProfileViewModel> GetUserProfileAsync();
        Task<bool> UserProfileUpdateAsync(UserProfileViewModel userProfile);
        Task<TUser> GetUserByNameAsync(string authorName);
        
    }
}
