using NetCore.DTO.ReponseViewModel;
using NetCore.IRepository.Common;
using NetCore.IServices;
using System;
using System.Threading.Tasks;

namespace NetCore.Services
{
    public class BaseServices<T> : IBaseServices<T> where T : class, new()
    {
        private readonly IRepository<T> _repository;
        public BaseServices(IRepository<T> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository", "repository为空");
            }
            _repository = repository;
        }

        public async Task<bool> AddService(T entity)
        {
            HttpReponseViewModel<bool> httpReponse = new HttpReponseViewModel<bool>();
            httpReponse.Data = await _repository.Add(entity);
            return httpReponse.Data;
        }
    }
}

