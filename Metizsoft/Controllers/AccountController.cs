using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Web.Security;
using Metizsoft.Service;
using Metizsoft.Model;
using Metizsoft.Data.ViewModal;
using System.Globalization;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using Metizsoft.Data.Modal;
using ClosedXML.Excel;
using Metizsoft.Service.Models;
using MapYourMeds.App_Start;


namespace Metizsoft.Controllers
{
    public class AccountController : Controller
    {
        #region constractor call
        private IAccount _IAccountService;
        private ICommon _ICommonService;
        private IMaster _IMasterService;

        public AccountController(ICommon ICommonService, IAccount IAccountService, IMaster IMasterService)
        {
            _ICommonService = ICommonService;
            _IAccountService = IAccountService;
            _IMasterService = IMasterService;
        }
        #endregion

        #region Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Login login)
        {
            bool success = WebSecurity.Login(login.username, login.password, false);
            if (success)
            {
                string UserId = Membership.GetUser(login.username).ProviderUserKey.ToString();
                LoginResponse responsedata = _ICommonService.GetUserDetails(UserId);

                string returnUrl = Request.QueryString["ReturnUrl"];
                if (success && responsedata != null)
                {
                    //LoginResponse responsedata = _ICommonService.GetUserDetails(Userdetails);
                    Session["UserID"] = responsedata.UserID;
                    Session["RoleID"] = responsedata.RoleId;
                    Session["Username"] = responsedata.UserName;
                    Session["Rolename"] = responsedata.RoleName;
                    Session["UserFullName"] = responsedata.UserFullName;

                    if (returnUrl == null)
                    {
                        Response.Redirect("~/Home/Index");
                    }
                    else
                    {
                        Response.Redirect(returnUrl);
                    }
                }
                else
                {
                    Response.Redirect(returnUrl);
                    return View(login);
                }
            }
            return View(login);
        }
        #endregion

        #region Register
        [SessionAuthentication]
        [RoleAuthentication]
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.RoleList = _ICommonService.webpages_Roles_RoleList();
            ViewBag.Position = _ICommonService.GetUserPosition();
            ViewBag.UserByRole = _ICommonService.GetUsersByRoleID(1);
            ViewBag.Area = _ICommonService.GetAreaListForDropDown();
            ViewBag.SubArea = _ICommonService.GetSubAreaListForDropDown();          
            var enumData = from DCREnums e in Enum.GetValues(typeof(DCREnums))
                           select new
                           {
                               ID = (int)e,
                               Name = e.ToString()
                           };
            ViewBag.DCRBlocking = new SelectList(enumData, "ID", "Name");                  
            return View();
        }

        //[Authorize(Roles = "SuperAdmin")]
        [SessionAuthentication]
        [RoleAuthentication]
        [HttpPost]
        public ActionResult Register(User register)
        {
            try
            {
                if (register.Id == 0)
                {
                    register.EmployeeCode = GetLastEmployeeCode();
                    if (!WebSecurity.UserExists(Request.Form["UserName"]))
                    {
                        WebSecurity.CreateUserAndAccount(Request.Form["UserName"], Request.Form["Password"],
                            new
                            {
                                UserRoleID = Convert.ToInt64(Request.Form["UserPosition"]),
                                FirstName = Request.Form["FirstName"],
                                LastName = Request.Form["LastName"],
                                UserName = Request.Form["UserName"],
                                Password = Request.Form["Password"],
                                MobileNumber = Request.Form["MobileNumber"],
                                EmailID = Request.Form["EmailID"],
                                Address = Request.Form["Address"],
                                AreaID = Convert.ToInt64(Request.Form["AreaID"]),
                                SubAreaID = Convert.ToInt64(Request.Form["SubAreaID"]),
                                PostCode = Request.Form["PostCode"],
                                UserPosition = Convert.ToInt64(Request.Form["UserPosition"]),
                                DirectorID = Convert.ToInt64(Request.Form["DirectorID"]),
                                HOID = Convert.ToInt64(Request.Form["HOID"]),
                                VPID = Convert.ToInt64(Request.Form["VPID"]),
                                NSMID = Convert.ToInt64(Request.Form["NSMID"]),
                                ZSMID = Convert.ToInt64(Request.Form["ZSMID"]),
                                RSMID = Convert.ToInt64(Request.Form["RSMID"]),
                                ASMID = Convert.ToInt64(Request.Form["ASMID"]),
                                MRID = 0,
                                DOB = Request.Form["DOB"],
                                DOW = Request.Form["DOW"],
                                DOJ = Request.Form["DOJ"],
                                CaseName = Request.Form["CaseName"],
                                DCRBlocking = Request.Form["DCRBlocking"],
                                ManualEmployeeCode = Request.Form["ManualEmployeeCode"],
                                EmployeeCode = register.EmployeeCode,
                                CreatedOn = DateTime.Now,
                                CreatedBy = 1,
                                UpdateOn = DateTime.Now,
                                UpdateBy = 1,
                                IsActive = true
                            });

                        if (Roles.IsUserInRole(Request.Form["UserName"], Request.Form["RoleName"]))
                        {
                            ViewBag.ResultMessage = "This user already has the role specified !";
                        }
                        else
                        {
                            Roles.AddUserToRole(Request.Form["UserName"], Request.Form["RoleName"]);
                            ViewBag.ResultMessage = "Username added to the role successfully !";
                        }
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    register.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    register.UpdateBy = Convert.ToInt32(Session["UserID"]);
                    register.CreatedOn = DateTime.UtcNow;
                    register.UpdateOn = DateTime.UtcNow;
                    register.IsActive = true;
                    bool data = _ICommonService.EditUser(register);
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #region edit register
        [HttpPost]
        public JsonResult EditRegister(int Id)
        {
            RegisterResponse objuser = _ICommonService.GetUserByUserId(Id);
            return Json(objuser, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Employee code
        public string GetLastEmployeeCode()
        {
            long EmpId = _ICommonService.GetLastEmployeeCode();
            string EmployeeCode = "";
            if (EmpId != 0)
            {
                string code = EmpId.ToString();
                //string number = code.Substring(1, 7);
                long incr = Convert.ToInt64(code) + 1;
                EmployeeCode = "EMP" + incr.ToString().PadLeft(4, '0');
            }
            else
            {
                EmployeeCode = "EMP0001";
            }
            return EmployeeCode;
        }
        #endregion
        

        [HttpPost]
        public JsonResult GetAllDirector()
        {
            var lstdata = _ICommonService.GetUsersByRoleID(1);
            return Json(lstdata, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult GetVPByDirectorID(long DirectorID)
        //{
        //    var lstdata = _ICommonService.GetVPByDirectorID(DirectorID, 2);

        //    return Json(lstdata, JsonRequestBehavior.AllowGet);
        //}


        //[HttpPost]
        //public JsonResult GetZSMByVPID(long DirectorID, long VPID)
        //{
        //    var lstdata = _ICommonService.GetZSMByVPID(DirectorID, VPID, 3);

        //    return Json(lstdata, JsonRequestBehavior.AllowGet);
        //}


        //[HttpPost]
        //public JsonResult GetRSMByZSMID(long DirectorID, long VPID, long ZSMID)
        //{
        //    var lstdata = _ICommonService.GetRSMByZSMID(DirectorID, VPID, ZSMID, 4);

        //    return Json(lstdata, JsonRequestBehavior.AllowGet);
        //}


        //[HttpPost]
        //public JsonResult GetASMByRSMID(long DirectorID, long VPID, long ZSMID, long RSMID)
        //{
        //    var lstdata = _ICommonService.GetASMByRSMID(DirectorID, VPID, ZSMID, RSMID, 5);

        //    return Json(lstdata, JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        public JsonResult GetHOByDirectorID(long DirectorID)
        {
            var lstdata = _ICommonService.GetHOByDirectorID(DirectorID, 2);

            return Json(lstdata, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetVPByHOID(long DirectorID, long HOID)
        {
            var lstdata = _ICommonService.GetVPByHOID(DirectorID, HOID, 3);

            return Json(lstdata, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetNSMByVPID(long DirectorID, long HOID, long VPID)
        {
            var lstdata = _ICommonService.GetNSMByVPID(DirectorID, HOID, VPID, 4);

            return Json(lstdata, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetZSMByNSMID(long DirectorID, long HOID, long VPID, long NSMID)
        {
            var lstdata = _ICommonService.GetZSMByNSMID(DirectorID, HOID, VPID, NSMID, 5);

            return Json(lstdata, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetRSMByZSMID(long DirectorID, long HOID, long VPID, long NSMID, long ZSMID)
        {
            var lstdata = _ICommonService.GetRSMByZSMID(DirectorID, HOID, VPID, NSMID, ZSMID, 6);

            return Json(lstdata, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetASMByRSMID(long DirectorID, long HOID, long VPID, long NSMID, long ZSMID, long RSMID)
        {
            var lstdata = _ICommonService.GetASMByRSMID(DirectorID, HOID, VPID, NSMID, ZSMID, RSMID, 7);

            return Json(lstdata, JsonRequestBehavior.AllowGet);
        }

        [SessionAuthentication]
        public ActionResult RegistrationList()
        {
            ViewBag.AreaList = _ICommonService.GetAreaListForDropDown();
            ViewBag.SubAreaList = _ICommonService.GetSubAreaListForDropDown();           
            return View();
        }

        public PartialViewResult RegistrationLists(Register model)
        {
            var enumData = from DCREnums e in Enum.GetValues(typeof(DCREnums))
                           select new
                           {
                               ID = (int)e,
                               Name = e.ToString()
                           };
            ViewBag.DCRBlocking = new SelectList(enumData, "ID", "Name");
            List<Register> objusers = _ICommonService.UsersListFilter(model);
            return PartialView(objusers);
        }

        [HttpPost]
        public ActionResult UpadateDCRBlockng(List<Register> data)
        {            
            //try
            //{
            _ICommonService.UpadateDCRBlockng(data);
            return Json("True", JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception ex)
            //{
            //    return Json(false, JsonRequestBehavior.AllowGet);
            //}
        }

        [HttpPost]
        public JsonResult ActiveDeactiveUser(activedeactive Data)
        {
            bool exist = _IAccountService.ActiveDeactive(Data);

            return Json(exist, JsonRequestBehavior.AllowGet);

        }

        //[HttpGet]
        //public PartialViewResult RegistrationList()
        //{
        //    var RegisterList = _ICommonService.UsersList();
        //    return PartialView(RegisterList);
        //}

        [SessionAuthentication]
        [RoleAuthentication]
        [HttpGet]
        public ActionResult RoleCreate()
        {
            return View();
        }

        [RoleAuthentication]
        [HttpPost]
        public JsonResult RoleCreate(int RoleId, string RoleName)
        {
            if (RoleId != 0)
            {
                if (Roles.RoleExists(RoleName))
                {
                    //update code
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                if (!Roles.RoleExists(RoleName))
                {
                    Roles.CreateRole(RoleName);

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json(false, JsonRequestBehavior.AllowGet);
                }

            }
            //if (Roles.RoleExists(role.RoleName))
            //{
            //    //ModelState.AddModelError("Error", "Rolename already exists");
            //    return View(role);
            //}
            //else
            //{
            //    Roles.CreateRole(role.RoleName);
            //    //return RedirectToAction("RoleIndex", "Account");
            //    return View();
            //}

        }

        //[RoleAuthentication]

        [HttpGet]
        public PartialViewResult RoleList()
        {
            var RoleList = _ICommonService.webpages_Roles_RoleList();
            return PartialView(RoleList);
        }


        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            //Session["UserID"] = null;
            //Session["RoleID"] = null;
            //Session["Username"] = null;
            //Session["Rolename"] = null;
            Session.Abandon();
            Response.Redirect("~/Account/Login");
            return View();
        }

        #region Expense
        [SessionAuthentication]
        public ActionResult ManageExpenseLimit()
        {
            var RoleList = _ICommonService.RoleList();
            ViewBag.RoleList = RoleList;
            return View();
        }

        [HttpPost]
        public ActionResult UpadateExpenseLimit(List<ExpenseLimit> data)
        {
            //try
            //{
            _ICommonService.UpadateExpenseLimit(data);
            return Json("True", JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception ex)
            //{
            //    return Json(false, JsonRequestBehavior.AllowGet);
            //}
        }

        public PartialViewResult ManageExpenseLimitList(int RoleId = 0)
        {
            List<ExpenseLimit> objusers = _ICommonService.ExpenseLimitList(RoleId);
            return PartialView(objusers);
        }
        #endregion

        #region Target
        [SessionAuthentication]
        public ActionResult ManageTarget()
        {
            var RoleList = _ICommonService.RoleList();
            ViewBag.RoleList = RoleList;
            ViewBag.AreaList = _ICommonService.GetAreaListForDropDown();
            ViewBag.SubAreaList = _ICommonService.GetSubAreaListForDropDown();
            return View();
        }

        public PartialViewResult ManageTargetList(Target model)
        {
            List<Target> objusers = _ICommonService.TargetList(model);
            return PartialView(objusers);
        }

        [SessionAuthentication]
        [HttpPost]
        public ActionResult UpadateTarget(List<Target> data)
        {

            _ICommonService.UpadateTarget(data);
            return Json("True", JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region UploadUserData
        public ActionResult UploadUserData()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadUserData(int UserType, HttpPostedFileBase file)
        {
            try
            {
                if (file != null)
                {
                    string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);

                    if (fileExtension == ".xlsx" || fileExtension == ".xls")
                    {
                        string fileLocation1 = Server.MapPath("~/importfile/") + "importfile.xlsx";
                        if (System.IO.File.Exists(fileLocation1))
                        {
                            System.IO.File.Delete(fileLocation1);
                        }

                        DataTable Alldata = new DataTable();

                        string filename = "importfile" + fileExtension;
                        string fileLocation = System.IO.Path.Combine(Server.MapPath("~/importfile"), filename);
                        // file is uploaded
                        file.SaveAs(fileLocation);

                        string excelConnectionString = string.Empty;
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        //connection String for xls file format.
                        //if (fileExtension == ".xls")
                        //{
                        //    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                        //    fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        //}
                        //connection String for xlsx file format.
                        if (fileExtension == ".xlsx" || fileExtension == ".xls")
                        {
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        }

                        //Create Connection to Excel work book and add oledb namespace
                        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                        excelConnection.Open();
                        DataTable dt = new DataTable();

                        dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        if (dt == null)
                        {
                            return null;
                        }

                        String[] excelSheets = new String[dt.Rows.Count];
                        int t = 0;
                        //excel data saves in temp file here.
                        foreach (DataRow row in dt.Rows)
                        {
                            excelSheets[t] = row["TABLE_NAME"].ToString();
                            t++;
                        }
                        OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                        string query = string.Format("Select * from [{0}]", excelSheets[0]);
                        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                        {
                            dataAdapter.Fill(Alldata);
                        }
                        excelConnection.Close();

                        if (UserType == 1)
                        {
                            //Alldata.Rows.Remove(Alldata.Rows[0]);
                            for (int i = 0; i < Alldata.Rows.Count; i++)
                            {
                                if (!DBNull.Value.Equals(Alldata.Rows[i][0]))
                                {

                                    UploadUserData objuplaoaduser = new UploadUserData();
                                    objuplaoaduser.DoctorCode = Alldata.Rows[i][0].ToString();
                                    DateTime? dd = Convert.ToDateTime(Alldata.Rows[i][1].ToString());
                                    //DateTime ConvertedBirthDate = DateTime.ParseExact(Alldata.Rows[i][1].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                                    objuplaoaduser.BirthDate = dd;
                                    DateTime? dd1 = Convert.ToDateTime(Alldata.Rows[i][2].ToString());
                                    // DateTime ConvertedAnniversaryDate = DateTime.ParseExact(Alldata.Rows[i][2].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                                    objuplaoaduser.DOW = dd1;
                                    try
                                    {
                                        _ICommonService.UpadateUserData(objuplaoaduser);
                                        // return Json("true", JsonRequestBehavior.AllowGet);

                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                            }

                        }
                        else
                        {
                            for (int i = 0; i < Alldata.Rows.Count; i++)
                            {
                                if (!DBNull.Value.Equals(Alldata.Rows[i][0]))
                                {
                                    UploadRetailerData objuplaoadRetailer = new UploadRetailerData();
                                    objuplaoadRetailer.FullName = Alldata.Rows[i][0].ToString();
                                    DateTime? dd = Convert.ToDateTime(Alldata.Rows[i][1].ToString());
                                    //DateTime? ConvertedBirthDate = DateTime.ParseExact(Alldata.Rows[i][1].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    objuplaoadRetailer.Dob = dd;

                                    DateTime? dd1 = Convert.ToDateTime(Alldata.Rows[i][2].ToString());
                                    // DateTime? ConvertedAnniversaryDate = DateTime.ParseExact(Alldata.Rows[i][2].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    objuplaoadRetailer.Anniversary = dd1;
                                    try
                                    {
                                        _ICommonService.UpadateRetailerData(objuplaoadRetailer);
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                            }
                        }

                        TempData["Importmsg"] = "Import Successfully";
                    }
                    else
                    {
                        TempData["Importmsg"] = "Please Select .xlsx File";
                    }

                }
                else
                {
                    TempData["Importmsg"] = "Please Select .xlsx File";
                }

            }
            catch (Exception ex)
            {
                TempData["Importmsg"] = "Import Failed";
            }
            //ViewBag.BarType = BarManagement.BarTypeList(true);
            return View();

        }
        #endregion

        [HttpPost]
        public JsonResult UploadExcelTarget()
        {
            try
            {
                HttpPostedFile UserFile = System.Web.HttpContext.Current.Request.Files["File"];
                long UserID = Convert.ToInt64(Request.Form["UaserID"]);
                long CaseName = 0;
                long UserId = Convert.ToInt64(Request.Form["UserId"]);
                if (Convert.ToBoolean(Request.Form["ChkisMass"]))
                {
                    CaseName = 2;
                }
                else
                {
                    CaseName = 1;
                }
                if (UserFile != null)
                {
                    string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);

                    if (fileExtension == ".xlsx" || fileExtension == ".xls")
                    {
                        string fileLocation1 = Server.MapPath("~/importfile/") + "importfile.xlsx";
                        if (System.IO.File.Exists(fileLocation1))
                        {
                            System.IO.File.Delete(fileLocation1);
                        }

                        DataTable Alldata = new DataTable();

                        string filename = "importfile" + fileExtension;
                        string fileLocation = System.IO.Path.Combine(Server.MapPath("~/importfile"), filename);
                        // file is uploaded
                        UserFile.SaveAs(fileLocation);

                        string excelConnectionString = string.Empty;
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        //connection String for xls file format.
                        //if (fileExtension == ".xls")
                        //{
                        //    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                        //    fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        //}
                        //connection String for xlsx file format.
                        if (fileExtension == ".xlsx" || fileExtension == ".xls")
                        {
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        }

                        //Create Connection to Excel work book and add oledb namespace
                        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                        excelConnection.Open();
                        DataTable dt = new DataTable();

                        dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        if (dt == null)
                        {
                            return Json(null, JsonRequestBehavior.AllowGet);
                        }

                        String[] excelSheets = new String[dt.Rows.Count];
                        int t = 0;
                        //excel data saves in temp file here.
                        foreach (DataRow row in dt.Rows)
                        {
                            excelSheets[t] = row["TABLE_NAME"].ToString();
                            t++;
                        }
                        OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                        string query = string.Format("Select * from [{0}]", excelSheets[0]);
                        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                        {
                            dataAdapter.Fill(Alldata);
                        }
                        excelConnection.Close();

                        //    long ProductID = _ICommonService.GetProductID(CaseName,);

                        if (UserId != 0)
                        {
                            //Alldata.Rows.Remove(Alldata.Rows[0]);
                            for (int i = 0; i < Alldata.Rows.Count; i++)
                            {

                                Productallocation objuplaoaduser = new Productallocation();
                                try
                                {
                                    string Product = Alldata.Rows[i][1].ToString(); // first cell 0,0
                                    long ProductID = _ICommonService.GetProductID(CaseName, Product);
                                    //long ProductID = _ICommonService.GetProductID(ProductCaseId, Product);
                                    if (ProductID != 0)
                                    {
                                        objuplaoaduser.ProductCaseId = ProductID;
                                        objuplaoaduser.UserID = UserId;
                                        objuplaoaduser.CaseID = CaseName;
                                        objuplaoaduser.Jan = Convert.ToDecimal(Alldata.Rows[i][4].ToString() == "" ? 0 : Alldata.Rows[i][4]);
                                        objuplaoaduser.Feb = Convert.ToDecimal(Alldata.Rows[i][5].ToString() == "" ? 0 : Alldata.Rows[i][5]);
                                        objuplaoaduser.Mar = Convert.ToDecimal(Alldata.Rows[i][6].ToString() == "" ? 0 : Alldata.Rows[i][6]);
                                        objuplaoaduser.Apr = Convert.ToDecimal(Alldata.Rows[i][7].ToString() == "" ? 0 : Alldata.Rows[i][7]);
                                        objuplaoaduser.May = Convert.ToDecimal(Alldata.Rows[i][8].ToString() == "" ? 0 : Alldata.Rows[i][8]);
                                        objuplaoaduser.Jun = Convert.ToDecimal(Alldata.Rows[i][9].ToString() == "" ? 0 : Alldata.Rows[i][9]);
                                        objuplaoaduser.July = Convert.ToDecimal(Alldata.Rows[i][10].ToString() == "" ? 0 : Alldata.Rows[i][10]);
                                        objuplaoaduser.Aug = Convert.ToDecimal(Alldata.Rows[i][11].ToString() == "" ? 0 : Alldata.Rows[i][11]);
                                        objuplaoaduser.Sep = Convert.ToDecimal(Alldata.Rows[i][12].ToString() == "" ? 0 : Alldata.Rows[i][12]);
                                        objuplaoaduser.Oct = Convert.ToDecimal(Alldata.Rows[i][13].ToString() == "" ? 0 : Alldata.Rows[i][13]);
                                        objuplaoaduser.Nov = Convert.ToDecimal(Alldata.Rows[i][14].ToString() == "" ? 0 : Alldata.Rows[i][14]);
                                        objuplaoaduser.Dec = Convert.ToDecimal(Alldata.Rows[i][15].ToString() == "" ? 0 : Alldata.Rows[i][15]);
                                        objuplaoaduser.Year = System.DateTime.Now.Year.ToString();
                                        _ICommonService.InsertTargate(objuplaoaduser);
                                    }

                                    //  _ICommonService.UpadateUserData(objuplaoaduser);

                                }
                                catch (Exception ex)
                                {
                                    return Json(ex.Message + ex.InnerException, JsonRequestBehavior.AllowGet);
                                    // TempData["Importmsg"] = "Target set fail please check your data";
                                }
                            }


                        }
                        TempData["Importmsg"] = "Import Successfully";
                    }
                    else
                    {
                        TempData["Importmsg"] = "Please Select .xlsx File";
                    }

                }
                else
                {
                    TempData["Importmsg"] = "Please Select .xlsx File";
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["Importmsg"] = "Import Failed";
                return Json(ex.Message + ex.InnerException, JsonRequestBehavior.AllowGet);
            }
            //ViewBag.BarType = BarManagement.BarTypeList(true);

        }


        //[HttpPost]
        //public ActionResult DownloadExcelTarget()
        //{
        //    try
        //    {
        //        HttpPostedFile UserFile = System.Web.HttpContext.Current.Request.Files["File"];
        //        long UserID = Convert.ToInt64(Request.Form["UaserID"]);

        //        if (UserFile != null)
        //        {
        //            string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);

        //            if (fileExtension == ".xlsx")
        //            {
        //                string fileLocation1 = Server.MapPath("~/importfile/") + "importfile.xlsx";
        //                if (System.IO.File.Exists(fileLocation1))
        //                {
        //                    System.IO.File.Delete(fileLocation1);
        //                }

        //                DataTable Alldata = new DataTable();

        //                string filename = "importfile" + fileExtension;
        //                string fileLocation = System.IO.Path.Combine(Server.MapPath("~/importfile"), filename);
        //                // file is uploaded
        //                UserFile.SaveAs(fileLocation);

        //                string excelConnectionString = string.Empty;
        //                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
        //                fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        //                //connection String for xls file format.
        //                //if (fileExtension == ".xls")
        //                //{
        //                //    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
        //                //    fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
        //                //}
        //                //connection String for xlsx file format.
        //                if (fileExtension == ".xlsx")
        //                {
        //                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
        //                    fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        //                }

        //                //Create Connection to Excel work book and add oledb namespace
        //                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
        //                excelConnection.Open();
        //                DataTable dt = new DataTable();

        //                dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //                if (dt == null)
        //                {
        //                    return null;
        //                }

        //                String[] excelSheets = new String[dt.Rows.Count];
        //                int t = 0;
        //                //excel data saves in temp file here.
        //                foreach (DataRow row in dt.Rows)
        //                {
        //                    excelSheets[t] = row["TABLE_NAME"].ToString();
        //                    t++;
        //                }
        //                OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

        //                string query = string.Format("Select * from [{0}]", excelSheets[0]);
        //                using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
        //                {
        //                    dataAdapter.Fill(Alldata);
        //                }
        //                excelConnection.Close();

        //                if (UserID == 1)
        //                {
        //                    //Alldata.Rows.Remove(Alldata.Rows[0]);
        //                    for (int i = 0; i < Alldata.Rows.Count; i++)
        //                    {
        //                        if (!DBNull.Value.Equals(Alldata.Rows[i][0]))
        //                        {

        //                            UploadUserData objuplaoaduser = new UploadUserData();
        //                            objuplaoaduser.DoctorCode = Alldata.Rows[i][0].ToString();
        //                            DateTime ConvertedBirthDate = DateTime.ParseExact(Alldata.Rows[i][1].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //                            objuplaoaduser.BirthDate = ConvertedBirthDate;
        //                            DateTime ConvertedAnniversaryDate = DateTime.ParseExact(Alldata.Rows[i][2].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //                            objuplaoaduser.DOW = ConvertedAnniversaryDate;
        //                            try
        //                            {
        //                                _ICommonService.UpadateUserData(objuplaoaduser);
        //                            }
        //                            catch (Exception ex)
        //                            {

        //                            }
        //                        }
        //                    }

        //                }
        //                else
        //                {
        //                    for (int i = 0; i < Alldata.Rows.Count; i++)
        //                    {
        //                        if (!DBNull.Value.Equals(Alldata.Rows[i][0]))
        //                        {

        //                            UploadRetailerData objuplaoadRetailer = new UploadRetailerData();
        //                            objuplaoadRetailer.FullName = Alldata.Rows[i][0].ToString();
        //                            DateTime ConvertedBirthDate = DateTime.ParseExact(Alldata.Rows[i][1].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //                            objuplaoadRetailer.Dob = ConvertedBirthDate;
        //                            DateTime ConvertedAnniversaryDate = DateTime.ParseExact(Alldata.Rows[i][2].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //                            objuplaoadRetailer.Anniversary = ConvertedAnniversaryDate;
        //                            try
        //                            {
        //                                _ICommonService.UpadateRetailerData(objuplaoadRetailer);
        //                            }
        //                            catch (Exception ex)
        //                            {

        //                            }
        //                        }
        //                    }
        //                }

        //                TempData["Importmsg"] = "Import Successfully";
        //            }
        //            else
        //            {
        //                TempData["Importmsg"] = "Please Select .xlsx File";
        //            }

        //        }
        //        else
        //        {
        //            TempData["Importmsg"] = "Please Select .xlsx File";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Importmsg"] = "Import Failed";
        //    }
        //    //ViewBag.BarType = BarManagement.BarTypeList(true);
        //    return View();

        //}


        public ActionResult DownloadExcelTarget(long Rxtype)
        {
            var ProductList = _ICommonService.GetAllProductallocationList(Rxtype.ToString());

            //DirectoryInfo obj = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath("~/Files"));

            List<EcxelRecord> lstproduct = ProductList.Select(x => new EcxelRecord()
            {
                SrNo = x.SrNo,
                ProductName = x.ProductName,
                MeasureIn = x.StrangthIn + " X " + x.MeasureIn,
                PTS = x.PTS,
                jan = x.jan == null ? "" : x.jan,
                Feb = x.Feb == null ? "" : x.Feb,
                Mar = x.Mar == null ? "" : x.Mar,
                Apr = x.Apr == null ? "" : x.Apr,
                May = x.May == null ? "" : x.May,
                Jun = x.Jun == null ? "" : x.Jun,
                July = x.July == null ? "" : x.July,
                Aug = x.Aug == null ? "" : x.Aug,
                Sep = x.Sep == null ? "" : x.Sep,
                Oct = x.Oct == null ? "" : x.Oct,
                Nov = x.Nov == null ? "" : x.Nov,
                Dec = x.Dec == null ? "" : x.Dec,
            }).ToList();

            DataSet ds = new DataSet();
            ds.Tables.Add(ToDataTable(lstproduct));

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(ds);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                Response.AddHeader("content-disposition", "attachment;filename= " + "Targatesheet.xls");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return View();
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //foreach (PropertyInfo prop in Props)
            //{
            //    //Defining type of data column gives proper data table 
            //    var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
            //    //Setting column names as Property names
            //    dataTable.Columns.Add(prop.Name, type);
            //}
            dataTable.Columns.Add("Sr.No", typeof(int));
            dataTable.Columns.Add("Product Name", typeof(string));
            dataTable.Columns.Add("Pack", typeof(string));
            dataTable.Columns.Add("PTS", typeof(decimal));
            dataTable.Columns.Add("jan-" + System.DateTime.Now.Year.ToString(), typeof(string));
            dataTable.Columns.Add("Feb-" + System.DateTime.Now.Year.ToString(), typeof(string));
            dataTable.Columns.Add("Mar-" + System.DateTime.Now.Year.ToString(), typeof(string));
            dataTable.Columns.Add("Apr-" + System.DateTime.Now.Year.ToString(), typeof(string));
            dataTable.Columns.Add("May-" + System.DateTime.Now.Year.ToString(), typeof(string));
            dataTable.Columns.Add("Jun-" + System.DateTime.Now.Year.ToString(), typeof(string));
            dataTable.Columns.Add("July-" + System.DateTime.Now.Year.ToString(), typeof(string));
            dataTable.Columns.Add("Aug-" + System.DateTime.Now.Year.ToString(), typeof(string));
            dataTable.Columns.Add("Sep-" + System.DateTime.Now.Year.ToString(), typeof(string));
            dataTable.Columns.Add("Oct-" + System.DateTime.Now.Year.ToString(), typeof(string));
            dataTable.Columns.Add("Nov-" + System.DateTime.Now.Year.ToString(), typeof(string));
            dataTable.Columns.Add("Dec-" + System.DateTime.Now.Year.ToString(), typeof(string));

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        #region AccessDenied
        //Do not put session or role authentication
        public ActionResult AccessDenied()
        {
            return View();
        }
        #endregion

        #region Download ExcelSheet
        public ActionResult UserDownloadExcelTarget()
        {

            Retailer_mst dr = new Retailer_mst();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "User Form";
            CreateRetailerColumnDataset(ds);
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(ds);

                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                Response.AddHeader("content-disposition", "attachment;filename= " + "UserTemplate.xls");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            //  return View();
            return null;
        }
        public DataSet CreateRetailerColumnDataset(DataSet ds)
        {

            ds.Tables[0].Columns.Add("FirstName");
            ds.Tables[0].Columns.Add("LastName");
            ds.Tables[0].Columns.Add("MobileNumber");
            ds.Tables[0].Columns.Add("EmailID");
            ds.Tables[0].Columns.Add("Address");
            ds.Tables[0].Columns.Add("City");
            ds.Tables[0].Columns.Add("AreaID");
            ds.Tables[0].Columns.Add("PostCode");
            ds.Tables[0].Columns.Add("DOB");
            ds.Tables[0].Columns.Add("DOW");
            ds.Tables[0].Columns.Add("DOJ");
            ds.Tables[0].Columns.Add("CaseName");
            ds.Tables[0].Columns.Add("DCRBlocking");
            ds.Tables[0].Columns.Add("UserPosition");
            ds.Tables[0].Columns.Add("Password");

            return ds;
        }

        #endregion

        #region ImportExcel
        [HttpPost]
        public JsonResult UserUploadExcelTarget()
        {
            string ReturnMessage = null;
            try
            {
                HttpPostedFile UserFile = System.Web.HttpContext.Current.Request.Files["File"];
                int UserID = Convert.ToInt32(Request.Form["UaserID"]);
                int SuccessCount = 0;
                int FailCount = 0;
                string Message = null;
                long UserId = Convert.ToInt64(Request.Form["UserId"]);
                bool Errorflag = true;
                if (UserFile != null)
                {
                    #region Read ExcelCode
                    string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);

                    if (fileExtension == ".xlsx" || fileExtension == ".xls")
                    {
                        string fileLocation1 = Server.MapPath("~/importfile/") + "importfile.xlsx";
                        if (System.IO.File.Exists(fileLocation1))
                        {
                            System.IO.File.Delete(fileLocation1);
                        }

                        DataTable Alldata = new DataTable();

                        string filename = "importfile" + fileExtension;
                        string fileLocation = System.IO.Path.Combine(Server.MapPath("~/importfile"), filename);
                        // file is uploaded
                        UserFile.SaveAs(fileLocation);

                        string excelConnectionString = string.Empty;
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        //connection String for xls file format.
                        //if (fileExtension == ".xls")
                        //{
                        //    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                        //    fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        //}
                        //connection String for xlsx file format.
                        if (fileExtension == ".xlsx" || fileExtension == ".xls")
                        {
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        }

                        //Create Connection to Excel work book and add oledb namespace
                        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                        excelConnection.Open();
                        DataTable dt = new DataTable();

                        dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        if (dt == null)
                        {
                            return Json(null, JsonRequestBehavior.AllowGet);
                            //return ReturnMessage;
                        }

                        String[] excelSheets = new String[dt.Rows.Count];
                        int t = 0;
                        //excel data saves in temp file here.
                        foreach (DataRow row in dt.Rows)
                        {
                            excelSheets[t] = row["TABLE_NAME"].ToString();
                            t++;
                        }
                        if (excelSheets[0].ToString() != "'User Form$'")
                        {
                            Message = "Invalid Excel Template.";
                            return Json(Message, JsonRequestBehavior.AllowGet);
                        }

                        OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                        string query = string.Format("Select * from [{0}]", excelSheets[0]);
                        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                        {
                            dataAdapter.Fill(Alldata);
                        }
                        excelConnection.Close();
                    #endregion
                        //    long ProductID = _ICommonService.GetProductID(CaseName,);
                        Doctor_Mst dr11 = new Doctor_Mst();
                        DataSet ds = new DataSet();
                        DataTable dt22 = new DataTable();
                        ds.Tables.Add(dt22);
                        CreateRetailerColumnDataset(ds);
                        //Alldata.Rows.Remove(Alldata.Rows[0]);
                        var RoleList = _ICommonService.webpages_Roles_RoleList();
                        for (int i = 0; i < Alldata.Rows.Count; i++)
                        {
                            Errorflag = true;
                            try
                            {
                                User register = new Data.Modal.User();
                                register.EmployeeCode = GetLastEmployeeCode();

                                register.Id = 0;
                                register.FirstName = Alldata.Rows[i]["FirstName"].ToString();
                                register.LastName = Alldata.Rows[i]["LastName"].ToString();
                                register.MobileNumber = Alldata.Rows[i]["MobileNumber"].ToString();
                                register.EmailID = Alldata.Rows[i]["EmailID"].ToString();
                                register.Address = Alldata.Rows[i]["Address"].ToString();

                                register.PostCode = Alldata.Rows[i]["PostCode"].ToString();
                                register.DOB = Convert.ToDateTime(Alldata.Rows[i]["DOB"].ToString());
                                register.DOW = Convert.ToDateTime(Alldata.Rows[i]["DOW"].ToString());
                                register.DOJ = Convert.ToDateTime(Alldata.Rows[i]["DOJ"].ToString());
                                register.UserName = register.EmployeeCode;
                                // Find Area ID
                                string Casenam = Alldata.Rows[i]["CaseName"].ToString();
                                if (Casenam == "Blaze")
                                    register.CaseName = 1;
                                else if (Casenam == "Magnar")
                                    register.CaseName = 2;
                                else
                                    Errorflag = false;

                                string city = Alldata.Rows[i]["City"].ToString();
                                List<Area_Mst> objworktype = _IMasterService.GetAllArealist();
                                long AreaIdd = 0;
                                try
                                {
                                    AreaIdd = objworktype.Where(x => x.AreaName.ToLower() == city.ToLower()).FirstOrDefault().AreaID;
                                    register.City = AreaIdd;
                                }
                                catch (Exception)
                                {
                                    Errorflag = false;
                                }

                                string Area = Alldata.Rows[i]["Area"].ToString();
                                long SubAreaIdd = 0;
                                try
                                {
                                    SubAreaIdd = objworktype.Where(x => x.SubAreaName.ToLower() == Area.ToLower()).FirstOrDefault().AreaID;
                                    register.SubAreaID = SubAreaIdd;
                                }
                                catch (Exception)
                                {
                                    Errorflag = false;
                                    //    register.SubAreaID = "Enter PRoper Subarea Name :" + objRetail.SubArea;
                                }

                                //Role List 
                                string UserPosition = Alldata.Rows[i]["UserPosition"].ToString();
                                long RollIdd = 0;
                                try
                                {
                                    RollIdd = RoleList.Where(x => x.RoleName.ToLower() == UserPosition.ToLower()).FirstOrDefault().RoleId;
                                    register.UserRoleID = RollIdd;
                                    register.UserPosition = RollIdd;
                                }
                                catch (Exception ex)
                                {
                                    Errorflag = false;
                                }
                                register.Password = Alldata.Rows[i]["Password"].ToString();
                                register.DCRBlocking = Convert.ToInt32(Alldata.Rows[i]["DCRBlocking"].ToString());
                                register.CreatedBy = Convert.ToInt32(Session["UserID"]);
                                register.UpdateBy = Convert.ToInt32(Session["UserID"]);
                                register.CreatedOn = DateTime.UtcNow;
                                register.UpdateOn = DateTime.UtcNow;
                                register.IsActive = true;

                                if (Errorflag == false)
                                {
                                    DataRow dr = dt22.NewRow();
                                    register.Id = 0;
                                    dr["FirstName"] = register.FirstName;
                                    dr["LastName"] = register.LastName;
                                    dr["MobileNumber"] = register.MobileNumber;
                                    dr["EmailID"] = register.EmailID;
                                    dr["Address"] = register.Address;
                                    dr["PostCode"] = register.PostCode;
                                    dr["DOB"] = register.DOB;
                                    dr["DOW"] = register.DOW;
                                    dr["DOJ"] = register.DOJ;
                                    dr["CaseName"] = register.CaseName;
                                    dr["DCRBlocking"] = register.DCRBlocking;
                                    dr["City"] = register.City;
                                    dr["AreaID"] = register.AreaID;
                                    dr["UserPosition"] = register.UserPosition;
                                    dr["Password"] = register.Password;
                                    dt22.Rows.Add(dr);
                                    FailCount++;
                                }
                                else
                                {
                                    bool data = _ICommonService.EditUser(register);
                                    SuccessCount++;
                                }
                            }
                            catch (Exception ex)
                            {
                                Message = "Invalid Excel Data.";
                                return Json(Message, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (Errorflag == false)
                        {
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                //wb.Worksheets.Add(ds.Tables[0]);
                                //wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                //wb.Style.Font.Bold = true;

                                //Response.Clear();
                                //Response.Buffer = true;
                                //Response.Charset = "";
                                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                                //Response.AddHeader("content-disposition", "attachment;filename= " + "Targatesheet.xls");

                                //using (MemoryStream MyMemoryStream = new MemoryStream())
                                //{
                                //    wb.SaveAs(MyMemoryStream);
                                //    MyMemoryStream.WriteTo(Response.OutputStream);
                                //    //   File(MyMemoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                                //    Response.Flush();
                                //    Response.End();

                                //}
                            }
                            ReturnMessage = "Error Found";
                            //TempData["Importmsg"] = ds;
                        }
                        else
                        {
                            Message = "Import Successfully";
                            return Json(Message, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        Message = "Please Select .xlsx File";
                        return Json(Message, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    Message = "Please Select .xlsx File";
                    return Json(Message, JsonRequestBehavior.AllowGet);
                }

                Message = "Total Record : " + (SuccessCount + FailCount) + Environment.NewLine + "Suceess Record : " + SuccessCount + Environment.NewLine + "Fail Record : " + FailCount;
                return Json(Message, JsonRequestBehavior.AllowGet);

                //return ReturnMessage;
                // return View();
            }
            catch (Exception ex)
            {
                TempData["Importmsg"] = "Import Failed";
                return Json(ex.Message + ex.InnerException, JsonRequestBehavior.AllowGet);
                //return ReturnMessage;
            }
            //ViewBag.BarType = BarManagement.BarTypeList(true);
        }
        #endregion
    }
}
