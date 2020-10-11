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
        public TKey Id { get; set; }

        public Guid ReservationId { get; set; }
        public Guid PolicyId { get; set; }
        public Guid RoomId { get; set; }
        public Room? Room { get; set; }
    }
    
}