/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/8/22 15:50:13

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.DTOEntity
{
    public static class AutoMapperHelper
    {
        static AutoMapperHelper()
        {
            Init();
        }

        public static void Init()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                 .SelectMany(a => a.GetTypes()
                 .Where(t => t.GetInterfaces().Contains(typeof(IRegisterMapper))))
                 .ToArray();

            foreach (var item in types)
            {
                var temp = (IRegisterMapper)Activator.CreateInstance(item);
                temp.Execute();
            }
        }

        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }
    }
}
