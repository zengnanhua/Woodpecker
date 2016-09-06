/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/6/15 11:14:40

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.IoC.Interception
{
    public class ExceptionLoggingBehavior : InterceptionBase
    {

        public override Microsoft.Practices.Unity.InterceptionExtension.IMethodReturn Invoke(Microsoft.Practices.Unity.InterceptionExtension.IMethodInvocation input, Microsoft.Practices.Unity.InterceptionExtension.GetNextInterceptionBehaviorDelegate getNext)
        {
           
            //方法执行前
            var methodReturn = getNext().Invoke(input, getNext);
           //方法执行后
            if (methodReturn.Exception != null)
            {
                //这里显示打印异常信息
            }
            return methodReturn;
        }
    }
}
