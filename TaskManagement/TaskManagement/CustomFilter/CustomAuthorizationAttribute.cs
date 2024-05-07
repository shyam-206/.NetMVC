using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TaskManagement.Session;

namespace TaskManagement.CustomFilter
{
    public class CustomAuthorizationAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if((SessionHelper.UserId != 0) && (SessionHelper.Username != "" ) && (SessionHelper.UserRole != ""))
            {
                return true;
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{
                {"controller", "Home"},
                {"action","Login"},
            });
        }
    }
}