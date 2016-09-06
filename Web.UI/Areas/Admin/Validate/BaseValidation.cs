
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Admin.Validate
{
    public abstract class BaseValidator<T> : AbstractValidator<T> where T : class
    {

    }
}