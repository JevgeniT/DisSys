using System;
using System.Collections.Generic;
using BLL.App.DTO;

namespace Public.DTO.Reservation
{
    public class ReservationCreateDTO 
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public Guid PropertyId { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public decimal TotalPrice { get; set; }
        public string? ArrivalTime { get; set; }
        public string? Message { get; set; }
        public ICollection<ReservationExtrasDTO>? ReservationExtras { get; set; }
        public ICollection<ReservationRoomDTO>? RoomDtos { get; set; }
     }
}