using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;

namespace BLL.App.DTO
{
    public class Room : Room<Guid>, IDomainBaseEntity
    {
    }
    public class Room<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public virtual string RoomName { get; set; }
        public virtual int RoomCapacity { get; set; }
        public virtual int RoomSize { get; set; }  //m2

        [ForeignKey(nameof(RoomPropertyId))]
        public Guid RoomPropertyId { get; set; }
        
        public Property? RoomProperty { get; set; }
        
        public ICollection<RoomAvailability>? RoomAvailabilities { get; set; }
        
        public enum BedType
        {
            Large, Single, Double 
        }

     }
}