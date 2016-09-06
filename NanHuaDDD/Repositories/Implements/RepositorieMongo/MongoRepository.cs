/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/6/15 18:00:30

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using MongoDB;
using MongoDB.Configuration;
using MongoDB.Linq;
using MongoDB.Attributes;
using NanHuaDDD.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Repositories.Implements.RepositorieMongo
{
    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : BaseNoSqlEntity, new()
    {
        static string connectionString = string.Empty;
        static string databaseName = string.Empty;
        string collectionName = string.Empty;
        static Mongo mongoSelect;
        static MongoRepository()
        {
            mongoSelect = new Mongo(configuration);
            connectionString = "Server=" + System.Configuration.ConfigurationManager.AppSettings["MongoDbServer"] ?? "127.0.0.1:27017";
            databaseName = System.Configuration.ConfigurationManager.AppSettings["MongoDbDatabase"] ?? "CarParkDB";
            mongoSelect.TryConnect();
        }
        public MongoRepository()
        {
            collectionName = typeof(TEntity).FullName;
        }
        public static MongoConfiguration configuration
        {
            get
            {
                var config = new MongoConfigurationBuilder();

                config.Mapping(mapping =>
                {
                    mapping.DefaultProfile(profile =>
                    {
                        profile.SubClassesAre(t => t.IsSubclassOf(typeof(TEntity)));
                    });
                    mapping.Map<TEntity>();
                    mapping.Map<TEntity>();
                });

                config.ConnectionString(connectionString);

                return config.BuildConfiguration();
            }
        }

        #region 插入操作
        /// <summary>
        /// 插入操作
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public void Insert(TEntity item)
        {

                try
                {


                //bool b1=    mongoSelect.TryConnect();
               // bool b2= mongoSelect.TryConnect();
                var db = mongoSelect.GetDatabase(databaseName);

                    var collection = db.GetCollection<TEntity>(collectionName);

                    collection.Insert(item, true);

           

                }
                catch (Exception)
                {
                   
                    throw;
                }
           
        }
        #endregion

        #region 更新操作
        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public void Update(TEntity item, Expression<Func<TEntity, bool>> func)
        {
          
                try
                {
                      mongoSelect.TryConnect();

                    var db = mongoSelect.GetDatabase(databaseName);

                    var collection = db.GetCollection<TEntity>(collectionName);

                    collection.Update<TEntity>(item, func, true);
                
                }
                catch (Exception ex)
                {
                   
                    throw ex;
                }
           
        }
        
        #endregion

        #region 读取单条记录
        /// <summary>
        ///读取单条记录
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public TEntity Single(Expression<Func<TEntity, bool>> func)
        {
         
                try
                {
                    //mongoSelect.TryConnect();

                    var db = mongoSelect.GetDatabase(databaseName);

                    var collection = db.GetCollection<TEntity>(collectionName);

                    var single = collection.Linq().FirstOrDefault(func);

                

                    return single;

                }
                catch (Exception ex)
                {
                  
                    throw ex;
                }
           
        }
        #endregion

        #region 删除操作
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public void Delete(Expression<Func<TEntity, bool>> func)
        {
           
                try
                {
                    mongoSelect.Connect();

                    var db = mongoSelect.GetDatabase(databaseName);

                    var collection = db.GetCollection<TEntity>(collectionName);

                    //这个地方要注意，一定要加上T参数，否则会当作object类型处理
                    collection.Remove<TEntity>(func);
                }
                catch (Exception ex)
                {
                  
                    throw ex;
                }
           
        }
        #endregion

        public TEntity Find(params object[] id)
        {
            string tempId = (string)id[0];
           return Single(c=>c.Id==tempId);
        }

        public IQueryable<TEntity> GetModel()
        {
          
                try
                {
                    //mongoSelect.TryConnect();

                    var db = mongoSelect.GetDatabase(databaseName);

                    var collection = db.GetCollection<TEntity>(collectionName);
                    
                    return collection.Linq();
                }
                catch (Exception ex)
                {
                   mongoSelect.Disconnect();
                    throw ex;
                }
           
        }

        public void Update(TEntity item)
        {
            
            Update(item,c=>c.Id==item.Id);
        }

        public void Delete(TEntity item)
        {
            Delete(c=>c.Id==item.Id);
        }

        public void UpdateEntityFields(TEntity entity, List<string> fileds)
        {
            TEntity temp = Single(c => c.Id == entity.Id);
            Type type = typeof(TEntity);
            foreach (var str in fileds)
            {
                var properInfo=type.GetProperty(str);
                 var val= properInfo.GetValue(entity);
                 properInfo.SetValue(temp, val);
            }
            Update(temp);
        }

        public IQueryable<TEntity> LoadPageEntities<S>(int pageSize, int pageIndex, out int totalCount, Expression<Func<TEntity, bool>> whereLambda, bool isAsc, Expression<Func<TEntity, S>> orderBy)
        {
            totalCount = 0;

          
                try
                {
                   // mongoSelect.TryConnect();

                    var db = mongoSelect.GetDatabase(databaseName);

                    var collection = db.GetCollection<TEntity>(collectionName);
                    Expression<Func<TEntity, bool>> countFunc = c => true;
                    totalCount = Convert.ToInt32(collection.FindAll().Documents.Count());

                    IQueryable<TEntity> temp = collection.Linq().Where(whereLambda).AsQueryable();

                    if (isAsc)
                    {
                        temp = temp.OrderBy(orderBy).Skip(pageSize * (pageIndex - 1))
                                                    .Take(pageSize).Select(i => i);
                    }
                    else
                    {
                        temp = temp.OrderByDescending(orderBy).Skip(pageSize * (pageIndex - 1))
                                                   .Take(pageSize).Select(i => i);
                    }


                  

                    return temp;

                }
                catch (Exception)
                {
                     mongoSelect.Disconnect();
                    throw;
                }
         
        }

        public IQueryable<TEntity> LoadEntities(Expression<Func<TEntity, bool>> whereLambda)
        {
          
                try
                {
                    //mongoSelect.TryConnect();
                    var db = mongoSelect.GetDatabase(databaseName);

                    var collection = db.GetCollection<TEntity>(collectionName);
                    Expression<Func<TEntity, bool>> countFunc = c => true;


                    var personList = collection.Linq().Where(whereLambda).Select(i => i);


                  
                    return personList;

                }
                catch (Exception ex)
                {
                   mongoSelect.Disconnect();
                    throw ex;
                }
       
           
        }
        ~MongoRepository()
        {
            try
            {
                mongoSelect.Disconnect();
            }
            catch (Exception)
            {
            }
        }
    }
}
