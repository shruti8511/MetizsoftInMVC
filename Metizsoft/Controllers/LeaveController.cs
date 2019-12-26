using MapYourMeds.App_Start;
using Metizsoft.Data.ViewModal;
using Metizsoft.Model;
using Metizsoft.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;


namespace MapYourMeds.Controllers
{
    public class LeaveController : Controller
    {
        // GET: Leave
        private ICommon _ICommonService;
        private ILeave _ILeaveService;

        public LeaveController(ICommon ICommonService, ILeave ILeaveService)
        {
            _ICommonService = ICommonService;
            _ILeaveService = ILeaveService;

        }

       
        public ActionResult UnapprovedVP()
        {
            List<Register> objusers = _ILeaveService.GetUnapprovedForVP();
            return View(objusers);
        }

        
        [HttpPost]
        public ActionResult ApproveLeaveVP(long LeaveId)
        {
            try
            {
                bool data = _ILeaveService.ApproveLeaveVP(LeaveId, "Approved");
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        
        [HttpPost]
        public ActionResult RejectLeaveVP(long LeaveId)
        {
            try
            {
                bool data = _ILeaveService.ApproveLeaveVP(LeaveId, "Rejected");
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}