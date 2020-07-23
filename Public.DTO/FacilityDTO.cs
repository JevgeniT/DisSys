using System;
using System.Text.Json.Serialization;

namespace Public.DTO
{
    public class FacilityDTO
    {
        // [JsonIgnore]
        // public Guid Id { get; set; }

        public string? Name { get; set; }
        
        // public Guid RoomId { get; set; }

    }
}