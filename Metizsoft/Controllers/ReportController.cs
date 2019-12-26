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
using Metizsoft.Service.Implementation;
using System.Web;


namespace MapYourMeds.Controllers
{
    public class ReportController : Controller
    {
        #region constractor call
        private IMaster _IMasterService;
        private ICommon _ICommonService;
        private IReport _IReportService;
        private IDoctor _IDoctorService;
        private IProduct _IProductService;
        private IRetail _IRetailService;
        private ITourProgram _ITourProgramService;
        private IExpenditure _IExpenditureservice;
        private ILeave _ILeaveService;
        private IGroup _IGroupservice;


        public ReportController(IMaster MasterServices, ICommon CommonService, IReport ReportService, IDoctor DoctorService, IProduct ProductService, IRetail IRetailService, ITourProgram TourProgramService, IExpenditure IExpenditureservice, ILeave ILeaveService, IGroup IGroup)
        {
            _IMasterService = MasterServices;
            _ICommonService = CommonService;
            _IReportService = ReportService;
            _IDoctorService = DoctorService;
            _IProductService = ProductService;
            _IRetailService = IRetailService;
            _ITourProgramService = TourProgramService;
            _IExpenditureservice = IExpenditureservice;
            _ILeaveService = ILeaveService;
            _IGroupservice = IGroup;
        }
        #endregion

        #region DCRReport

        [SessionAuthentication]
        public ActionResult DCRReport()
        {
            ViewBag.WorkType = _ICommonService.GetWorkTypeListForDropDown();
            ViewBag.Area = _ICommonService.GetAreaListForDropDown();
            ViewBag.SubArea = _ICommonService.GetSubAreaListForDropDown();
            ViewBag.User = _ICommonService.GetUserListForDropDown(Convert.ToInt32(Session["RoleID"]));
            ViewBag.WorkWith = _ICommonService.webpages_Roles_RoleList();
            return View();
        }

        [HttpPost]
        public PartialViewResult DCRReportList(DCRResponse model)
        {
            List<DCRResponse> objModel = _IReportService.GetDCRReportList(model.StartDate, model.EndDate, model.WorkTypeId, model.UserId, model.AreaID, model.SubAreaId, model.WorkWithId);
            return PartialView(objModel);
        }
        //
        // GET: /Report/
        [SessionAuthentication]
        public ActionResult GetDCRReport()
        {
            ViewBag.DCRList = _IMasterService.GetDCRReport();
            return View();
        }
        #endregion

        #region DoctorSummaryReport
        [SessionAuthentication]
        public ActionResult DoctorReport()
        {
            ViewBag.Doctor = _ICommonService.GetDoctorListForDropDown();
            //  ViewBag.Area = _ICommonService.GetAreaListForDropDown();
            return View();
        }

        [HttpPost]
        public PartialViewResult DoctorReportList(DoctorResponse model)
        {
            List<DoctorResponse> objModel = _IReportService.GetDoctorReportList(model.StartDate, model.EndDate, model.DoctorId, model.DoctorCode, model.VisitSection, model.CategoryName, model.GradeName, model.PhoneNo, model.CaseName, model.AreaName, model.SubAreaName);
            return PartialView(objModel);
        }
        //
        // GET: /Report/

        [SessionAuthentication]
        public ActionResult GetDoctorReport()
        {
            ViewBag.DoctorList = _IDoctorService.GetDoctorReport();
            return View();
        }
        #endregion

        #region Retailer Report
        [SessionAuthentication]
        public ActionResult RetailerReport()
        {
            ViewBag.Retailer = _ICommonService.GetRetailerListForDropDown();
            // ViewBag.Area = _ICommonService.GetAreaListForDropDown();
            //ViewBag.SubArea = _ICommonService.GetAreaListForDropDown();
            return View();
        }

        [HttpPost]
        public PartialViewResult RetailerReportList(RetailerResponse model)
        {
            List<RetailerResponse> objModel = _IReportService.GetRetailerReportList(model.StartDate, model.EndDate, model.ReatailerId, model.AreaName, model.SubAreaName, model.UserType, model.DOB, model.Anniversary, model.CaseName, model.RetailerCode, model.ContactName);
            return PartialView(objModel);
        }
        //
        // GET: /Report/
        [SessionAuthentication]
        public ActionResult GetRetailerReport()
        {
            ViewBag.RetailerList = _IRetailService.GetAllRetailerList();
            return View();
        }
        #endregion

        #region Promotional Report
        [SessionAuthentication]
        public ActionResult PromotionalReport()
        {
            ViewBag.Promotional = _ICommonService.GetPromotionalListForDropDown();
            ViewBag.Area = _ICommonService.GetAreaListForDropDown();
            ViewBag.User = _ICommonService.GetUserListForDropDown(Convert.ToInt32(Session["RoleID"]));
            ViewBag.Manager = _ICommonService.GetUserListForGroup();
            return View();
        }

        [HttpPost]
        public PartialViewResult PromotionalReportList(PromotionalResponse model)
        {
            List<PromotionalResponse> objModel = _IReportService.GetPromotionalReportList(model.StartDate, model.EndDate, model.PromotionalId, model.AreaId, model.UserId, model.Date, model.manager, model.managerName);
            return PartialView(objModel);
        }
        //
        // GET: /Report/

        [SessionAuthentication]
        public ActionResult GetPromotionalReport()
        {
            ViewBag.PromotionalList = _IMasterService.GetPromotionalReport();
            return View();
        }
        #endregion

        #region Sales Report
        [SessionAuthentication]
        public ActionResult SalesReport()
        {
            long RoleID = Convert.ToInt64(Session["RoleID"]);
            long UserID = Convert.ToInt64(Session["UserID"]);
            string UserId = Convert.ToString(Session["UserID"]);

            var userdetails = _ICommonService.GetUserDetailsForReports(UserId);           
            if (RoleID == 1)
            {
                ViewBag.UserByRoleDirector = _ICommonService.GetUsersByRoleID(1);
                ViewBag.UserByRoleVP = _ICommonService.GetUsersByParentID(3, UserID);
                ViewBag.UserByRoleNSM = _ICommonService.GetUsersByRoleID(4);
                ViewBag.UserByRoleZSM = _ICommonService.GetUsersByRoleID(5);
                ViewBag.UserByRoleRSM = _ICommonService.GetUsersByRoleID(6);
                ViewBag.UserByRoleASM = _ICommonService.GetUsersByRoleID(7);
                ViewBag.UserByRoleMR = _ICommonService.GetUsersByRoleID(8);
            }
            else if (RoleID == 3)
            {
                ViewBag.UserByRoleDirector = _ICommonService.GetUsersByRoleID(1);
                ViewBag.UserByRoleVP = _ICommonService.GetUsersByRoleID(3);
                ViewBag.UserByRoleNSM = _ICommonService.GetUsersByParentID(4, UserID);
                ViewBag.UserByRoleZSM = _ICommonService.GetUsersByRoleID(5);
                ViewBag.UserByRoleRSM = _ICommonService.GetUsersByRoleID(6);
                ViewBag.UserByRoleASM = _ICommonService.GetUsersByRoleID(7);
                ViewBag.UserByRoleMR = _ICommonService.GetUsersByRoleID(8);
            }
            else if (RoleID == 4)
            {
                ViewBag.UserByRoleDirector = _ICommonService.GetUsersByRoleID(1);
                ViewBag.UserByRoleVP = _ICommonService.GetUsersByRoleID(3);
                ViewBag.UserByRoleNSM = _ICommonService.GetUsersByRoleID(4);
                ViewBag.UserByRoleZSM = _ICommonService.GetUsersByParentID(5, UserID);            
                ViewBag.UserByRoleRSM = _ICommonService.GetUsersByRoleID(6);
                ViewBag.UserByRoleASM = _ICommonService.GetUsersByRoleID(7);
                ViewBag.UserByRoleMR = _ICommonService.GetUsersByRoleID(8);
            }
            else if (RoleID == 5)
            {
                ViewBag.UserByRoleDirector = _ICommonService.GetUsersByRoleID(1);
                ViewBag.UserByRoleVP = _ICommonService.GetUsersByRoleID(3);
                ViewBag.UserByRoleNSM = _ICommonService.GetUsersByRoleID(4);
                ViewBag.UserByRoleZSM = _ICommonService.GetUsersByRoleID(5);
                ViewBag.UserByRoleRSM = _ICommonService.GetUsersByParentID(6, UserID);             
                ViewBag.UserByRoleASM = _ICommonService.GetUsersByRoleID(7);
                ViewBag.UserByRoleMR = _ICommonService.GetUsersByRoleID(8);
            }
            else if (RoleID == 6)
            {
                ViewBag.UserByRoleDirector = _ICommonService.GetUsersByRoleID(1);
                ViewBag.UserByRoleVP = _ICommonService.GetUsersByRoleID(3);
                ViewBag.UserByRoleNSM = _ICommonService.GetUsersByRoleID(4);
                ViewBag.UserByRoleZSM = _ICommonService.GetUsersByRoleID(5);
                ViewBag.UserByRoleRSM = _ICommonService.GetUsersByRoleID(6);
                ViewBag.UserByRoleASM = _ICommonService.GetUsersByParentID(7, UserID);
                ViewBag.UserByRoleMR = _ICommonService.GetUsersByRoleID(8);
            }
            else if (RoleID == 7)
            {
                ViewBag.UserByRoleDirector = _ICommonService.GetUsersByRoleID(1);
                ViewBag.UserByRoleVP = _ICommonService.GetUsersByRoleID(3);
                ViewBag.UserByRoleNSM = _ICommonService.GetUsersByRoleID(4);
                ViewBag.UserByRoleZSM = _ICommonService.GetUsersByRoleID(5);
                ViewBag.UserByRoleRSM = _ICommonService.GetUsersByRoleID(6);
                ViewBag.UserByRoleASM = _ICommonService.GetUsersByRoleID(7);
                ViewBag.UserByRoleMR = _ICommonService.GetUsersByParentID(8, UserID);
            }
            
            ViewBag.Product = _IProductService.GetAllProductNameDropDown();
            ViewBag.Area = _ICommonService.GetAreaListForDropDown();
            return View();
        }

        public PartialViewResult SalesReportList(SalesTargetResponse model)
        {
            List<SalesTargetResponse> objModel = _IReportService.GetSalesReportList(model.StartDate, model.EndDate, model.Id, model.UserName, model.ProductId, model.CaseName, model.AreaID);

            return PartialView(objModel);
        }

        public ActionResult GetSalesReport()
        {
            ViewBag.SalesList = _ICommonService.GetSalesReport();
            return View();
        }

        [HttpPost]
        public JsonResult GetAllDirector()
        {
            var lstdata = _ICommonService.GetUsersByRoleID(1);
            return Json(lstdata, JsonRequestBehavior.AllowGet);
        }

       
        [HttpPost]
        public JsonResult GetUserDetailsForReports(long UserID = 0)
        {
            LoginResponse objlog = new LoginResponse();
            if (UserID == 0)
            {
                objlog = _ICommonService.GetUserDetailsForReports(UserID.ToString());
            }
            else
            {
                objlog = _ICommonService.GetUserDetailsForReports(Convert.ToString(Session["UserID"]));
            }

            return Json(objlog, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetVPByDirectorReport(long DirectorID)
        {
            var lstdata = _IReportService.GetVPByDirectorReport(DirectorID, 3);
            return Json(lstdata, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetNSMByVPIDReport(long DirectorID, long VPID)
        {
            var lstdata = _IReportService.GetNSMByVPIDReport(DirectorID, VPID, 4);
            return Json(lstdata, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetZSMByNSMIDReport(long DirectorID, long VPID, long NSMID)
        {
            var lstdata = _IReportService.GetZSMByNSMIDReport(DirectorID, VPID, NSMID, 5);
            return Json(lstdata, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetRSMByZSMIDReport(long DirectorID, long VPID, long NSMID, long ZSMID)
        {
            var lstdata = _IReportService.GetRSMByZSMIDReport(DirectorID, VPID, NSMID, ZSMID, 6);
            return Json(lstdata, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetASMByRSMIDReport(long DirectorID, long VPID, long NSMID, long ZSMID, long RSMID)
        {
            var lstdata = _IReportService.GetASMByRSMIDReport(DirectorID, VPID, NSMID, ZSMID, RSMID, 7);
            return Json(lstdata, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetMRByASMIDReport(long DirectorID, long VPID, long NSMID, long ZSMID, long RSMID, long ASMID)
        {
            var lstdata = _IReportService.GetMRByASMIDReport(DirectorID, VPID, NSMID, ZSMID, RSMID, ASMID, 8);
            return Json(lstdata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region RCPAReport
        [SessionAuthentication]
        public ActionResult RCPAReport()
        {
            ViewBag.Retailer = _ICommonService.GetRetailerListForDropDown();
            ViewBag.Doctor = _ICommonService.GetDoctorListForDropDown();
            ViewBag.Product = _ICommonService.GetProductListForDropDown();
            return View();
        }

        public PartialViewResult RCPAReportList(RCPAResponse model)
        {
            List<RCPAResponse> objModel = _IReportService.GetRCPAReportList(model.StartDate, model.EndDate, model.ReatailerId, model.ProductId, model.CaseName, model.NameID);
            return PartialView(objModel);
        }

        [SessionAuthentication]
        public ActionResult GetRCPAReport()
        {
            ViewBag.RCPAReportList = _IRetailService.GetRCPAReport();
            return View();
        }
        #endregion

        #region TourPlanVarianceHistory
        [SessionAuthentication]
        public ActionResult TourPlanVarianceHistory()
        {
            ViewBag.User = _ICommonService.GetUserListForDropDown(Convert.ToInt32(Session["RoleID"]));
            ViewBag.Area = _ICommonService.GetAreaListForDropDown();
            ViewBag.SubArea = _ICommonService.GetSubAreaListForDropDown();
            return View();
        }

        [HttpPost]
        public PartialViewResult TourPlanVarianceHistoryList(TourPlanResponse model)
        {
            List<TourPlanResponse> objModel = _IReportService.TourPlanVarianceHistory(model.StartDate, model.EndDate, model.UserId, model.AreaId, model.SubAreaId, model.Date);
            return PartialView(objModel);
        }

        [SessionAuthentication]
        public ActionResult GetTourPlanVarianceHistory()
        {
            ViewBag.TourPlanVarianceHistoryList = _ITourProgramService.GetTourPlanVarianceHistory();
            return View();
        }
        #endregion

        #region PreCallAnalysis Report
        [SessionAuthentication]
        public ActionResult PreCallAnalysisReport()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult PreCallAnalysisReportList(PrecallResponse model)
        {
            List<PrecallResponse> objModel = _IReportService.GetPreCallAnalysisReportList(model.StartDate, model.EndDate, model.SelectType);
            ViewBag.SelectType = model.SelectType;
            return PartialView(objModel);
        }

        [SessionAuthentication]
        public ActionResult GetPreCallAnalysisReport()
        {
            ViewBag.PreCallList = _IDoctorService.GetPreCallAnalysisReport();
            return View();
        }
        #endregion

        #region Doctor Details
        [SessionAuthentication]
        public ActionResult DoctorDetailsReport()
        {
            return View();
        }

        [SessionAuthentication]
        public ActionResult DoctorDetailsReportList(DoctorDetailsResponce model)
        {
            List<DoctorDetailsResponce> objModel = _IReportService.GetDoctorDetailsReportList();
            return View(objModel);
        }

        [SessionAuthentication]
        public ActionResult GetDoctorDetailsReport()
        {
            ViewBag.DoctorDetailList = _IDoctorService.GetDoctorDetailsReport();
            return View();
        }
        #endregion

        #region Expenditure Report
        [SessionAuthentication]
        public ActionResult ExpenditureReport()
        {
            ViewBag.User = _ICommonService.GetUserListForDropDown(Convert.ToInt32(Session["RoleID"]));
            ViewBag.Expense = _ICommonService.GetExpenseListForDropDown();
            return View();
        }

        [HttpPost]
        public PartialViewResult ExpenditureReportList(ExpenditureResponse model)
        {
            List<ExpenditureResponse> objModel = _IReportService.GetExpenditureReportList(model.StartDate, model.EndDate, model.UserId, model.ExpenserId, model.Amount);
            return PartialView(objModel);
        }

        [SessionAuthentication]
        public ActionResult GetExpenditureReport()
        {
            ViewBag.ExpenditureList = _IExpenditureservice.GetExpenditureReport();
            return View();
        }
        #endregion

        #region Leave Report
        [SessionAuthentication]
        public ActionResult LeaveReport()
        {
            ViewBag.Manager = _ICommonService.GetUserListForGroup();
            ViewBag.User = _ICommonService.GetUserListForDropDown(Convert.ToInt32(Session["RoleID"]));
            ViewBag.Leave = _ICommonService.GetLeaveListForDropDown();
            return View();
        }


        [HttpPost]
        public PartialViewResult LeaveReportList(LeaveResponse model)
        {
            List<LeaveResponse> objModel = _IReportService.GetLeaveReportList(model.StartDate, model.EndDate, model.UserId, model.LeaveId, model.Reason, model.DaysOfLeave, model.manager, model.managerName);
            return PartialView(objModel);
        }

        [SessionAuthentication]
        public ActionResult GetLeaveReport()
        {
            ViewBag.LeaveList = _ILeaveService.GetLeaveReport();
            return View();
        }
        #endregion

        #region ProductList Report
        [SessionAuthentication]
        public ActionResult ProductReport()
        {
            ViewBag.Product = _ICommonService.GetProductListForDropDown();
            ViewBag.ProductType = _ICommonService.GetProductTyepeListForDropDown();
            return View();
        }

        [HttpPost]
        public PartialViewResult ProductReportList(ProductListResponse model)
        {
            List<ProductListResponse> objModel = _IReportService.GetProductReportList(model.StartDate, model.EndDate, model.ProductId, model.StrangthIn, model.MeasureIn, model.ProductCode, model.PTS, model.CaseName, model.NameID, model.MRP, model.STDRate, model.BrandName, model.ProductTypeId);
            return PartialView(objModel);
        }

        [SessionAuthentication]
        public ActionResult GetProductReport()
        {
            ViewBag.ProductList = _IProductService.GetProductReport();
            return View();
        }
        #endregion

        #region DoctorFieldUserReport
        [SessionAuthentication]
        public ActionResult DoctorFieldUserReport()
        {
            ViewBag.Doctor = _ICommonService.GetDoctorListForDropDown();
            ViewBag.User = _ICommonService.GetUserListForDropDown(Convert.ToInt32(Session["RoleID"]));
            ViewBag.Product = _ICommonService.GetProductListForDropDown();
            ViewBag.ProductType = _ICommonService.GetProductTyepeListForDropDown();
            return View();
        }

        [HttpPost]
        public PartialViewResult DoctorFieldUserReportList(DoctorFieldUserResponse model)
        {
            List<DoctorFieldUserResponse> objModel = _IReportService.GetDoctorFieldUserReportList(model.StartDate, model.EndDate, model.DoctorCode, model.ProductId, model.UserId, model.CategoryName, model.GradeName, model.CaseName, model.DoctorId, model.ProductTypeId, model.AreaName, model.SubAreaName);
            return PartialView(objModel);
        }

        [SessionAuthentication]
        public ActionResult GetDoctorFieldUserReport()
        {
            ViewBag.DoctorFieldUserList = _IDoctorService.GetDoctorFieldUserReport();
            return View();
        }
        #endregion

        #region DoctorList Report
        [SessionAuthentication]
        public ActionResult DoctorListReport()
        {
            ViewBag.Group = _ICommonService.GetGroupListForDropDown();
            return View();
        }

        [HttpPost]
        public PartialViewResult DoctorListReportList(DoctorListResponse model)
        {
            List<DoctorListResponse> objModel = _IReportService.GetDoctorListReportList(model.StartDate, model.EndDate, model.GroupID, model.Dob, model.Dow, model.CaseName, model.AreaName, model.SubAreaName, model.GroupCode);
            return PartialView(objModel);
        }

        [SessionAuthentication]
        public ActionResult GetDoctorListReport()
        {
            ViewBag.DoctorList = _IDoctorService.GetDoctorListReport();
            return View();
        }
        #endregion

        #region DoctorProductList Report
        [SessionAuthentication]
        public ActionResult DoctorProductReport()
        {
            ViewBag.Group = _ICommonService.GetGroupListForDropDown();
            ViewBag.Product = _ICommonService.GetProductListForDropDown();
            ViewBag.Doctor = _ICommonService.GetDoctorListForDropDown();
            ViewBag.ProductType = _ICommonService.GetProductTyepeListForDropDown();
            return View();
        }

        [HttpPost]
        public PartialViewResult DoctorProductReportList(DoctorProductResponse model)
        {
            List<DoctorProductResponse> objModel = _IReportService.GetDoctorProductReportList(model.StartDate, model.EndDate, model.DoctorCode, model.DoctorId, model.ProductId, model.GroupID, model.GradeName, model.CaseName, model.CategoryName, model.ProductTypeId, model.GroupCode);
            return PartialView(objModel);
        }

        [SessionAuthentication]
        public ActionResult GetDoctorProductReport()
        {
            ViewBag.DoctorProductList = _IDoctorService.GetDoctorProductReport();
            return View();
        }
        #endregion

        #region Doctor Retailer Report
        [SessionAuthentication]
        public ActionResult DoctorRetailerReport()
        {
            ViewBag.Group = _ICommonService.GetGroupListForDropDown();
            ViewBag.Product = _ICommonService.GetProductListForDropDown();
            ViewBag.Retailer = _ICommonService.GetRetailerListForDropDown();
            ViewBag.ProductType = _ICommonService.GetProductTyepeListForDropDown();
            return View();
        }

        [HttpPost]
        public PartialViewResult DoctorRetailerReportList(DoctorRetailerResponse model)
        {
            List<DoctorRetailerResponse> objModel = _IReportService.GetDoctorRetailerReportList(model.StartDate, model.EndDate, model.RetailerId, model.GroupID, model.ProductId, model.CaseName, model.ProductTypeId, model.RetailerCode, model.GroupCode);
            return PartialView(objModel);
        }

        [SessionAuthentication]
        public ActionResult GetDoctorRetailerReport()
        {
            ViewBag.DoctorRetailerList = _IDoctorService.GetDoctorRetailerReport();
            return View();
        }
        #endregion

        #region Group Report
        [SessionAuthentication]
        public ActionResult GroupReport()
        {
            ViewBag.Group = _ICommonService.GetGroupListForDropDown();
            ViewBag.User = _ICommonService.GetUserListForDropDown(Convert.ToInt32(Session["RoleID"]));
            ViewBag.Manager = _ICommonService.GetUserListForGroup();
            return View();
        }

        [HttpPost]
        public PartialViewResult GroupReportList(GroupreportResponse model)
        {
            List<GroupreportResponse> objModel = _IReportService.GetGroupReportList(model.StartDate, model.EndDate, model.GroupID, model.UserId, model.CaseName, model.GroupCode, model.manager, model.managerName);
            return PartialView(objModel);
        }

        [SessionAuthentication]
        public ActionResult GetGroupReport()
        {
            ViewBag.GroupList = _IGroupservice.GetGroupReport();
            return View();
        }
        #endregion

        #region Group Details Report
        [SessionAuthentication]
        public ActionResult GroupDetailsReport()
        {
            ViewBag.Group = _ICommonService.GetGroupListForDropDown();
            ViewBag.User = _ICommonService.GetUserListForDropDown(Convert.ToInt32(Session["RoleID"]));
            ViewBag.Product = _ICommonService.GetProductListForDropDown();
            ViewBag.Manager = _ICommonService.GetUserListForGroup();
            return View();
        }

        [HttpPost]
        public PartialViewResult GroupDetailsReportList(GroupDetailsResponse model)
        {
            List<GroupDetailsResponse> objModel = _IReportService.GetGroupDetailsReportList(model.StartDate, model.EndDate, model.GroupID, model.UserId, model.ProductId, model.CaseName,model.NameID, model.GroupCode, model.manager, model.managerName);
            return PartialView(objModel);
        }

        [SessionAuthentication]
        public ActionResult GetGroupDetailsReport()
        {
            ViewBag.GroupDetailsList = _IGroupservice.GetGroupDetailsReport();
            return View();
        }
        #endregion

        #region ProductTarget Report
        [SessionAuthentication]
        public ActionResult ProductTargetReport()
        {
            ViewBag.Product = _ICommonService.GetProductListForDropDown();
            ViewBag.SubArea = _ICommonService.GetSubAreaListForDropDown();
            ViewBag.ProductGroup = _IGroupservice.SelectGroupTypeForProduct();
            return View();
        }

        [HttpPost]
        public JsonResult GetNameByRxType(int RxType)
        {
            var lstdata = _IProductService.GetNameByRxType(RxType);
            return Json(lstdata, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public PartialViewResult ProductTargetReportList(ProductTargetViewModel model)
        {
            List<ProductTargetViewModel> objModel = _IReportService.GetProductTargetReportList(model.StartDate, model.EndDate, model.ProductId, model.CaseName, model.NameID, model.ProductGroupId, model.SubAreaId);
            return PartialView(objModel);
        }

        [SessionAuthentication]
        public ActionResult GetProductTargetReport()
        {
            ViewBag.ProductTargetList = _IProductService.GetProductTargetReport();
            return View();
        }
        #endregion

        #region DoctorDCR Report
        [SessionAuthentication]
        public ActionResult DoctorDCRReport()
        {
            ViewBag.WorkType = _ICommonService.GetWorkTypeListForDropDown();
            ViewBag.Area = _ICommonService.GetAreaListForDropDown();
            ViewBag.SubArea = _ICommonService.GetSubAreaListForDropDown();
            ViewBag.User = _ICommonService.GetUserListForDropDown(Convert.ToInt32(Session["RoleID"]));
            ViewBag.WorkWith = _ICommonService.webpages_Roles_RoleList();
            ViewBag.Product = _ICommonService.GetProductListForDropDown();
            ViewBag.ProductType = _ICommonService.GetProductTyepeListForDropDown();
            return View();
        }

        [HttpPost]
        public PartialViewResult DoctorDCRReportList(DoctorDCRViewModel model)
        {
            List<DoctorDCRViewModel> objModel = _IReportService.GetDoctorDCRReportList(model.StartDate, model.EndDate, model.UserId, model.AreaID, model.SubAreaId, model.ProductId, model.ProductTypeId, model.CaseName, model.VisitSection, model.Prescription, model.WorkTypeId, model.WorkWithId);
            return PartialView(objModel);
        }

        [SessionAuthentication]
        public ActionResult GetDoctorDCRReport()
        {
            ViewBag.DoctorDCRList = _IDoctorService.GetDoctorDCRReport();
            return View();
        }
        #endregion

        #region RetailerDCRReport
        [SessionAuthentication]
        public ActionResult RetailerDCRReport()
        {
            ViewBag.WorkWith = _ICommonService.webpages_Roles_RoleList();
            ViewBag.WorkType = _ICommonService.GetWorkTypeListForDropDown();
            ViewBag.Area = _ICommonService.GetAreaListForDropDown();
            ViewBag.SubArea = _ICommonService.GetSubAreaListForDropDown();
            ViewBag.User = _ICommonService.GetUserListForDropDown(Convert.ToInt32(Session["RoleID"]));
            ViewBag.ProductType = _ICommonService.GetProductTyepeListForDropDown();
            ViewBag.Product = _ICommonService.GetProductListForDropDown();
            return View();
        }

        [HttpPost]
        public PartialViewResult RetailerDCRReportList(RetailerDCRViewModel model)
        {
            List<RetailerDCRViewModel> objModel = _IReportService.GetRetailerDCRReportList(model.StartDate, model.EndDate, model.UserId, model.AreaID, model.SubAreaId, model.ProductId, model.ProductTypeId, model.CaseName, model.VisitSection, model.WorkTypeId, model.WorkWithId);
            return PartialView(objModel);
        }

        [SessionAuthentication]
        public ActionResult GetRetailerDCRReport()
        {
            ViewBag.RetailerDCRList = _IRetailService.GetRetailerDCRReport();
            return View();
        }
        #endregion

        #region FieldUser Target Report
        [SessionAuthentication]
        public ActionResult FieldUserTargetReport()
        {
            ViewBag.User = _ICommonService.GetUserListForDropDown(Convert.ToInt32(Session["RoleID"]));
            ViewBag.Product = _ICommonService.GetProductListForDropDown();
            ViewBag.Manager = _ICommonService.GetUserListForGroup();
            ViewBag.Area = _ICommonService.GetAreaListForDropDown();
            ViewBag.SubArea = _ICommonService.GetSubAreaListForDropDown();
            return View();
        }

        [HttpPost]
        public PartialViewResult FieldUserTargetReportList(FieldUserTargetViewModel model)
        {
            List<FieldUserTargetViewModel> objModel = _IReportService.GetFieldUserTargetReportList(model.StartDate, model.EndDate, model.UserId, model.ProductId, model.CaseName, model.NameID, model.AreaId, model.SubAreaId, model.manager, model.managerName);
            return PartialView(objModel);
        }

        [SessionAuthentication]
        public ActionResult GetFieldUserTargetReport()
        {
            ViewBag.FieldUserTargetList = _ICommonService.GetFieldUserTargetReport();
            return View();
        }
        #endregion

        #region FieldUser Report
        [SessionAuthentication]
        public ActionResult FieldUserReport()
        {
            ViewBag.WorkWith = _ICommonService.webpages_Roles_RoleList();
            ViewBag.User = _ICommonService.GetUserListForDropDown(Convert.ToInt32(Session["RoleID"]));
            ViewBag.Area = _ICommonService.GetAreaListForDropDown();
            ViewBag.Manager = _ICommonService.GetUserListForGroup();
            return View();
        }

        [HttpPost]
        public PartialViewResult FieldUserReportList(FieldUserViewModel model)
        {
            List<FieldUserViewModel> objModel = _IReportService.GetFieldUserReportList(model.StartDate, model.EndDate, model.UserId, model.RoleId, model.AreaId, model.DOB, model.DOW, model.DOJ, model.EmployeeCode, model.manager, model.managerName, model.Reportingto, model.ReportingName, model.CaseName);
            return PartialView(objModel);
        }

        [SessionAuthentication]
        public ActionResult GetFieldUserReport()
        {
            ViewBag.FieldUserList = _ICommonService.GetFieldUserReport();
            return View();
        }
        #endregion
    }
}