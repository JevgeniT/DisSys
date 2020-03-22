using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class DomainBaseEntity : IDomainBaseEntity
    {
        public virtual int Id { get; set; }
    }
}