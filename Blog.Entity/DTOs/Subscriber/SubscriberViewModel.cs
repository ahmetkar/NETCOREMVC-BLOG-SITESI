
namespace Blog.Entity.DTOs.Subscriber
{
public class SubscriberViewModel {
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
    public string? IpAddress { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? CreatedBy { get; set; }
}

}
