using AutoMapper;
using AutoMapper.Configuration;
 using DAL.Base.EF;

namespace DAL.App.EF.Mappers
{
    public class DALMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DALMapper() : base()
        { 
            // add more mappings
            MapperConfigurationExpression.CreateMap<Domain.Property, DAL.App.DTO.Property>();
            MapperConfigurationExpression.CreateMap<Domain.Room, DAL.App.DTO.Room>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Property, Domain.Property>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Property, DAL.App.DTO.Property>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Property, BLL.App.DTO.Property>();
            // DAL.App.DTO.Property -> BLL.App.DTO.Property

            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Room , BLL.App.DTO.Room
                >();
            //
            // MapperConfigurationExpression.CreateMap<Domain.App.GpsSession, DAL.App.DTO.GpsSession>();
            // MapperConfigurationExpression.CreateMap<DAL.App.DTO.GpsSession, Domain.App.GpsSession>();
            //
            // MapperConfigurationExpression.CreateMap<Domain.App.GpsLocation, DAL.App.DTO.GpsLocation>();
            // MapperConfigurationExpression.CreateMap<DAL.App.DTO.GpsLocation, Domain.App.GpsLocation>();

            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}