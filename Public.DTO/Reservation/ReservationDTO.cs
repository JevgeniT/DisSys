using System;
using System.Collections.Generic;

namespace Public.DTO.Reservation
{
    public class ReservationDTO 
    {
        public Guid Id { get; set; }
        public int ReservationNumber { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid PropertyId { get; set; }
        public string? PropertyName { get; set; }
        public string? PropertyLocation { get; set; }
        public string? Status { get; set; }
        public decimal TotalPrice { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public string? ArrivalTime { get; set; }
        public ReviewDTO? Review { get; set; }
        public ICollection<RoomDTO>? RoomDtos { get; set; }
        public ICollection<ExtraDTO>? ExtraDtos { get; set; }
        public string? ReservedBy { get; set; }
    }
}