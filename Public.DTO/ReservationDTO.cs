using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Public.DTO
{
    public class ReservationDTO
    {
        public Guid Id { get; set; }
        public int ReservationNumber { get; set; }
        public string CheckInDate { get; set; }
        
        public string CheckOutDate { get; set; }
        
        public ICollection<RoomDTO>? Rooms { get; set; }

        public Guid PropertyId { get; set; }

        public bool IsCancelled { get; set; }
        
        public decimal TotalPrice { get; set; }
        
        public int Adults { get; set; }
        
        public int Children { get; set; }
        
        public string? ReservedBy { get; set; }
    }
}