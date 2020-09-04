using System;
using Contracts.DAL.Base;

namespace Domain
{
    public class AvailabilityPolicies : IDomainEntityBaseMetadata
    {
        public Guid Id { get; set; }

        public Guid AvailabilityId { get; set; }
        public Availability? Availability { get; set; }

        public Guid PolicyId { get; set; }
        public Policy? Policy { get; set; }
    }
}