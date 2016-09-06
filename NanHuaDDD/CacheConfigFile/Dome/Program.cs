using NanHuaDDD.CacheConfigFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace NanHuaDDD.CacheConfigFile.Dome
{
    class Program
    {
        static void Main(string[] args)
        {
            //写入缓存
            //SystemConfig config = new SystemConfig() { MSMConnt = 20, Email = "418852693@qq.com", QQ = "418852693" };
            //ConfigFactory.Instance.WriteConfig(null,config);

          //读取缓存
          SystemConfig systemConfig = ConfigFactory.Instance.GetConfig<SystemConfig>();
          Console.WriteLine(systemConfig.ToString());
    
      

            Console.ReadLine();
        }
    }
}
