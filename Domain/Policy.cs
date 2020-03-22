using DAL.Base;

namespace Domain
{
    public class Policy : DomainEntity
    {
        public string PolicyName { get; set; }

        public int PrepaymentBefore { get; set; }

        public int CancellationBefore { get; set; }

    }
}