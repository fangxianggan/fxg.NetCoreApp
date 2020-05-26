using NetCore.EntityModel.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.IRepository
{
    public interface IEFRepository<T> where T : class
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">新增实体</param>
        /// <returns></returns>
       Task<T> Add(T entity);

        Task<bool> Add(T entity, bool retType);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<List<T>> AddList(IEnumerable<T> list);



        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="current">更新实体</param>
        /// <returns></returns>
        Task<T> Update(T current);

        Task<bool> Update(T current, bool retType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<List<T>> UpdateList(IEnumerable<T> list);
        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<int> Delete(params object[] keyValues);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<int> Delete(Expression<Func<T, bool>> whereLambda);


        /// <summary>
        /// 根据主键获取数据
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        Task<T> GetEntity(params object[] keyValues);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<T> GetEntity(Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<List<T>> GetList(Expression<Func<T, bool>> whereLambda);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        Task<List<T>> GetPageList(QueryModel queryParam);


    }
}
