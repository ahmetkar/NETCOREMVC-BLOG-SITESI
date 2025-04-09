using AutoMapper;
using Blog.Entity.DTOs.Comment;
using Blog.Entity.DTOs.Images;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.AutoMapper.Images
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<ImageViewModel, Image>().ReverseMap();

        }
    }
}
