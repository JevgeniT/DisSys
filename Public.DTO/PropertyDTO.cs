using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Public.DTO
{
    public class PropertyDTO
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }

        public string? Description { get; set; }
        
        public string Type { get; set; }
        public double Score { get; set; }
        
        public int ReviewsCount { get; set; }
        
        public ICollection<RoomDTO>? PropertyRooms { get; set; }
         
    }
}