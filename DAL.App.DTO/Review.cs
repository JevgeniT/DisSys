using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Review : Review<Guid>, IDomainBaseEntity
    {
    }

    public class Review<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; }= default!;
        
        public int Score { get; set; }
        
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; }

        public TKey PropertyId { get; set; } = default!;
        
        public Property? Property { get; set; }

        public TKey ReservationId { get; set; }= default!;

        public TKey AppUserId { get; set; }= default!;
        public AppUser AppUser { get; set; } = default!;
        
        
     }
    
}