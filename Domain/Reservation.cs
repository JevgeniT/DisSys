using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    
    
    public class Reservation : Reservation<Guid>, IDomainEntity
    {
    }
    
    public class Reservation<TKey> : DomainEntity<TKey>
        where TKey : struct, IEquatable<TKey>
    {

        public virtual Guid ReservationNumber { get; set; }

        // public DateTime MadeAt { get; set; } = DateTime.Now;
        //approx
        // public DateTime CheckIn { get; set; }

        public virtual DateTime CheckInDate{ get; set; }
        
        public virtual DateTime CheckOutDate { get; set; }
         
        // public ICollection<Room> Rooms { get; set; }

        public virtual Guid PropertyId { get; set; }        
        
        // public Property Property { get; set; }

        // public Extra Extra { get; set; }    
        public virtual TKey AppUserId { get; set; }
        public virtual AppUser? AppUser { get; set; }

        [ForeignKey(nameof(ReservedBy))]
        public virtual Guid GuestReservationsId { get; set; }
        public virtual Guest? ReservedBy { get; set; }
    }
}