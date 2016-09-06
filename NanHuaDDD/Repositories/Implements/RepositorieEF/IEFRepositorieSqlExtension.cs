using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Repositories.Implements.RepositorieEF
{
    public interface IEFRepositorieSqlExtension<TEntity>
    {
        /// <summary>
        /// 用知道sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        IQueryable<TEntity> LoadSql(string sql);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="sql"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <param name="isAsc"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        IQueryable<TEntity> LoadPageSql<S>(string sql, int pageSize, int pageIndex,
                                                  out int totalCount, bool isAsc, Expression<Func<TEntity, S>> orderBy);


        IQueryable<TEntity> LoadPageEntities(int pageSize, int pageIndex,
                                         out int totalCount,
         Expression<Func<TEntity, bool>> whereLambda, bool isAsc, string orderByProperty);


    }
}
