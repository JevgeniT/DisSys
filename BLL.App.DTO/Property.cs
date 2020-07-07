using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
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

        public int Score { get; set; }

        public ICollection<Room>? PropertyRooms { get; set; }
        
        [JsonIgnore]
        public PropertyType Type { get; set; }
        
        public string PropertyType
        {
            get { return Type.ToString(); }
        }

        [JsonIgnore]
        public TKey AppUserId { get; set; }= default!;
        [JsonIgnore]
        
        public Identity.AppUser<TKey>? AppUser { get; set; }

    }

    public enum PropertyType
    {
        [EnumMember(Value = "Hotel")]
        Hotel,
        Hostel,
        Apartments 
    }
}

