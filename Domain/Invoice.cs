
using System;
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

        public decimal TotalPrice { get; set; }
        
        public Guid ResrvationId { get; set; }
        
        public Reservation? Reservation { get; set; }
        
        public DateTime CreatedAt { get; set; } 
        
        
    }
    
}                            