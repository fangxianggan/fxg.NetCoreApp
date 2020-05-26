using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCore.DTO.ReponseViewModel;
using NetCore.EntityFrameworkCore.Models;
using NetCore.IService;

namespace NetCoreApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class TestController : Controller
    {
        private readonly IBaseService<TaskJob> _service;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public TestController(IBaseService<TaskJob> service)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public async Task<HttpReponseViewModel<TaskJob>> GetTest(int x)
        {
            return await _service.GetEntityService(x);
        }
        
    }
}