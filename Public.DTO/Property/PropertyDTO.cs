using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Public.DTO
{
    public class PropertyDTO
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid AppUserId { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; } = default!;
        public double Score { get; set; }
        public int ReviewsCount { get; set; } 
        public PropertyRulesDTO? PropertyRules { get; set; }
        public ICollection<ExtraDTO>? Extras { get; set; }
        public ICollection<RoomDTO>? PropertyRooms { get; set; }
         
    }
}