using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Policy : IDomainEntityBaseMetadata
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public string Name { get; set; }
        public int? CancellationBefore { get; set; }
        public int? PrepaymentBefore { get; set; }

        public int? CancellationFee { get; set; }
        
        public double PriceCoefficient { get; set; }
        
        public ICollection<AvailabilityPolicies> PolicyAvailabilities { get; set; }

    }
}