using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;


namespace Domain
{
 
    public class Availability : IDomainEntityBaseMetadata
    {
        private DateTime _from;
        private DateTime _to;
        public Guid Id { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime From
        {
            get
            {
                return _from.Date;
            }
            set
            {
                _from = value.Date;
            }
        }
        
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime To {
            get
            {
                return _to.Date;
            }
            set
            {
                _to = value.Date;
            }
        }
        
        public Guid RoomId { get; set; }

        public bool IsUsed { get; set; } = false;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerNight { get; set; }


        public override string ToString()
        {
            return $"From: {From}, To: {To}, IsUsed: {IsUsed}";
        }
    }
}

// dotnet aspnet-codegenerator controller -name PolicyController -actions -m Policy -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
// dotnet aspnet-codegenerator controller -name RoomFacilityController -actions -m Domain.RoomFacilities -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
