using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public string? Name { get; set; }
  
        public string? Address { get; set; }
        
        public string? Country { get; set; }
        
        public ICollection<Room>? PropertyRooms { get; set; }

        public PropertyType Type { get; set; }

        public TKey AppUserId { get; set; }= default!;
        public AppUser<TKey>? AppUser { get; set; }
 
    }

    public enum PropertyType
    {
        Hotel,Hostel, Apartments 
    }
}