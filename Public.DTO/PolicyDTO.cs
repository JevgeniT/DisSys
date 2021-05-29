using System;

namespace Public.DTO
{
    public class PolicyDTO
    {
        public Guid Id { get; set; }

        public Guid PropertyId { get; set; }
        
        public string? Name { get; set; }

        public int PrepaymentBefore { get; set; }

        public int CancellationBefore { get; set; }
        
        public double PriceCoefficient { get; set; }
    }
}