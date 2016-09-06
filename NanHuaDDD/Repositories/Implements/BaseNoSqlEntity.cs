/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/6/16 11:48:25

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using MongoDB.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Repositories.Implements
{
    public abstract class BaseNoSqlEntity
    {
        public BaseNoSqlEntity()
        {
          Id=Guid.NewGuid().ToString("D");
        }
        [MongoAlias("_id")]
        public string Id {get;set; }
    }
}
