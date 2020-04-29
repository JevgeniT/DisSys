using System;
using DAL.Base;

namespace Domain
{
    public class RoomPolicies : DomainEntity
    {

        public Guid RoomId { get; set; }
        public Room? Room { get; set; }

        public Guid PolicyId { get; set; }
        public Policy? Policy { get; set; }
    }
}