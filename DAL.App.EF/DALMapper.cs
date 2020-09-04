using System;
using System.Linq;
using AutoMapper;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class DALMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DALMapper() : base()
        { 
            // add more mappings
            MapperConfigurationExpression.CreateMap<Domain.Property, DAL.App.DTO.Property>();
            MapperConfigurationExpression.CreateMap<Domain.Availability, DAL.App.DTO.Availability>();
            MapperConfigurationExpression.CreateMap<Domain.Facility, DAL.App.DTO.Facility>();
            MapperConfigurationExpression.CreateMap<Domain.Room , DAL.App.DTO.Room>();
            MapperConfigurationExpression.CreateMap<Domain.Availability , DAL.App.DTO.Availability>();
            MapperConfigurationExpression.CreateMap<Domain.Policy, DAL.App.DTO.Policy>();
            MapperConfigurationExpression.CreateMap<Domain.AvailabilityPolicies, DAL.App.DTO.AvailabilityPolicies>();
            MapperConfigurationExpression.CreateMap<Domain.Review , DAL.App.DTO.Review>();
            MapperConfigurationExpression.CreateMap<Domain.Identity.AppUser, DAL.App.DTO.Identity.AppUser>();


            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}