﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineAptitudeExam.Models
{
    public class Authentication : ActionFilterAttribute
    {

        bool requireAdmin;
        public Authentication()
        {
        }
        public Authentication(bool requireAdmin)
        {
            this.requireAdmin = requireAdmin;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if (session != null && session["UserInfo"] is Account)
            {
                if (!requireAdmin || (session["UserInfo"] as Account).type == ((int)Enums.Type.ADMIN))
                {
                    return;
                }
            }
            filterContext.Result = new RedirectToRouteResult(
                   new RouteValueDictionary {
                                { "Controller", "Auth" },
                                { "Action", "Login" }
                   });
        }
    }
}