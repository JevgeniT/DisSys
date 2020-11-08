using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;
  
namespace DAL.App.DTO
{
    public class Property : Property<Guid>, IDomainBaseEntity
    {
        
    }
    public class Property<TKey>: IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        public TKey AppUserId { get; set; }= default!;
        public AppUser<TKey>? AppUser { get; set; }
        public string Type { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? Description { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Room>? PropertyRooms { get; set; }
        public ICollection<Extra>? Extras { get; set; }
        public PropertyRules? PropertyRules { get; set; }
    }

}