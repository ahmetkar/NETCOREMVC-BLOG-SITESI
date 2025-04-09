using Blog.Entity.DTOs.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Abstractions
{
    public interface IMessagesService
    {
        Task<List<MessageViewModel>> GetMessagesAsync();
        Task<Guid> AddMessageAsync(MessageCreateModel msg);
        Task<Guid> DeleteMessageAsync(Guid msgId);
    }
}
