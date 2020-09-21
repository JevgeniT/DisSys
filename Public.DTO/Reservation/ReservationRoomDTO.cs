using System;

namespace Public.DTO
{
    public class ReservationRoomDTO
    {
        public Guid RoomId { get; set; }

        public Guid ReservationId { get; set; }

        public Guid PolicyId { get; set; }
    }
}