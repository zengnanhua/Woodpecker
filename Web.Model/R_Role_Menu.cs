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
    public partial class R_Role_Menu
    {
        public int R_Role_MenuId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<int> MenuId { get; set; }
    
        public virtual Menu Menu { get; set; }
        public virtual Role Role { get; set; }
    }
}
