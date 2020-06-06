using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace BLL.App.DTO
{
    public class Policy : Policy<Guid>, IDomainBaseEntity
    {
    }
    public class Policy<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    
    {
        public TKey Id { get; set; } = default!;

        public virtual string Title { get; set; }

        public virtual int PrepaymentBefore { get; set; }

        public virtual int CancellationBefore { get; set; }

 
    }
}