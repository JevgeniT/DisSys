using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace BLL.App.DTO
{
    public class Reservation : Reservation<Guid>, IDomainBaseEntity
    {
    }
    
    public class Reservation<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; }= default!;

        public int ReservationNumber { get; set; } = 1;
        public DateTime CheckInDate{ get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public Review? Review { get; set; }
        public ICollection<ReservationRooms>? ReservationRooms { get; set; }
        public Guid PropertyId { get; set; }
        public Property? Property { get; set; }
        public bool Active { get; set; } = true;
        public decimal TotalPrice { get; set; }
        public TKey AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
      
    }
}