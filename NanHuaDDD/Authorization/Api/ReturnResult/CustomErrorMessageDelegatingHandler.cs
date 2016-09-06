/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/13 11:09:50

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using NanHuaDDD.Authorization.Api.ReturnResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;


namespace NanHuaDDD.Authorization.Api
{
    /// <summary>
    /// //处理的异常类
    /// </summary>
    public class CustomErrorMessageDelegatingHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            #region 返回截获
            return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>((responseToCompleteTask) =>
            {
                HttpResponseMessage response = responseToCompleteTask.Result;
                HttpError error = null;
                if (response.TryGetContentValue<HttpError>(out error))
                {
                    //添加自定义错误处理
                    //error.Message = "Your Customized Error Message";
                }

                if (error != null)
                {
                    //获取抛出自定义异常，有拦截器统一解析
                    return new HttpResponseMessage(HttpStatusCode.OK)
                      {
                          //封装处理异常信息，返回指定JSON对象
                          Content = new StringContent(BaseResultJson.GetError(error.Message).ToJson()),
                          ReasonPhrase = "Exception"
                      };
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        return new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            //封装处理异常信息，返回指定JSON对象
                            Content = new StringContent(BaseResultJson.GetForbidden().ToJson()),
                            ReasonPhrase = "ok"
                        };
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StringContent(BaseResultJson.GetNotLogin().ToJson()),
                            ReasonPhrase = "ok"
                        };
                    }
                    return response;
                }
            });
            #endregion
        }
    }
}
