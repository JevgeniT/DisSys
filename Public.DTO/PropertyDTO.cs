using System;
using System.Collections;
using System.Collections.Generic;

namespace Public.DTO
{
    public class PropertyDTO
    {
        
        public Guid Id { get; set; }
        
        public string? PropertyName { get; set; }
        public string? PropertyLocation { get; set; }
        public string? Address { get; set; }
        public ICollection<RoomDTO> Rooms { get; set; }

         
    }
}