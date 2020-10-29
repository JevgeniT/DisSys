using System;
using System.ComponentModel.DataAnnotations;

namespace Public.DTO
{
    public class ReviewDTO
    {
        public Guid Id { get; set; }
        
        [Range(1,10)]
        public int Score { get; set; }
        
        public string? Comment { get; set; }
        
        public Guid PropertyId { get; set; }
        public Guid ReservationId { get; set; }
        public Guid AppUserId { get; set; }
        
        public string? CreatedAt { get; set; }
        
    }
}