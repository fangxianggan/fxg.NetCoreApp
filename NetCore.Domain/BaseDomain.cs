using NetCore.IRepository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Domain
{
    public class BaseDomain<T> : IBaseDomain<T> where T : class, new()
    {
        private readonly IRepository<T> _repository;
        public BaseDomain(IRepository<T> repository)
        {
            _repository = repository;
        }
        public  async Task<bool> AddDomain(T entity)
        {
            return  await _repository.Add(entity);
        }
    }
}
