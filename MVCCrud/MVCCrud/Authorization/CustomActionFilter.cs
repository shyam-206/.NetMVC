using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCCrud.Authorization
{
    public class CustomActionFilter : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return HttpContext.Current.Session["UserID"] != null && HttpContext.Current.Session["UserName"] != null;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Login" },
                    { "action", "Login" }
               });
        }
    }
}

