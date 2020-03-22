
using System;
using DAL.Base;

namespace Domain
{
    public class Invoice: DomainEntity 
    {

        public Reservation Reservation { get; set; }

        public Guest Guest { get; set; }

        public DateTime MadeAt { get; set; } = DateTime.Now;
        
        
    }
}                            