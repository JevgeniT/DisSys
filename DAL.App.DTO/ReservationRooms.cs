using System;
using Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class ReservationRooms : ReservationRooms<Guid>,IDomainBaseEntity
    {
    }

    public class ReservationRooms<TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        public TKey ReservationId { get; set; }= default!;
        public Reservation? Reservation { get; set; }
        public TKey RoomId { get; set; }= default!;
        public Room? Room { get; set; }
        public TKey PolicyId { get; set; }= default!;
        public string? GuestFirstLastName { get; set; }
        public string? BedType { get; set; }
        public decimal RoomTotalPrice { get; set; }
    }
}
