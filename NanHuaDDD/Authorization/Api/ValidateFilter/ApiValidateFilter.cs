/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/12 10:43:54

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using NanHuaDDD.Encryptor;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace NanHuaDDD.Authorization.Api
{
    [AttributeUsage(AttributeTargets.Method)]
    class ApiValidateFilter : ActionFilterAttribute
    {
       public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            #region 初始化
            var context = (HttpContextBase)actionContext.Request.Properties["MS_HttpContext"];//获取传统context
            var request = context.Request;//定义传统request对象
            var paramStr = new StringBuilder();
            var coll = new NameValueCollection();
            if (request.HttpMethod.ToLower() == "get")
                coll = request.QueryString;
            else
                coll = request.Form;
            #endregion

            #region 解析XML配置文件
            var config = CacheConfigFile.ConfigFactory.Instance.GetConfig<ApiValidateModelConfig>().ApiValidateModelList.FirstOrDefault(i => i.AppKey == coll["AppKey"]);
            if (config == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("AppKey不是合并的，请先去组织生成有效的Key", Encoding.GetEncoding("UTF-8"))
                };
                base.OnActionExecuting(actionContext);
                return;
            }
            if (config.ExpireDate < DateTime.Now)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("AppKey不是合并的，密钥已过期", Encoding.GetEncoding("UTF-8"))
                };
                base.OnActionExecuting(actionContext);
                return;
            }
            #endregion

            #region 验证算法
            var keys = new List<string>();
            foreach (string param in coll.Keys)
            {
                if (!string.IsNullOrEmpty(param))
                {
                    keys.Add(param.ToLower());
                }

            }
            keys.Sort();
            foreach (string p in keys)
            {
                if (p != "ciphertext")
                {
                    if (!string.IsNullOrEmpty(coll[p]))
                    {
                        paramStr.Append(coll[p]);
                    }

                }
            }
            paramStr.Append(DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmm"));
            paramStr.Append(config.PassKey);
            #endregion


            if (EncryptorManager.EncryptString(paramStr.ToString(),EncryptorType.MD5)
                != request["cipherText"])
            {

                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("验证失败，请求非法", Encoding.GetEncoding("UTF-8"))
                };
            }

            base.OnActionExecuting(actionContext);
        
        }
    }
}
