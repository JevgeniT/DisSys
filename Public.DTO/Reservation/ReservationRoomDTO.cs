using System;
using AutoMapper.Configuration.Annotations;

namespace Public.DTO.Reservation
{
    public class ReservationRoomDTO
    {
        public Guid RoomId { get; set; }
        public Guid? ReservationId { get; set; }
        public Guid PolicyId { get; set; }
        public string? GuestFirstLastName { get; set; }
        public decimal RoomTotalPrice { get; set; }
        public string? BedType { get; set; }
    }
}