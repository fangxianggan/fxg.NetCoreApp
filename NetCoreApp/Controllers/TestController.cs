using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCore.DTO.UserModel;
using NetCore.IServices;

namespace NetCoreApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IUserServices _service;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public TestController(IUserServices service)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        /// 
        [HttpGet, Route("getddd")]
        public async Task<bool> GetTest()
        {
            try {

                UserViewModel test = new UserViewModel();
                test.id = 1;
                test.name = "22";
                return await _service.AddService(test);
            } catch (Exception ex) {

                throw new Exception(ex.Message);
            }
          
        }
       

    }
}