using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        bool EditFields(TEntity entity, List<string> fields);

        bool Delete(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        bool DeleteRange(IEnumerable<TEntity> entities);
        Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 查找数据集
        /// </summary>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="excludeSoftDelete">是否排除已经软删除的记录</param>
        /// <returns>数据集</returns>
        IQueryable<TEntity> FindList(Expression<Func<TEntity, bool>> whereLambda, bool excludeSoftDelete = true);
        /// <summary>
        /// 异步查找数据集
        /// </summary>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="excludeSoftDelete">是否排除已经软删除的记录</param>
        /// <returns>数据集</returns>
        Task<List<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> whereLambda, bool excludeSoftDelete = true);
        /// <summary>
        /// 查找数据集并且对结果排序
        /// </summary>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="sortLambda">排序表达式</param>
        /// <param name="excludeSoftDelete">是否排除已经软删除的记录</param>
        /// <returns>已排序的数据集</returns>
        IQueryable<TEntity> FindList<TField>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc, bool excludeSoftDelete = true);
        /// <summary>
        /// 异步查找数据集并且对结果排序
        /// </summary>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="sortLambda">排序表达式</param>
        /// <param name="excludeSoftDelete">是否排除已经软删除的记录</param>
        /// <returns>已排序的数据集</returns>
        Task<List<TEntity>> FindListAsync<TField>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc, bool excludeSoftDelete = true);
        /// <summary>
        /// 查找分页数据集
        /// </summary>
        /// <typeparam name="TField">排序字段</typeparam>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalRecord">记录总数量</param>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="sortLambda">排序表达式</param>
        /// <param name="isAsc">是否降序</param>
        /// <param name="excludeSoftDelete">是否排除已经软删除的记录
        /// 如果实现了ISoftDelete接口,那么可对结果集进行过滤,否则此选项不做任何操作
        /// </param>
        /// <returns></returns>
        IQueryable<TEntity> FindPageList<TField>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc, bool excludeSoftDelete = true);
        /// <summary>
        /// 异步查找分页数据集
        /// </summary>
        /// <typeparam name="TField">排序字段</typeparam>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalRecord">记录总数量</param>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="sortLambda">排序表达式</param>
        /// <param name="isAsc">是否降序</param>
        /// <param name="excludeSoftDelete">是否排除已经软删除的记录
        /// 如果实现了ISoftDelete接口,那么可对结果集进行过滤,否则此选项不做任何操作
        /// </param>
        /// <returns></returns>
        Task<List<TEntity>> FindPageListAsync<TField>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TField>> sortLambda, bool isAsc, bool excludeSoftDelete = true);
    }
}
