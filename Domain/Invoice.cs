
using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Invoice: IDomainEntityBaseMetadata 
    {

        public Reservation Reservation { get; set; }

        public Guest Guest { get; set; }

        public DateTime MadeAt { get; set; } = DateTime.Now;


        public Guid Id { get; set; }
    }
}                            