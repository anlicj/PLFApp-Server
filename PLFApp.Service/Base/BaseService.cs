using PLFApp.Server.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PLFApp.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        protected IBaseRepository<TEntity> repository { get; set; }
       
        public virtual TEntity Add(TEntity entity)
        {
            repository.Add(entity);
            return repository.SaveChanges() > 0 ? entity : null;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            repository.Add(entity);
            var changeRows = await repository.SaveChangesAsync();
            return changeRows > 0 ? entity : null;
        }

        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            repository.AddRange(entities);
            return repository.SaveChanges() > 0 ? entities : null;
        }

        public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            repository.AddRange(entities);
            var changeRows = await repository.SaveChangesAsync();
            return changeRows > 0 ? entities : null;
        }

        public virtual bool Delete(TEntity entity)
        {
            repository.Delete(entity);
            return repository.SaveChanges() > 0;
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            repository.Delete(entity);
            var changeRows = await repository.SaveChangesAsync();
            return changeRows > 0;
        }

        public virtual bool DeleteRange(IEnumerable<TEntity> entities)
        {
            repository.DeleteRange(entities);
            return repository.SaveChanges() > 0;
        }

        public virtual async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            repository.DeleteRange(entities);
            var changeRows = await repository.SaveChangesAsync();
            return changeRows > 0;
        }

        public virtual bool Edit(TEntity entity)
        {
            repository.Edit(entity);
            return repository.SaveChanges() > 0;
        }

        public virtual async Task<bool> EditAsync(TEntity entity)
        {
            repository.Edit(entity);
            var changeRows = await repository.SaveChangesAsync();
            return changeRows > 0;
        }

        public virtual bool EditRange(IEnumerable<TEntity> entities)
        {
            repository.EditRange(entities);
            var changeRows = repository.SaveChanges();
            return changeRows > 0;
        }

        public virtual async Task<bool> EditRangeAsync(IEnumerable<TEntity> entities)
        {
            repository.EditRange(entities);
            var changeRows = await repository.SaveChangesAsync();
            return changeRows > 0;
        }

        public virtual bool EditFields(TEntity entity, List<string> fields)
        {
            repository.EditFields(entity, fields);
            return repository.SaveChanges() > 0;
        }

        public virtual IQueryable<TEntity> FindList(Expression<Func<TEntity, bool>> whereLambda, bool excludeSoftDelete = true)
        {
            return repository.FindList(whereLambda, excludeSoftDelete);
        }

        public virtual IQueryable<TEntity> FindList<TField>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc, bool excludeSoftDelete = true)
        {
            return repository.FindList(whereLambda,sortLambda,isAsc, excludeSoftDelete);
        }

        public virtual Task<List<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> whereLambda, bool excludeSoftDelete = true)
        {
            return repository.FindListAsync(whereLambda, excludeSoftDelete);
        }

        public virtual Task<List<TEntity>> FindListAsync<TField>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc, bool excludeSoftDelete = true)
        {
            return repository.FindListAsync(whereLambda,sortLambda,isAsc, excludeSoftDelete);
        }

        public virtual IQueryable<TEntity> FindPageList<TField>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc, bool excludeSoftDelete = true)
        {
            return repository.FindPageList(pageIndex,pageSize,out totalRecord,whereLambda, sortLambda, isAsc, excludeSoftDelete);
        }

        public virtual Task<List<TEntity>> FindPageListAsync<TField>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc, bool excludeSoftDelete = true)
        {
            return repository.FindPageListAsync(pageIndex, pageSize, out totalRecord, whereLambda, sortLambda, isAsc, excludeSoftDelete);
        }
    }
}
