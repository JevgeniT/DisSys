using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Contracts.DAL.Base;
using DAL.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain
{

    public class Availability : Availability<Guid>, IDomainEntityBaseMetadata
    {
    }


    public class Availability<TKey> : DomainEntityBaseMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
 
        public TKey Id { get; set; }
    
        [Column(TypeName="date")]
        public DateTime From { get; set; }
        
        [Column(TypeName="date")]
        public DateTime To { get; set; }
        public TKey RoomId { get; set; }
        public Room? Room { get; set; }
 
        public ICollection<AvailabilityPolicies>? AvailabilityPolicies { get; set; }
        public bool Active { get; set; } = true;
 
        [Column(TypeName="decimal(5, 2)")]
        public decimal PricePerNightForAdult { get; set; }
 
        [Column(TypeName="decimal(5, 2)")]
        public decimal PricePerNightForChild { get; set; }
 
        public bool PricePerPerson { get; set; }
 
        public int RoomsAvailable { get; set; }
    }
    
}