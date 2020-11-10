using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class PropertyRules: PropertyRules<Guid>, IDomainBaseEntity
    {}
    public class PropertyRules<TKey> : IDomainBaseEntity<TKey>
    where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; }  = default!;
        public TimeSpan CheckInFrom { get; set; }
        public TimeSpan CheckInTo { get; set; }
        public TimeSpan CheckOutBefore { get; set; }
        public bool DamageDepositRequired { get; set; }
        public decimal? DamageDeposit { get; set; }
        public ICollection<string>? PaymentMethodsAccepted { get; set; }
        public bool AllowPets { get; set; }
        public bool AllowParties { get; set; }
        public int CheckInAge { get; set; }

     }
}