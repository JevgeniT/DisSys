using System;

namespace Public.DTO
{
    public class AvailabilityPoliciesDTO
    {
        public Guid Id { get; set; }

      
        public Guid AvailabilityId { get; set; }
        public AvailabilityDTO? Availability { get; set; }

        public Guid PolicyId { get; set; }
        public PolicyDTO? Policy { get; set; }
    }
}