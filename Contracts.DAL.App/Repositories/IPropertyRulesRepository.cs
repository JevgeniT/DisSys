using System;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    
        public interface IPropertyRulesRepository : IPropertyRulesRepository<Guid, PropertyRules>,
            IBaseRepository<PropertyRules>
        {
        }
    
        public interface IPropertyRulesRepository <TKey, TDALEntity> : IBaseRepository<TKey,TDALEntity> 
            where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
            where TKey : IEquatable<TKey>
        {
        }  
        
}