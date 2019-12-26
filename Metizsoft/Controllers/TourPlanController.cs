using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Metizsoft.Data;
using Metizsoft.Service;
using Metizsoft.Data.Modal;
using System.Web.Mvc;
using MapYourMeds.App_Start;
using Metizsoft.Data.ViewModal;


namespace MapYourMeds.Controllers
{
    public class TourPlanController : Controller
    {
        #region constractor call
        private ITourProgram _ITourProgramService;

        public TourPlanController(ITourProgram ITourProgramService)
        {
            _ITourProgramService = ITourProgramService;

        }
        #endregion

        #region Tour Plan Index
        [SessionAuthentication]
        public ActionResult Index()
        {
            ViewBag.Employee = _ITourProgramService.GetUserByManagerId(Convert.ToInt32(Session["UserID"]), Convert.ToInt32(Session["RoleID"]));
            return View();
        }
        #endregion

        #region Tourplan List
        public PartialViewResult TourPlanList(TourPlanResponse model)
        {
            List<TourPlanResponse> objtour = _ITourProgramService.GetAllTourList(model);
            return PartialView(objtour);
        }
        #endregion

        #region Approve and reject

        [HttpPost]
        public ActionResult ApproveMR(long TourID, string IsApproved)
        {
            try
            {
                bool data = _ITourProgramService.ApproveMR(TourID, IsApproved, Convert.ToInt32(Session["UserID"]));
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult RejectMR(long TourID, string IsApproved)
        {
            try
            {
                bool data = _ITourProgramService.RejectMR(TourID, IsApproved, Convert.ToInt32(Session["UserID"]));
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Tourplan approve button
        [HttpPost]
        public ActionResult UpdateAllTourApprove(List<TourPlanResponse> data)
        {
            try
            {
                bool tourapprove = _ITourProgramService.UpdateAllTourApprove(data);
                return Json(tourapprove, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Tourplan reject button
        [HttpPost]
        public ActionResult UpdateAllTourRejected(List<TourPlanResponse> data)
        {
            try
            {
                bool tourapprove = _ITourProgramService.UpdateAllTourRejected(data);
                return Json(tourapprove, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}