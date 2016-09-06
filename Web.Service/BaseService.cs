/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/8/10 14:24:25

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Service
{
    public abstract class BaseService<TEntity> : IService.IBaseService<TEntity> where TEntity : class,new()
    {

        //数据父接口对象
        public IRespository.IBaseRespository<TEntity> iBaseDal = null;

        public BaseService()
        {
            SetIDAL();
        }

        public abstract void SetIDAL();

        public IRespository.IDBSession DBSession
        {
            get
            {
                return DBSessionFactory.GetDBSession();
            }
        }


        public void Insert(IEnumerable<TEntity> item)
        {
            iBaseDal.Insert(item);
        }

        public void Update(IEnumerable<TEntity> item)
        {
            iBaseDal.Update(item);
        }

        public void Delete(IEnumerable<TEntity> item)
        {
            iBaseDal.Delete(item);
        }

        public bool BulkInsert(IEnumerable<TEntity> item, bool isRemoveIdentity)
        {
            return iBaseDal.BulkInsert(item,isRemoveIdentity);
        }

        public bool BulkInsert(IEnumerable<TEntity> item)
        {
            return iBaseDal.BulkInsert(item);
        }

        public bool BulkUpdate(IEnumerable<TEntity> item, params string[] fieldParams)
        {
            return iBaseDal.BulkUpdate(item, fieldParams);
        }

        public bool BulkDelete(IEnumerable<TEntity> item)
        {
            return iBaseDal.BulkDelete(item);
        }

        public void SetDataContext(object db)
        {
             iBaseDal.SetDataContext(db);
        }

        public IQueryable<TEntity> GetModel(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return iBaseDal.GetModel(predicate);
        }

        public TEntity Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return iBaseDal.Find(predicate);
        }

        public TEntity Find(params object[] id)
        {
            return iBaseDal.Find(id);
        }

        public IQueryable<TEntity> GetModel()
        {
            return iBaseDal.GetModel();
        }

        public void Insert(TEntity item)
        {
             iBaseDal.Insert(item);
        }

        public void Update(TEntity item)
        {
            iBaseDal.Update(item);
        }

        public void Update(TEntity item, System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda)
        {
            iBaseDal.Update(item,whereLambda);
        }

        public void Delete(TEntity item)
        {
            iBaseDal.Delete(item);
        }

        public void UpdateEntityFields(TEntity entity, List<string> fileds)
        {
            iBaseDal.UpdateEntityFields(entity,fileds);
        }

        public IQueryable<TEntity> LoadPageEntities<S>(int pageSize, int pageIndex, out int totalCount, System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda, bool isAsc, System.Linq.Expressions.Expression<Func<TEntity, S>> orderBy)
        {
            return iBaseDal.LoadPageEntities<S>(pageSize, pageIndex, out totalCount, whereLambda, isAsc,orderBy);
        }

        public IQueryable<TEntity> LoadEntities(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda)
        {
            return iBaseDal.LoadEntities(whereLambda);
        }

        public IQueryable<TEntity> LoadSql(string sql)
        {
            return iBaseDal.LoadSql(sql);
        }

        public IQueryable<TEntity> LoadPageSql<S>(string sql, int pageSize, int pageIndex, out int totalCount, bool isAsc, System.Linq.Expressions.Expression<Func<TEntity, S>> orderBy)
        {
            return iBaseDal.LoadPageSql<S>(sql,pageSize,pageIndex,out totalCount,isAsc,orderBy);
        }


        public IQueryable<TEntity> LoadPageEntities(int pageSize, int pageIndex, out int totalCount, System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda, bool isAsc, string orderByProperty)
        {
            return iBaseDal.LoadPageEntities(pageSize, pageIndex, out totalCount, whereLambda, isAsc, orderByProperty);
        }
    }
}
