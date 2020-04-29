using System;
using DAL.Base;

namespace Domain
{
    public class RoomFacilities : DomainEntity
    {

        public Guid FacilityId { get; set; }
        public Facility Facility { get; set; }
        
        public Guid RoomId { get; set; }
        public Room? Room { get; set; }
        
        
    }
}