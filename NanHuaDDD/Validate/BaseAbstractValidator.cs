/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/8/24 10:36:00

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Validate
{
    public abstract class BaseAbstractValidator<T> : AbstractValidator<T> where T : class
    {

        public IRuleBuilderInitial<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            return RuleFor(expression);
        }
    }
    public class inpud
    {

    }

}
