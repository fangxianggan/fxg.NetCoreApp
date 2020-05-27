using Dapper.Contrib.Extensions;
using NetCore.EntityModel.QueryModels;
using NetCore.IRepository;
using NetCore.IRepository.UnitWork;
using NetCore.Repository.Dapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NetCore.Repository
{
    public class DapperRepository<T> : BaseRepository, IDapperRepository<T>
         where T : class, new()
    {

        private readonly IUnitOfWork _unitOfWork;

        public DapperRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Add(T entity)
        {
            return await DapperExtensions.Insert(_unitOfWork, entity, _unitOfWork.DbTransaction);
        }

        public async Task<bool> Delete(params object[] keyValues)
        {
            T entity = await GetEntity(keyValues);
            return await _unitOfWork.DbConnection.DeleteAsync(entity, _unitOfWork.DbTransaction);
        }

        public Task<int> Delete(Expression<Func<T, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetEntity(params object[] keyValues)
        {
            return await _unitOfWork.DbConnection.GetAsync<T>(keyValues, _unitOfWork.DbTransaction);
        }

        public Task<T> GetEntity(Expression<Func<T, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetList(Expression<Func<T, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetPageList(QueryModel queryParam)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(T current)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(T current, bool retType)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> UpdateList(IEnumerable<T> list)
        {
            throw new NotImplementedException();
        }

        Task<int> IDapperRepository<T>.Delete(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        Task<long> IDapperRepository<T>.Update(T current)
        {
            throw new NotImplementedException();
        }
    }
}
