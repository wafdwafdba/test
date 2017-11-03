using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupThink.CRM.Model.Sys
{
    public class Other_Model
    {
    }


    /// <summary>
    /// 请求结果
    /// </summary>
    public class RequestResult
    {
        //public RequestResult(int _Result,string _Message)
        //{
        //    this.Result = _Result;
        //    this.Message = _Message;
        //}

        /// <summary>
        /// 请求结果(0 异常 1 成功)
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }
    }
}
