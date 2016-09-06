/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/13 11:14:35

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Authorization.Api.ReturnResult
{
    public class MyApiException : Exception
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 如果不成功，返回的错误信息
        /// </summary>
        public string errMsg { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 把对象转换为json
        /// </summary>
        /// <returns></returns>
        public MyApiException(int errcode, string errMsg, bool success)
        {
            this.errcode = errcode;
            this.errMsg = errMsg;
            this.success = success;
        }
    }
}
