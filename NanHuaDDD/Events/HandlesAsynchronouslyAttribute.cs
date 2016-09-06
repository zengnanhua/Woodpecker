﻿/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/16 11:28:14

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Events
{
     [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class HandlesAsynchronouslyAttribute:Attribute
    {

    }
}
