/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/10 20:06:45

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Singleton
{
    public abstract  class Singleton<TEntity> where TEntity:class
    {
        private static readonly Lazy<TEntity> m_Instance = new Lazy<TEntity>(() => {
            var ctors = typeof(TEntity).GetConstructors(
                BindingFlags.Instance
                |BindingFlags.NonPublic
                |BindingFlags.Public);
            if (ctors.Count() != 1)
            {
                throw new InvalidOperationException(String.Format("类 {0} 必须包含一个构造函数", typeof(TEntity)));
            }
            var ctor = ctors.SingleOrDefault(c=>c.GetParameters().Count()==0&&c.IsPrivate);
            if (ctor == null)
            {
                throw new InvalidOperationException(String.Format("构造函数{0}必须是无参数并且私有的", typeof(TEntity)));
            }

            return (TEntity)ctor.Invoke(null);
        
        });

        public static TEntity Instance
        {
            get { return m_Instance.Value; }
        }
    }
}
