using NetCore.EntityModel.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.IRepository
{
    public interface IRepository<T>: IEFRepository<T> where T : class
    {
    }
}
