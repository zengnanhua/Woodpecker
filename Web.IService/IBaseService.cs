/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/8/10 14:21:08

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using NanHuaDDD.Repositories.Implements.RepositorieEF;
using NanHuaDDD.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.IService
{
    public interface IBaseService<TEntity> : IExtensionRepository<TEntity>, IEFRepositorieSqlExtension<TEntity> where TEntity : class,new()
    {

    }
}
