using System;
using AutoMapper.Configuration.Annotations;

namespace Public.DTO
{
    public class ReservationRoomDTO
    {
        public Guid RoomId { get; set; }

        [Ignore]
        public Guid? ReservationId { get; set; }
        public Guid PolicyId { get; set; }

        public decimal RoomTotalPrice { get; set; }
    }
}