using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using Domain.Validation;


namespace Domain
{
 
    public class Availability : IDomainEntityBaseMetadata
    {
        private DateTime _from;
        private DateTime _to;
        public Guid Id { get; set; }

        [Availability]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime From
        { get { return _from.Date; } set { _from = value.Date; } }
        
        [Availability]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime To {
            get => _to.Date;
            set => _to = value.Date;
        }
        
        public Guid RoomId { get; set; }
        public Room? Room { get; set; }
        public Guid PolicyId { get; set; }
        public Policy? Policy { get; set; }
        public bool IsUsed { get; set; } = false;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerNightForAdult { get; set; }

        [Column(TypeName = "decimal(18,2)")]

        public decimal PricePerNightForChild { get; set; }

        public bool PricePerPerson { get; set; }

        public int RoomsAvailable { get; set; }
    }
}