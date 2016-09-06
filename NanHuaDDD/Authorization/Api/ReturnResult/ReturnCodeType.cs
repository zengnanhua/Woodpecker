/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/13 11:13:48

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
    public enum ReturnCodeType
    {
        //有异常
        Error = 500,
        //禁止访问
        Forbidden = 403,
        //登录成功
        Success = 200,
        //未成功
        NotSuccess = 203,
        //未登录
        NotLogin = 202,
        //失败
        Fail = 204
    }
}
