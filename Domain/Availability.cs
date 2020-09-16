using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
 
    public class Availability : Availability<Guid>, IDomainEntityBaseMetadata
    {
    }
    public class Availability<TKey> : DomainEntityBaseMetadata<TKey>
        where TKey: IEquatable<TKey>
    {
        private DateTime _from;
        private DateTime _to;
        public TKey Id { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime From { get { return _from.Date; } set { _from = value.Date; } }
        
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime To { get => _to.Date; set => _to = value.Date; }
        public Guid RoomId { get; set; }
        public Room? Room { get; set; }

        public ICollection<AvailabilityPolicies>? AvailabilityPolicies { get; set; }
        public bool Active { get; set; } = false;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerNightForAdult { get; set; }

        [Column(TypeName = "decimal(18,2)")]

        public decimal PricePerNightForChild { get; set; }

        public bool PricePerPerson { get; set; }

        public int RoomsAvailable { get; set; }
    }
}