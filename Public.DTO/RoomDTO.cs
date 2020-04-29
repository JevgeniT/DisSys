using System;

namespace Public.DTO
{
    public class RoomDTO
    {
        public Guid Id { get; set; }
        public string RoomName { get; set; }
        public int RoomCapacity { get; set; }
        public int RoomSize { get; set; }
    }
}