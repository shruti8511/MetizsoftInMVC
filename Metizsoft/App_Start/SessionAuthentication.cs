using Metizsoft.Data.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MapYourMeds.App_Start
{
    public class SessionAuthentication : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();
            var user = HttpContext.Current.Session["UserID"];
                int? RoleId =Convert.ToInt32(HttpContext.Current.Session["RoleID"]);


            bool allowtoaccess = false;

            using (AllurionEntities cx = new AllurionEntities())
            {
               
                if (user != null)
                {
                    try
                    {
                        long UserID = Convert.ToInt64(HttpContext.Current.Session["UserID"].ToString());
                      
                        var data = cx.DynamicMenuMasters.Where(i => i.Controller == controllerName && i.Action == actionName).FirstOrDefault();

                        var secounddat = cx.AuthorizeMasters.Where(z => z.MenuID == data.MenuID && z.RoleID == RoleId).FirstOrDefault();
                        if (secounddat != null)
                        {

                            allowtoaccess = true;
                        }
                        else
                        {
                            allowtoaccess = false;
                        }
                    }
                    catch
                    {
                        allowtoaccess = false;
                       
                    }
                   
                }
            }

            if (user != null)
            {


                //bool success = false;
                if (user.ToString() != "0")
                {
                    if (!allowtoaccess)
                    {

                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Account",
                            action = "AccessDenied"
                        }));

                    }
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