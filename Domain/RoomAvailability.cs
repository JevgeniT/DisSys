using System;
using Contracts.DAL.Base;

namespace Domain
{
    public class RoomAvailability : IDomainEntityBaseMetadata
    {
        public Guid Id { get; set; }


        public Guid RoomId { get; set; }

        public Room? Room { get; set; }

        public Guid AvailabilityId { get; set; }
        public Availability? Availability { get; set; }
        
        
    }
}