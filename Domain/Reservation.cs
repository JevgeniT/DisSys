﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public ICollection<ReservationRooms>? ReservationRooms { get; set; }
        public TKey PropertyId { get; set; }
        public Property? Property { get; set; }

        public Review? Review { get; set; }

        public bool Active { get; set; } = true;
        public virtual TKey AppUserId { get; set; }
        public virtual TUser? AppUser { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }

    }
}