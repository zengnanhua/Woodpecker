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
    public partial class Role
    {
        public Role()
        {
            this.R_Role_Menu = new HashSet<R_Role_Menu>();
        }
    
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDiscription { get; set; }
        public Nullable<bool> IsLoginBack { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    
        public virtual ICollection<R_Role_Menu> R_Role_Menu { get; set; }
    }
}
