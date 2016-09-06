/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/6/15 14:25:15

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using NanHuaDDD.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NanHuaDDD.LinqExtensions;

namespace NanHuaDDD.Repositories.Implements.RepositorieEF
{
    public class EFRepository<TEntity> : IExtensionRepository<TEntity>, IEFRepositorieSqlExtension<TEntity> where TEntity :class,new()
    {
        #region 构造函数
        /// <summary>
        /// EF数据上下文
        /// </summary>
        private DbContext Db;

        public EFRepository()
            : this(null)
        { 
        }
        public EFRepository(DbContext db)
        {
            Db = db;
        }
        #endregion

        protected virtual void SaveChanges()
        {
            try {
                Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException("框架在更新时引起了乐观并发，后修改的数据不会被保存");
            }
        }

        #region 接口实现
        public IQueryable<TEntity> LoadSql(string sql)
        {
            IQueryable<TEntity> temp = Db.Database.SqlQuery<TEntity>(sql).AsQueryable();
            return temp;
        }

        public IQueryable<TEntity> LoadPageSql<S>(string sql, int pageSize, int pageIndex, out int totalCount, bool isAsc, System.Linq.Expressions.Expression<Func<TEntity, S>> orderBy)
        {
            IQueryable<TEntity> temp = Db.Database.SqlQuery<TEntity>(sql).AsQueryable();
            totalCount = temp.Count();
            if (isAsc)
            {
                temp = temp.OrderBy(orderBy)
                           .Skip(pageSize * (pageIndex - 1))
                           .Take(pageSize).AsQueryable();
            }
            else
            {
                temp = temp.OrderByDescending(orderBy)
                          .Skip(pageSize * (pageIndex - 1))
                          .Take(pageSize).AsQueryable();
            }
            return temp;
        }

        public void Insert(IEnumerable<TEntity> item)
        {
            item.ToList().ForEach(i => this.Insert(i));
        }

        public void Update(IEnumerable<TEntity> item)
        {
            item.ToList().ForEach(i => this.Update(i));
        }

        public void Delete(IEnumerable<TEntity> item)
        {
            item.ToList().ForEach(i => this.Delete(i));
        }

        public bool BulkInsert(IEnumerable<TEntity> item, bool isRemoveIdentity)
        {
            string startTag = "", endTag = "";
            if (isRemoveIdentity)
            {
                startTag = "SET IDENTITY_INSERT " + typeof(TEntity).Name + " ON;";
                endTag = "SET IDENTITY_INSERT " + typeof(TEntity).Name + "  OFF;";
            }
            ((IObjectContextAdapter)Db).ObjectContext.CommandTimeout = 0;//永不超时
            return Db.Database.ExecuteSqlCommand(startTag
                + DoSql(item, "Add")
                + endTag) > 0;
        }

        public bool BulkInsert(IEnumerable<TEntity> item)
        {
            return BulkInsert(item, false);
        }

        public bool BulkUpdate(IEnumerable<TEntity> item, params string[] fieldParams)
        {
            ((IObjectContextAdapter)Db).ObjectContext.CommandTimeout = 0;//永不超时
            return Db.Database.ExecuteSqlCommand(DoSql(item, "Update", fieldParams)) > 0;
        }

        public bool BulkDelete(IEnumerable<TEntity> item)
        {
            ((IObjectContextAdapter)Db).ObjectContext.CommandTimeout = 0;//永不超时
            return Db.Database.ExecuteSqlCommand(DoSql(item, "Delete")) > 0;
        }

        public void SetDataContext(object db)
        {
            try
            {
                Db = (DbContext)db;
            }
            catch (Exception)
            {

                throw new ArgumentException("EF.SetDataContext要求上下文为DbContext类型");
            }
        }

        public IQueryable<TEntity> GetModel(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Db.Set<TEntity>().Where(predicate).AsQueryable();
        }

        public TEntity Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return GetModel().FirstOrDefault(predicate);
        }

        public TEntity Find(params object[] id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetModel()
        {
            return Db.Set<TEntity>();
        }

        public void Insert(TEntity item)
        {
            Db.Set<TEntity>().Add(item);
        }

        public void Update(TEntity item)
        {
            Db.Entry(item).State = EntityState.Modified;
        }
        public void Update(TEntity item, System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda)
        {
            throw new Exception("目前没有实现");
                //List<TEntity> tempArr = Db.Set<TEntity>().Where(whereLambda).ToList();
                //foreach (var temp in tempArr)
                //{
 
                //}
        }
        public void Delete(TEntity item)
        {
            Db.Entry(item).State = EntityState.Deleted;
        }

        public void UpdateEntityFields(TEntity entity, List<string> fileds)
        {
            Db.Set<TEntity>().Attach(entity);
            var SetEntry = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)Db).ObjectContext.ObjectStateManager.GetObjectStateEntry(entity);
            foreach (var t in fileds)
                SetEntry.SetModifiedProperty(t);
        }


        public IQueryable<TEntity> LoadPageEntities<S>(int pageSize, int pageIndex, out int totalCount, System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda, bool isAsc, System.Linq.Expressions.Expression<Func<TEntity, S>> orderBy)
        {

            IQueryable<TEntity> temp = Db.Set<TEntity>().Where(whereLambda).AsQueryable();

            totalCount = temp.Count();

            if (isAsc)
            {
                temp = temp.OrderBy(orderBy)
                           .Skip(pageSize * (pageIndex - 1))
                           .Take(pageSize).AsQueryable();
            }
            else
            {
                temp = temp.OrderByDescending(orderBy)
                          .Skip(pageSize * (pageIndex - 1))
                          .Take(pageSize).AsQueryable();
            }
            return temp;
        }

        public IQueryable<TEntity> LoadEntities(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda)
        {
            return Db.Set<TEntity>().Where(whereLambda).AsQueryable();
        }
        #endregion

        #region 私有方法
        #region 辅助方法
        #region  得到实体键EntityKey
        /// <summary>
        /// 得到实体键EntityKey
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        private ReadOnlyMetadataCollection<EdmMember> GetPrimaryKey()
        {
            EntitySetBase primaryKey = ((IObjectContextAdapter)Db).ObjectContext.GetEntitySet(typeof(TEntity));
            if (primaryKey == null)
                return null;
            ReadOnlyMetadataCollection<EdmMember> arr = primaryKey.ElementType.KeyMembers;
            return arr;

        }
        #endregion
        private static string GetParamTag(int paramId)
        {
            return "{" + paramId + "}";
        }
        private static string GetEqualStatment(string fieldName, int paramId, Type pkType)
        {
            if (pkType.IsValueType)
                return string.Format("{0} = {1}", fieldName, GetParamTag(paramId));
            return string.Format("{0} = '{1}'", fieldName, GetParamTag(paramId));

        }
        private string DoSql(IEnumerable<TEntity> list, string sqlType)
        {
            return DoSql(list, sqlType, null);
        }
        /// <summary>
        /// 分页进行数据提交的逻辑
        /// </summary>
        /// <param name="item">原列表</param>
        /// <param name="method">处理方法</param>
        /// <param name="currentItem">要进行处理的新列表</param>

        #region  执行SQL，根据SQL操作的类型
        /// <summary>
        /// 执行SQL，根据SQL操作的类型
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="list"></param>
        /// <param name="sqlType"></param>
        /// <returns></returns>
        private string DoSql(IEnumerable<TEntity> list, string sqlType, params string[] fieldParams)
        {
            var sqlstr = new StringBuilder();
            switch (sqlType)
            {
                case "Add":
                    list.ToList().ForEach(i =>
                    {
                        Tuple<string, object[]> sql = CreateInsertSql(i);
                        sqlstr.AppendFormat(sql.Item1, sql.Item2);
                    });
                    break;
                case "Update":
                    list.ToList().ForEach(i =>
                    {
                        Tuple<string, object[]> sql = CreateUpdateSql(i, fieldParams);
                        sqlstr.AppendFormat(sql.Item1, sql.Item2);
                    });
                    break;
                case "Delete":
                    list.ToList().ForEach(i =>
                    {
                        Tuple<string, object[]> sql = CreateDeleteSql(i);
                        sqlstr.AppendFormat(sql.Item1, sql.Item2);
                    });
                    break;
                default:
                    throw new ArgumentException("请输入正确的参数");
            }
            return sqlstr.ToString();
        }
        #endregion
        #endregion
        #region 构建Insert语句串
        /// <summary>
        /// 构建Insert语句串
        /// 主键为自增时，如果主键值为0，我们将主键插入到SQL串中
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        private Tuple<string, object[]> CreateInsertSql(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException("实例对象为空");
            Type entityType = entity.GetType();
            var table = entityType.GetProperties().Where(i => i.PropertyType != typeof(EntityKey)
                 && i.PropertyType != typeof(EntityState)
                 && i.Name != "IsValid"
                 && i.GetValue(entity, null) != null
                 && !i.PropertyType.IsEnum
                 && i.GetCustomAttributes(false) != null
                 && (i.PropertyType.IsValueType || i.PropertyType == typeof(string))).ToArray();//过滤主键，航行属性，状态属性等

            var pkList = new List<string>();
            if (GetPrimaryKey() != null)//有时主键可能没有设计，这对于添加操作是可以的
                pkList = GetPrimaryKey().Select(i => i.Name).ToList();
            var arguments = new List<object>();
            var fieldbuilder = new StringBuilder();
            var valuebuilder = new StringBuilder();

            fieldbuilder.Append(" INSERT INTO " + string.Format("[{0}]", entityType.Name) + " (");

            foreach (var member in table)
            {
                if (pkList.Contains(member.Name) && Convert.ToString(member.GetValue(entity, null)) == "0")
                    continue;
                object value = member.GetValue(entity, null);
                if (value != null)
                {
                    if (arguments.Count != 0)
                    {
                        fieldbuilder.Append(", ");
                        valuebuilder.Append(", ");
                    }

                    fieldbuilder.Append(member.Name);
                    if (member.PropertyType == typeof(string)
                        || member.PropertyType == typeof(DateTime)
                        || member.PropertyType == typeof(DateTime?)
                        || member.PropertyType == typeof(Boolean?)
                        || member.PropertyType == typeof(Boolean)
                        )
                        valuebuilder.Append("'{" + arguments.Count + "}'");
                    else
                        valuebuilder.Append("{" + arguments.Count + "}");
                    if (value is string)
                        value = value.ToString().Replace("'", "char(39)");
                    arguments.Add(value);

                }
            }


            fieldbuilder.Append(") Values (");

            fieldbuilder.Append(valuebuilder.ToString());
            fieldbuilder.Append(");");
            return new Tuple<string, object[]>(fieldbuilder.ToString(), arguments.ToArray());
        }
        #endregion
        #region 构建Update语句串
        /// <summary>
        /// 构建Update语句串
        /// 注意：如果本方法过滤了int,decimal类型更新为０的列，如果希望更新它们需要指定FieldParams参数
        /// </summary>
        /// <param name="entity">实体列表</param>
        /// <param name="fieldParams">要更新的字段</param>
        /// <returns></returns>
        private Tuple<string, object[]> CreateUpdateSql(TEntity entity, params string[] fieldParams)
        {
            if (entity == null)
                throw new ArgumentException("The database entity can not be null.");
            var pkList = GetPrimaryKey().Select(i => i.Name).ToList();

            var entityType = entity.GetType();
            var tableFields = new List<PropertyInfo>();
            if (fieldParams != null && fieldParams.Count() > 0)
            {
                tableFields = entityType.GetProperties().Where(i => fieldParams.Contains(i.Name)).ToList();
            }
            else
            {
                tableFields = entityType.GetProperties().Where(i =>
                              !pkList.Contains(i.Name)
                              && i.GetValue(entity, null) != null
                              && !i.PropertyType.IsEnum
                              && !(i.PropertyType == typeof(ValueType) && Convert.ToInt64(i.GetValue(entity, null)) == 0)
                              && !(i.PropertyType == typeof(DateTime) && Convert.ToDateTime(i.GetValue(entity, null)) == DateTime.MinValue)
                              && i.PropertyType != typeof(EntityState)
                              && i.GetCustomAttributes(false) != null//过滤导航属性
                              && (i.PropertyType.IsValueType || i.PropertyType == typeof(string))
                              ).ToList();
            }




            //过滤主键，航行属性，状态属性等
            if (pkList == null || pkList.Count == 0)
                throw new ArgumentException("The Table entity have not a primary key.");
            var arguments = new List<object>();
            var builder = new StringBuilder();

            foreach (var change in tableFields)
            {
                if (pkList.Contains(change.Name))
                    continue;
                if (arguments.Count != 0)
                    builder.Append(", ");
                builder.Append(change.Name + " = {" + arguments.Count + "}");
                if (change.PropertyType == typeof(string)
                    || change.PropertyType == typeof(DateTime)
                    || change.PropertyType == typeof(DateTime?)
                    || change.PropertyType == typeof(bool?)
                    || change.PropertyType == typeof(bool))
                    arguments.Add("'" + change.GetValue(entity, null).ToString().Replace("'", "char(39)") + "'");
                else
                    arguments.Add(change.GetValue(entity, null));
            }

            if (builder.Length == 0)
                throw new Exception("没有任何属性进行更新");

            builder.Insert(0, " UPDATE " + string.Format("[{0}]", entityType.Name) + " SET ");

            builder.Append(" WHERE ");
            bool firstPrimaryKey = true;

            foreach (var primaryField in pkList)
            {
                if (firstPrimaryKey)
                    firstPrimaryKey = false;
                else
                    builder.Append(" AND ");

                object val = entityType.GetProperty(primaryField).GetValue(entity, null);
                Type pkType = entityType.GetProperty(primaryField).GetType();
                builder.Append(GetEqualStatment(primaryField, arguments.Count, pkType));
                arguments.Add(val);
            }
            return new Tuple<string, object[]>(builder.ToString(), arguments.ToArray());

        }
        #endregion
        #region 构建Delete语句串
        /// <summary>
        /// 构建Delete语句串
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        private Tuple<string, object[]> CreateDeleteSql(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException("The database entity can not be null.");

            Type entityType = entity.GetType();
            List<string> pkList = GetPrimaryKey().Select(i => i.Name).ToList();
            if (pkList == null || pkList.Count == 0)
                throw new ArgumentException("The Table entity have not a primary key.");

            var arguments = new List<object>();
            var builder = new StringBuilder();
            builder.Append(" Delete from " + string.Format("[{0}]", entityType.Name));

            builder.Append(" WHERE ");
            bool firstPrimaryKey = true;

            foreach (var primaryField in pkList)
            {
                if (firstPrimaryKey)
                    firstPrimaryKey = false;
                else
                    builder.Append(" AND ");

                Type pkType = entityType.GetProperty(primaryField).GetType();
                object val = entityType.GetProperty(primaryField).GetValue(entity, null);
                builder.Append(GetEqualStatment(primaryField, arguments.Count, pkType));
                arguments.Add(val);
            }
            return new Tuple<string, object[]>(builder.ToString(), arguments.ToArray());
        }
        #endregion

        #endregion



        public IQueryable<TEntity> LoadPageEntities(int pageSize, int pageIndex, out int totalCount, System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda, bool isAsc, string orderByProperty)
        {
            IQueryable<TEntity> temp = Db.Set<TEntity>().Where(whereLambda).AsQueryable();

            totalCount = temp.Count();
            temp=temp.OrderBy(orderByProperty, isAsc).Skip(pageSize * (pageIndex - 1))
                          .Take(pageSize).AsQueryable(); ;

            return temp;
        }
    }
    #region 扩展方法
    /// <summary>
    ///  ObjectContext扩展方法
    /// </summary>
    public static class ObjectContextExtensions
    {
        /// <summary>
        /// 得到实体键
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static EntitySetBase GetEntitySet(this ObjectContext context, Type entityType)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context can't  null!");
            }

            if (entityType == null)
            {
                throw new ArgumentNullException("entityType can't null!");
            }

            EntityContainer container =
                context.MetadataWorkspace
                       .GetEntityContainer(context.DefaultContainerName, DataSpace.CSpace);

            if (container == null)
            {
                return null;
            }

            EntitySetBase entitySet =
                container.BaseEntitySets
                         .Where(item => item.ElementType.Name.Equals(entityType.Name))
                         .FirstOrDefault();

            return entitySet;
        }

    }
        #endregion
}
