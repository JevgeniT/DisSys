namespace Domain
{
    public class RoomPolicies
    {
        public int RoomPoliciesId { get; set; }

        public int RoomId { get; set; }
        public Room? Room { get; set; }

        public int PolicyId { get; set; }
        public Policy? Policy { get; set; }
    }
}