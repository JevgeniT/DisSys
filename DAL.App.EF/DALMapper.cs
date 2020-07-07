using System;
using AutoMapper;
using DAL.App.DTO;
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
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Availability, BLL.App.DTO.Availability>();
            MapperConfigurationExpression.CreateMap<Domain.Facility, DAL.App.DTO.Facility>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Facility, BLL.App.DTO.Facility>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Room , BLL.App.DTO.Room>();
            MapperConfigurationExpression.CreateMap<Domain.Room , DAL.App.DTO.Room>();
            MapperConfigurationExpression.CreateMap<Domain.Availability , DAL.App.DTO.Availability>();
            MapperConfigurationExpression.CreateMap<Domain.Policy , DAL.App.DTO.Policy>();


            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}