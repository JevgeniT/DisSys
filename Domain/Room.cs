using System;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class Room : DomainEntity
    {

        public string RoomName { get; set; }
        public int RoomCapacity { get; set; }
        public int RoomSize { get; set; }  //m2

        [ForeignKey(nameof(RoomPropertyId))]
        public Guid RoomPropertyId { get; set; }
        
        public Property? RoomProperty { get; set; }
        
        public enum BedType
        {
            Large, Single, Double 
        }
    }
}