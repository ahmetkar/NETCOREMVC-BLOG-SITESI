using Blog.Data.UnitOfWorks;
using Blog.Entity.Entities;
using Blog.Entity.DTOs.Subscriber;
using Blog.Service.Services.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using System.IO;
using System.Security.Cryptography;
using System.Text;



namespace Blog.Service.Services.Concrete
{
    
    public class SubscriberService : ISubscriberService
    {
        private readonly IUnitOfWorkk _unitOfWork;
        private readonly IConfiguration _configuration;

         private readonly IMapper mapper;

        public SubscriberService(IUnitOfWorkk unitOfWork,IConfiguration configuration,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            this.mapper = mapper;
        }

        public async Task<bool> SubscribeAsync(string email, string? ipAddress = null)
        {
            email = email?.Trim().ToLowerInvariant() ?? string.Empty;
            var emailAttribute = new EmailAddressAttribute();
            if (!emailAttribute.IsValid(email))
            {
                return false;
            }

            var existingSubscriber = await _unitOfWork.GetRepository<Subscriber>().GetAsync(x => x.Email == email);
            if (existingSubscriber != null)
            {
                if (existingSubscriber.IsDeleted)
                {
                    existingSubscriber.IsDeleted = false;
                    existingSubscriber.DeletedBy = null;
                    existingSubscriber.DeleteDate = null;
                }

                existingSubscriber.IsActive = true;
                existingSubscriber.IpAddress = ipAddress;
                existingSubscriber.ModifiedBy = "System";
                existingSubscriber.ModifiedDate = DateTime.Now;
                await _unitOfWork.GetRepository<Subscriber>().UpdateAsync(existingSubscriber);
                await _unitOfWork.SaveAsync();

                return true; // Zaten kayıtlı
            }

            var subscriber = new Subscriber(email, ipAddress);
            await _unitOfWork.GetRepository<Subscriber>().AddAsync(subscriber);
            await _unitOfWork.SaveAsync();

            return true;
        }
        public async Task<List<SubscriberViewModel>> GetSubscribersAsync()
        {
            var subscribers = await _unitOfWork.GetRepository<Subscriber>()
                .GetAllAsync(x => !x.IsDeleted);

            return mapper.Map<List<SubscriberViewModel>>(
                subscribers.OrderByDescending(x => x.CreatedDate).ToList());
        }

        public async Task<bool> DeleteSubscriberAsync(Guid id, string? deletedBy = null)
        {
            var subscriber = await _unitOfWork.GetRepository<Subscriber>().GetByGuidAsync(id);
            if (subscriber == null || subscriber.IsDeleted)
            {
                return false;
            }

            subscriber.IsDeleted = true;
            subscriber.IsActive = false;
            subscriber.DeletedBy = string.IsNullOrWhiteSpace(deletedBy) ? "Admin" : deletedBy;
            subscriber.DeleteDate = DateTime.Now;
            subscriber.ModifiedBy = subscriber.DeletedBy;
            subscriber.ModifiedDate = DateTime.Now;
            await _unitOfWork.GetRepository<Subscriber>().UpdateAsync(subscriber);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> SetSubscriberActiveAsync(Guid id, bool isActive, string? modifiedBy = null)
        {
            var subscriber = await _unitOfWork.GetRepository<Subscriber>().GetByGuidAsync(id);
            if (subscriber == null || subscriber.IsDeleted)
            {
                return false;
            }

            subscriber.IsActive = isActive;
            subscriber.ModifiedBy = string.IsNullOrWhiteSpace(modifiedBy) ? "Admin" : modifiedBy;
            subscriber.ModifiedDate = DateTime.Now;
            await _unitOfWork.GetRepository<Subscriber>().UpdateAsync(subscriber);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> IsUnsubscribeTokenValidAsync(Guid id, string token)
        {
            var subscriber = await _unitOfWork.GetRepository<Subscriber>().GetByGuidAsync(id);
            return subscriber != null && !subscriber.IsDeleted && IsUnsubscribeTokenValid(subscriber, token);
        }

        public async Task<bool> UnsubscribeAsync(Guid id, string token)
        {
            var subscriber = await _unitOfWork.GetRepository<Subscriber>().GetByGuidAsync(id);
            if (subscriber == null || subscriber.IsDeleted || !IsUnsubscribeTokenValid(subscriber, token))
            {
                return false;
            }

            subscriber.IsDeleted = true;
            subscriber.IsActive = false;
            subscriber.DeletedBy = "Subscriber";
            subscriber.DeleteDate = DateTime.Now;
            subscriber.ModifiedBy = "Subscriber";
            subscriber.ModifiedDate = DateTime.Now;
            await _unitOfWork.GetRepository<Subscriber>().UpdateAsync(subscriber);
            await _unitOfWork.SaveAsync();
            return true;
        }

        private bool IsUnsubscribeTokenValid(Subscriber subscriber, string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return false;

            var expectedToken = CreateUnsubscribeToken(subscriber.Id);
            return CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(expectedToken),
                Encoding.UTF8.GetBytes(token));
        }

        private string CreateUnsubscribeUrl(Guid subscriberId)
        {
            var siteUrl = _configuration["SiteBlogLink"]?.TrimEnd('/');
            if (string.IsNullOrWhiteSpace(siteUrl))
            {
                throw new InvalidOperationException("SiteBlogLink yapılandırması tanımlanmalıdır.");
            }

            return $"{siteUrl}/abonelikten-cik?id={subscriberId}&token={Uri.EscapeDataString(CreateUnsubscribeToken(subscriberId))}";
        }

        private string CreateUnsubscribeToken(Guid subscriberId)
        {
            var secret = _configuration["Newsletter:UnsubscribeSecret"];
            if (string.IsNullOrWhiteSpace(secret))
            {
                throw new InvalidOperationException("Newsletter:UnsubscribeSecret yapılandırması tanımlanmalıdır.");
            }

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
            return Convert.ToHexString(hmac.ComputeHash(Encoding.UTF8.GetBytes(subscriberId.ToString("N"))));
        }

        public async Task<bool> SendMailAsync(string title,string body,string email){

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_configuration["SiteName"],_configuration["Email:FromAddress"]));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = title;
            message.Body = new BodyBuilder { HtmlBody=body}.ToMessageBody();
            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(_configuration["Email:SmtpHost"],int.Parse(_configuration["Email:SmtpPort"]!),SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(_configuration["Email:Username"],_configuration["Email:Password"]);

            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);

            return true;

        }

        public async Task<bool> SendAllSubscriberToNew(string title,string bodytitle,string bodycontent,string bodylink){
       
            var subscribers = await _unitOfWork.GetRepository<Subscriber>().GetAllAsync(x => !x.IsDeleted && x.IsActive);
            
            if(subscribers!=null && subscribers.Count == 0){
                return false;
            }

            var subscribers_mapped = mapper.Map<List<SubscriberViewModel>>(subscribers);

            if(subscribers_mapped.Count == 0){
                return false;
            }

            var template = await System.IO.File.ReadAllTextAsync(
                            "Template/Email/NewArticle.html"
                        );
            template = template
                        .Replace("{{TITLE}}", bodytitle)
                        .Replace("{{BODY}}", bodycontent)
                        .Replace("{{ARTICLE_URL}}", bodylink);
                        

            for(int i=0;i<subscribers_mapped.Count;i++){
                Console.WriteLine(subscribers_mapped[i].Email," e email gönderiliyor");
                if(subscribers_mapped[i].IsDeleted != true){
                var personalizedTemplate = template.Replace("{{UNSUBSCRIBE_URL}}", CreateUnsubscribeUrl(subscribers_mapped[i].Id));
                bool send = await this.SendMailAsync(title,personalizedTemplate,subscribers_mapped[i].Email);
                if(send){
                    Console.WriteLine(subscribers_mapped[i].Email," e emaile gönderim başarılı oldu.");
                }else {
                    Console.WriteLine(subscribers_mapped[i].Email," e email gönderilemedi.");
                }
                }
            }

            return true;
        }
    }
}
