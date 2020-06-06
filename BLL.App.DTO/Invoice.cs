
using System;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;
using DAL.Base;

namespace BLL.App.DTO
{
    public class Invoice: Invoice<Guid>, IDomainBaseEntity
    {
    }
    public class Invoice<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public Reservation Reservation { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }

        public DateTime MadeAt { get; set; } = DateTime.Now;

        public TKey Id { get; set; } = default!;
    }
}                            