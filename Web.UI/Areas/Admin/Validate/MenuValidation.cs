using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.EFEntity;

namespace Web.Admin.Validate
{
    public class MenuValidation:BaseValidator<Menu>
    {
        public MenuValidation()
        {
            RuleFor(c => c.MenuName).NotNull().NotEmpty().WithMessage("菜单名不能为空");
            RuleFor(c => c.Area).NotNull().NotEmpty().WithMessage("区域名不能为空") ;
            RuleFor(c => c.ControllerName).NotNull().NotEmpty().WithMessage("控制器名不能为空");
            RuleFor(c => c.ActionName).NotNull().NotEmpty().WithMessage("方法名不能为空");
            RuleFor(c => c.IsDelete).NotNull().WithMessage("IsDelete要添加");
            RuleFor(c => c.FatherID).NotNull().WithMessage("父Id必写");
        }
    }
}