using NetCore.DTO.ReponseViewModel;
using NetCore.EntityModel.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.IService
{
   public  interface IBaseService<T> where T : class
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">新增实体</param>
        /// <returns></returns>
        Task<HttpReponseViewModel<T>> AddService(T entity);

        /// <summary>
        /// SqlBulkCopy批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<HttpReponseViewModel<List<T>>> AddListService(IEnumerable<T> list);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="current">更新实体</param>
        /// <returns></returns>
        Task<HttpReponseViewModel<T>> UpdateService(T entity);


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="current">更新实体</param>
        /// <returns></returns>
        Task<HttpReponseViewModel<List<T>>> UpdateListService(IEnumerable<T> list);


        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<HttpReponseViewModel<int>> DeleteService(params object[] keyValues);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<HttpReponseViewModel<int>> DeleteService(Expression<Func<T, bool>> whereLambda);


        /// <summary>
        /// 根据主键获取数据
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        Task<HttpReponseViewModel<T>> GetEntityService(params object[] keyValues);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<HttpReponseViewModel<T>> GetEntityService(Expression<Func<T, bool>> whereLambda);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<HttpReponseViewModel<List<T>>> GetListService(Expression<Func<T, bool>> whereLambda);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        Task<HttpReponseViewModel<List<T>>> GetPageListService(QueryModel queryParam);
    }
}
