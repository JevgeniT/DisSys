using System;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Availability:Availability<Guid>, IDomainBaseEntity
    {
    }
    
    public class Availability<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public TKey AppUserId { get; set; }
        
        public virtual Identity.AppUser<TKey>? AppUser { get; set; }
        public virtual DateTime From { get; set; }
        
        public virtual DateTime To { get; set; }
        
        public virtual Guid PropertyRoomId { get; set; }

        public virtual bool IsUsed { get; set; } = false;
        public virtual decimal PricePerNight { get; set; }
    }
     
}