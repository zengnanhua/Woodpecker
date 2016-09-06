/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/8/10 15:51:50

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using Autofac;
using Autofac.Configuration;
using NanHuaDDD.IoC.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.IoC
{
    /// <summary>
    /// IoCFactory  implementation 
    /// </summary>
    public sealed class IoCFactory
    {
        #region Singleton
        static IoCFactory instance;
        static object lockObj = new object();
        /// <summary>
        /// Get singleton instance of IoCFactory
        /// </summary>
        public static IoCFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new IoCFactory();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #region Members

        IContainer _CurrentContainer;

        /// <summary>
        /// Get current configured IContainer
        /// </summary>
        public IContainer CurrentContainer
        {
            get
            {
                return _CurrentContainer;
            }
        }

        #endregion

        #region Constructor

        private IoCFactory()
        {
  

            switch (ConfigConstants.FrameConfigManager.Config.IoCType)
            {
                case "UnityAdapterContainer":
                    _CurrentContainer = new UnityAdapterContainer();

                    break;
                case "AutofacAdapterContainer":

                    Action<AutofacAdapterContainer, ContainerBuilder> autofacAction = (_container, builder) =>
                    {
                        builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
                        _container.container = builder.Build();
                    };

                    
                     var builder1 = new ContainerBuilder();
                     _CurrentContainer = new AutofacAdapterContainer(builder1);
                     autofacAction((AutofacAdapterContainer)_CurrentContainer, builder1);
                     
                  
                    break;
                default:
                    throw new ArgumentException("不支持此IoC类型");
            }
        }
        #endregion
    }
}
