using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;
using DAL.Base;
 
namespace DAL.App.DTO
{
    
    
    public class Reservation : Reservation<Guid>, IDomainBaseEntity
    {
    }
    
    public class Reservation<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
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
 
        public TKey Id { get; set; }
    }
}