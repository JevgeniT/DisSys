using System;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class ReservationRooms : ReservationRooms<Guid>,IDomainBaseEntity
    {
    }

    public class ReservationRooms<TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public Guid ReservationId { get; set; }
        public Guid PolicyId { get; set; }
        public TKey RoomId { get; set; } = default!;
        public Room? Room { get; set; }
    }
    
}