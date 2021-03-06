﻿
 //------------------------------------------------------------------------------
// <auto-generated>
// 业务仓储接口，作用：
// 1.调用数据仓储 SaveChanges 批量 执行 sql语句
// 2.方便通过 子接口属性直接 获取 对应业务子接口对象
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.Service
{
    using System;
    public partial class ServiceSession:Web.IService.IServiceSession
    {
    	  /// <summary>
         /// 调用 数据仓储接口的 SaveChanges 帮数据层完成 数据批量提交
         /// </summary>
         /// <returns></returns>
         public int SaveChange()
         {
             return DBSessionFactory.GetDBSession().SaveChanges();
         }
    	Web.IService.IMenuService _MenuService;
        public Web.IService.IMenuService MenuService 
        { 
        	get
        	{
                if (_MenuService == null)
                    _MenuService = new Web.Service.MenuService();
                return _MenuService;
        	}
        }
    
    	Web.IService.IOtherMenuService _OtherMenuService;
        public Web.IService.IOtherMenuService OtherMenuService 
        { 
        	get
        	{
                if (_OtherMenuService == null)
                    _OtherMenuService = new Web.Service.OtherMenuService();
                return _OtherMenuService;
        	}
        }
    
    	Web.IService.IR_Role_MenuService _R_Role_MenuService;
        public Web.IService.IR_Role_MenuService R_Role_MenuService 
        { 
        	get
        	{
                if (_R_Role_MenuService == null)
                    _R_Role_MenuService = new Web.Service.R_Role_MenuService();
                return _R_Role_MenuService;
        	}
        }
    
    	Web.IService.IR_UserInfo_RoleService _R_UserInfo_RoleService;
        public Web.IService.IR_UserInfo_RoleService R_UserInfo_RoleService 
        { 
        	get
        	{
                if (_R_UserInfo_RoleService == null)
                    _R_UserInfo_RoleService = new Web.Service.R_UserInfo_RoleService();
                return _R_UserInfo_RoleService;
        	}
        }
    
    	Web.IService.IRoleService _RoleService;
        public Web.IService.IRoleService RoleService 
        { 
        	get
        	{
                if (_RoleService == null)
                    _RoleService = new Web.Service.RoleService();
                return _RoleService;
        	}
        }
    
    	Web.IService.IUserInfoService _UserInfoService;
        public Web.IService.IUserInfoService UserInfoService 
        { 
        	get
        	{
                if (_UserInfoService == null)
                    _UserInfoService = new Web.Service.UserInfoService();
                return _UserInfoService;
        	}
        }
    
    }
}
