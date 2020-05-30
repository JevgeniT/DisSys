using System;
using Contracts.DAL.Base;
   
namespace DAL.App.DTO
{
    public class Review : Review<Guid>, IDomainBaseEntity
    {
    }

    public class Review<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public virtual int score { get; set; }

    }
    
}