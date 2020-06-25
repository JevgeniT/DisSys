using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;
using Domain.Validation;

namespace Domain
{
    
    
    public class Reservation : Reservation<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
    }
    
    public class Reservation<TKey,TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : struct, IEquatable<TKey>
            where TUser : AppUser<TKey>
    {
        
        public int ReservationNumber { get; set; }

        [Availability]
        public DateTime CheckInDate{ get; set; }
        [Availability]

        public DateTime CheckOutDate { get; set; }

        public Guid RoomId { get; set; }

        public ICollection<Room>? Rooms { get; set; }

        public Guid PropertyId { get; set; }

        public bool IsCancelled { get; set; } = false;
        public virtual TKey AppUserId { get; set; }
        public virtual TUser? AppUser { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]

        public decimal TotalPrice { get; set; }
        public int Adults { get; set; }
        
        public int Children { get; set; }

        // [ForeignKey(nameof(ReservedBy))]
        // public virtual Guid GuestId { get; set; }
        // public virtual Guest? ReservedBy { get; set; }
    }
}