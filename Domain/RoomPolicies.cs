using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class RoomPolicies : IDomainEntityBaseMetadata
    {

        public Guid PropertyRoomsRoomId { get; set; }
        public PropertyRooms? PropertyRooms { get; set; }

        public Guid PolicyId { get; set; }
        public Policy? Policy { get; set; }
        public Guid Id { get; set; }
    }
}