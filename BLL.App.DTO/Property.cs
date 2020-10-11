using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class  Property: Property<Guid>, IDomainBaseEntity
    {
    }
    
    public class Property<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }
        public ICollection<Review>? Reviews { get; set; }

        public ICollection<Room>? PropertyRooms { get; set; }
        
        public string Type { get; set; }
        
        public TKey AppUserId { get; set; } = default!;
        
        public Identity.AppUser<TKey>? AppUser { get; set; }

    }
}

