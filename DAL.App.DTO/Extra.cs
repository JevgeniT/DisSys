﻿using System;
using Contracts.DAL.Base;
 
namespace DAL.App.DTO
{
    public class Extra : Extra<Guid>, IDomainBaseEntity
    {
    }
    
    public class Extra<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        public string? Name { get; set; }
        public double Price { get; set; }
        public TKey PropertyId { get; set; }
  
    }
}