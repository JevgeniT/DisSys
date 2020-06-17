using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Policy : IDomainEntityBaseMetadata, IPolicy
    {
        public Guid Id { get; set; }
        
        public string PolicyName { get; set; }

        public int PrepaymentBefore { get; set; }

        public int CancellationBefore { get; set; }

    }

    public class CancellationPolicy : IPolicy
    {
        
    }
    
    public class PaymentPolicy : IPolicy
    {
        
    }


    public interface IPolicy
    {
        
    }
}