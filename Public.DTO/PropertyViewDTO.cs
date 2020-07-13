using System;

namespace Public.DTO
{
    public class PropertyViewDTO
    {
        public Guid Id { get; set; } = default!;

        public string? Name { get; set; }
  
        public string? Address { get; set; }
        
        public string? Country { get; set; }

        public string? Description { get; set; }

        public int ReviewsCount { get; set; }
        
        public double Score { get; set; }
        
        public string? Type { get; set; }
    }
}