
using Metizsoft.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Metizsoft
{
    public class RoleAuthentication : System.Web.Mvc.ActionFilterAttribute
    {
       
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();
            //var message = String.Format("{0} controller:{1} action:{2}", "onactionexecuting", controllerName, actionName);
            //Debug.WriteLine(message, "Action Filter Log");

            base.OnActionExecuting(filterContext);

            var role = System.Web.Security.Roles.GetRolesForUser().FirstOrDefault();

            var allRoles = System.Web.Security.Roles.GetAllRoles();

            if (role != null)
            {
                IAuthorizeAccess _authorizeservice = new AuthorizeAccessService();

                int RoleID = _authorizeservice.AuthorizeMaster_CheckAuthorizeAccess(role.ToString(), controllerName, actionName);

                HttpContext.Current.Session["RoleId"] = RoleID.ToString();
                //bool success = false;
                if (RoleID != 0)
                {
                    return;
                }
                else
                {
                    // need to redirect unauthorize access page oops moment!!!!!!! ;)
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Account",
                        action = "Login"
                    }));
                    return;
                }

            }
            else
            {
                if (controllerName == "Account" && actionName == "Login")
                {
                    return;
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Account",
                        action = "Login"
                    }));
                    return;
                }
            }
        }

    }
}