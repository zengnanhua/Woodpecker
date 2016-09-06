﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Repositories.IRepositories
{
    /// <summary>
    /// 扩展的Repository操作规范
    /// </summary>
    public interface IExtensionRepository<TEntity> : IRepository<TEntity>
    {
        #region 行为操作
        /// <summary>
        /// 添加集合[集合数目不大时用此方法，超大集合使用BulkInsert]
        /// </summary>
        /// <param name="item"></param>
        void Insert(IEnumerable<TEntity> item);
        /// <summary>
        /// 修改集合[集合数目不大时用此方法，超大集合使用BulkUpdate]
        /// </summary>
        /// <param name="item"></param>
        void Update(IEnumerable<TEntity> item);
        /// <summary>
        /// 删除集合[集合数目不大时用此方法，超大集合使用批量删除]
        /// </summary>
        /// <param name="item"></param>
        void Delete(IEnumerable<TEntity> item);
        /// <summary>
        /// 批量添加，添加之前可以去除自增属性,默认不去除
        /// </summary>
        /// <param name="item"></param>
        /// <param name="isRemoveIdentity"></param>
        bool BulkInsert(IEnumerable<TEntity> item, bool isRemoveIdentity);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="item"></param>
        bool BulkInsert(IEnumerable<TEntity> item);
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="item"></param>
        bool BulkUpdate(IEnumerable<TEntity> item, params string[] fieldParams);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="item"></param>
        bool BulkDelete(IEnumerable<TEntity> item);

        /// <summary>
        /// 设置当前数据库上下文对象，与具体ORM无关，上下文应该继承Lind.DDD.UnitOfWork.IDataContext
        /// </summary>
        void SetDataContext(object db);
        #endregion

        #region 查询操作
        /// <summary>
        /// 根据指定lambda表达式,得到延时结果集
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetModel(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据指定lambda表达式,得到第一个实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity Find(Expression<Func<TEntity, bool>> predicate);

   
        #endregion
    }
}
