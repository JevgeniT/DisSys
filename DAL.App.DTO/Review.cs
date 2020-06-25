﻿using System;
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
        public int Score { get; set; }
        
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public Guid PropertyId { get; set; }
        
        public Property? Property { get; set; }
        
        public TKey AppUserId { get; set; }= default!;
        
     }
    
}