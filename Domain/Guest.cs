using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{


    public class Guest : Guest<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {

    }

    public class Guest<TKey,TUser>:DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public virtual string FirstName { get; set; } =default!;
        public virtual string LastName { get; set; } =default!;

        public virtual TKey AppUserId{ get; set; }
        public virtual TUser? AppUser { get; set; } 
        
        
        //
        // [InverseProperty(nameof(Reservation.ReservedBy))]
        //
        // public virtual ICollection<Reservation>? GuestReservations { get; set; }
    }
    
}
