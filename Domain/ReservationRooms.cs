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
        public TKey ReservationId { get; set; } = default!;
        public Reservation? Reservation { get; set; }
        
        public TKey RoomId { get; set; }= default!;
        public Room? Room { get; set; }
        public TKey PolicyId { get; set; }= default!;

        public string? GuestFirstLastName { get; set; }
        
        public string? BedType { get; set; }

    }
}