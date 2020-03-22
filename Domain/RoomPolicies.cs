using DAL.Base;

namespace Domain
{
    public class RoomPolicies : DomainEntity
    {

        public int RoomId { get; set; }
        public Room? Room { get; set; }

        public int PolicyId { get; set; }
        public Policy? Policy { get; set; }
    }
}