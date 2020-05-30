using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Policy : IDomainEntityBaseMetadata
    {
        public string PolicyName { get; set; }

        public int PrepaymentBefore { get; set; }

        public int CancellationBefore { get; set; }

        public Guid Id { get; set; }
    }
}