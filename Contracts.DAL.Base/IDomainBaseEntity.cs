﻿using System;

namespace Contracts.DAL.Base
{
    public interface IDomainBaseEntity : IDomainBaseEntity<int>
    {
    }

    public interface IDomainBaseEntity<TKey> 
       // where TKey : struct, IComparable
        where TKey : struct, IEquatable<TKey>


    {
        public TKey Id { get; set; } 
    }
}