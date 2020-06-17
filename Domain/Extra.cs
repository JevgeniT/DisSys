using System;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Extra : IDomainEntityBaseMetadata
    {
 
        public string? Name { get; set; }

        public Guid? FacilityId { get; set; }
        
        public Facility? Facility { get; set; }

        [Column(TypeName = "decimal(18,2)")]

        public decimal Fee { get; set; }
        public Guid Id { get; set; }
    }
    
}