/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/10 21:13:59

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using NanHuaDDD.Singleton;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NanHuaDDD.CacheConfigFile
{
    public class ConfigFactory : Singleton<ConfigFactory>
    {
        private ConfigFactory()
        {
            
        }

        public T GetConfig<T>() where T : IConfiger
        {
            return GetConfig<T>(null);
        }
        /// <summary>
        /// 获取默认地址和检查地址合法性
        /// </summary>
        /// <param name="configPath"></param>
        /// <param name="iConfigerName"></param>
        /// <returns></returns>
        private string GetConfigPath(string configPath, string iConfigerName)
        {
            string configFilePath = configPath;
            string filename = iConfigerName;
            HttpContext context = HttpContext.Current;
            string siteVirtrualPath = string.IsNullOrEmpty(ConfigurationManager.AppSettings["SiteVirtrualPath"]) ?
            "/" : ConfigurationManager.AppSettings["SiteVirtrualPath"];

            if (string.IsNullOrWhiteSpace(configPath))
            {
                if (context != null)
                {
                    configFilePath = context.Server.MapPath(string.Format("{0}/Configs/{1}.Config", siteVirtrualPath, filename));
                }
                else
                {
                    configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"Configs\{0}.Config", filename));
                }
            }
            return configFilePath;
        }

        /// <summary>
        /// 可以根据绝对路径得到文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configPath"></param>
        /// <returns></returns>
        public T GetConfig<T>(string configPath) where T : IConfiger
        {
            string configFilePath = GetConfigPath(configPath,typeof(T).Name);
            if (!File.Exists(configFilePath))
            {
                throw new Exception("没有找到"+configFilePath);
            }
            return (T)ConfigFilesManager.Instance.LoadConfig(configFilePath,typeof(T));
        }

        public bool WriteConfig(IConfiger iConfiger)
        {
           return WriteConfig(null, iConfiger);
        }
        /// <summary>
        /// 写入配置文件
        /// </summary>
        /// <param name="configPath">路径</param>
        /// <param name="iConfiger">对象</param>
        /// <returns></returns>
        public bool WriteConfig(string configPath,IConfiger iConfiger)
        {
            string configFilePath = GetConfigPath(configPath, iConfiger.GetType().Name);
            return ConfigFilesManager.Instance.WriteConfig(configFilePath, iConfiger);
        }


    }
}
