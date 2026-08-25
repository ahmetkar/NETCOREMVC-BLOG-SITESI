using Blog.Core.Entities;
using System;

namespace Blog.Entity.Entities
{
    public class Subscriber : EntityBase, IEntityBase
    {
        public Subscriber()
        {
        }

        public Subscriber(string email, string? ipAddress = null)
        {
            Email = email;
            IpAddress = ipAddress;
            IsActive = true;
            IsDeleted = false;
            CreatedDate = DateTime.Now;
            CreatedBy = "System";
        }

        public string Email { get; set; }
        public string? IpAddress { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
