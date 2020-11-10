using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class PropertyRules : PropertyRules<Guid>, IDomainEntityBaseMetadata
    {
        
    }
    public class PropertyRules<TKey>: DomainEntityBaseMetadata<TKey>
        where TKey: IEquatable<TKey>
    {
     
        public TimeSpan CheckInFrom { get; set; }
        public TimeSpan CheckInTo { get; set; }
        public TimeSpan CheckOutBefore { get; set; } 
        public bool DamageDepositRequired { get; set; }
        [Column(TypeName="decimal(5, 2)")]
        public decimal? DamageDeposit { get; set; }
        public ICollection<string>? PaymentMethodsAccepted { get; set; }
        public bool AllowPets { get; set; }
        public bool? AllowParties { get; set; }
        public int? CheckInAge { get; set; }
        public Property? Property { get; set; }
        
     }
}