using Microsoft.EntityFrameworkCore;
using PLFApp.Server.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PLFApp.Server.EntityFrameworkCore
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        protected readonly PLFAppDbContext context;
        public BaseRepository(PLFAppDbContext _context)
        {
            this.context = _context;
        }
        public TEntity Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            return entity;
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            context.AddRange(entities);
            return entities;
        }

        public bool Delete(TEntity entity)
        {
            if ((typeof(ISoftDelete)).IsAssignableFrom(typeof(TEntity)))
            {
                //var parameter = Expression.Parameter(typeof(ISoftDelete), "m");
                //var body = Expression.Equal(Expression.Property(parameter, "IsDelete"), Expression.Constant(true));
                //var whereLambda = Expression.Lambda<Func<TEntity, bool>>(body, parameter);
                return EditFields(entity, new List<string>() { "IsDelete" });
            }
            else
            {
                context.Entry<TEntity>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            }
            return true;
        }

        public bool DeleteRange(IEnumerable<TEntity> entities)
        {
            context.AttachRange(entities);
            foreach (var item in entities)
            {
                context.Entry<TEntity>(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            }
            return true;
        }

        public TEntity GetEntity(Expression<Func<TEntity, bool>> whereLambda)
        {
            return context.Set<TEntity>().FirstOrDefault(whereLambda);
        }

        public bool Edit(TEntity entity)
        {
            context.Set<TEntity>().Attach(entity);
            context.Entry<TEntity>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return true;
        }

        public bool EditRange(IEnumerable<TEntity> entities)
        {
            context.AttachRange(entities);
            foreach (var item in entities)
            {
                context.Entry<TEntity>(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            return true;
        }

        public bool EditFields(TEntity entity, List<string> fields)
        {
            if (entity == null || fields == null)
            {
                return false;
            }
            context.Set<TEntity>().Attach(entity);
            foreach (var field in fields)
            {
                context.Entry(entity).Property(field).IsModified = true;
            }
            return true;
        }

        public IQueryable<TEntity> FindList(Expression<Func<TEntity, bool>> whereLambda, bool excludeSoftDelete)
        {
            if (excludeSoftDelete)
            {
                whereLambda = GetSoftDeleteExpression(whereLambda.Parameters[0].Name).And(whereLambda);
                return context.Set<TEntity>().Where(whereLambda);
            }
            else
            {
                return context.Set<TEntity>().Where(whereLambda);
            }
        }

        public IQueryable<TEntity> FindList<TField>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc, bool excludeSoftDelete)
        {
            if (excludeSoftDelete)
            {
                whereLambda = GetSoftDeleteExpression(whereLambda.Parameters[0].Name).And(whereLambda);
            }
            var list = context.Set<TEntity>().Where(whereLambda);
            if (isAsc)
            {
                list = list.OrderBy(sortLambda);
            }
            else
            {
                list = list.OrderByDescending(sortLambda);
            }
            return list;
        }
        
        public Task<List<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> whereLambda, bool excludeSoftDelete)
        {
            return FindList(whereLambda, excludeSoftDelete).ToListAsync();
        }        

        public Task<List<TEntity>> FindListAsync<TField>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc, bool excludeSoftDelete = true)
        {
            return FindList(whereLambda, sortLambda, isAsc, excludeSoftDelete).ToListAsync();
        }

        public IQueryable<TEntity> FindPageList<TField>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc, bool excludeSoftDelete = true)
        {
            if (excludeSoftDelete)
            {
                whereLambda = GetSoftDeleteExpression(whereLambda.Parameters[0].Name).And(whereLambda);
            }
            var list = context.Set<TEntity>().Where(whereLambda);
            totalRecord = list.Count();
            if (isAsc)
            {
                list = list.OrderBy(sortLambda);
            }
            else
            {
                list = list.OrderByDescending(sortLambda);
            }
            return list.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public Task<List<TEntity>> FindPageListAsync<TField>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc, bool excludeSoftDelete = true)
        {
            var list = FindPageList(pageIndex, pageSize, out totalRecord, whereLambda, sortLambda, isAsc,excludeSoftDelete);
            return list.ToListAsync();
        }
        
        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }

        protected Expression<Func<TEntity, bool>> GetSoftDeleteExpression(string parameterName = "m")
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                var parameter = Expression.Parameter(typeof(ISoftDelete), parameterName);
                var body = Expression.Equal(Expression.Property(parameter, "IsDelete"), Expression.Constant(true));
                return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
            }
            Expression<Func<TEntity, bool>> result = m => true;
            return result;
        }
    }
}
