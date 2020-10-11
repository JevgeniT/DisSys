using System.Linq;
using Public.DTO.Reservation;
using BLL.App.DTO;
using AutoMapper;

namespace Public.DTO.Mappers
{
    public class ReservationMapper : BaseMapper<BLL.App.DTO.Reservation ,ReservationDTO>
    {

        public ReservationMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Reservation, ReservationDTO>()
                .ForMember(dto => dto.ReservedBy,opt
                    => opt.MapFrom(r  => $"{r.AppUser.FirstName} {r.AppUser.LastName}"))
                .ForMember(dto => dto.RoomDtos, opt 
                    => opt.MapFrom(r  =>  r.ReservationRooms.Select(rooms => rooms.Room)));
            MapperConfigurationExpression.CreateMap<ReservationCreateDTO,BLL.App.DTO.Reservation >()
                .ForMember(res=> res.TotalPrice, opt=> opt.MapFrom(r=>r.RoomDtos.Sum(p=>p.RoomTotalPrice)));
            
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Reservation, ReservationPreviewDTO>().ForMember(res=> res.PropertyName, opt=> opt.MapFrom(r=>r.Property.Name));
            
            
            MapperConfigurationExpression.CreateMap<Room, RoomDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Reservation, ReservationCreateDTO>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }


        public ReservationPreviewDTO MapPreviewDto(BLL.App.DTO.Reservation bllReservation)
        {
            return Mapper.Map<ReservationPreviewDTO>(bllReservation);
        }
        
        public BLL.App.DTO.Reservation MapCreateDto(ReservationCreateDTO createDto)
        {
            return Mapper.Map<BLL.App.DTO.Reservation>(createDto);
        }
    }
}