using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Policy : Policy<Guid>, IDomainEntityBaseMetadata
    {
    }
    public class Policy<TKey> : DomainEntityBaseMetadata<TKey>
    where TKey : IEquatable<TKey>
    {
        public TKey PropertyId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int? CancellationBefore { get; set; }
        public int? PrepaymentBefore { get; set; }
        public int? CancellationFee { get; set; }
        public double PriceCoefficient { get; set; }
 
    }
}