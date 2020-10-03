using System;
using System.Collections.Generic;
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
        private DateTime _from;
        private DateTime _to;

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public TKey Id { get; set; }
        
        public List<Month> Months { get; set; }

        
        
        [BsonIgnore]
        public DateTime From
        {
            get { return _from.Date; }
            set { _from = value.Date; }
        }
        
        [BsonIgnore]
        public DateTime To
        {
            get => _to.Date;
            set => _to = value.Date;
        }

        [BsonIgnore]

        public Guid RoomId { get; set; }
        [BsonIgnore]

        public Room? Room { get; set; }
        [BsonIgnore]

        public ICollection<AvailabilityPolicies>? AvailabilityPolicies { get; set; }
        [BsonIgnore]

        public bool Active { get; set; } = true;
        [BsonIgnore]

        public decimal PricePerNightForAdult { get; set; }
        [BsonIgnore]

        public decimal PricePerNightForChild { get; set; }
        [BsonIgnore]

        public bool PricePerPerson { get; set; }
        [BsonIgnore]

        public int RoomsAvailable { get; set; }
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