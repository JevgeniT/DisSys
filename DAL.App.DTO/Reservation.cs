using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    
    
    public class Reservation : Reservation<Guid>, IDomainBaseEntity
    {
    }
    
    public class Reservation<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; }= default!;
        public int ReservationNumber { get; set; }
        public DateTime CheckInDate{ get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public Review? Review { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public decimal TotalPrice { get; set; }
        public TKey PropertyId { get; set; } = default!;
        public Property? Property { get; set; }
        public bool Active { get; set; }
        public string ArrivalTime { get; set; } = default!;
        public TKey AppUserId { get; set; } = default!;
        public AppUser AppUser { get; set; } = default!;
        public ICollection<ReservationRooms>? ReservationRooms { set; get; }
        public ICollection<Extra>? Extras { get; set; }
    }
}