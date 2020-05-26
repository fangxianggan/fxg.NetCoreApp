using NetCore.DTO.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.DTO.ReponseViewModel
{
    /// <summary>
    /// httpreponse
    /// </summary>
    public class HttpReponseViewModel
    {
        public HttpReponseViewModel()
        {
            ResultSign = ResultSign.Successful;
            Message = HttpReponseMessageViewModel.SuccessMsg;
            Code = StatusCode.OK;
            RequestParams = null;
            Flag = true;

            //var token = HttpContext.Current.Request.RequestContext.RouteData.Values["X-Token"];
            //if (token != null)
            //{
            //    Token = token.ToString();
            //}
            //else
            //{
            //    Token = "";
            //}

        }

        #region 属性

        /// <summary>
        ///     返回标记
        /// </summary>
        public ResultSign ResultSign { get; set; }

        /// <summary>
        ///     消息字符串(有多语言后将删除该属性)
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///  传入的参数
        /// </summary>
        public object RequestParams { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public StatusCode Code { set; get; }

        /// <summary>
        /// 判断执行的是不是成功
        /// </summary>
        public bool Flag { set; get; }

        /// <summary>
        /// token
        /// </summary>
        public string Token { set; get; }
        #endregion
    }

    /// <summary>
    /// 返回实体对象 包含分页功能
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpReponseViewModel<T> : HttpReponseViewModel
    {
        public HttpReponseViewModel()
        {
            PageIndex = 0;
            PageSize = 0;
            Total = 0;
            Data = default(T);
        }
        public int PageIndex { set; get; }

        public int PageSize { set; get; }

        public long Total { set; get; }
        /// <summary>
        /// 泛型对象
        /// </summary>
        public T Data { get; set; }

    }
}
