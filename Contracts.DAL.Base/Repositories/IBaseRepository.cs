using System;
using System.Collections.Generic;
  using System.Threading.Tasks;

namespace Contracts.DAL.Base.Repositories
{
    public interface IBaseRepository<TEntity>: IBaseRepository<TEntity, int>
        where TEntity: class, IDomainEntity<int>, new()
    {
        
    }


    public interface IBaseRepository<TEntity, Tkey>
        where TEntity: class, IDomainEntity<Tkey>,new()
        where Tkey: struct, IComparable

    {

        IEnumerable<TEntity> All();
        
        Task<IEnumerable<TEntity>> AllAsync();
        //
        // IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null);
        // Task<IEnumerable<TEntity>>GetAsync(Expression<Func<TEntity,bool>>? filter =null);
            
        TEntity Find(params object[] id);
        Task<TEntity> FindAsync(params object[] id);

        TEntity Add(TEntity entity);

        TEntity Update( TEntity entity);

        TEntity Remove(TEntity entity);
        
        TEntity Remove(params object[] id);

        int SaveChanges();
        
        Task<int> SaveChangesAsync();

    }
}