/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/6/16 17:02:12

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Repositories.DistributedReadWriteForEF
{
    /// <summary>
    /// 配置信息加载
    /// </summary>
    internal class DistributedReadWriteManager
    {
        public static IList<DistributedReadWriteSection> Instance {
            get {
                return GetSection();
            }
        }
        private static IList<DistributedReadWriteSection> GetSection()
        {
            var dic = ConfigurationManager.GetSection("DistributedReadWriteSection") as  Dictionary<string, DistributedReadWriteSection>;
            if (dic != null)
                return dic.Values.ToList();
            return null;
        }
    }
}
