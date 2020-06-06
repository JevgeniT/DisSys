﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
 using Contracts.DAL.Base;
using DAL.App.DTO;
 
namespace BLL.App.DTO
{
    public class  Property: Property<Guid>, IDomainBaseEntity
    {
    }
    
    public class Property<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string PropertyName { get; set; }
  
        public string Address { get; set; }
        
        public string PropertyLocation { get; set; }

        
        [InverseProperty(nameof(Room.RoomProperty))]
        public ICollection<BLL.App.DTO.Room>? PropertyRooms { get; set; }

        public PropertyType Type { get; set; }

        public TKey AppUserId { get; set; }
        public Identity.AppUser<TKey>? AppUser { get; set; }
    }

    public enum PropertyType
    {
        Hotel,Hostel, Apartments 
    }
}