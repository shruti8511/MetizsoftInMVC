using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Metizsoft.Data;
using Metizsoft.Service;
using Metizsoft.Data.Modal;
using System.Web.Mvc;
using MapYourMeds.App_Start;
using Metizsoft.Service.Models;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using System.Web;
using System.Data.OleDb;

namespace MapYourMeds.Controllers
{
    public class RetailerController : Controller
    {
        private IRetail _IRetailService;
        private ICommon _ICommonService;
        private IMaster _IMasterService;

        public RetailerController(IRetail IRetailService, ICommon ICommonService, IMaster IMasterService)
        {
            _IRetailService = IRetailService;
            _ICommonService = ICommonService;
            _IMasterService = IMasterService;
        }

        public ActionResult Retailerlist()
        {
            try
            {
                List<RetailerMst_mst> objRetail = _IRetailService.GetAllRetailerList();
                return View(objRetail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region retailer Create
        [SessionAuthentication]
        public ActionResult RetailerCreate()
        {
            ViewBag.AreaList = _ICommonService.GetAreaListForDropDown();
            ViewBag.SubAreaList = _ICommonService.GetSubAreaListForDropDown();
            return View();
        }

        [SessionAuthentication]
        [HttpPost]
        public ActionResult RetailerCreate(Retailer_mst objRetail)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    if (objRetail.ReatailerId == 0)
                    {
                        objRetail.RetailerCode = GetLastRetailerCode();
                    }
                    objRetail.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    objRetail.UpdateBy = Convert.ToInt32(Session["UserID"]);
                    objRetail.ApprovedBy = Convert.ToInt32(Session["UserID"]);
                }
                int UserID = Convert.ToInt32(Session["UserID"]);
                int RoleID = _IRetailService.GetApprovedByRollId(UserID);


                if (RoleID == 7)
                {
                    objRetail.Approved = false;
                }
                else
                {
                    objRetail.Approved = true;
                }
                objRetail.CreatedOn = DateTime.UtcNow;
                objRetail.UpdateOn = DateTime.UtcNow;
                objRetail.IsActive = true;

                bool data = _IRetailService.AddRetailer(objRetail);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Delete Retailer
        public JsonResult DeleteRetailer(long ID, bool IsActive)
        {
            try
            {
                bool data = _IRetailService.DeleteRetailer(ID, IsActive);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(false, JsonRequestBehavior.AllowGet);

            }
        }
        #endregion

        #region Edit Retailer
        [HttpPost]
        public JsonResult EditRetailer(int ReatailerId)
        {
            RetailerMst_mst objuser = _IRetailService.GetRetailerByRetailerId(ReatailerId);
            return Json(objuser, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Auto generate code
        private string GetLastRetailerCode()
        {
            long RetId = _IRetailService.GetLastRetailerCode();
            string RetailerCode = "";
            if (RetId != 0)
            {
                string code = RetId.ToString();
                //string number = code.Substring(1, 7);
                long incr = Convert.ToInt64(code) + 1;
                RetailerCode = "RET" + incr.ToString().PadLeft(4, '0');
            }
            else
            {
                RetailerCode = "RET0001";
            }
            return RetailerCode;
        }
        #endregion

        #region Approve and reject retailer by RSM
        [SessionAuthentication]
        public ActionResult UnapprovedByRSM()
        {
            List<RetailerMst_mst> ObjRetailer = _IRetailService.GetAllRetailerListUnapprovedByRSM(Convert.ToInt32(Session["UserID"]));
            return View(ObjRetailer);
        }

        [HttpPost]
        public ActionResult ApproveRetailerRSM(int ReatailerId)
        {
            try
            {
                bool data = _IRetailService.ApproveRetailerRSM(ReatailerId, Convert.ToInt32(Session["UserID"]), 1);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult RejectRetailerRSM(int ReatailerId)
        {
            try
            {
                bool data = _IRetailService.ApproveRetailerRSM(ReatailerId, Convert.ToInt32(Session["UserID"]), 0);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region RetailerList AreaWise
        [SessionAuthentication]
        public ActionResult RetailerListAreaWise()
        {
            ViewBag.AreaList = _ICommonService.GetAreaListForDropDown();
            ViewBag.SubAreaList = _ICommonService.GetSubAreaListForDropDown();
            return View();
        }

        public PartialViewResult RetailerlistArea(RetailerMst_mst model)
        {
            try
            {
                List<RetailerMst_mst> objRetail = _IRetailService.GetAllRetailerListForFilter(model);
                return PartialView(objRetail);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Download ExcelSheet
        public ActionResult DownloadExcelTarget()
        {

            Retailer_mst dr = new Retailer_mst();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "Retailer Form";
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

                Response.AddHeader("content-disposition", "attachment;filename= " + "RetailerTemplate.xls");

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
            ds.Tables[0].Columns.Add("RetailerType");
            ds.Tables[0].Columns.Add("FirstName");
            ds.Tables[0].Columns.Add("LastName");
            ds.Tables[0].Columns.Add("Address");
            ds.Tables[0].Columns.Add("City");
            ds.Tables[0].Columns.Add("SubArea");
            ds.Tables[0].Columns.Add("State");
            ds.Tables[0].Columns.Add("Zip");
            ds.Tables[0].Columns.Add("OfficePhone");
            ds.Tables[0].Columns.Add("Mobile");
            ds.Tables[0].Columns.Add("Email");
            ds.Tables[0].Columns.Add("Fax");
            //ds.Tables[0].Columns.Add("DOB");
            //ds.Tables[0].Columns.Add("Anniversary");
            ds.Tables[0].Columns.Add("GradeName");
            ds.Tables[0].Columns.Add("Refrence");
            ds.Tables[0].Columns.Add("ShortName");
            ds.Tables[0].Columns.Add("ContactName");
            ds.Tables[0].Columns.Add("Division");

            return ds;
        }

        #endregion

        #region ImportExcel
        [HttpPost]
        public JsonResult UploadExcelTarget()
        {
            string ReturnMessage = null;
            try
            {
                HttpPostedFile UserFile = System.Web.HttpContext.Current.Request.Files["File"];
                int UserID = Convert.ToInt32(Request.Form["UaserID"]);
                string Message = null;
                long UserId = Convert.ToInt64(Request.Form["UserId"]);
                bool Errorflag = true;
                int SuccessCount = 0;
                int FailCount = 0;
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
                        if (excelSheets[0].ToString() != "'Retailer Form$'")
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
                        for (int i = 0; i < Alldata.Rows.Count; i++)
                        {
                            Errorflag = true;
                            try
                            {
                                Retailer_mst objRetail = new Retailer_mst();

                                if (objRetail.ReatailerId == 0)
                                {
                                    objRetail.RetailerCode = GetLastRetailerCode();
                                }
                                string USerType = Alldata.Rows[i]["RetailerType"].ToString();
                                if (USerType == "Chemist")
                                    objRetail.Usertype = 1;
                                else if (USerType == "Stockist")
                                    objRetail.Usertype = 2;
                                else
                                    Errorflag = false;

                                objRetail.FirstName = Alldata.Rows[i]["FirstName"].ToString();
                                objRetail.LastName = Alldata.Rows[i]["LastName"].ToString();
                                objRetail.Address = Alldata.Rows[i]["Address"].ToString();
                                objRetail.City = Alldata.Rows[i]["City"].ToString();

                                List<Area_Mst> objworktype = _IMasterService.GetAllArealist();
                                long AreaIdd = 0;
                                try
                                {
                                    AreaIdd = objworktype.Where(x => x.AreaName.ToLower() == objRetail.City.ToLower()).FirstOrDefault().AreaID;
                                    objRetail.City = Alldata.Rows[i]["City"].ToString(); ;
                                }
                                catch (Exception)
                                {
                                    Errorflag = false;
                                    objRetail.City = "Enter PRoper Address :" + objRetail.City;
                                }


                                objRetail.SubArea = Alldata.Rows[i]["SubArea"].ToString();
                                try
                                {
                                    AreaIdd = objworktype.Where(x => x.SubAreaName.ToLower() == objRetail.SubArea.ToLower()).FirstOrDefault().AreaID;
                                    objRetail.SubArea = Alldata.Rows[i]["SubArea"].ToString();
                                }
                                catch (Exception)
                                {
                                    Errorflag = false;
                                    objRetail.SubArea = "Enter PRoper Subarea Name :" + objRetail.SubArea;
                                }
                                objRetail.State = Alldata.Rows[i]["State"].ToString();
                                objRetail.Zip = Alldata.Rows[i]["Zip"].ToString();

                                // Find Area ID
                                objRetail.OfficePhone = Alldata.Rows[i]["OfficePhone"].ToString();
                                objRetail.Mobile = Alldata.Rows[i]["Mobile"].ToString();
                                objRetail.Email = Alldata.Rows[i]["Email"].ToString();
                                objRetail.Fax = Alldata.Rows[i]["Fax"].ToString();
                                objRetail.GradeName = Convert.ToInt32(Alldata.Rows[i]["GradeName"].ToString());
                                objRetail.Refrence = Alldata.Rows[i]["Refrence"].ToString();
                                objRetail.ShortName = Alldata.Rows[i]["ShortName"].ToString();
                                objRetail.ContactName = Alldata.Rows[i]["ContactName"].ToString();

                                objRetail.DivName = Alldata.Rows[i]["Division"].ToString();
                                if (objRetail.DivName == "Blaze")
                                    objRetail.DivName = "1";
                                else if (objRetail.DivName == "Magnar")
                                    objRetail.DivName = "2";
                                else
                                    Errorflag = false;

                                objRetail.CreatedBy = Convert.ToInt32(Session["UserID"]);
                                objRetail.UpdateBy = Convert.ToInt32(Session["UserID"]);
                                objRetail.ApprovedBy = Convert.ToInt32(Session["UserID"]);
                                int RoleID = _IRetailService.GetApprovedByRollId(Convert.ToInt32(Session["UserID"]));

                                if (RoleID == 7)
                                {
                                    objRetail.Approved = false;
                                }
                                else
                                {
                                    objRetail.Approved = true;
                                }

                                objRetail.CreatedOn = DateTime.UtcNow;
                                objRetail.UpdateOn = DateTime.UtcNow;
                                objRetail.IsActive = true;

                                if (Errorflag == false)
                                {

                                    DataRow dr = dt22.NewRow();
                                    dr["RetailerType"] = objRetail.Usertype;
                                    dr["FirstName"] = objRetail.FirstName;
                                    dr["LastName"] = objRetail.LastName;
                                    dr["Address"] = objRetail.Address;
                                    dr["City"] = objRetail.City;
                                    dr["SubArea"] = objRetail.SubArea;
                                    dr["State"] = objRetail.State;
                                    dr["Zip"] = objRetail.Zip;
                                    dr["OfficePhone"] = objRetail.OfficePhone;
                                    dr["Mobile"] = objRetail.Mobile;
                                    dr["Email"] = objRetail.Email;
                                    dr["Fax"] = objRetail.Fax;
                                    dr["GradeName"] = objRetail.GradeName;
                                    dr["Refrence"] = objRetail.Refrence;
                                    dr["ShortName"] = objRetail.ShortName;
                                    dr["ContactName"] = objRetail.ContactName;
                                    dr["Division"] = objRetail.DivName;
                                    dt22.Rows.Add(dr);
                                    FailCount++;

                                }
                                else
                                {
                                    bool data = _IRetailService.AddRetailer(objRetail);
                                    if (data == true)
                                        SuccessCount++;
                                }
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                        }
                        if (Errorflag == false)
                        {
                            //using (XLWorkbook wb = new XLWorkbook())
                            //{
                            //    wb.Worksheets.Add(ds.Tables[0]);
                            //    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            //    wb.Style.Font.Bold = true;

                            //    Response.Clear();
                            //    Response.Buffer = true;
                            //    Response.Charset = "";
                            //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                            //    Response.AddHeader("content-disposition", "attachment;filename= " + "Targatesheet.xls");

                            //    using (MemoryStream MyMemoryStream = new MemoryStream())
                            //    {
                            //        wb.SaveAs(MyMemoryStream);
                            //        MyMemoryStream.WriteTo(Response.OutputStream);
                            //        //   File(MyMemoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                            //        Response.Flush();
                            //        Response.End();

                            //    }
                            //}
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
                Message = "Total Record :" + (SuccessCount + FailCount) + Environment.NewLine + " Suceess Record:" + SuccessCount + Environment.NewLine + "Fail Record :" + FailCount;
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