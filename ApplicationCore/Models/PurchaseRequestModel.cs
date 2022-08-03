using System;
using ApplicationCore.Entities;

namespace ApplicationCore.Models
{
    public class PurchaseRequestModel
    {
        public string PurchaseNumber { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime PurchaseDateTime { get; set; }
        public int MovieId { get; set; }
        public string? PosterUrl { get; set; }
    }
}

