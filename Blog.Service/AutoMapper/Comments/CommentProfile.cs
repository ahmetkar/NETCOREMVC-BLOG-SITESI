using AutoMapper;
using Blog.Entity.DTOs.Article;
using Blog.Entity.DTOs.Comment;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.AutoMapper.Comments
{
   
        public class CommentProfile : Profile
        {
            public CommentProfile()
            {
                CreateMap<CommentViewModel, Comment>().ReverseMap();
       
            }
        }
}
