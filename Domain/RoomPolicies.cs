using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class RoomPolicies : IDomainEntityBaseMetadata
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public Room? Room { get; set; }

        public Guid PolicyId { get; set; }
        public Policy? Policy { get; set; }
    }
}