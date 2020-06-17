using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Property : Property<Guid, AppUser>, IDomainEntity
    {
        
    }
    public class Property<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public string? PropertyName { get; set; }
        public string Description { get; set; }
        public string? Address { get; set; }
        
        public string? Country { get; set; }
        
        public ICollection<Room>? PropertyRooms { get; set; }

        public PropertyType Type { get; set; }

        public TKey AppUserId { get; set; }= default!;
        public TUser? AppUser { get; set; }
 
    }

    public enum PropertyType
    {
        Hotel,Hostel, Apartments 
    }
}