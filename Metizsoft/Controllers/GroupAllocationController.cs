using MapYourMeds.App_Start;
using Metizsoft.Data.Modal;
using Metizsoft.Service;
using Metizsoft.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MapYourMeds.Controllers
{
    public class GroupAllocationController : Controller
    {
        // GET: GroupAllocation

        public IGroup _IGroup;
        private IProduct _IProductservice;
        private IDoctor _IDoctorService;
        private IRetail _IRetailService;

        public GroupAllocationController(IGroup IGroup, IProduct IProductservice, IDoctor IDoctorService, IRetail IRetailService)
        {
            _IGroup = IGroup;
            _IProductservice = IProductservice;
            _IDoctorService = IDoctorService;
            _IRetailService = IRetailService;
        }

        [SessionAuthentication]
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                ViewBag.UserGroup = _IGroup.SelectGroupType();
                ////List<Group_Mst> lstEntity = _IGroup.SelectAllGroup();
                //ViewBag.UserGroup = lstEntity;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public PartialViewResult GroupAlloctionlist(long GroupType = 0)
        {
            List<Group_MstModel> lstEntity = new List<Group_MstModel>();
            lstEntity = _IGroup.GetAlloctedList();
            if (GroupType == 0)
            {
                return PartialView(lstEntity);
            }
            else
            {
                return PartialView(lstEntity.Where(i => i.GroupTypeID == GroupType).ToList());
            }
        }

       
        public ActionResult GroupNameByTypeID(long GroupID)
        {
            List<Group_Mst> lstGroup = _IGroup.SelectGroup(GroupID);
            List<Doctor_MstModel> ObjDoctor = new List<Doctor_MstModel>();
            List<RetailerMst_mst> objRetail = new List<RetailerMst_mst>();
            List<Product_Mst> ObjProduct = new List<Product_Mst>();

            if (GroupID == 1)
            {
                ObjDoctor = _IDoctorService.GetAllDoctorList();
            }
            else if (GroupID == 2)
            {
                objRetail = _IRetailService.GetAllRetailerList();
            }
            else if (GroupID == 3)
            {
                ObjProduct = _IProductservice.GetAllProductList();
            }

            return Json(new { lstGroup = lstGroup, ObjDoctor = ObjDoctor, objRetail = objRetail, ObjProduct = ObjProduct }, JsonRequestBehavior.AllowGet);
        }
                        
        public ActionResult BindListGroupWise(long GroupID, long GroupTypeID)
        {
            List<Group_Mst> lstFacilitor = null;
            List<Group_Mst> allocatedFacility = null;

            #region Allocated objects
            List<Doctor_MstModel> ObjDoctor = new List<Doctor_MstModel>();
            List<Retailer_mst> objRetail = new List<Retailer_mst>();
            List<Product_Mst> ObjProduct = new List<Product_Mst>();
            #endregion

            #region Not allocated objects
            List<Doctor_MstModel> disObjDoctor = new List<Doctor_MstModel>();
            List<Retailer_mst> disobjRetail = new List<Retailer_mst>();
            List<Product_Mst> disObjProduct = new List<Product_Mst>();
            #endregion

            #region old
            //if (regId > 0)
            //{
            //    var registerModel = registerMgmt.RegisterMaster_GetRegisterIDFacility(regId);
            //    lstFacilitor = userAllocateFacilityMgmt.FacilityMaster_GroupWiseOtherFacilityDropDown(Convert.ToInt64(Session["GroupID"]), registerModel.FacilityID, regId);
            //    allocatedFacility = userAllocateFacilityMgmt.FacilityMaster_GroupWiseAllocateFacilityDropDown(Convert.ToInt64(Session["GroupID"]), registerModel.FacilityID, regId);
            //} 
            #endregion

            #region Allocated list

            if (GroupTypeID == 1)
            {
                ObjDoctor = _IDoctorService.GetAllDoctorList(GroupID, GroupTypeID);
            }
            else if (GroupTypeID == 2)
            {
                objRetail = _IRetailService.GetAllRetailerList(GroupID, GroupTypeID);
            }
            else if (GroupTypeID == 3)
            {
                ObjProduct = _IProductservice.GetAllProductList(GroupID, GroupTypeID);
            }

            #endregion

            #region Not allocated list
            if (GroupTypeID == 1)
            {
                disObjDoctor = _IDoctorService.GetAllDoctorList(GroupID);
            }
            else if (GroupTypeID == 2)
            {
                disobjRetail = _IRetailService.GetAllRetailerList(GroupID);
            }
            else if (GroupTypeID == 3)
            {
                disObjProduct = _IProductservice.GetAllProductList(GroupID);
            }
            #endregion

            if (disObjDoctor.Count != 0 || ObjDoctor.Count != 0)
            {
                return Json(new { otherfacility = disObjDoctor, allocated = ObjDoctor }, JsonRequestBehavior.AllowGet);
            }
            else if (disobjRetail.Count != 0 || objRetail.Count != 0)
            {
                return Json(new { otherfacility = disobjRetail, allocated = objRetail }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { otherfacility = disObjProduct, allocated = ObjProduct }, JsonRequestBehavior.AllowGet);
            }
        }
               
        public JsonResult SaveAllocatedGroup(long GroupID, long GroupTypeID, int[] rightlist, int[] leftlist)
        {
            try
            {
                if (rightlist != null)
                {
                    #region Allocate facility
                    foreach (var item in rightlist)
                    {
                        ALlocationModel objAllocate = new ALlocationModel();
                        objAllocate.ID = item;
                        objAllocate.GroupID = GroupID;
                        objAllocate.GroupTypeID = GroupTypeID;
                        objAllocate.CreatedOn = DateTime.Now;
                        objAllocate.CreatedBy = Session["UserID"] == null ? 1 : Convert.ToInt32(Session["UserID"]);
                        objAllocate.UpdateOn = DateTime.Now;
                        objAllocate.UpdateBy = Session["UserID"] == null ? 1 : Convert.ToInt32(Session["UserID"]);
                        objAllocate.IsActive = true;

                        _IGroup.GroupAllocation(objAllocate);
                    }
                    #endregion
                }
                if (leftlist != null)
                {
                    #region Disallocate
                    foreach (var disitem in leftlist)
                    {
                        ALlocationModel objAllocate = new ALlocationModel();
                        objAllocate.ID = disitem;
                        objAllocate.GroupID = GroupID;
                        objAllocate.GroupTypeID = GroupTypeID;
                        objAllocate.CreatedOn = DateTime.Now;
                        objAllocate.CreatedBy = Session["UserID"] == null ? 1 : Convert.ToInt32(Session["UserID"]);
                        objAllocate.UpdateOn = DateTime.Now;
                        objAllocate.UpdateBy = Session["UserID"] == null ? 1 : Convert.ToInt32(Session["UserID"]);
                        objAllocate.IsActive = false;


                        _IGroup.GroupAllocation(objAllocate);
                    }
                    #endregion
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(false, JsonRequestBehavior.AllowGet);
            }
            //return RedirectToAction("UserAllocateFacility", "Index");
        }
              
        public JsonResult DeleteAlloction(long GroupID, long GroupTypeID, long AlloctionID)
        {
            try
            {
                bool result = _IGroup.DeleteAlloction(GroupID, GroupTypeID, AlloctionID);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}