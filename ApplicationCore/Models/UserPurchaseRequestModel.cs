using System;
namespace ApplicationCore.Models
{
    public class UserPurchaseRequestModel
    {
        public Guid PurchaseNumber => Guid.NewGuid();
        public DateTime PurchaseDateTime => DateTime.UtcNow;
        public int MovieId { get; set; }
        public int UserId { get; set; }
    }
}

