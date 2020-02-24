namespace Domain
{
    public class RoomFacilities
    {
        public int RoomFacilitiesId { get; set; }

        public int RoomId { get; set; }
        public Room? Room { get; set; }
        
        
    }
}