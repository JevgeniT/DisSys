using System;
using System.Collections.Generic;

namespace Public.DTO
{
    public class PropertyRulesDTO
    {
        public Guid Id { get; set; }
        public string CheckInFrom { get; set; }= default!;
        public string CheckInTo { get; set; }= default!;
        public string CheckOutBefore { get; set; }= default!;
        public bool DamageDepositRequired { get; set; }
        public decimal? DamageDeposit { get; set; }
        public ICollection<string>? PaymentMethodsAccepted { get; set; }
        public bool AllowPets { get; set; }
        public bool AllowParties { get; set; }
        public int CheckInAge { get; set; }
    }
}