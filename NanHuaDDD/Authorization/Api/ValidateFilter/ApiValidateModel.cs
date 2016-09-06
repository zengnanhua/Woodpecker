/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/12 10:54:34

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using NanHuaDDD.CacheConfigFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Authorization.Api
{
    public class ApiValidateModelConfig : IConfiger
    {
        public ApiValidateModelList ApiValidateModelList { get; set; }
    }
    /// <summary>
    /// 配置列表
    /// </summary>
    [Serializable]
    public class ApiValidateModelList : List<ApiValidateModel>
    {
 
    }
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ApiValidateModel
    {
        public string AppKey { get; set; }
        public string AppName { get; set; }
        public string PassKey { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
