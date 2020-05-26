using NetCore.DTO.ReponseViewModel;
using NetCore.EntityModel.QueryModels;
using NetCore.IRepository;
using NetCore.IService;
using NetCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Service
{
    public class BaseService<T> : IBaseService<T> where T : class, new()
    {
        private readonly IRepository<T> _repository;
        public BaseService(IRepository<T> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository", "repository为空");
            }
            _repository = repository;
        }

        public async Task<HttpReponseViewModel<T>> AddService(T entity)
        {
            HttpReponseViewModel<T> httpReponse = new HttpReponseViewModel<T>();
            httpReponse.Data = await _repository.Add(entity);
            return httpReponse;
        }

        public async Task<HttpReponseViewModel<List<T>>> AddListService(IEnumerable<T> list)
        {
            HttpReponseViewModel<List<T>> httpReponse = new HttpReponseViewModel<List<T>>();
            httpReponse.Data = await _repository.AddList(list);
            return httpReponse;
        }

        public async Task<HttpReponseViewModel<T>> UpdateService(T entity)
        {
            HttpReponseViewModel<T> httpReponse = new HttpReponseViewModel<T>();
            httpReponse.Data = await _repository.Update(entity);
            return httpReponse;
        }

        public async Task<HttpReponseViewModel<List<T>>> UpdateListService(IEnumerable<T> list)
        {
            HttpReponseViewModel<List<T>> httpReponse = new HttpReponseViewModel<List<T>>();
            httpReponse.Data = await _repository.UpdateList(list);
            return httpReponse;
        }
        public async Task<HttpReponseViewModel<int>> DeleteService(params object[] keyValues)
        {
            HttpReponseViewModel<int> httpReponse = new HttpReponseViewModel<int>();
            httpReponse.Data = await _repository.Delete(keyValues);
            return httpReponse;
        }

        public async Task<HttpReponseViewModel<int>> DeleteService(Expression<Func<T, bool>> whereLambda)
        {
            HttpReponseViewModel<int> httpReponse = new HttpReponseViewModel<int>();
            httpReponse.Data = await _repository.Delete(whereLambda);
            return httpReponse;
        }

        public async Task<HttpReponseViewModel<T>> GetEntityService(params object[] keyValues)
        {
            HttpReponseViewModel<T> httpReponse = new HttpReponseViewModel<T>();
            httpReponse.Data = await _repository.GetEntity(keyValues);
            return httpReponse;
        }

        public async Task<HttpReponseViewModel<T>> GetEntityService(Expression<Func<T, bool>> whereLambda)
        {
            HttpReponseViewModel<T> httpReponse = new HttpReponseViewModel<T>();
            httpReponse.Data = await _repository.GetEntity(whereLambda);
            return httpReponse;
        }

        public async Task<HttpReponseViewModel<List<T>>> GetListService(Expression<Func<T, bool>> whereLambda)
        {
            HttpReponseViewModel<List<T>> httpReponse = new HttpReponseViewModel<List<T>>();
            httpReponse.Data = await _repository.GetList(whereLambda);

            return httpReponse;
        }

        public async Task<HttpReponseViewModel<List<T>>> GetPageListService(QueryModel queryParam)
        {
            HttpReponseViewModel<List<T>> httpReponse = new HttpReponseViewModel<List<T>>();
            httpReponse.Data = await _repository.GetPageList(queryParam);
            httpReponse.Total = queryParam.Total;
            httpReponse.PageIndex = queryParam.PageIndex;
            httpReponse.PageSize = queryParam.PageSize;
            httpReponse.RequestParams = queryParam;
            return httpReponse;
        }

       
    }
}

