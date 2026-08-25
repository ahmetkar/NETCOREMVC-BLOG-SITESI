using System.Threading.Tasks;
using Blog.Entity.DTOs.Subscriber;

namespace Blog.Service.Services.Abstractions
{
    public interface ISubscriberService
    {
        Task<bool> SubscribeAsync(string email, string? ipAddress = null);
        Task<List<SubscriberViewModel>> GetSubscribersAsync();
        Task<bool> DeleteSubscriberAsync(Guid id, string? deletedBy = null);
        Task<bool> SetSubscriberActiveAsync(Guid id, bool isActive, string? modifiedBy = null);
        Task<bool> IsUnsubscribeTokenValidAsync(Guid id, string token);
        Task<bool> UnsubscribeAsync(Guid id, string token);
        Task<bool> SendAllSubscriberToNew(string title,string bodytitle,string bodycontent,string bodylink);
        Task<bool> SendMailAsync(string title,string body,string email);
    }
}
