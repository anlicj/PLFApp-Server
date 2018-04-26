using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PLFApp.Server.Core
{
    public interface IBaseRepository<TEntity> where TEntity:class,new()
    {
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        bool Edit(TEntity entity);
        bool EditRange(IEnumerable<TEntity> entities);
        bool EditFields(TEntity entity, List<string> fields);

        bool Delete(TEntity entity);
        bool DeleteRange(IEnumerable<TEntity> entities);
        TEntity GetEntity(Expression<Func<TEntity,bool>> whereLambda);
        IQueryable<TEntity> FindList(Expression<Func<TEntity, bool>> whereLambda,bool excludeSoftDelete);
        Task<List<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> whereLambda, bool excludeSoftDelete);
        IQueryable<TEntity> FindList<TField>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity,TField>> sortLambda,bool isAsc, bool excludeSoftDelete);
        Task<List<TEntity>> FindListAsync<TField>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc, bool excludeSoftDelete);
        IQueryable<TEntity> FindPageList<TField>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc, bool excludeSoftDelete);
        Task<List<TEntity>> FindPageListAsync<TField>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc, bool excludeSoftDelete);

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
