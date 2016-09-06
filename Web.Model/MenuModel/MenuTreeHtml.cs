/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/8/18 15:44:02

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.EFEntity.MenuModel
{
    public class MenuTreeHtml
    {
        //最外层菜单
        public string firstTemplate = @" 
             <li class='treeview'>
              <a href='{#url#}'>
                <i class='fa fa-dashboard'></i> <span>{#menuName#}</span> <i class='{#class#}'></i>
              </a>
              {#SonMenu#}
            </li>";
        //添加菜单
        public string secondTemplate = @"
               <ul class='treeview-menu'>
                 {#SonMenu#}
               </ul>
        "; //fa fa-angle-left pull-right   //fa fa-circle-o
        //添加菜单子项
        public string TreenTemplate = @"<li><a href='{#url#}'><i class='{#class1#}'></i><i class='{#class#}'></i>{#menuName#}</a>
                {#SonMenu#}
        </li>";

        public string GetHtmlMennTree(List<Menu> menus, int FatherID, bool Isfirst = true)
        {

            string html = "";
            int SonCount = 0;
            List<Menu> sonMenus = menus.Where(c => c.FatherID == FatherID && !c.IsDelete.Value).OrderBy(c => c.OrderNo).ToList();
            foreach (var m in sonMenus)
            {
                string temp = "";
                if (Isfirst)
                {
                    temp = firstTemplate.Replace("{#url#}", CreateUrl(m.Area,m.ControllerName,m.ActionName))
                                 .Replace("{#menuName#}", m.MenuName);
                }
                else
                {
                    temp = TreenTemplate.Replace("{#url#}", CreateUrl(m.Area, m.ControllerName, m.ActionName) ).Replace("{#menuName#}", m.MenuName);
                }
                var sonsonMenus = menus.Where(c => c.FatherID == m.MenuId);
                SonCount = sonsonMenus.Count();
                if (sonsonMenus.Count() > 0)
                {

                    temp = temp.Replace("{#class#}", "fa fa-angle-left pull-right");
                    temp = temp.Replace("{#class1#}", "fa fa-circle-o");
                    temp = temp.Replace("{#SonMenu#}", GetHtmlMennTree(menus, m.MenuId, false));
                }
                else
                {
                    temp = temp.Replace("{#class1#}", "");
                    temp = temp.Replace("{#class#}", Isfirst ? "" : "fa fa-circle-o");
                    temp = temp.Replace("{#SonMenu#}", "");
                }
                html += temp;
            }
            //判断是有层次
            if (!Isfirst)
            {
                html = secondTemplate.Replace("{#SonMenu#}", html);
            }
            return html;
        }
        private string CreateUrl(string area, string controllName, string Action)
        {
            return "/"+area+"/"+controllName+"/"+Action;
        }
    }
}
