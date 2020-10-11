using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.App.DTO
{
    public class Availability : Availability<Guid>, IDomainBaseEntity
    {
    }

    

    public class Availability<TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public ICollection<AvailabilityPolicies>? AvailabilityPolicies { get; set; }
        public bool Active { get; set; }
        public Guid RoomId { get; set; }
        public Room? Room { get; set; }
        public decimal PricePerNightForAdult { get; set; }
        
        public decimal PricePerNightForChild { get; set; }
        
        public bool PricePerPerson { get; set; }
        
        public int RoomsAvailable { get; set; }
        public Availability ShallowCopy()
        {
            return (Availability) this.MemberwiseClone();
        }
        public Availability DeepCopy()
        {
            return (Availability) this.MemberwiseClone();
        }
    }
}