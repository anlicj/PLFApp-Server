using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PLFApp.Service
{
    public interface IBaseService<TEntity> where TEntity:class,new()
    {
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);

        bool Edit(TEntity entity);
        Task<bool> EditAsync(TEntity entity);
        bool EditRange(IEnumerable<TEntity> entities);
        Task<bool> EditRangeAsync(IEnumerable<TEntity> entities);

        bool Delete(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        bool DeleteRange(IEnumerable<TEntity> entities);
        Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities);

        IQueryable<TEntity> FindList(Expression<Func<TEntity, bool>> whereLambda);
        Task<List<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> whereLambda);
        IQueryable<TEntity> FindList<TField>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc);
        Task<List<TEntity>> FindListAsync<TField>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc);
        IQueryable<TEntity> FindPageList<TField>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc);
        Task<List<TEntity>> FindPageListAsync<TField>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc);
    }
}
