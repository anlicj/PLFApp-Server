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
            context.Entry<TEntity>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
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

        public IQueryable<TEntity> FindList(Expression<Func<TEntity, bool>> whereLambda)
        {
            return context.Set<TEntity>().Where(whereLambda);
        }

        public IQueryable<TEntity> FindList<TField>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc)
        {
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

        public IQueryable<TEntity> FindPageList<TField>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc)
        {
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

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }

        public Task<List<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> whereLambda)
        {
            return FindList(whereLambda).ToListAsync();
        }

        public Task<List<TEntity>> FindListAsync<TField>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc)
        {
            return FindList(whereLambda, sortLambda, isAsc).ToListAsync();
        }

        public Task<List<TEntity>> FindPageListAsync<TField>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc)
        {
            var list = FindPageList(pageIndex, pageSize, out totalRecord, whereLambda, sortLambda, isAsc);
            return list.ToListAsync();
        }
    }
}
