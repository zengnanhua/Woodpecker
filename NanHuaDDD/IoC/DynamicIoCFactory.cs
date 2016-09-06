/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/8/10 15:49:29

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.IoC
{
    public class DynamicIoCFactory
    {
        public static I GetService<I>(string @class)
        {
            using (IUnityContainer container = new UnityContainer())
            {
                var tGeneric = Type.GetType(@class);//拿到泛型类型
                container.RegisterType(typeof(I), tGeneric);//注意类型与实现的关系
                return container.Resolve<I>();//生产对象
            }
        }
    }
}
