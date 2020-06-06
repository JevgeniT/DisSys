using AutoMapper;
using AutoMapper.Configuration;
using DAL.App.DTO;

namespace BLL.Base.Mappers
{
    public class PropertyMapper : BaseBLLMapper<Domain.Property,Property>
    {
        public PropertyMapper() : base()
        {
            
         }
    }
}