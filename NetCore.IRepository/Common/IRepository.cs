using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.IRepository.Common
{
    public interface IRepository<T> : IDapperRepository<T> where T : class
    {

    }
}
