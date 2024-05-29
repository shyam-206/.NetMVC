using QuizManagement.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuizManagement.CustomFilter
{
    public class CustomAuthorizeFilter : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if ((SessionHelper.UserId != 0) && (SessionHelper.Username != "") && (SessionHelper.Useremail != "") && (SessionHelper.Role != "") )
            {
                return true;
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{
                {"controller", "Login"},
                {"action","Login"},
            });
        }

    }
}