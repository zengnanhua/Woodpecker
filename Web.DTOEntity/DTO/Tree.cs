/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/8/22 15:55:38

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.DTOEntity.DTO
{
    public class Tree
    {
        /// <summary>
        /// id
        /// </summary>
        private string _id = string.Empty;
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 显示的状态
        /// </summary>
        private string _state = string.Empty;
        public string state
        {
            get { return _state; }
            set { _state = value; }
        }
        private string _checked = string.Empty;
        public string @checked
        {
            get { return _checked; }
            set { _checked = value; }
        }
        /// <summary>
        /// Tree 的名字
        /// </summary>
        private string _text = string.Empty;
        public string text
        {
            get { return _text; }
            set { _text = value; }
        }
        private Dictionary<string, string> _attributes = new Dictionary<string, string>();
        public Dictionary<string, string> attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }

        /// <summary>
        /// 孩子节点
        /// </summary>
        private List<Tree> _children = null;
        public List<Tree> children
        {
            get { return _children; }
            set { _children = value; }
        }
    }
}
