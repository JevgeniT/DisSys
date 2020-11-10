using System;
using Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class AvailabilityPolicies : AvailabilityPolicies<Guid>, IDomainBaseEntity
    {
        
    }
    public class AvailabilityPolicies<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public Guid AvailabilityId { get; set; }
        public Availability? Availability { get; set; }

        public Guid PolicyId { get; set; }
        public Policy? Policy { get; set; }

        public bool Active { get; set; }
    }
}