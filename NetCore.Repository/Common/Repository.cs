using NetCore.IRepository.Common;
using NetCore.IRepository.UnitWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Repository.Common
{
    public class Repository<T> : DapperRepository<T>,IRepository<T> where T : class, new()
    {
        private readonly IUnitOfWork _unitOfWork;
        public Repository(IUnitOfWork unitOfWork):base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
