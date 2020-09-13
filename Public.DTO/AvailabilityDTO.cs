using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BLL.App.DTO;

namespace Public.DTO
{
    public class AvailabilityDTO
    {
        public Guid Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        
        public Guid RoomId { get; set; }
        public string? RoomName  { get; set; }
        public  ICollection<PolicyDTO>? PolicyDtos { get; set; }
        
        public bool IsUsed { get; set; }
        
        public decimal PricePerNightForAdult { get; set; }
        
        public decimal PricePerNightForChild { get; set; }

        public bool PricePerPerson { get; set; }
        
        public int RoomsAvailable { get; set; }
    }
}