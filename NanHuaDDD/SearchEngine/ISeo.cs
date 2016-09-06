using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.LuceneNet
{
    public interface ISeo
    {
       /// <summary>
        /// 建立索引库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        /// <param name="feilds">要建立的索引</param>
        void WriteIndexFile<T>(List<T> dataList, List<string> feilds) where T : class,new();
        /// <summary>
        /// 建立你索引库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        void WriteIndexFile(DataTable table, List<string> feilds);

        /// <summary>
        /// 搜索关键字
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="searchFeilds">在哪些字段里面读取</param>
        /// <param name="feilds">要哪些字段</param>
        /// <param name="indexPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
         List<T> SearchMultiField<T>(string keyword, string[] searchFeilds, List<string> feilds, int indexPage, int pageSize)where T:class,new();
    }
}
