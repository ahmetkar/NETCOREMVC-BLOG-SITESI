using Blog.Entity.DTOs.User;
using Blog.Service.Services.Abstractions;
using Blog.Web.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Policy = "UserViewPolicy")]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUserWithRoleAsync();
            return Ok(users);
        }

        [HttpGet("roles")]
        [Authorize(Policy = "UserViewPolicy")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _userService.GetAllRolesAsync();
            if (!User.IsInRole(RoleConsts.SuperAdmin))
            {
                var roleToRemove = roles.Find(x => x.Name == "Superadmin");
                if (roleToRemove != null)
                {
                    roles.Remove(roleToRemove);
                }
            }
            return Ok(roles);
        }

        [HttpPost]
        [Authorize(Policy = "UserViewPolicy")]
        public async Task<IActionResult> Add([FromBody] UserCreateModel userCreateModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateUserAsync(userCreateModel);
                if (result.Succeeded)
                {
                    return Ok(new { success = true, message = "Kullanıcı başarıyla eklendi." });
                }
                return BadRequest(new { success = false, errors = result.Errors });
            }
            return BadRequest(new { success = false, message = "Form verileri geçersiz." });
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "UserViewPolicy")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound(new { message = "Kullanıcı bulunamadı." });
        }

        [HttpPut]
        [Authorize(Policy = "UserViewPolicy")]
        public async Task<IActionResult> Update([FromBody] UserUpdateModel userUpdate)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateUserAsync(userUpdate);
                if (result.Succeeded)
                {
                    return Ok(new { success = true, message = "Kullanıcı başarıyla güncellendi." });
                }
                return BadRequest(new { success = false, errors = result.Errors });
            }
            return BadRequest(new { success = false, message = "Form verileri geçersiz." });
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "UserViewPolicy")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result.result.Succeeded)
            {
                return Ok(new { success = true, message = "Kullanıcı başarıyla silindi." });
            }
            return BadRequest(new { success = false, errors = result.result.Errors });
        }

        [HttpGet("profile")]
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Editor},{RoleConsts.User}")]
        public async Task<IActionResult> Profile()
        {
            var profile = await _userService.GetUserProfileAsync();
            return Ok(profile);
        }

        [HttpPut("profile")]
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Editor},{RoleConsts.User}")]
        public async Task<IActionResult> UpdateProfile([FromForm] UserProfileViewModel userProfile)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UserProfileUpdateAsync(userProfile);
                if (result)
                {
                    return Ok(new { success = true, message = "Profil bilgileri başarıyla güncellendi." });
                }
                return BadRequest(new { success = false, message = "Profil güncellenirken hata oluştu." });
            }
            return BadRequest(new { success = false, message = "Geçersiz veriler." });
        }
    }
}
