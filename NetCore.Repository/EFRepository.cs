using NetCore.EntityModel.QueryModels;
using NetCore.IRepository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Repository
{
    public class EFRepository<T> : BaseRepository, IEFRepository<T>
         where T : class, new()
    {

        public EFRepository()
        {

        }

        public Task<T> Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Add(T entity, bool retType)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> AddList(IEnumerable<T> list)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Expression<Func<T, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetEntity(params object[] keyValues)
        {
            throw new NotImplementedException();
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
    }
}
