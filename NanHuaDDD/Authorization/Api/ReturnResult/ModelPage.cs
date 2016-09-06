/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/13 11:15:07

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Authorization.Api.ReturnResult
{
    public class ModelPage
    {
        private int total = 0;

        public int Total
        {
            get { return total; }
            set { total = value; }
        }

        private int currentPageIndex;

        public int CurrentPageIndex
        {
            get { return currentPageIndex; }
            set { currentPageIndex = value; }
        }
        private object rows;

        public object Rows
        {
            get { return rows; }
            set { rows = value; }
        }
    }
}
