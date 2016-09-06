/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/13 11:13:02

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace NanHuaDDD.Authorization.Api.ReturnResult
{
    public class BaseResultJson
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errcode"></param>
        /// <param name="errMsg"></param>
        /// <param name="success"></param>
        public BaseResultJson(ReturnCodeType Code, string Msg, object Data)
        {
            this.Code = Code;
            this.Msg = Msg;
            this.Data = Data;
        }
        /// <summary>
        /// 错误代码
        /// </summary>
        public ReturnCodeType Code { get; set; }
        /// <summary>
        /// 如果不成功，返回的错误信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 把对象转换为json
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(this);
        }
        public static BaseResultJson GetNotLogin()
        {
            return new BaseResultJson(ReturnCodeType.NotLogin, "未登录", "");
        }
        public static BaseResultJson GetForbidden()
        {
            return new BaseResultJson(ReturnCodeType.Forbidden, "禁止访问", "");
        }
        public static BaseResultJson GetForbidden(string msg)
        {
            return new BaseResultJson(ReturnCodeType.Forbidden, msg, "");
        }

        public static BaseResultJson GetError(object obj)
        {
            return new BaseResultJson(ReturnCodeType.Error, "操作异常", obj);
        }
        public static BaseResultJson GetSuccess(object obj)
        {
            return new BaseResultJson(ReturnCodeType.Success, "操作成功", obj);
        }
        public static BaseResultJson GetSuccess(string msg, object obj)
        {
            return new BaseResultJson(ReturnCodeType.Success, msg, obj);
        }

        public static BaseResultJson GetFail(string msg)
        {
            return new BaseResultJson(ReturnCodeType.Fail, msg, "");
        }

    }
}
