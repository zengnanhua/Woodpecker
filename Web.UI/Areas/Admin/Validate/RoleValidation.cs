using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.EFEntity;
using FluentValidation;
namespace Web.Admin.Validate
{
    public class RoleValidation:BaseValidator<Role>
    {
        public RoleValidation()
        {
            RuleFor(c => c.RoleName).NotNull().NotEmpty().WithMessage("角色名称不能为空");
            RuleFor(c => c.IsLoginBack).NotNull().WithMessage("IsLoginBack后台字段不能为空");
            RuleFor(c => c.IsDelete).NotNull().WithMessage("IsDelete后台字段不能为空");
        }
    }
}