using System;
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
    public Reservation Reservation { get; set; }

        public int Score { get; set; }

        public Guest Guest { get; set; }

        public string? Comment { get; set; }

        public Property Property { get; set; }
        public TKey AppUserId { get; set; }
        public TUser? AppUser { get; set; }
    }
}