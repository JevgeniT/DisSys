using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    
    
    public class Reservation : Reservation<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
    }
    
    public class Reservation<TKey,TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : struct, IEquatable<TKey>
            where TUser : AppUser<TKey>
    {

        public virtual int ReservationNumber { get; set; }

        // public DateTime MadeAt { get; set; } = DateTime.Now;
        //approx
        // public DateTime CheckIn { get; set; }

        public virtual DateTime CheckInDate{ get; set; }
        
        public virtual DateTime CheckOutDate { get; set; }

        public Guid RoomId { get; set; }
        
        public ICollection<Room>? Rooms { get; set; }

        public virtual Guid PropertyId { get; set; }        
        
        // public Property Property { get; set; }

        // public Extra Extra { get; set; }    
        public virtual TKey AppUserId { get; set; }
        public virtual TUser? AppUser { get; set; }

        // [ForeignKey(nameof(ReservedBy))]
        // public virtual Guid GuestId { get; set; }
        // public virtual Guest? ReservedBy { get; set; }
    }
}