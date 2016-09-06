/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/14 10:11:26

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using NanHuaDDD.LuceneNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.SearchEngine.Implements
{
    internal class LuceneNetSeo:ISeo
    {
         public string indexPath = null;

        /// <summary>
        /// 文件名字
        /// </summary>
        /// <param name="pathDirectory"></param>
         public LuceneNetSeo(string pathDirectory)
        {
            this.indexPath = pathDirectory;
        }

         public void WriteIndexFile<T>(List<T> dataList, List<string> feilds) where T : class,new()
        {
            if (dataList == null || dataList.Count() <= 0)
            {
                throw new Exception("没有任何数据");
            }
            IndexWriter writer = GetWriteIndex();

            WriteIndexDocument(writer, dataList, feilds);
            writer.Commit();
            writer.Optimize();
            writer.Dispose();
        }

        #region 创建索引写入
        /// <summary>
        /// 创建索引写入
        /// </summary>
        /// <returns></returns>
        private IndexWriter GetWriteIndex()
        {
            Analyzer stander = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_CURRENT);
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());
            bool isUpdate = IndexReader.IndexExists(directory);
            if (isUpdate)
            {
                if (IndexWriter.IsLocked(directory))
                {
                    IndexWriter.Unlock(directory);
                }
            }
            IndexWriter writer = new IndexWriter(directory, stander, !isUpdate, IndexWriter.MaxFieldLength.LIMITED);
            return writer;
        }
        #endregion

       
        private void WriteIndexDocument<T>(IndexWriter indexWriter, List<T> dataList, List<string> feilds) 
        {
            Type type = typeof(T);
            PropertyInfo[] memberInfos = type.GetProperties();//得到所有公共成员
            foreach (var t in dataList)
            {
                Document document = new Document();
                foreach (PropertyInfo item in memberInfos)
                {
                    if (feilds == null || feilds.Contains(item.Name))
                    {
                        document.Add(new Field(item.Name, item.GetValue(t, null).ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    }
                }
                indexWriter.AddDocument(document);
            }

        }

        public void WriteIndexFile(System.Data.DataTable table, List<string> feilds)
        {
            IndexWriter writer = GetWriteIndex();
            DataColumnCollection columns = table.Columns;

            foreach (DataRow row in table.Rows)
            {
                Document document = new Document();
                foreach (DataColumn column in columns)
                {
                    if (feilds == null || feilds.Contains(column.ColumnName))
                    {
                        document.Add(new Field(column.ColumnName, row[column].ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    }
                }
                writer.AddDocument(document);
            }
            writer.Commit();
            writer.Optimize();
            writer.Dispose();
        }


        /// <summary>
        /// 搜索字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyword"></param>
        /// <param name="searchFeilds"></param>
        /// <param name="feilds"></param>
        /// <param name="indexPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<T> SearchMultiField<T>(string keyword, string[] searchFeilds, List<string> feilds, int indexPage, int pageSize) where T : class,new()
        {
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            IndexSearcher searcher = new IndexSearcher(reader);
            Analyzer stander = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_CURRENT);
            MultiFieldQueryParser parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, searchFeilds, stander);
            //QueryParser qp = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "content", stander);
            Query query = parser.Parse(keyword);
            //query.Slop=100;
            TopDocs tds = searcher.Search(query, int.MaxValue);
            //查询起始记录位置
            int begin = pageSize * (indexPage - 1);
            //查询终止记录位置
            int end = Math.Min(begin + pageSize, tds.ScoreDocs.Count());
            List<T> objArr = new List<T>();
            for (int i = begin; i < end; i++)
            {
                ScoreDoc s = tds.ScoreDocs[i];
                int docid = s.Doc;
                Document doc = searcher.Doc(docid);
                Type type = typeof(T);
                object obj = Activator.CreateInstance(type, null);
                PropertyInfo[] memberInfos = type.GetProperties();//得到所有公共成员
                //string  doc.Get("id")

                foreach (PropertyInfo propertyInfo in memberInfos)
                {
                    if (feilds == null || feilds.Contains(propertyInfo.Name))
                    {
                        propertyInfo.SetValue(obj, doc.Get(propertyInfo.Name), null);
                    }
                }
                objArr.Add((T)obj);

            }

            return objArr;
        }
    }
}
