
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Invoice: Invoice<Guid, AppUser>, IDomainEntity
    {
    }


    public class Invoice<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {


        public TKey AppUserId { get; set; } = default!;
        
        public TUser? AppUser { get; set; }

        public bool IsPaid { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]

        public decimal TotalPrice { get; set; }
        
        public TKey ReservationId { get; set; } = default!;
        
        public Reservation? Reservation { get; set; }
        
         
        
    }
    
}                            