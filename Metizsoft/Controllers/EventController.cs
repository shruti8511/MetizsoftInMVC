using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Metizsoft.Data;
using Metizsoft.Service;
using Metizsoft.Data.Modal;
using System.Web.Mvc;
using MapYourMeds.App_Start;


namespace MapYourMeds.Controllers
{
    public class EventController : Controller
    {
        #region constractor call
        private IEvent _IEventService;

        public EventController(IEvent IEventService)
        {
            _IEventService = IEventService;

        }
        #endregion

        #region EventList
        public PartialViewResult EventList()
        {
            List<Event_Mst> objevents = _IEventService.GetAllEventList();
            return PartialView(objevents);
        }
        #endregion

        #region EventCreate
        [SessionAuthentication]
        public ActionResult EventCreate()
        {
            ViewBag.Event = _IEventService.GetAllEventName();
            return View();
        }

        [SessionAuthentication]
        [HttpPost]
        public ActionResult EventCreate(Event_Mst objevents)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    //if (objevents.EventID == 0)
                    // {
                    objevents.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    // }
                    objevents.UpdatedBy = Convert.ToInt32(Session["UserID"]);
                }
                objevents.CreatedOn = DateTime.UtcNow;
                objevents.UpdatedOn = DateTime.UtcNow;
                objevents.IsActive = true;
                bool data = _IEventService.AddEvents(objevents);

                if (data == true)
                {
                    NotificationMaster obj = new NotificationMaster();
                    // obj.UserId = objevents.CreatedBy;
                    obj.NotificationTitle = objevents.EventName;
                    obj.NotificationMessage = "New Events is Created";

                    bool result = CommonService.SaveNotification(obj);
                }

                ViewBag.Event = _IEventService.GetAllEventName();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Delete Event
        public JsonResult DeleteEvent(long ID, bool IsActive)
        {
            try
            {
                bool data = _IEventService.DeleteEvent(ID, IsActive);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);

            }
        }
        #endregion
    }
}