using AutoMapper;
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
                config.CreateMap<DAL.App.DTO.Room, BLL.App.DTO.Room>();
                config.CreateMap<DAL.App.DTO.RoomFacilities, BLL.App.DTO.RoomFacilities>();
                config.CreateMap<DAL.App.DTO.Property, BLL.App.DTO.Property>();
                config.CreateMap<DAL.App.DTO.Facility, BLL.App.DTO.Facility>();
                config.CreateMap<DAL.App.DTO.Availability, BLL.App.DTO.Availability>();
                config.CreateMap<DAL.App.DTO.ReservationRooms, BLL.App.DTO.ReservationRooms>();
                config.CreateMap<DAL.App.DTO.AvailabilityPolicies, BLL.App.DTO.AvailabilityPolicies>();
                config.CreateMap<DAL.App.DTO.Availability, BLL.App.DTO.Availability>();
                config.CreateMap<DAL.App.DTO.Policy, BLL.App.DTO.Policy>();
                config.CreateMap<DAL.App.DTO.Reservation, BLL.App.DTO.Reservation>();
                config.CreateMap<DAL.App.DTO.Review, BLL.App.DTO.Review>();
                config.CreateMap<DAL.App.DTO.PropertyRules, BLL.App.DTO.PropertyRules>();
                config.CreateMap<DAL.App.DTO.Identity.AppUser, BLL.App.DTO.Identity.AppUser>();
            }).CreateMapper();
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