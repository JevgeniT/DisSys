using System;
using Contracts.DAL.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Base
{
    public abstract class DomainEntityBaseMetadata :  DomainEntityBaseMetadata<Guid>
    {
    }

    
    public abstract class DomainEntityBaseMetadata<TKey> :  IDomainEntityBaseMetadata<TKey> 
        where TKey : IEquatable<TKey>
    {
        [BsonIgnore]
        public virtual TKey Id { get; set; } = default!;
        public virtual string? CreatedBy { get; set; }
        public virtual DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual string? ChangedBy { get; set; }
        public virtual DateTime ChangedAt { get; set; } = DateTime.Now;
    }

}