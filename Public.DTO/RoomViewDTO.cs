using System;

namespace Public.DTO
{
    public class RoomViewDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Size { get; set; }
        public int AdultsCapacity { get; set; }
        public int ChildCapacity { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}