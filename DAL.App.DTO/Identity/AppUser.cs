using System;

namespace DAL.App.DTO.Identity
{
    public class AppUser : AppUser<Guid>
    {
    }

    public class AppUser<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        public virtual string FirstName { get; set; } = default!;
        public virtual string LastName { get; set; } = default!;
    }
}