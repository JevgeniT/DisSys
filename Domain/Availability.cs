using System;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    
    // public class Availability : Availability<Guid, AppUser>, IDomainEntityUser<AppUser>
    // {
    // }
    //
    // public class Availability<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
    //     where TKey : IEquatable<TKey> 
    //     where TUser : AppUser<TKey>
    // {
    //
    //      public DateTime start { get; set; }
    //
    //
    //     public DateTime end { get; set; }
    //
    //
    //     public Guid RoomId { get; set; }
    //
    //
    //     public TKey AppUserId { get; set; }
    //     public TUser? AppUser { get; set; }
    // }
    public class Availability : IDomainEntityBaseMetadata
    {
        public Guid Id { get; set; }

        public DateTime start { get; set; }

 
        public DateTime end { get; set; }


        public Guid PropertyRoomId { get; set; }


        public decimal Price { get; set; }
        
    }
}