using NetCore.Core.Extensions;
using NetCore.DTO.UserModel;
using NetCore.EntityFrameworkCore.Models;
using NetCore.IRepository.Common;
using NetCore.IServices;
using System.Threading.Tasks;

namespace NetCore.Services
{
    public class UserServices : BaseServices<UserViewModel>,IUserServices
    {
        private readonly IRepository<UserViewModel> _repository;
        public UserServices(IRepository<UserViewModel> repository):base(repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> AddService(UserViewModel entity)
        {
            var t = entity.MapTo<User>();
            return await  _repository.Add(t);
        }
    }
}
