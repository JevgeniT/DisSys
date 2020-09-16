using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Availability:Availability<Guid>, IDomainBaseEntity
    {
    }
    
    public class Availability<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        [DataType(DataType.Date)]
        public DateTime From { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime To { get; set; }
        
        public Guid RoomId { get; set; }

        public Room? Room { get; set; }
        
        public ICollection<AvailabilityPolicies>? AvailabilityPolicies { get; set; }
        public bool Active { get; set; }
        public decimal PricePerNightForAdult { get; set; }
        
        public decimal PricePerNightForChild { get; set; }

        public bool PricePerPerson { get; set; }
        
        public int RoomsAvailable { get; set; }

    }
     
}