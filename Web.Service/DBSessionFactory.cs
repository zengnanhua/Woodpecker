/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/8/10 14:31:19

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Web.Service
{
    public  class DBSessionFactory
    {
        public static IRespository.IDBSession GetDBSession()
        {
            //1.从线程中取出 键 对应的值
            var db = CallContext.GetData("IDBSession") as Web.IRespository.IDBSession;
            //2.如果为空（线程中不存在）
            if (db == null)
            {
                //3.实例化 EF容器 子类对象
                db = NanHuaDDD.IoC.IoCFactory.Instance.CurrentContainer.Resolve<Web.IRespository.IDBSession>(); //Utility.DI.GetObject<IRespository.IDBSession>("dalSession");
                //4.并存入线程
                CallContext.SetData("IDBSession", db);
            }
            //5.返回DBSession对象
            return db; 
        }
    }
}
