using System;
using Public.DTO;

namespace Public.DTO
{
    public class PropertyViewDTO
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
  
        public string? Address { get; set; }
        
        public string? Country { get; set; }

        public string? Description { get; set; }

        public int ReviewsCount { get; set; }
        
        public RoomViewDTO? Room { get; set; }
        public double Score { get; set; } = 0;
        
        public string? Type { get; set; }
    }
}