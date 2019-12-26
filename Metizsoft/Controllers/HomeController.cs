

using MapYourMeds.App_Start;
using Metiz;
using Metizsoft.Data.Modal;
using Metizsoft.Data.ViewModal;
using Metizsoft.Model;
using Metizsoft.Models;
using Metizsoft.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Metizsoft.Controllers
{
    
    public class HomeController : Controller
    {
        private ICommon _ICommonService;
        private INotification _INotification;

        public HomeController(ICommon ICommonService,INotification INotification)
        {
            _ICommonService = ICommonService;
            _INotification = INotification;

        }

        [RoleAuthentication]
        public ActionResult index()
        {
            return View();
        }

        //[HttpGet]
        public PartialViewResult MenuList()
        {
            int RoleId = Utility.GetUserRoleId();
            DynamicMenuModel objMenu = new DynamicMenuModel();
            objMenu = _ICommonService.DynamicMenuMaster_RoleWiseMenuList(RoleId);

            return PartialView(objMenu);
        }

        public ActionResult Retailer()
        {
            return View();
        }

        [SessionAuthentication]
        public ActionResult AddNotification()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetNotificationList()
        {
            List<NotificationMaster> lstobj = _ICommonService.GetNotificationList();
            return Json(lstobj, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SaveNotification(NotificationMaster obj)
        {
            string msg = obj.NotificationMessage;

            bool result = _INotification.SaveNotification(obj);

            if (result == true)
            {
                List<Register> Users = _ICommonService.UsersList();
                try
                {
                    if (Users.Count > 0)
                    {
                        foreach (var item in Users)
                        {
                            if (!string.IsNullOrEmpty(item.InstanceID))
                            {
                                PushNotification.pushnotification(item.InstanceID, obj.NotificationTitle, obj.NotificationMessage, "1");
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    result = false;
                }

            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DashboardCount()
        {
            long UserId = WebSecurity.CurrentUserId;
            DashboardCounts Obj = _ICommonService.DashboardCount(UserId);

            return Json(Obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DashboardProductCount()        
        {
            long UserId = WebSecurity.CurrentUserId;
            List<ProductCount> Obj = _ICommonService.DashboardProductCount(UserId);

            TarGetDetails Obj1 = _ICommonService.GetTotalProductCountDashboard(UserId);
            if (Obj.Count == 0)
            {

                ProductCount pc = new ProductCount();

               

                    pc.TotalTarget = Obj1.TotalTarget;
                    pc.TotalAchieved = Obj1.TotalAchieved;

              
                Obj.Add(pc);
            }
            else
            {
                Obj[0].TotalTarget = Obj1.TotalTarget;
                Obj[0].TotalAchieved = Obj1.TotalAchieved;
            }
            if (Obj[0].TotalTarget != 0)
            {
                Obj[0].Goal = ((Obj[0].TotalAchieved * 100) / Obj[0].TotalTarget);
            }
            else
            {
                Obj[0].Goal = 0;
            }
            return Json(Obj, JsonRequestBehavior.AllowGet);
        }

    }
}
