using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Common
{
    public interface ICacheWriter
    {
        void Set(string key, object value, DateTime exp);
        void Set(string key, object value);
        object Get(string key);
        void Delete(string key);
    }
}
