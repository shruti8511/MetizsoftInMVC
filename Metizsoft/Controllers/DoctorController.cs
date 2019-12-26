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
    public class DoctorController : Controller
    {
        #region constractor call
        private IDoctor _IDoctorService;
        private ICommon _ICommonService;
        private IMaster _IMasterService;

        public DoctorController(IDoctor IDoctorService, ICommon ICommonService, IMaster IMasterService)
        {
            _IDoctorService = IDoctorService;
            _ICommonService = ICommonService;
            _IMasterService = IMasterService;
        }
        #endregion
        //
        // GET: /Doctor/

        public ActionResult Doctorlist()
        {
            try
            {
                List<Doctor_MstModel> ObjDoctor = _IDoctorService.GetAllDoctorList();
                return View(ObjDoctor);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Doctor Create
        [SessionAuthentication]
        public ActionResult DoctorCreate()
        {
            ViewBag.AreaList = _ICommonService.GetAreaListForDropDown();
            ViewBag.SubAreaList = _ICommonService.GetSubAreaListForDropDown();
            return View();
        }

        [SessionAuthentication]
        [HttpPost]
        public ActionResult DoctorCreate(Doctor_Mst ObjDoctor)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    if (ObjDoctor.DoctorId == 0)
                    {
                        ObjDoctor.DoctorCode = GetLastDoctorCode();
                    }
                    ObjDoctor.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    ObjDoctor.UpdateBy = Convert.ToInt32(Session["UserID"]);
                    ObjDoctor.ApprovedBy = Convert.ToInt32(Session["UserID"]);
                }
                int UserID = Convert.ToInt32(Session["UserID"]);
                int RoleID = _IDoctorService.GetApprovedByRollId(UserID);

                if (RoleID == 7)
                {
                    ObjDoctor.Approved = false;
                }
                else
                {
                    ObjDoctor.Approved = true;
                }

                ObjDoctor.CreatedOn = DateTime.UtcNow;
                ObjDoctor.UpdateOn = DateTime.UtcNow;
                ObjDoctor.IsActive = true;

                bool data = _IDoctorService.AddDoctor(ObjDoctor);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Delete Doctor
        public JsonResult DeleteDoctor(long ID, bool IsActive)
        {
            try
            {
                bool data = _IDoctorService.DeleteDoctor(ID, IsActive);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Edit Doctor
        [HttpPost]
        public JsonResult EditDoctor(int DoctorId)
        {
            Doctor_MstModel objuser = _IDoctorService.GetDoctorByDoctorId(DoctorId);
            return Json(objuser, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Generate doctorCode
        private string GetLastDoctorCode()
        {
            long DocId = _IDoctorService.GetLastDoctorCode();
            string DoctorCode = "";
            if (DocId != 0)
            {
                string code = DocId.ToString();
                //string number = code.Substring(1, 7);
                long incr = Convert.ToInt64(code) + 1;
                DoctorCode = "DOC" + incr.ToString().PadLeft(4, '0');
            }
            else
            {
                DoctorCode = "DOC0001";
            }
            return DoctorCode;
        }
        #endregion

        #region Approve and Reject Doctor by RSM
        [SessionAuthentication]
        public ActionResult ApprovedByRSM()
        {

            List<Doctor_MstModel> ObjDoctor = _IDoctorService.GetAllDoctorListApprovedByRSM(Convert.ToInt32(Session["UserID"]));
            return View(ObjDoctor);
        }

        [HttpPost]
        public ActionResult ApproveDoctorRSM(int DoctorId)
        {
            try
            {
                bool data = _IDoctorService.ApproveDoctorRSM(DoctorId, Convert.ToInt32(Session["UserID"]), 1);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult RejectDoctorRSM(int DoctorId)
        {
            try
            {
                bool data = _IDoctorService.ApproveDoctorRSM(DoctorId, Convert.ToInt32(Session["UserID"]), 0);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region DoctorList AreaWise Filter
        [SessionAuthentication]
        public ActionResult DoctorListAreaWise()
        {
            ViewBag.AreaList = _ICommonService.GetAreaListForDropDown();
            ViewBag.SubAreaList = _ICommonService.GetSubAreaListForDropDown();
            return View();
        }

        public PartialViewResult DoctorlistArea(Doctor_MstModel model)
        {
            try
            {
                List<Doctor_MstModel> objRetail = _IDoctorService.GetAllDoctorListForFilter(model);
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

            Doctor_Mst dr = new Doctor_Mst();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "Doctor Form";
            CreateDoctorColumnDataset(ds);
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(ds);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                Response.AddHeader("content-disposition", "attachment;filename= " + "DoctorTemplate.xls");

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
        public DataSet CreateDoctorColumnDataset(DataSet ds)
        {

            ds.Tables[0].Columns.Add("DoctorName");
            ds.Tables[0].Columns.Add("CertificateId");
            ds.Tables[0].Columns.Add("GradeName");
            ds.Tables[0].Columns.Add("Qualification");
            ds.Tables[0].Columns.Add("Add1");
            ds.Tables[0].Columns.Add("Add2");
            ds.Tables[0].Columns.Add("Add3");
            ds.Tables[0].Columns.Add("Add4");
            ds.Tables[0].Columns.Add("SubAreaName");
            ds.Tables[0].Columns.Add("PhoneNo");
            ds.Tables[0].Columns.Add("RefrenceNo");
            ds.Tables[0].Columns.Add("CategoryName");
            ds.Tables[0].Columns.Add("Pin");
            ds.Tables[0].Columns.Add("Email");
            ds.Tables[0].Columns.Add("FieldStaffName");
            ds.Tables[0].Columns.Add("DivisionName");
            ds.Tables[0].Columns.Add("DoctorClass");

            return ds;
        }

        #endregion

        #region ImportExcel
        [HttpPost]
        public ActionResult UploadExcelTarget()
        {
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
                        if (excelSheets[0].ToString() != "'Doctor Form$'")
                        {
                            Message = "Invalid Excel Template    .";
                            return Json(Message, JsonRequestBehavior.AllowGet);
                        }

                        OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                        string query = string.Format("Select * from [{0}]", excelSheets[0]);
                        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                        {
                            dataAdapter.Fill(Alldata);
                        }
                        excelConnection.Close();

                        //    long ProductID = _ICommonService.GetProductID(CaseName,);
                        Doctor_Mst dr11 = new Doctor_Mst();
                        DataSet ds = new DataSet();
                        DataTable dt22 = new DataTable();
                        ds.Tables.Add(dt22);

                        CreateDoctorColumnDataset(ds);
                        //Alldata.Rows.Remove(Alldata.Rows[0]);
                        for (int i = 0; i < Alldata.Rows.Count; i++)
                        {
                            Errorflag = true;
                            try
                            {
                                Doctor_Mst ObjDoctor = new Doctor_Mst();

                                if (ObjDoctor.DoctorId == 0)
                                {
                                    ObjDoctor.DoctorCode = GetLastDoctorCode();
                                }
                                ObjDoctor.DoctorName = Alldata.Rows[i]["DoctorName"].ToString();
                                ObjDoctor.CertificateId = Alldata.Rows[i]["CertificateId"].ToString();
                                ObjDoctor.GradeName = Convert.ToInt32(Alldata.Rows[i]["GradeName"].ToString());
                                ObjDoctor.Qualification = Alldata.Rows[i]["Qualification"].ToString();
                                ObjDoctor.Add1 = Alldata.Rows[i]["Add1"].ToString();
                                ObjDoctor.Add2 = Alldata.Rows[i]["Add2"].ToString();
                                ObjDoctor.Add3 = Alldata.Rows[i]["Add3"].ToString();
                                ObjDoctor.Add4 = Alldata.Rows[i]["Add4"].ToString();

                                // Find Area ID
                                List<Area_Mst> objworktype = _IMasterService.GetAllArealist();
                                long AreaIdd = 0;
                                try
                                {
                                    AreaIdd = objworktype.Where(x => x.AreaName.ToLower() == ObjDoctor.Add4.ToLower()).FirstOrDefault().AreaID;
                                    ObjDoctor.Add4 = Alldata.Rows[i]["Add4"].ToString(); ;
                                }
                                catch (Exception)
                                {
                                    Errorflag = false;
                                    ObjDoctor.Add4 = "Enter PRoper Address :" + ObjDoctor.Add4;
                                }
                                ObjDoctor.SubAreaName = Alldata.Rows[i]["SubAreaName"].ToString();
                                try
                                {
                                    AreaIdd = objworktype.Where(x => x.SubAreaName.ToLower() == ObjDoctor.SubAreaName.ToLower()).FirstOrDefault().AreaID;
                                    ObjDoctor.SubAreaName = Alldata.Rows[i]["SubAreaName"].ToString();
                                }
                                catch (Exception)
                                {
                                    Errorflag = false;
                                    ObjDoctor.SubAreaName = "Enter PRoper Subarea Name :" + ObjDoctor.SubAreaName;
                                }

                                ObjDoctor.SubAreaName = Alldata.Rows[i]["Add4"].ToString();
                                ObjDoctor.PhoneNo = Alldata.Rows[i]["PhoneNo"].ToString();
                                ObjDoctor.RefrenceNo = Alldata.Rows[i]["RefrenceNo"].ToString();
                                ObjDoctor.CategoryName = Alldata.Rows[i]["CategoryName"].ToString();
                                ObjDoctor.Pin = Alldata.Rows[i]["Pin"].ToString();
                                ObjDoctor.Email = Alldata.Rows[i]["Email"].ToString();
                                ObjDoctor.FieldStaffName = Alldata.Rows[i]["FieldStaffName"].ToString();
                                ObjDoctor.DivisionName = Alldata.Rows[i]["DivisionName"].ToString();
                                ObjDoctor.DoctorClass = Alldata.Rows[i]["DoctorClass"].ToString();
                                ObjDoctor.CreatedBy = Convert.ToInt32(Session["UserID"]);
                                ObjDoctor.UpdateBy = Convert.ToInt32(Session["UserID"]);
                                ObjDoctor.ApprovedBy = Convert.ToInt32(Session["UserID"]);
                                int RoleID = _IDoctorService.GetApprovedByRollId(Convert.ToInt32(Session["UserID"]));

                                if (RoleID == 7)
                                {
                                    ObjDoctor.Approved = false;
                                }
                                else
                                {
                                    ObjDoctor.Approved = true;
                                }

                                ObjDoctor.CreatedOn = DateTime.UtcNow;
                                ObjDoctor.UpdateOn = DateTime.UtcNow;
                                ObjDoctor.IsActive = true;

                                if (Errorflag == false)
                                {

                                    DataRow dr = dt22.NewRow();
                                    dr["DoctorName"] = ObjDoctor.DoctorName + ":" + "Enter Proper ";
                                    dr["CertificateId"] = ObjDoctor.CertificateId;
                                    dr["GradeName"] = ObjDoctor.GradeName;
                                    dr["Qualification"] = ObjDoctor.Qualification;
                                    dr["Add1"] = ObjDoctor.Add1;
                                    dr["Add2"] = ObjDoctor.Add2;
                                    dr["Add3"] = ObjDoctor.Add3;
                                    dr["Add4"] = ObjDoctor.Add4;
                                    dr["SubAreaName"] = ObjDoctor.SubAreaName;
                                    dr["PhoneNo"] = ObjDoctor.PhoneNo;
                                    dr["RefrenceNo"] = ObjDoctor.RefrenceNo;
                                    dr["CategoryName"] = ObjDoctor.CategoryName;
                                    dr["Pin"] = ObjDoctor.Pin;
                                    dr["Email"] = ObjDoctor.Email;
                                    dr["FieldStaffName"] = ObjDoctor.FieldStaffName;
                                    dr["DivisionName"] = ObjDoctor.DivisionName;
                                    dr["FieldStaffName"] = ObjDoctor.FieldStaffName;
                                    dr["DoctorClass"] = ObjDoctor.DoctorClass;
                                    dt22.Rows.Add(dr);
                                    FailCount++;
                                }
                                else
                                {
                                    bool data = _IDoctorService.AddDoctor(ObjDoctor);
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
                                wb.Worksheets.Add(ds.Tables[0]);
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
                                    //   File(MyMemoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                                    Response.Flush();
                                    Response.End();

                                }
                            }
                            //return Json(ds, JsonRequestBehavior.AllowGet);
                            return View();
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

                //return Json(true, JsonRequestBehavior.AllowGet);
                Message = "Total Record : " + (SuccessCount + FailCount) + Environment.NewLine + "Suceess Record : " + SuccessCount + Environment.NewLine + "Fail Record : " + FailCount;
                return Json(Message, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                TempData["Importmsg"] = "Import Failed";
                return Json(ex.Message + ex.InnerException, JsonRequestBehavior.AllowGet);
            }
            //ViewBag.BarType = BarManagement.BarTypeList(true);

        }
        #endregion
    }
}