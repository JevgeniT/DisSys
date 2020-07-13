using System;
using System.Linq;
using AutoMapper;
using BLL.App.DTO;

namespace Public.DTO.Mappers
{
    public class PropertyMapper: BaseMapper<BLL.App.DTO.Property, PropertyDTO>
    {
        public PropertyMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Property, PropertyDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Room, RoomDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Facility, FacilityDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Property, PropertyDTO>()
                .ForMember(dto => dto.Score, opt=> 
                    opt.MapFrom(property => 
                        Math.Round(property.Reviews.Average(review => review.Score),1)));
            
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.PropertyView, PropertyViewDTO>();



            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public PropertyViewDTO MapPropertyView(BLL.App.DTO.Property inObject)
        {
            return Mapper.Map<PropertyViewDTO>(inObject);
        }
    }
}
