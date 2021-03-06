﻿using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Review : Review<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
    }
    public class Review<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
     
        [Range(1,10)]
        public int Score { get; set; }
        public TKey ReservationId { get; set; } = default!;
        
        public Reservation? Reservation { get; set; }

        public string? Comment { get; set; }
        public TKey PropertyId { get; set; } = default!;
        
        public Property? Property { get; set; }
        public TKey AppUserId { get; set; }= default!;
        public virtual TUser? AppUser { get; set; }
        
    }
}