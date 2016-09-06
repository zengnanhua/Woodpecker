using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Repositories.IRepositories
{
    /// <summary>
    /// 基本仓储操作接口
    /// out 表示类型的协变，可以进行子类与父类的返回类型转换，in表示逆变
    /// </summary>
    public interface IRepository<TEntity>:IUnitOfWorkRepository
    {
        /// <summary>
        /// 通过主键拿一个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Find(params object[] id);
        /// <summary>
        /// 拿到可查询结果集
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetModel();
        /// <summary>
        /// 插入对象
        /// </summary>
        /// <param name="item"></param>
        void Insert(TEntity item);
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="item"></param>
        void Update(TEntity item);
        void Update(TEntity item, Expression<Func<TEntity, bool>> whereLambda);
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="item"></param>
        void Delete(TEntity item);
        /// <summary>
        /// 指定更新字段
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileds"></param>
         void UpdateEntityFields(TEntity entity, List<string> fileds);

         IQueryable<TEntity> LoadPageEntities<S>(int pageSize, int pageIndex,
                                             out int totalCount,
       Expression<Func<TEntity, bool>> whereLambda, bool isAsc, Expression<Func<TEntity, S>> orderBy);

         IQueryable<TEntity> LoadEntities(Expression<Func<TEntity, bool>> whereLambda);
    }
}
