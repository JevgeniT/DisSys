using System;

namespace Public.DTO
{
    public class ExtraDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid PropertyId { get; set; }
    }
}