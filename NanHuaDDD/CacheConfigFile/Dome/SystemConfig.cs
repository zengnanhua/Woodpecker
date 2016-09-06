/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/10 21:42:32

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

namespace NanHuaDDD.CacheConfigFile.Dome
{
   public class SystemConfig : IConfiger
    {
        public int MSMConnt { get; set; }
        public string Email { get; set; }
        public string QQ {get;set; }

        public override string ToString()
        {
            return MSMConnt + "----" + Email + "-----"+QQ;
        }
    }
}
