using System;
using DAL.Base;

namespace Domain
{
    public class Review : DomainEntity
    {

        public Reservation Reservation { get; set; }

        public int Score { get; set; }

        public Guest Guest { get; set; }

        public string? Comment { get; set; }

        public Property Property { get; set; }
    }
}