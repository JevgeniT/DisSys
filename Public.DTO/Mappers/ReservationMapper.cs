using System.Linq;
using Public.DTO.Reservation;
using BLL.App.DTO;
using AutoMapper;
using Public.DTO.Room;

namespace Public.DTO.Mappers
{
    public class ReservationMapper : BaseMapper<BLL.App.DTO.Reservation ,ReservationDTO>
    {

        public ReservationMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Reservation, ReservationDTO>()
                .ForMember(dto => dto.Review, opt
                    => opt.MapFrom(r=>r.Review))
                .ForMember(dto => dto.ReservedBy,opt
                    => opt.MapFrom(r  => $"{r.AppUser!.FirstName} {r.AppUser!.LastName}"))
                .ForMember(dto => dto.RoomDtos, opt 
                    => opt.MapFrom(r  =>  r.ReservationRooms!.Select(rooms => rooms.Room)))
                .ForMember(dto => dto.PropertyName, opt
                    => opt.MapFrom(reservation => reservation.Property!.Name))
                .ForMember(dto => dto.PropertyLocation,opt
                    => opt.MapFrom(r => $"{r.Property!.Address}, {r.Property!.Country}"))
                .ForMember(res => res.ExtraDtos, opt=> opt.MapFrom(e => e.ReservationExtras!.Select(ex=>ex)));
            
            MapperConfigurationExpression.CreateMap<ReservationExtras, ExtraDTO>();

            
            MapperConfigurationExpression.CreateMap<ReservationCreateDTO,BLL.App.DTO.Reservation >()
                // .ForMember(res=> res.TotalPrice, opt=> opt.MapFrom(r=>r.RoomDtos!.Sum(p=>p.RoomTotalPrice)))
                .ForMember(res=> res.ReservationRooms, opt=> opt.MapFrom(r=> r.RoomDtos!.Select(d=>d)))
                .ForMember(res => res.ReservationExtras, opt=> opt.MapFrom(e => e.ReservationExtras!.Select(ex=>ex)));

            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Reservation, ReservationPreviewDTO>()
                .ForMember(res => res.PropertyName, opt
                    => opt.MapFrom(r => r.Property!.Name))
                .ForMember(res => res.ReservedBy, opt
                    => opt.MapFrom(r => $"{r.AppUser!.FirstName} {r.AppUser!.LastName}"));
            
            MapperConfigurationExpression.CreateMap<Property, PropertyDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Room, RoomDTO>();
            MapperConfigurationExpression.CreateMap<Review, ReviewDTO>();
            MapperConfigurationExpression.CreateMap<ReservationRooms, ReservationRoomDTO>();
            MapperConfigurationExpression.CreateMap<ReservationRoomDTO, ReservationRooms>();
            MapperConfigurationExpression.CreateMap<ReservationExtrasDTO, ReservationExtras>();
            MapperConfigurationExpression.CreateMap<ReservationExtras, ReservationExtrasDTO>();

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