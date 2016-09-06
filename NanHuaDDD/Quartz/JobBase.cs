/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/13 9:56:49

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Quartz
{
    /// <summary>
    /// 工作任务基类
    /// </summary>
    [DisallowConcurrentExecution()]
    public abstract class JobBase : IJob
    {
        /// <summary>
        /// log4日志对象
        /// </summary>
        protected log4net.ILog Logger
        {
            get
            {
                return log4net.LogManager.GetLogger(this.GetType());//得到当前类类型（当前实实例化的类为具体子类）
            }
        }

        #region IJob 成员

        public void Execute(IJobExecutionContext context)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            ExcuteJob();
            Console.WriteLine(DateTime.Now.ToString() + "{0}这个Job开始执行", context.JobDetail.Key.Name);
        }

        #endregion

        /// <summary>
        /// Job具体类去实现自己的逻辑
        /// </summary>
        protected abstract void ExcuteJob();
    }
}
