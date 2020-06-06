using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Policy : IDomainEntityBaseMetadata
    {
        public Guid Id { get; set; }
        
        public PolicyName PolicyName { get; set; }

        public int PrepaymentBefore { get; set; }

        public int CancellationBefore { get; set; }

    }


    public enum PolicyName
    {
        NonRefundable, General, Flexible
    }
}