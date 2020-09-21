using System;
using System.Collections.Generic;

namespace Public.DTO
{
    public class ReservationCreateDTO
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public Guid PropertyId { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public ICollection<ReservationRoomDTO>? RoomDtos { get; set; }
     }
}