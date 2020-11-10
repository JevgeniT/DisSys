using System;
using System.Globalization;
using AutoMapper;
using BLL.App.DTO;

namespace Public.DTO.Mappers
{
    public class ReviewMapper:  BaseMapper<Review, ReviewDTO>
    {
        public ReviewMapper()
        {
            MapperConfigurationExpression.CreateMap<Review, ReviewDTO>();
            MapperConfigurationExpression.CreateMap<Review, ReviewPublicDTO>()
                .ForMember(r => r.UserName, opt => opt.MapFrom(review => review.AppUser!.FirstName))
                .ForMember(r=>r.CreatedAt, opt => opt.MapFrom(review => GetMonthAndYear(review.CreatedAt)));

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public ReviewPublicDTO MapPublicView(Review inObject)
        {
            return Mapper.Map<ReviewPublicDTO>(inObject);
        }


        private string GetMonthAndYear(DateTime date)
        {
            return $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month)} {date.Year}";
        }
    }
     
}