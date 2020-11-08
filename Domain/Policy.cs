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
        public TKey Id { get; set; }
        public TKey PropertyId { get; set; }
        public string Name { get; set; }
        public int? CancellationBefore { get; set; }
        public int? PrepaymentBefore { get; set; }

        public int? CancellationFee { get; set; }
        
        public double PriceCoefficient { get; set; }
        
        public ICollection<ReservationRooms> PolicyAvailabilities { get; set; }

    }
}