/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/8/10 10:46:33

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Web.Respository
{
    public class EFFatory
    {
        #region 1.0 从 当前处理线程中获取 EF容器对象 +DbContext GetEFContext()
        /// <summary>
        /// 从 当前处理线程中获取 EF容器对象
        /// </summary>
        /// <returns></returns>
        public static DbContext GetEFContext()
        {
            //if (db == null) db = new NewCRMEntities();
            //return db;
            #region 从线程中 操作数据的 方式一
            //1.从线程中取出 键 对应的值
            var db = CallContext.GetData("efContext") as DbContext;
            //2.如果为空（线程中不存在）
            if (db == null)
            {
                //3.实例化 EF容器 子类对象
                db = new WebEntities();
                //4.并存入线程
                CallContext.SetData("efContext", db);
            }
            //5.返回EF容器对象
            return db;
            #endregion
        }

        #endregion
    }
}
