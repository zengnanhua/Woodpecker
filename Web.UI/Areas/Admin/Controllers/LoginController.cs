using NanHuaDDD.Encryptor;
using NanHuaDDD.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Admin.Models;
using Web.IService;
using Web.EFEntity;
using NanHuaDDD.Util.CString;

namespace Web.Admin.Controllers
{
    [Web.Admin.Attr.SkipLogin]
    public class LoginController : BaseController
    {
        //
        // GET: /Login/
  
        public ActionResult Index()
        {
            return View();
        }
        #region 获取验证码 --GetValidateCode()
        public ActionResult GetValidateCode()
        {
            ValidateCodeHelper codeHelper = new ValidateCodeHelper();
            string strCode = codeHelper.CreateValidateCode(5);
            Session[Sessionvalues.LoginValideCode] = strCode;
            byte[] data = codeHelper.CreateValidateGraphic(strCode);
            return File(data, "image/jpeg");
        }
        #endregion
        
        #region 登录--ActionResult LoginAc(string userName, string pwd, string checkNub)
        public ActionResult LoginAc(string userName, string pwd, string checkNub)
        {

            #region 1.判断验证是否正确
            if (Session[Sessionvalues.LoginValideCode] != null && !checkNub.IsSame(Session[Sessionvalues.LoginValideCode].ToString()))
            {
                return OpeCur.AjaxMsgNOOK("验证码输入错误");
            }

            #endregion

            #region 2.判断用户名密码是否正确
            pwd = EncryptorManager.EncryptString(pwd, EncryptorType.MD5);

            UserInfo userInfo = OpeCur.BllServices.UserInfoService.LoadEntities(c => c.UserName == userName && c.Pwd == pwd).SingleOrDefault();
            if (userInfo == null)
            {
                return OpeCur.AjaxMsgNOOK("用户名或密码错误");
            }
            //后台是否有权限
            if (!IsLoginBackSystem(userName))
            {
                return OpeCur.AjaxMsgNOOK("没有权限登录后台");
            }
           
            #region 设置为永久登录
            OpeCur.CurrentUserInfo = userInfo;
            OpeCur.UserMenus = OpeCur.BllServices.MenuService.GetUserPermission(userInfo.UserName);
            OpeCur.UserNameCookie = userInfo.UserName;
            #endregion
            #endregion
            return OpeCur.AjaxMsgOK("登录成功了~", "/admin/base/MenuManage");
        }
        #endregion
        #region 判断是否有权限登录后台
        public bool IsLoginBackSystem(string UserName)
        {
            var r_useinfo_role =OpeCur.BllServices.R_UserInfo_RoleService.LoadEntities(c=>c.UserName==UserName).FirstOrDefault();
            if (r_useinfo_role == null)
            {
                return false;
            }
            var role = OpeCur.BllServices.RoleService.LoadEntities(c => c.RoleId == r_useinfo_role.RoleId).FirstOrDefault();
            if (!role.IsLoginBack.Value)
            {
                return false;
            }
            return true;
           
        }
        #endregion

        #region 退出登录
        public ActionResult LoginExit()
        {

            OpeCur.ExitLogin();
            return OpeCur.AjaxMsgOK("退出成功", "/admin/login/index");
        }
        public ActionResult ClearFlush()
        {
            OpeCur.ClearFlush();
            return OpeCur.AjaxMsgOK("清楚缓存成功");
        }
        #endregion
    }
}
