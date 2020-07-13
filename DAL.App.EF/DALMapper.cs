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
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Property, Domain.Property>();
            MapperConfigurationExpression.CreateMap<Domain.Property, DAL.App.DTO.Property>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Property, BLL.App.DTO.Property>();
            MapperConfigurationExpression.CreateMap<Domain.Availability, DAL.App.DTO.Availability>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Availability, Domain.Availability>();
            MapperConfigurationExpression.CreateMap<Domain.Facility, DAL.App.DTO.Facility>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Facility, BLL.App.DTO.Facility>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Room , BLL.App.DTO.Room>();
            MapperConfigurationExpression.CreateMap<Domain.Room , DAL.App.DTO.Room>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Room , Domain.Room>();

            MapperConfigurationExpression.CreateMap<Domain.Availability , DAL.App.DTO.Availability>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Availability , DAL.App.DTO.Availability>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Policy , BLL.App.DTO.Policy>();
            MapperConfigurationExpression.CreateMap<Domain.Policy, DAL.App.DTO.Policy>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Policy , Domain.Policy>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.PropertyView, DAL.App.DTO.PropertyView>();
            MapperConfigurationExpression.CreateMap<Domain.Review , DAL.App.DTO.Review>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Availability , Domain.Availability>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}