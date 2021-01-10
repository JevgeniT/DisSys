using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    
    
    public class Reservation : Reservation<Guid>, IDomainBaseEntity
    {
    }
    
    public class Reservation<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; }= default!;
        public int ReservationNumber { get; set; }
        public DateTime CheckInDate{ get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public Review? Review { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Message { get; set; }
        public string? ArrivalTime { get; set; }
        public TKey PropertyId { get; set; } = default!;
        public Property? Property { get; set; }
        public TKey AppUserId { get; set; } = default!;
        public AppUser AppUser { get; set; } = default!;
        public Status Status { get; set; }
        public ICollection<ReservationRooms>? ReservationRooms { set; get; }
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