using AutoMapper;
using Blog.Entity.DTOs.Category;
using Blog.Entity.DTOs.User;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.AutoMapper.Users
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserViewModel, TUser>().ReverseMap();
            CreateMap<UserCreateModel, TUser>().ReverseMap();
            CreateMap<UserUpdateModel, TUser>().ReverseMap();
            CreateMap<UserProfileViewModel, TUser>().ReverseMap();

        }
    }
}
