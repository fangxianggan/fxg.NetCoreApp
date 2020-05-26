using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Repository
{
    public class Repository<T> : EFRepository<T> where T : class, new()
    {


    }
}
