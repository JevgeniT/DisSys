using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class RoomFacilities : IDomainEntityBaseMetadata
    {

        public Guid FacilityId { get; set; }
        
        public Facility? Facility { get; set; }
        
        public Guid PropertyRoomsId { get; set; }
        public PropertyRooms? PropertyRooms { get; set; }


        public Guid Id { get; set; }
    }
}