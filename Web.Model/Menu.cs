//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.EFEntity
{
    using System;
    using System.Collections.Generic; using System.Runtime.Serialization;
    [Serializable][DataContract]
    public partial class Menu
    {
        public Menu()
        {
            this.OtherMenu = new HashSet<OtherMenu>();
            this.R_Role_Menu = new HashSet<R_Role_Menu>();
        }
    
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public Nullable<int> FatherID { get; set; }
        public string Area { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public Nullable<int> OrderNo { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> Method { get; set; }
        public string ToolIco { get; set; }
    
        public virtual ICollection<OtherMenu> OtherMenu { get; set; }
        public virtual ICollection<R_Role_Menu> R_Role_Menu { get; set; }
    }
}