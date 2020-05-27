using NetCore.Core.Extensions;
using NetCore.Domain;
using NetCore.DTO.UserModel;
using NetCore.EntityFrameworkCore.Models;
using NetCore.IRepository.Common;
using NetCore.IServices;
using System.Threading.Tasks;

namespace NetCore.Services
{
    public class UserServices :IUserServices
    {
        private readonly IBaseDomain<User> _domain;
        public UserServices(IBaseDomain<User> domain)
        {
            _domain = domain;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> AddService(UserViewModel entity)
        {
            var t = entity.MapTo<User>();
            return await _domain.AddDomain(t);
        }
    }
}
