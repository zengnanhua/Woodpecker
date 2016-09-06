using NanHuaDDD.Repositories.Implements.RepositorieEF;
using NanHuaDDD.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.IRespository
{
    public interface IBaseRespository<TEntity> : IExtensionRepository<TEntity>, IEFRepositorieSqlExtension<TEntity> where TEntity : class,new()
    {

    }
}
