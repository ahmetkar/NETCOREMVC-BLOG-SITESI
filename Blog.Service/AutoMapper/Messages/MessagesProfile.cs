using AutoMapper;
using Blog.Entity.DTOs.Message;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.AutoMapper.Messages
{
    public class MessagesProfile : Profile
    {
        public MessagesProfile()
        {
            CreateMap<MessageViewModel, Message>().ReverseMap();
            CreateMap<MessageCreateModel, Message>().ReverseMap();
        }
    }
}
