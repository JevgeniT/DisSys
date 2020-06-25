using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;
using DAL.Base;
 
namespace BLL.App.DTO
{
    
    
    public class Reservation : Reservation<Guid>, IDomainBaseEntity
    {
    }
    
    public class Reservation<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {

        public  int ReservationNumber { get; set; }

        public  DateTime CheckInDate{ get; set; }
        
        public  DateTime CheckOutDate { get; set; }

        public Guid RoomId { get; set; }
        
        public ICollection<Room>? Rooms { get; set; }

        public Guid PropertyId { get; set; }
        public decimal TotalPrice { get; set; }
        public TKey AppUserId { get; set; }= default!;
 
        public TKey Id { get; set; }= default!;
    }
}