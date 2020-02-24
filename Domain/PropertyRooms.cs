namespace Domain
{
    public class PropertyRooms
    {
        public int PropertyRoomsId { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; }
        
        public int RoomId { get; set; }
        public Room? Room { get; set; }
    }
}