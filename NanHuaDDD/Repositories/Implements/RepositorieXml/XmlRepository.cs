/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/6/16 16:09:44

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using NanHuaDDD.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NanHuaDDD.Repositories.Implements.RepositorieXml
{
    public class XmlRepository<TEntity> : IRepository<TEntity>where TEntity:BaseNoSqlEntity,new()
    {
        #region Fields
        private XDocument _doc;
        private string _filePath;
        private static object lockObj = new object();
        #endregion

        #region Constructors
        /// <summary>
        /// 初始化XML仓储
        /// </summary>
        /// <param name="dbName">XML文件名及根名称</param>
        public XmlRepository(string dbName)
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbName + ".xml");
            if (!File.Exists(file))
            {
                using (System.IO.StreamWriter srFile = new System.IO.StreamWriter(file, true))
                {
                    srFile.WriteLine("<?xml version=\"1.0\"?><" + dbName + "></" + dbName + ">");
                }
            }
            _filePath = file;
            _doc = XDocument.Load(file);
        }
        public XmlRepository() : this("XmlDocument") { }
        #endregion

        public TEntity Find(params object[] id)
        {
            return GetModel().FirstOrDefault(i => i.Id == Convert.ToString(id[0]));
        }

        public IQueryable<TEntity> GetModel()
        {
            IEnumerable<XElement> list = _doc.Root.Elements(typeof(TEntity).Name);
            IList<TEntity> returnList = new List<TEntity>();
            foreach (var item in list)
            {
                TEntity entity = new TEntity();
                foreach (var member in entity.GetType()
                                             .GetProperties()
                                             .Where(i => i.PropertyType.IsValueType
                                                 || i.PropertyType == typeof(String)))//只找简单类型的属性,支持枚举
                {
                    if (item.Element(member.Name) != null)
                    {
                        if (member.PropertyType.IsEnum)
                        {
                            var obj = Enum.Parse(member.PropertyType, item.Element(member.Name).FirstAttribute.Value);
                            member.SetValue(entity, obj, null);
                        }
                        else
                        {
                            member.SetValue(entity, Convert.ChangeType(item.Element(member.Name).FirstAttribute.Value, member.PropertyType), null);
                        }
                    }
                }
                returnList.Add(entity);
            }
            return returnList.AsQueryable();
        }

        public void Insert(TEntity item)
        {
            if (item == null)
                throw new ArgumentException("The database entity can not be null.");


            XElement db = new XElement(typeof(TEntity).Name);
            foreach (var member in item.GetType().GetProperties().OrderByDescending(i => i.Name == "Id")
                .Where(i => i.PropertyType.IsValueType || i.PropertyType == typeof(String)))//只找简单类型的属性
            {
                var value = member.GetValue(item, null);
                var description = (DisplayNameAttribute)member.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() ?? new DisplayNameAttribute();
                if (value == null)
                    db.Add(new XElement(member.Name, new XAttribute("value", ""), new XAttribute("description", description.DisplayName)));
                else
                    db.Add(new XElement(member.Name, new XAttribute("value", value), new XAttribute("description", description.DisplayName)));
            }

            _doc.Root.Add(db);
            lock (lockObj)
            {
                _doc.Save(_filePath);
            }
        }

        public void Update(TEntity item)
        {
            if (item == null)
                throw new ArgumentException("The database entity can not be null.");

            XElement xe = (from db in _doc.Root.Elements(typeof(TEntity).Name)
                           where db.Element("Id").Attribute("value").Value == item.Id
                           select db).Single();
            try
            {
                foreach (var member in item.GetType()
                                           .GetProperties()
                                           .Where(i => i.PropertyType.IsValueType
                                               || i.PropertyType == typeof(String)))//只找简单类型的属性
                {
                    xe.Add(new XElement(member.Name, new XAttribute("value", member.GetValue(item, null))));
                }
                lock (lockObj)
                {
                    _doc.Save(_filePath);
                }
            }

            catch
            {
                throw;
            }
        }

        public void Update(TEntity item, System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity item)
        {
            if (item == null)
                throw new ArgumentException("The database entity can not be null.");


            XElement xe = (from db in _doc.Root.Elements(typeof(TEntity).Name)
                           where db.Element("Id").Attribute("value").Value == item.Id
                           select db).Single() as XElement;
            xe.Remove();
            lock (lockObj)
            {
                _doc.Save(_filePath);
            }
        }

        public void UpdateEntityFields(TEntity entity, List<string> fileds)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> LoadPageEntities<S>(int pageSize, int pageIndex, out int totalCount, System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda, bool isAsc, System.Linq.Expressions.Expression<Func<TEntity, S>> orderBy)
        {
            var temp = GetModel().Where(whereLambda);
            totalCount = temp.Count();
            if (isAsc)
            {
                temp = temp.OrderBy(orderBy).Skip(pageSize * (pageIndex - 1))
                                                 .Take(pageSize).Select(i => i);
            }
            else{
                temp = temp.OrderByDescending(orderBy).Skip(pageSize * (pageIndex - 1))
                                                     .Take(pageSize).Select(i => i);
            }
            return temp;
        }

        public IQueryable<TEntity> LoadEntities(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda)
        {
            return GetModel().Where(whereLambda);
        }
    }
}
