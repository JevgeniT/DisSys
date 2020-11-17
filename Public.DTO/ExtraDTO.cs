using System;

namespace Public.DTO
{
    public class ExtraDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
    }
}