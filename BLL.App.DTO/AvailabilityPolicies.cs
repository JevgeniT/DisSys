using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace BLL.App.DTO
{
    public class AvailabilityPolicies : AvailabilityPolicies<Guid>, IDomainEntityBaseMetadata
    {
        
    }
    public class AvailabilityPolicies<TKey> : DomainEntityBaseMetadata<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; }

        public Guid AvailabilityId { get; set; }
        public Availability? Availability { get; set; }

        public Guid PolicyId { get; set; }
        public Policy? Policy { get; set; }
    }
}