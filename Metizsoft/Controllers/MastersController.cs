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
    public class MastersController : Controller
    {

        private IMaster _IMasterService;

        public MastersController(IMaster IMasterService)
        {
            _IMasterService = IMasterService;

        }

        #region WorkType

        public PartialViewResult WorkTypelist()
        {
            try
            {
                List<WorkType_mst> objworktype = _IMasterService.GetAllWorkTypelist();
                return PartialView(objworktype);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [SessionAuthentication]
        public ActionResult WorkTypeCreate()
        {
            return View();
        }

        [SessionAuthentication]
        [HttpPost]
        public ActionResult WorkTypeCreate(WorkType_mst objworktype)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    //if (objworktype.WorkTypeId == 0)
                    //{
                    objworktype.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    //}
                    objworktype.UpdateBy = Convert.ToInt32(Session["UserID"]);
                }


                objworktype.CreatedOn = DateTime.UtcNow;

                objworktype.UpdateOn = DateTime.UtcNow;
                objworktype.IsActive = true;
                bool data = _IMasterService.AddWorkType(objworktype);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult DeleteWorkType(long ID , bool IsActive)
        {
            try
            {
                bool data = _IMasterService.DeleteWorkType(ID, IsActive);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Meeting
        [SessionAuthentication]
        public ActionResult MeetingTypeCreate()
        {
            return View();
        }

        [SessionAuthentication]
        [HttpPost]
        public ActionResult MeetingTypeCreate(MeetingType_mst objMeetingtype)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    // if (objMeetingtype.MeetingTypeId == 0)
                    //{
                    objMeetingtype.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    // }
                    objMeetingtype.UpdateBy = Convert.ToInt32(Session["UserID"]);
                }
                objMeetingtype.CreatedOn = DateTime.UtcNow;
                objMeetingtype.UpdateOn = DateTime.UtcNow;
                objMeetingtype.IsActive = true;
                bool data = _IMasterService.AddMeetingType(objMeetingtype);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult MeetingTypelist()
        {
            try
            {
                List<MeetingType_mst> objMeetingtype = _IMasterService.GetAllMeetingTypelist();
                return PartialView(objMeetingtype);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public JsonResult DeleteMeetingType(long ID, bool IsActive)
        {
            try
            {
                bool data = _IMasterService.DeleteMeetingType(ID, IsActive);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Area
        [SessionAuthentication]
        public ActionResult AreaCreate()
        {
            return View();
        }

        [SessionAuthentication]
        [HttpPost]
        public ActionResult AreaCreate(Area_Mst objarea)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    //if (objarea.AreaID == 0)
                    //{
                    objarea.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    // }
                    objarea.UpdatedBy = Convert.ToInt32(Session["UserID"]);
                }

                objarea.CreatedOn = DateTime.UtcNow;
                objarea.UpdatedOn = DateTime.UtcNow;
                objarea.IsActive = true;
                bool data = _IMasterService.AddArea(objarea);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult Arealist()
        {
            try
            {
                List<Area_Mst> objworktype = _IMasterService.GetAllArealist();
                return PartialView(objworktype);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public JsonResult DeleteArea(long ID, bool IsActive)
        {
            try
            {
                bool data = _IMasterService.DeleteArea(ID, IsActive);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region promotional
        public PartialViewResult Promotionallist()
        {
            try
            {
                List<Promotional_Mst> objpromst = _IMasterService.GetAllPromotionallist();
                return PartialView(objpromst);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [SessionAuthentication]
        public ActionResult PromotionalCreate()
        {
            return View();
        }

        [SessionAuthentication]
        [HttpPost]
        public ActionResult PromotionalCreate(Promotional_Mst objpromst)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    //if (objpromst.PromotionalId == 0)
                    //{
                    objpromst.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    //}
                    objpromst.UpdatedBy = Convert.ToInt32(Session["UserID"]);
                }


                objpromst.CreatedOn = DateTime.UtcNow;

                objpromst.UpdatedOn = DateTime.UtcNow;
                objpromst.IsActive = true;
                bool data = _IMasterService.AddPromotional(objpromst);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeletePromotional(long ID, bool IsActive)
        {
            try
            {
                bool data = _IMasterService.DeletePromotional(ID, IsActive);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region EventType
        [SessionAuthentication]
        public ActionResult EventTypeCreate()
        {
            return View();
        }

        [SessionAuthentication]
        [HttpPost]
        public ActionResult EventTypeCreate(EventType_Mst objeventtype)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    // if (objeventtype.EventTypeID == 0)
                    //{
                    objeventtype.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    // }
                    objeventtype.UpdatedBy = Convert.ToInt32(Session["UserID"]);
                }

                objeventtype.CreatedOn = DateTime.UtcNow;
                objeventtype.UpdatedOn = DateTime.UtcNow;
                objeventtype.IsActive = true;
                bool data = _IMasterService.AddEvent(objeventtype);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult EventTypelist()
        {
            try
            {
                List<EventType_Mst> objevent = _IMasterService.GetAllEventTypelist();
                return PartialView(objevent);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult DeleteEvent(long ID , bool IsActive)
        {
            try
            {
                bool data = _IMasterService.DeleteEvent(ID, IsActive);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region RxType
        public PartialViewResult RxTypeList()
        {
            try
            {
                List<RxType_mst> objrxtype = _IMasterService.GetAllRxTypelist();
                return PartialView(objrxtype);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [SessionAuthentication]
        public ActionResult RxTypeCreate()
        {
            return View();
        }

        [SessionAuthentication]
        [HttpPost]
        public ActionResult RxTypeCreate(RxType_mst objrxtype)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    //if (objrxtype.RxTypeId == 0)
                    //{
                    objrxtype.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    //}
                    objrxtype.UpdatedBy = Convert.ToInt32(Session["UserID"]);
                }
                objrxtype.CreatedOn = DateTime.UtcNow;
                objrxtype.UpdatedOn = DateTime.UtcNow;
                objrxtype.IsActive = true;
                bool data = _IMasterService.AddRxType(objrxtype);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult DeleteRxType(long ID, bool IsActive)
        {
            try
            {
                bool data = _IMasterService.DeleteRxType(ID, IsActive);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region ProductType
        public PartialViewResult ProductTypeList()
        {
            try
            {
                List<ProductType_Mst> objproducttype = _IMasterService.GetAllProductTypelist();
                return PartialView(objproducttype);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [SessionAuthentication]
        public ActionResult ProductTypeCreate()
        {
            return View();
        }

        [SessionAuthentication]
        [HttpPost]
        public ActionResult ProductTypeCreate(ProductType_Mst objproducttype)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    //if (objproducttype.ProductTypeId == 0)
                    //{
                    objproducttype.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    //}
                    objproducttype.UpdatedBy = Convert.ToInt32(Session["UserID"]);
                }
                objproducttype.CreatedOn = DateTime.UtcNow;
                objproducttype.UpdatedOn = DateTime.UtcNow;
                objproducttype.IsActive = true;
                bool data = _IMasterService.AddProductType(objproducttype);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteProductType(long ID , bool IsActive)
        {
            try
            {
               bool data =  _IMasterService.DeleteProductType(ID , IsActive);
               return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region LeaveType
        public PartialViewResult LeaveTypeList()
        {
            try
            {
                List<LeaveType_Mst> objleavetype = _IMasterService.GetAllLeaveTypelist();
                return PartialView(objleavetype);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [SessionAuthentication]
        public ActionResult LeaveTypeCreate()
        {
            return View();
        }

        [SessionAuthentication]
        [HttpPost]
        public ActionResult LeaveTypeCreate(LeaveType_Mst objleavetype)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    //if (objleavetype.LeaveTypeId == 0)
                    //{
                    objleavetype.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    //}
                    objleavetype.UpdatedBy = Convert.ToInt32(Session["UserID"]);
                }

                objleavetype.CreatedOn = DateTime.UtcNow;
                objleavetype.UpdatedOn = DateTime.UtcNow;
                objleavetype.IsActive = true;
                bool data = _IMasterService.AddLeaveType(objleavetype);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteLeaveType(long ID,bool IsActive )
        {
            try
            {
                bool data = _IMasterService.DeleteLeaveType(ID , IsActive);
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