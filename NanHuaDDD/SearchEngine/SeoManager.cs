/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/14 10:01:26

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using NanHuaDDD.SearchEngine.Implements;
using NanHuaDDD.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.LuceneNet
{
    public class SeoManager:Singleton<SeoManager>,ISeo
    {

        private string indexPath = "SeoIndexFile";
        private string _seoProvider = "LuceneNet";
        private  ISeo iSeo;
        static SeoManager()
        {
        }
        private SeoManager()
        {
            switch (_seoProvider.ToLower())
            {
                case "lucenenet":
                    iSeo = new LuceneNetSeo(indexPath);
                    break;
                default:
                    throw new ArgumentException("搜索引擎只支持lucenenet");
            }
        }

        public void WriteIndexFile<T>(List<T> dataList, List<string> feilds) where T : class,new()
        {
            iSeo.WriteIndexFile(dataList, feilds);
        }

        public void WriteIndexFile(System.Data.DataTable table, List<string> feilds)
        {
            iSeo.WriteIndexFile(table, feilds);
        }

        public List<T> SearchMultiField<T>(string keyword, string[] searchFeilds, List<string> feilds, int indexPage, int pageSize) where T : class,new()
        {
           return iSeo.SearchMultiField<T>(keyword,searchFeilds,feilds,indexPage,pageSize);
        }
    }
}
