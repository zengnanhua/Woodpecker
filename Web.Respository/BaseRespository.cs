/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/8/10 10:43:05

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.IRespository;
using  NanHuaDDD.Repositories.Implements.RepositorieEF;
using System.Data.Entity;

namespace Web.Respository
{
    public class BaseRespository<TEntity> : IBaseRespository<TEntity> where TEntity : class,new()
    {
        EFRepository<TEntity> eFRepository = null;

        public BaseRespository()
        {
            DbContext db = EFFatory.GetEFContext();
            //关闭 EF容器的 为空检查
            db.Configuration.ValidateOnSaveEnabled = false;
            eFRepository = new EFRepository<TEntity>(db);
        }

        public void Insert(IEnumerable<TEntity> item)
        {

            eFRepository.Insert(item);
        }

        public void Update(IEnumerable<TEntity> item)
        {
            eFRepository.Update(item);
        }

        public void Delete(IEnumerable<TEntity> item)
        {
            eFRepository.Delete(item);
        }

        public bool BulkInsert(IEnumerable<TEntity> item, bool isRemoveIdentity)
        {
            return eFRepository.BulkInsert(item, isRemoveIdentity);
        }

        public bool BulkInsert(IEnumerable<TEntity> item)
        {
            return eFRepository.BulkInsert(item);
        }

        public bool BulkUpdate(IEnumerable<TEntity> item, params string[] fieldParams)
        {
            return eFRepository.BulkUpdate(item,fieldParams);
        }

        public bool BulkDelete(IEnumerable<TEntity> item)
        {
            return eFRepository.BulkDelete(item);
        }

        public void SetDataContext(object db)
        {
             eFRepository.SetDataContext(db);
        }

        public IQueryable<TEntity> GetModel(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
           return eFRepository.GetModel(predicate);
        }

        public TEntity Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return eFRepository.Find(predicate);
        }

        public TEntity Find(params object[] id)
        {
            return eFRepository.Find(id);
        }

        public IQueryable<TEntity> GetModel()
        {
            return eFRepository.GetModel();
        }

        public void Insert(TEntity item)
        {
             eFRepository.Insert(item);
        }

        public void Update(TEntity item)
        {
            eFRepository.Update(item);
        }

        public void Update(TEntity item, System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda)
        {
            throw new NotImplementedException("还未实现这个方法");
        }

        public void Delete(TEntity item)
        {
            eFRepository.Delete(item);
        }

        public void UpdateEntityFields(TEntity entity, List<string> fileds)
        {
            eFRepository.UpdateEntityFields(entity,fileds);
        }

        public IQueryable<TEntity> LoadPageEntities<S>(int pageSize, int pageIndex, out int totalCount, System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda, bool isAsc, System.Linq.Expressions.Expression<Func<TEntity, S>> orderBy)
        {
            return eFRepository.LoadPageEntities<S>(pageSize,pageIndex,out totalCount,whereLambda,isAsc,orderBy);
        }

        public IQueryable<TEntity> LoadEntities(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda)
        {
            return eFRepository.LoadEntities(whereLambda);
        }

        public IQueryable<TEntity> LoadSql(string sql)
        {
            return eFRepository.LoadSql(sql);
        }

        public IQueryable<TEntity> LoadPageSql<S>(string sql, int pageSize, int pageIndex, out int totalCount, bool isAsc, System.Linq.Expressions.Expression<Func<TEntity, S>> orderBy)
        {
            return eFRepository.LoadPageSql<S>(sql, pageSize, pageIndex, out totalCount, isAsc, orderBy);
        }


        public IQueryable<TEntity> LoadPageEntities(int pageSize, int pageIndex, out int totalCount, System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda, bool isAsc, string orderByProperty)
        {
            return eFRepository.LoadPageEntities(pageSize, pageIndex, out totalCount, whereLambda, isAsc, orderByProperty);
        }
    }
}
