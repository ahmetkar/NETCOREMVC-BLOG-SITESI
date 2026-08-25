namespace Blog.Entity.DTOs.Subscriber
{
    public class NewsletterUnsubscribeModel
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
