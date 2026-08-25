using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Entity.DTOs.Article;
using Blog.Entity.DTOs.Subscriber;

namespace Blog.Service.AutoMapper.Articles
{
    public class SubscriberProfile : Profile
    {
        public SubscriberProfile()
        {
            CreateMap<SubscriberViewModel, Subscriber>().ReverseMap();
   
        }
    }
}
