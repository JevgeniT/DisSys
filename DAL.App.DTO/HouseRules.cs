using System;
using Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class HouseRules : IDomainEntityBaseMetadata
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public DateTime CheckInFrom { get; set; }
        public DateTime CheckInTo { get; set; }
        public DateTime CheckOutBefore { get; set; }
        public int AdultIfOlderThan { get; set; }
        public bool DamageDepositRequired { get; set; }
        public decimal DamageDeposit { get; set; }
        public string PaymentMethodsAccepted { get; set; } //todo implement
        public bool AllowPets { get; set; }
        public bool AllowParties { get; set; }
    }
}