using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MidProject.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UserAccess : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authenticated = base.AuthorizeCore(httpContext);
            return authenticated && httpContext.Session["Role"].ToString().Equals("User");
        }
    }
}