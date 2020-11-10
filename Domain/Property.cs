using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Property : Property<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
    }
    public class Property<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string Description { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string Type { get; set; } = default!;
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Room>? PropertyRooms { get; set; }
        public ICollection<Extra>? Extras { get; set; }
        public PropertyRules? PropertyRules { get; set; }
        public TKey AppUserId { get; set; }= default!;
        public TUser? AppUser { get; set; }
 
    }

}