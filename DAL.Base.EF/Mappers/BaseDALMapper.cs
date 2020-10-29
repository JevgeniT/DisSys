using System.Linq;
using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.Base.EF.Mappers
{

    public class BaseDALMapper<TInObject, TOutObject> : IBaseDALMapper<TInObject, TOutObject>
        where TInObject : class, new()
        where TOutObject : class, new()

    {
        private readonly IMapper _mapper;

        public BaseDALMapper()
        {
            _mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<TInObject, TOutObject>();
                config.CreateMap<TOutObject, TInObject>();
            }).CreateMapper();
        }
        
        

        public TOutObject Map(TInObject inObject)
        {
            return _mapper.Map<TInObject, TOutObject>(inObject);
        }    

        public TMapOutObject Map<TMapInObject, TMapOutObject>(TMapInObject inObject) where TMapInObject : class
            where TMapOutObject : class, new()
        {
            var inProperties = inObject
                .GetType()
                .GetProperties()
                .ToDictionary(
                    key => key.Name,
                    val => val.GetValue(inObject));

            var result = new TMapOutObject();
            foreach (var property in result.GetType().GetProperties())
            {
                if (inProperties.TryGetValue(property.Name, out var value))
                {
                    property.SetValue(result, value);
                }
            }

            return result;
        }
    }
}
