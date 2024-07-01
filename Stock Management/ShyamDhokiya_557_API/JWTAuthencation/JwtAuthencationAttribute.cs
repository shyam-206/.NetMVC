using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ShyamDhokiya_557_API.JWTAuthencation
{
    public class JwtAuthencationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var request = actionContext.Request;
            var Token = request.Headers.Authorization;

            if(Token == null || Token.Scheme == null)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.Unauthorized,"Authencation is invalid");

                return;
            }

            var username = Authencation.ValidateToken(Token.Scheme);

            if(username == null)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.Unauthorized, "Token is invalid");
                return;
            }

            base.OnActionExecuting(actionContext);
        }
    }
}