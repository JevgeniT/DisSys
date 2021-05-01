using System;

namespace Public.DTO
{
    public record PropertyCreateDTO
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; } = default!;
        public Guid Id { get; set; }
    }
}