using AutoMapper;
using BLL.App.DTO;
using Contracts.BLL.Base.Mappers;
 
namespace BLL.Base.Mappers
{
    public class BaseBLLMapper<TInObject, TOutObject> : IBaseBLLMapper<TInObject, TOutObject>
        where TInObject : class, new()
        where TOutObject : class, new()

    {
    private readonly IMapper _mapper;

    public BaseBLLMapper()
    {
        _mapper = new MapperConfiguration(
            config =>
            {
                config.CreateMap<TInObject, TOutObject>();
                config.CreateMap<TOutObject, TInObject>();
                config.CreateMap<Room, DAL.App.DTO.Room>();
                
                config.CreateMap<DAL.App.DTO.Property, Property>().ForMember(property => property.PropertyRooms, opt=>opt.Ignore());;

                // config.CreateMap<Domain.Property, DAL.App.DTO.Property>().ForMember(property => property.PropertyRooms, opt=>opt.Ignore());


            }).CreateMapper();
    }

    public virtual TInObject Map(TOutObject inObject)
    {
        return _mapper.Map<TOutObject, TInObject>(inObject);
    }
    
    public TOutObject Map(TInObject inObject)
    {
        return _mapper.Map<TInObject, TOutObject>(inObject);
    }

    public TMapOutObject Map<TMapInObject, TMapOutObject>(TMapInObject inObject)
        where TMapInObject : class, new()
        where TMapOutObject : class, new()
    {
        return _mapper.Map<TMapInObject, TMapOutObject>(inObject);
    }

    
    }
}