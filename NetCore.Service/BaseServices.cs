using NetCore.Domain;
using NetCore.DTO.ReponseViewModel;
using NetCore.IRepository.Common;
using NetCore.IServices;
using System;
using System.Threading.Tasks;

namespace NetCore.Services
{
    public class BaseServices<T> : IBaseServices<T> where T  : class, new()
    {
        private readonly IBaseDomain<T> _domain;
        public BaseServices(IBaseDomain<T> domain)
        {
            _domain = domain;
        }
        public Task<bool> AddService(T entity)
        {
            return _domain.AddDomain(entity);
        }
    }
}

