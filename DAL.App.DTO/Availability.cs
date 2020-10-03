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
        // public DateTime From { get; set; }
        // public DateTime To { get; set; }

        public ICollection<AvailabilityPolicies>? AvailabilityPolicies { get; set; }
        
        public Dictionary<string, Month> Years { get; set; }
        // public bool Active { get; set; }
        // public Guid RoomId { get; set; }
        // public Room? Room { get; set; }
        // public decimal PricePerNightForAdult { get; set; }
        //
        // public decimal PricePerNightForChild { get; set; }
        //
        // public bool PricePerPerson { get; set; }
        //
        // public int RoomsAvailable { get; set; }
    }
    
    public class Month
    {
        public string Name { get; set; }
        public List<Day> Days { get; set; }
        
    }
    
    
    public class Day
    {
        public string Name { get; set; }
        public List<RoomPrice> RoomPrices { get; set; }
         
    }
    
    public class RoomPrice
    {
        [BsonRepresentation(BsonType.String)]
        public string RoomId { get; set; }
        public decimal Price { get; set; }
    }
}