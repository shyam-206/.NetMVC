using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManagement.CustomFilter
{
    public class CustomErrorMessage : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            if (!filterContext.ExceptionHandled)
            {
                filterContext.ExceptionHandled = true;

                // Redirect to custom error page
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(new HandleErrorInfo(filterContext.Exception, filterContext.RouteData.Values["controller"].ToString(), filterContext.RouteData.Values["action"].ToString())),
                    TempData = filterContext.Controller.TempData
                };
            }
        }
    }
}