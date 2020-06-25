﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Contracts.DAL.Base;
using DAL.Base;

namespace BLL.App.DTO
{
    public class Room : Room<Guid>, IDomainBaseEntity
    {
    }
    public class Room<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public string? Name { get; set; }
        public int Capacity { get; set; }
        public int Size { get; set; }  //m2
        public BedType Bed { get; set; }
        public string? Description { get; set; }
        public Guid PropertyId { get; set; }
        
        public ICollection<Facility>? RoomFacilities { get; set; }
        
        public ICollection<Availability>? RoomAvailabilities { get; set; }
        
    }
        public enum BedType
        {
            Large,
            Single,
            Double 
        }
}