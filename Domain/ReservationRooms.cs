using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class ReservationRooms : ReservationRooms<Guid>, IDomainEntityBaseMetadata
    {
        
    }
    public class ReservationRooms<TKey> : DomainEntityBaseMetadata<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; }

        public Guid ReservationId { get; set; }
        public Reservation? Reservation { get; set; }

        public Guid RoomId { get; set; }
        public Room? Room { get; set; }
    }
}