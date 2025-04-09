using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Entity.DTOs.Article;

namespace Blog.Service.AutoMapper.Articles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleViewModel, Article>().ReverseMap();
            CreateMap<ArticleUpdateModel, Article>().ReverseMap();
            CreateMap<ArticleUpdateModel, ArticleViewModel>().ReverseMap();
            CreateMap<ArticleCreateModel, Article>().ReverseMap();
            CreateMap<ArticleIndexViewModel, Article>().ReverseMap();
        }
    }
}
