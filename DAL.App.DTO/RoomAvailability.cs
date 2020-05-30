using System;
using Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class RoomAvailability : RoomAvailability<Guid>, IDomainBaseEntity
    {
    }
    public class RoomAvailability<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public Guid RoomId { get; set; }

        public Room? Room { get; set; }

        public Guid AvailabilityId { get; set; }
        public Availability? Availability { get; set; }
        
        
    }
}