using System;
using Contracts.DAL.Base;

namespace DAL.Base
{
    
    
    
    public  class DomainEntity :  IDomainEntity
        {
            public virtual int Id { get; set; }
            public virtual string? CreatedBy { get; set; }
            public virtual DateTime? CreatedAt { get; set; }
            public virtual string? DeletedBy { get; set; }
            public virtual DateTime? DeletedAt { get; set; }
        }
    
       

    // public abstract class DomainEntity: IDomainBaseEntity
    // {
    //     public int Id { get; set; }
    //
    //     public string? CreatedBy { get; set; }
    //     public DateTime? CreatedAt { get; set; }
    //     public string? DeletedBy { get; set; }
    //     public DateTime? DeletedAt { get; set; }
    // }
}