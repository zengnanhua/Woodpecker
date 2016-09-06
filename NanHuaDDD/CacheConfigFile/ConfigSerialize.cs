using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NanHuaDDD.CacheConfigFile
{
    internal class ConfigSerialize
    {
        /// <summary>
        /// 反序列化指定的类并保存
        /// </summary>
        /// <param name="path">config 文件的路径</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IConfiger DeserializeInfo(string path, Type type)
        {
            IConfiger iconfig;
            FileStream fs = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                iconfig = (IConfiger)serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return iconfig;
        }
        /// <summary>
        /// 保存序列化对象
        /// </summary>
        /// <param name="path">保存路径</param>
        /// <param name="Iconfiginfo">被保存的对象</param>
        /// <returns></returns>
        public static bool Serializer(string path, IConfiger Iconfiginfo)
        {
            bool success = false;
            FileStream fs = null;
            XmlSerializer serializer = null;
            try
            {
                
                string sPath = Path.GetDirectoryName(path);
                if (!Directory.Exists(sPath))
                {
                    //没有指定文件夹就生成指定文件夹
                    Directory.CreateDirectory(sPath);
                }

                fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                serializer = new XmlSerializer(Iconfiginfo.GetType());
                serializer.Serialize(fs, Iconfiginfo);
                success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return success;
        }

    }
}
