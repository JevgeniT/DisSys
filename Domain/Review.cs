using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Review : Review<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
    }
    public class Review<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
     
        [Range(0,10)]
        public int Score { get; set; }

        public Guid ReservationId { get; set; }
        
        public Reservation? Reservation { get; set; }

        public string? Comment { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public Guid PropertyId { get; set; }
        
        public Property? Property { get; set; }
        
        public TKey AppUserId { get; set; }= default!;
        
        public TUser? AppUser { get; set; }
        
    }
}