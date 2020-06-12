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
                config.CreateMap<BLL.App.DTO.Room, DAL.App.DTO.Room>();
                config.CreateMap<DAL.App.DTO.Room, BLL.App.DTO.Room>();

                config.CreateMap<DAL.App.DTO.Property, BLL.App.DTO.Property>();
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