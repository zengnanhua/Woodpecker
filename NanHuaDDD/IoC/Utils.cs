/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/8/10 15:40:17

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using Autofac;
using Autofac.Core;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.IoC
{
    /// <summary>
    /// 表示用于整个IoC系统的工具类。
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// unity参数组合
        /// </summary>
        /// <param name="overridedArguments"></param>
        /// <returns></returns>
        public static IEnumerable<ParameterOverride> GetParameterOverrides(object overridedArguments)
        {
            List<ParameterOverride> overrides = new List<ParameterOverride>();

            Type argumentsType = overridedArguments.GetType();
            argumentsType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToList()
                .ForEach(property =>
                {
                    var propertyValue = property.GetValue(overridedArguments, null);
                    var propertyName = property.Name;
                    overrides.Add(new ParameterOverride(propertyName, propertyValue));
                });

            return overrides;
        }

        /// <summary>
        /// autofac参数组合
        /// </summary>
        /// <param name="overridedArguments"></param>
        /// <returns></returns>
        public static IEnumerable<Parameter> GetParameter(object overridedArguments)
        {
            List<NamedParameter> overrides = new List<NamedParameter>();

            Type argumentsType = overridedArguments.GetType();
            argumentsType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToList()
                .ForEach(property =>
                {
                    var propertyValue = property.GetValue(overridedArguments, null);
                    var propertyName = property.Name;
                    overrides.Add(new NamedParameter(propertyName, propertyValue));
                });

            return overrides;
        }

    }
}
