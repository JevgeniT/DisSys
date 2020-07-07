namespace Public.DTO.Mappers
{
    public abstract class BaseMapper<TLeftObject, TRightObject> 
        : DAL.Base.EF.BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
    }

}