using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Public.DTO
{
    public class PropertyDTO
    {
        
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }

        public string? Description { get; set; }
        [JsonIgnore]
        public PropertyType PropertyType { get; set; }

        public string Type { get { return PropertyType.ToString();} }
        public int Score { get; set; }

        public int ReviewCount { get; set; }
        
        public ICollection<RoomDTO>? PropertyRooms { get; set; }

         
    }
    public enum PropertyType
    {
        [EnumMember(Value = "Hotel")]
        Hotel,
        Hostel,
        Apartments 
    }
}