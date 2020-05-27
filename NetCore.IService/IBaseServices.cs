using NetCore.DTO.ReponseViewModel;
using NetCore.EntityModel.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.IServices
{
   public  interface IBaseServices<T> where T : class
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">新增实体</param>
        /// <returns></returns>
        Task<bool> AddService(T entity);

       
    }
}
