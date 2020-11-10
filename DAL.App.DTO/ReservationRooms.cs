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

        public Guid ReservationId { get; set; }
        public Reservation? Reservation { get; set; }

        public Guid RoomId { get; set; }
        public Room? Room { get; set; }
        public Guid PolicyId { get; set; }
    }
}
