using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
   
namespace DAL.App.DTO
{
    public class Review : Review<Guid>, IDomainBaseEntity
    {
    }

    public class Review<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; }= default!;
        
        [Range(0,10)]
        public virtual int Score { get; set; }
        
        public virtual string? Comment { get; set; }

        public virtual DateTime CreatedAt { get; set; }
        
        public virtual Guid PropertyId { get; set; }
        
        public virtual Property? Property { get; set; }
        
        public virtual TKey AppUserId { get; set; }= default!;
        
     }
    
}