using DAL.Base;

namespace Domain
{
    public class RoomFacilities : DomainEntity
    {

        public int FacilityId { get; set; }
        public Facility Facility { get; set; }
        
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        
        
    }
}