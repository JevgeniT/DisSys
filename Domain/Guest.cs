using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    
    
    public class Guest : Guest<Guid>, IDomainEntity
    {
        
    }
     
    
    public class Guest<TKey>: DomainEntity <TKey>
    where TKey: struct , IEquatable<TKey>
    {
        public virtual string FirstName { get; set; } =default!;
        public virtual string LastName { get; set; } =default!;

        public virtual TKey AppUserId{ get; set; }
        public virtual AppUser? AppUser { get; set; } 
        
        [InverseProperty(nameof(Reservation.ReservedBy))]
        
        public virtual ICollection<Reservation>? GuestReservations { get; set; }
    }
    
    
    
    
}
