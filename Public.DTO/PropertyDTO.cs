using System;
using System.Collections;
using System.Collections.Generic;
using BLL.App.DTO;

namespace Public.DTO
{
    public class PropertyDTO
    {
        
        public Guid Id { get; set; }
        public string? PropertyName { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        
        public PropertyType Type { get; set; }

        public int Score { get; set; }

        public int ReviewCount { get; set; }
        public ICollection<RoomDTO> Rooms { get; set; }

         
    }
}