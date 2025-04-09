using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOs.Message;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blog.Service.Services.Concrete
{
    public class MessagesService : IMessagesService
    {
        private readonly IUnitOfWorkk unitOfWorkk;
        private readonly IMapper mapper;

        public MessagesService(IUnitOfWorkk unitOfWorkk,IMapper mapper)
        {
            this.unitOfWorkk = unitOfWorkk;
            this.mapper = mapper;
        }


        public async Task<List<MessageViewModel>> GetMessagesAsync()
        {

            var messages = await unitOfWorkk.GetRepository<Message>().GetAllAsync(x=>!x.Id.Equals(null));

            var map = mapper.Map<List<MessageViewModel>>(messages);

            return map;
        }

        private string NormalizeText(string text)
        {
            text = Regex.Replace(text, "[\\/:*?\"<>|=]", " ").Trim();
            return text;
        }

        public async Task<Guid> AddMessageAsync(MessageCreateModel msg)
        {

            var map = new Message { };
            map.Name = NormalizeText(msg.Name);
            map.Subject = NormalizeText(msg.Subject);
            map.Email = NormalizeText(msg.Email);
            map.Body = NormalizeText(msg.Body);
            map.Tel = NormalizeText(msg.Tel);
            map.Id = Guid.NewGuid();

            await unitOfWorkk.GetRepository<Message>().AddAsync(map);
            await unitOfWorkk.SaveAsync();

            return map.Id;

        }

        public async Task<Guid> DeleteMessageAsync(Guid msgId)
        {
            var get = await unitOfWorkk.GetRepository<Message>().GetAsync(x=>x.Id == msgId);
            await unitOfWorkk.GetRepository<Message>().DeleteAsync(get);

            await unitOfWorkk.SaveAsync();

            return get.Id;
        }
    }
}
