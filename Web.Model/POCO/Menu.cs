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
	public partial class Menu
	{
		public Menu ToPOCO(){
			return new Menu(){
								MenuId=this.MenuId,
				MenuName=this.MenuName,
				FatherID=this.FatherID,
				Area=this.Area,
				ControllerName=this.ControllerName,
				ActionName=this.ActionName,
				OrderNo=this.OrderNo,
				IsDelete=this.IsDelete,
				Type=this.Type,
				Method=this.Method,
				ToolIco=this.ToolIco,
			};
		}
	}
}
