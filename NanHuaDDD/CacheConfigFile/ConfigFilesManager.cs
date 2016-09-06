/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/10 20:05:15

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：加载配置文件类
*************************************************************************/


using NanHuaDDD.Singleton;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.CacheConfigFile
{
    public class ConfigFilesManager : Singleton<ConfigFilesManager>
    {
        #region 私有字段
        /// <summary>
        /// 锁对象
        /// </summary>
        object lockHelper = new object();
        /// <summary>
        /// 配置文件修改时间,以文件名为键，修改时间为值
        /// </summary>
         static Dictionary<string, DateTime> fileChangeTime;

        /// <summary>
        /// 配置文件类型
        /// </summary>
        Type configType = null;
        #endregion

        #region 构造方法
        static ConfigFilesManager()
        {
            fileChangeTime = new Dictionary<string, DateTime>();
        }
        /// <summary>
        /// 私用无参方法，实例单例时用
        /// </summary>
        private ConfigFilesManager()
        {

        }

        #region 加载配置类
        #region 私有方法
        /// <summary>
        /// 从配置文件中读取
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private IConfiger LoadConfigFile(string path, Type type)
        {
            this.configType = type;
            fileChangeTime[path] = File.GetLastWriteTime(path);
            return ConfigSerialize.DeserializeInfo(path, this.configType);
        }
        #endregion
        internal IConfiger LoadConfig(string path, Type type)
        {
            return LoadConfig(path, type, true);
        }
        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="type">实体类型</param>
        /// <param name="isCache">是否从缓存加载</param>
        /// <returns></returns>
        internal IConfiger LoadConfig(string path, Type type, bool isCache)
        {
            if (!isCache)
            {
                return LoadConfigFile(path, type);
            }
            lock (lockHelper)
            {
                if (ConfigFileCache.GetCache(path) == null)
                {
                    ConfigFileCache.SetCache(path, LoadConfigFile(path, type));
                }
                DateTime newfileChangeTime = File.GetLastWriteTime(path);
                if (!newfileChangeTime.Equals(fileChangeTime[path]))
                {
                    IConfiger temp = LoadConfigFile(path, type);
                    ConfigFileCache.SetCache(path, temp);
                    return temp;
                }
                else
                {
                    return ConfigFileCache.GetCache(path) as IConfiger;
                }
            }
        }

        internal bool WriteConfig(string path, IConfiger iConfigInfo)
        {
            return ConfigSerialize.Serializer(path, iConfigInfo);
        }

        #endregion
        #endregion
    }
}
