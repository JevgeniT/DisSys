using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    
    
    public class Reservation : Reservation<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
    }
    
    public class Reservation<TKey,TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : struct, IEquatable<TKey>
            where TUser : AppUser<TKey>
    {
        public int ReservationNumber { get; set; } = 0;

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime CheckInDate{ get; set; }
        
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }
        public TKey PropertyId { get; set; }  = default!;
        public Property? Property { get; set; }
        public Review? Review { get; set; }
        public Status Status { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public virtual TKey AppUserId { get; set; } = default!;
        public virtual TUser? AppUser { get; set; }
        [MaxLength(128)]
        public string? Message { get; set; }
        public string? ArrivalTime { get; set; }
        public ICollection<ReservationRooms>? ReservationRooms { get; set; }
        public ICollection<ReservationExtras>? ReservationExtras { get; set; }
    }
    public enum Status
    { 
        Active,
        Cancelled,
        [Display(Name = "In the past")] 
        InThePast
    }
}