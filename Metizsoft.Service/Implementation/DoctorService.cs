using Metizsoft.Data;
using Metizsoft.Data.Modal;
using Metizsoft.Data.ViewModal;
using Metizsoft.Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Service.Implementation
{
    public class DoctorService : IDoctor
    {

        public bool AddDoctor(Doctor_Mst ObjDoctor)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                if (ObjDoctor.DoctorId == 0)
                {
                    context.Doctor_Mst.Add(ObjDoctor);
                    context.SaveChanges();
                }
                else
                {
                    context.Entry(ObjDoctor).State = EntityState.Modified;
                    context.SaveChanges();
                }
                if (ObjDoctor.DoctorId > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DeleteDoctor(long DoctorId, bool IsActive)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                var exist = context.Doctor_Mst.FirstOrDefault(x => x.DoctorId == DoctorId && x.IsActive == IsActive);
                if (exist != null)
                {
                    exist.IsActive = false;
                    context.Entry(exist).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public List<DoctorReportViewModel> GetDoctorReport()
        {
            List<DoctorReportViewModel> objects = new List<DoctorReportViewModel>();
            return objects;
        }

        public int GetApprovedByRollId(int UserID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetApprovedByRollId";
            cmdGet.Parameters.AddWithValue("@UserID", UserID);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            int RoleID = 0;
            while (dr.Read())
            {
                RoleID = objBaseSqlManager.GetInt32(dr, "RoleId");
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return RoleID;
        }

        public List<PrecallResponse> GetPreCallAnalysisReport()
        {
            List<PrecallResponse> objects = new List<PrecallResponse>();
            return objects;
        }

        public List<DoctorDetailsResponce> GetDoctorDetailsReport()
        {
            List<DoctorDetailsResponce> objects = new List<DoctorDetailsResponce>();
            return objects;
        }

        public List<DoctorFieldUserResponse> GetDoctorFieldUserReport()
        {
            List<DoctorFieldUserResponse> objects = new List<DoctorFieldUserResponse>();
            return objects;
        }

        public List<DoctorListResponse> GetDoctorListReport()
        {
            List<DoctorListResponse> objects = new List<DoctorListResponse>();
            return objects;
        }

        public List<DoctorProductResponse> GetDoctorProductReport()
        {
            List<DoctorProductResponse> objects = new List<DoctorProductResponse>();
            return objects;
        }

        public List<DoctorRetailerResponse> GetDoctorRetailerReport()
        {
            List<DoctorRetailerResponse> objects = new List<DoctorRetailerResponse>();
            return objects;
        }

        public List<DoctorDCRViewModel> GetDoctorDCRReport()
        {
            List<DoctorDCRViewModel> objects = new List<DoctorDCRViewModel>();
            return objects;
        }

        #region api

        public ProductViewModel1 GetallProductName()
        {
            ProductViewModel1 lstProduct = new ProductViewModel1();
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllRxtype";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<RxTypevalue> lstrxtype = new List<RxTypevalue>();
            while (dr.Read())
            {
                RxTypevalue objrxtyperesponse = new RxTypevalue();
                objrxtyperesponse.RxTypeId = objBaseSqlManager.GetInt32(dr, "RxTypeId");
                objrxtyperesponse.RxType = objBaseSqlManager.GetInt32(dr, "RxType");
                objrxtyperesponse.Name = objBaseSqlManager.GetTextValue(dr, "Name");
                lstrxtype.Add(objrxtyperesponse);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            SqlCommand cmdGetproduct = new SqlCommand();
            BaseSqlManager objBaseSqlManagerproduct = new BaseSqlManager();
            cmdGetproduct.CommandType = CommandType.StoredProcedure;
            cmdGetproduct.CommandText = "GetallProductNames";
            SqlDataReader drproducr = objBaseSqlManagerproduct.ExecuteDataReader(cmdGetproduct);
            List<ProductModel> lstproduct = new List<ProductModel>();
            while (drproducr.Read())
            {
                ProductModel objrxtyperesponse = new ProductModel();
                objrxtyperesponse.ProductCaseId = objBaseSqlManager.GetInt32(drproducr, "ProductCaseId");
                objrxtyperesponse.CaseName = objBaseSqlManager.GetTextValue(drproducr, "CaseName");
                objrxtyperesponse.MeasureIn = objBaseSqlManager.GetTextValue(drproducr, "MeasureIn");
                objrxtyperesponse.Name = objBaseSqlManager.GetTextValue(drproducr, "Name");
                objrxtyperesponse.ProductId = objBaseSqlManager.GetInt32(drproducr, "ProductId");
                objrxtyperesponse.ProductName = objBaseSqlManager.GetTextValue(drproducr, "ProductName");
                objrxtyperesponse.StrangthIn = objBaseSqlManager.GetTextValue(drproducr, "StrangthIn");
                objrxtyperesponse.Pack = objBaseSqlManager.GetTextValue(drproducr, "Pack");
                objrxtyperesponse.PTR = objBaseSqlManager.GetTextValue(drproducr, "PTR");
                objrxtyperesponse.MRP = objBaseSqlManager.GetTextValue(drproducr, "MRP");
                objrxtyperesponse.STDRate = objBaseSqlManager.GetTextValue(drproducr, "STDRate");
                objrxtyperesponse.BrandName = objBaseSqlManager.GetTextValue(drproducr, "BrandName");
                lstproduct.Add(objrxtyperesponse);
            }
            drproducr.Close();
            objBaseSqlManagerproduct.ForceCloseConnection();
            lstProduct.lstRxtype = lstrxtype;
            lstProduct.lstProductModel = lstproduct;
            return lstProduct;
        }

        public DoctorViewModel GetallDoctorName(int UserId, string Area , string SubArea)
        {
            DoctorViewModel objMOdel = new DoctorViewModel();
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetallDoctorName";
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            cmdGet.Parameters.AddWithValue("@Area", Area);
            cmdGet.Parameters.AddWithValue("@SubArea", SubArea);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<DoctorModelList> lstDoctor = new List<DoctorModelList>();
            while (dr.Read())
            {
                DoctorModelList objProductresponse = new DoctorModelList();
                objProductresponse.DoctorId = objBaseSqlManager.GetInt32(dr, "DoctorId");
                objProductresponse.DoctorName = objBaseSqlManager.GetTextValue(dr,"Doctor");
                lstDoctor.Add(objProductresponse);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            objMOdel.lstdoctorview = new List<DoctorModelList>();
            objMOdel.lstdoctorview.AddRange(lstDoctor);

            SqlCommand cmdGetmeeting = new SqlCommand();
            BaseSqlManager objBaseSqlManagermeeting = new BaseSqlManager();
            cmdGetmeeting.CommandType = CommandType.StoredProcedure;
            cmdGetmeeting.CommandText = "GetAllMeetingTypelist";

            SqlDataReader drmetting = objBaseSqlManagermeeting.ExecuteDataReader(cmdGetmeeting);
            List<MeetingTypeModelList> lstMettingType = new List<MeetingTypeModelList>();
            while (drmetting.Read())
            {
                MeetingTypeModelList objProductresponseMeeting = new MeetingTypeModelList();
                objProductresponseMeeting.MeetingTypeId =objBaseSqlManagermeeting.GetInt32(drmetting, "MeetingTypeId");
                objProductresponseMeeting.MeetingType =objBaseSqlManagermeeting.GetTextValue(drmetting, "MeetingType");
                lstMettingType.Add(objProductresponseMeeting);
            }
            drmetting.Close();
            objBaseSqlManagermeeting.ForceCloseConnection();
            objMOdel.lstmeetingview = new List<MeetingTypeModelList>();
            objMOdel.lstmeetingview.AddRange(lstMettingType);
            return objMOdel;
        }

        public bool AddDoctorReport(DoctorReport_Mst objdoctorreportmst, List<DoctorProductReport_Mst> objdoctorreport)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                var data = context.DoctorReport_Mst.FirstOrDefault(x => x.DoctorReportId == objdoctorreportmst.DoctorReportId);
                if (data == null)
                {
                    context.DoctorReport_Mst.Add(objdoctorreportmst);
                    context.SaveChanges();

                    long DoctorReportId = objdoctorreportmst.DoctorReportId;
                    long UserId = Convert.ToInt16(objdoctorreportmst.DoctorId);
                    long Createdby = Convert.ToInt16(objdoctorreportmst.CreatedBy);
                    long Updatedby = Convert.ToInt16(objdoctorreportmst.UpdatedBy);
                    DateTime CreatedOn = Convert.ToDateTime(objdoctorreportmst.CreatedOn);
                    DateTime UpdatedOn = Convert.ToDateTime(objdoctorreportmst.UpdatedOn);                  
                    
                    foreach (var item in objdoctorreport)
                    {
                        item.ReportId = DoctorReportId;
                        item.UserId = UserId;
                        item.GrouptypeID = 1; // 1 Doctor grouptypeId
                        item.CreatedBy = Createdby;
                        item.UpdateBy = Updatedby;
                        item.CreatedOn = CreatedOn;
                        item.UpdateOn = UpdatedOn;
                        context.DoctorProductReport_Mst.Add(item);
                        context.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<ProductListViewModel> GetallProductNameList(int ProductId)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetallProductNameList";
            cmdGet.Parameters.AddWithValue("@ProductId", ProductId);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<ProductListViewModel> lstProduct = new List<ProductListViewModel>();
            while (dr.Read())
            {
                ProductListViewModel objProductresponse = new ProductListViewModel();
                objProductresponse.ProductCaseId = objBaseSqlManager.GetInt32(dr, "ProductCaseId");

                objProductresponse.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objProductresponse.CaseName = objBaseSqlManager.GetTextValue(dr, "CaseName");
                objProductresponse.CaseSize = objBaseSqlManager.GetTextValue(dr, "CaseSize");
                objProductresponse.RatePerNo = objBaseSqlManager.GetTextValue(dr, "RatePerNo");
                objProductresponse.RatePerLTKG = objBaseSqlManager.GetTextValue(dr, "RatePerLTKG");
                objProductresponse.CaseValue = objBaseSqlManager.GetTextValue(dr, "CaseValue");
                objProductresponse.MRPPerNo = objBaseSqlManager.GetTextValue(dr, "MRPPerNo");
                lstProduct.Add(objProductresponse);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProduct;
        }



        //public List<DoctorReportViewModel> GetDoctorReport()
        //{
        //    throw new NotImplementedException();
        //}

        public List<DoctorSummaryReport> DoctorSummaryReport(int CurrentUserId, DateTime? StartDate, DateTime? EndDate)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "DoctorSummaryReport";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@CreatedBy", CurrentUserId);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<DoctorSummaryReport> lstDoctor = new List<DoctorSummaryReport>();
            while (dr.Read())
            {
                DoctorSummaryReport objdoctorsummaryreport = new DoctorSummaryReport();
                objdoctorsummaryreport.DoctorId = objBaseSqlManager.GetInt32(dr, "DoctorId");
                objdoctorsummaryreport.DoctorName = objBaseSqlManager.GetTextValue(dr, "Doctor");
                objdoctorsummaryreport.VisitSection = objBaseSqlManager.GetTextValue(dr, "VisitSection");
                objdoctorsummaryreport.ReportDate = objBaseSqlManager.GetDateTime(dr, "CreatedOn");
                objdoctorsummaryreport.Quantity = objBaseSqlManager.GetTextValue(dr, "Quantity");
                objdoctorsummaryreport.MeetingType = objBaseSqlManager.GetTextValue(dr, "MeetingType");
                objdoctorsummaryreport.AreaName = objBaseSqlManager.GetTextValue(dr, "Add3");
                objdoctorsummaryreport.SubAreaName = objBaseSqlManager.GetTextValue(dr, "Add2");
                objdoctorsummaryreport.WorkWithIds = objBaseSqlManager.GetTextValue(dr, "WorkWithIds");
                objdoctorsummaryreport.WorkWithUserId = objBaseSqlManager.GetTextValue(dr, "WorkWithUserId");
                objdoctorsummaryreport.FullName = objBaseSqlManager.GetTextValue(dr, "FullName");
                objdoctorsummaryreport.ReportDateInDate = Convert.ToDateTime(objdoctorsummaryreport.ReportDate).ToString("dd-MM-yyyy");  
                //objdcrsummaryreport.Date = objBaseSqlManager.GetDateTime(dr, "Date");
                lstDoctor.Add(objdoctorsummaryreport);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            var data = lstDoctor.GroupBy(x => new { x.DoctorId, x.DoctorName, x.VisitSection, x.ReportDate, x.Quantity, x.MeetingType, x.AreaName, x.SubAreaName, x.WorkWithIds, x.WorkWithUserId, x.ReportDateInDate })
                .Select(y => new DoctorSummaryReport() { DoctorId = y.Key.DoctorId, DoctorName = y.Key.DoctorName, VisitSection = y.Key.VisitSection, ReportDate = y.Key.ReportDate, Quantity = y.Key.Quantity, MeetingType = y.Key.MeetingType, AreaName = y.Key.AreaName, SubAreaName = y.Key.SubAreaName, WorkWithIds = y.Key.WorkWithIds, WorkWithUserId = y.Key.WorkWithUserId, ReportDateInDate = y.Key.ReportDateInDate, WorkWithUserName = string.Join(",", y.ToList().Select(z => z.FullName)) });

            return data.ToList();
        }

        public List<RCPA> RCPA(int CurrentUserId, DateTime? StartDate, DateTime? EndDate)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "RCPA";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@CreatedBy", CurrentUserId);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<RCPA> lstRCPA = new List<RCPA>();
            while (dr.Read())
            {
                RCPA objdcrsummaryreport = new RCPA();
                objdcrsummaryreport.ReatailerId = objBaseSqlManager.GetInt32(dr, "ReatailerId");
                objdcrsummaryreport.FullName = objBaseSqlManager.GetTextValue(dr, "FullName");
                objdcrsummaryreport.Quantity = objBaseSqlManager.GetInt64(dr, "Quantity");
                objdcrsummaryreport.CaseName = objBaseSqlManager.GetTextValue(dr, "CaseName");
                objdcrsummaryreport.Name = objBaseSqlManager.GetTextValue(dr, "RxType");
                objdcrsummaryreport.MeasureIn = objBaseSqlManager.GetTextValue(dr, "ValueType");
                objdcrsummaryreport.StrangthIn = objBaseSqlManager.GetTextValue(dr, "WeigtageType");
                objdcrsummaryreport.CreatedOn = objBaseSqlManager.GetDateTime(dr, "CreatedOn");
                objdcrsummaryreport.CreatedOnInDate = Convert.ToDateTime(objdcrsummaryreport.CreatedOn).ToString("dd-MM-yyyy"); 
                lstRCPA.Add(objdcrsummaryreport);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstRCPA;
        }

        public List<PreCallAnalysisReport> PreCallAnalysisReport(int CurrentUserId, DateTime? StartDate, DateTime? EndDate)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "PreCallAnalysisReport";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@CreatedBy", CurrentUserId);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<PreCallAnalysisReport> lstprecall = new List<PreCallAnalysisReport>();
            while (dr.Read())
            {
                PreCallAnalysisReport objdcrsummaryreport = new PreCallAnalysisReport();
                // objdcrsummaryreport.AreaID = objBaseSqlManager.GetInt32(dr, "AreaID");
                objdcrsummaryreport.AreaName = objBaseSqlManager.GetTextValue(dr, "Add2");
                objdcrsummaryreport.DoctorId = objBaseSqlManager.GetInt32(dr, "DoctorId");
                objdcrsummaryreport.DoctorName = objBaseSqlManager.GetTextValue(dr, "DoctorName");
                objdcrsummaryreport.ProductId = objBaseSqlManager.GetInt32(dr, "ProductId");
                objdcrsummaryreport.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objdcrsummaryreport.ReportDate = objBaseSqlManager.GetDateTime(dr, "CreatedOn");
                objdcrsummaryreport.ReportDateInDate = Convert.ToDateTime(objdcrsummaryreport.ReportDate).ToString("dd-MM-yyyy"); 
                lstprecall.Add(objdcrsummaryreport);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstprecall;
        }
        #endregion

        public Doctor_MstModel GetDoctorByDoctorId(int DoctorId)
        {
            Doctor_MstModel objdoctor = new Doctor_MstModel();
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetDoctorByDoctorId";
            cmdGet.Parameters.AddWithValue("@DoctorId", DoctorId);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            while (dr.Read())
            {
                objdoctor.DoctorId = objBaseSqlManager.GetInt32(dr, "DoctorId");
                objdoctor.DoctorCode = objBaseSqlManager.GetTextValue(dr, "DoctorCode");
                objdoctor.DoctorName = objBaseSqlManager.GetTextValue(dr, "DoctorName");
                objdoctor.GradeName = objBaseSqlManager.GetInt32(dr, "GradeName");
                objdoctor.Qualification = objBaseSqlManager.GetTextValue(dr, "Qualification");
                objdoctor.Add1 = objBaseSqlManager.GetTextValue(dr, "Add1");
                objdoctor.Add2 = objBaseSqlManager.GetTextValue(dr, "Add2");
                objdoctor.Add3 = objBaseSqlManager.GetTextValue(dr, "Add3");
                objdoctor.Add4 = objBaseSqlManager.GetTextValue(dr, "Add4");
                objdoctor.Pin = objBaseSqlManager.GetTextValue(dr, "Pin");
                objdoctor.Email = objBaseSqlManager.GetTextValue(dr, "Email");
                objdoctor.PhoneNo = objBaseSqlManager.GetTextValue(dr, "PhoneNo");
                objdoctor.RefrenceNo = objBaseSqlManager.GetTextValue(dr, "RefrenceNo");
                objdoctor.CategoryName = objBaseSqlManager.GetTextValue(dr, "CategoryName");
                objdoctor.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                objdoctor.FieldStaffName = objBaseSqlManager.GetTextValue(dr, "FieldStaffName");
                objdoctor.DivisionName = objBaseSqlManager.GetTextValue(dr, "DivisionName");
                objdoctor.DoctorClass = objBaseSqlManager.GetTextValue(dr, "DoctorClass");
                objdoctor.CertificateId = objBaseSqlManager.GetTextValue(dr, "CertificateId");
                objdoctor.Dobdt = objBaseSqlManager.GetDateTime(dr, "Dob");
                objdoctor.strDob = Convert.ToDateTime(objdoctor.Dobdt).ToString("dd/MM/yyyy");
                objdoctor.Dowdt = objBaseSqlManager.GetDateTime(dr, "Dow");
                objdoctor.strDow = Convert.ToDateTime(objdoctor.Dowdt).ToString("dd/MM/yyyy");

            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();

            return objdoctor;
        }

        public List<Doctor_MstModel> GetAllDoctorListApprovedByRSM(int UserId)
        {

            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllDoctorListApprovedByRSM";
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Doctor_MstModel> lstdoctor = new List<Doctor_MstModel>();
            while (dr.Read())
            {
                Doctor_MstModel objDocotrmst = new Doctor_MstModel();
                objDocotrmst.DoctorId = objBaseSqlManager.GetInt32(dr, "DoctorId");
                objDocotrmst.DoctorCode = objBaseSqlManager.GetTextValue(dr, "DoctorCode");
                objDocotrmst.DoctorName = objBaseSqlManager.GetTextValue(dr, "DoctorName");
                objDocotrmst.GradeName = objBaseSqlManager.GetInt32(dr, "GradeName");
                objDocotrmst.Qualification = objBaseSqlManager.GetTextValue(dr, "Qualification");
                objDocotrmst.Add1 = objBaseSqlManager.GetTextValue(dr, "Add1");
                objDocotrmst.Add2 = objBaseSqlManager.GetTextValue(dr, "Add2");
                objDocotrmst.Add3 = objBaseSqlManager.GetTextValue(dr, "Add3");
                objDocotrmst.Add4 = objBaseSqlManager.GetTextValue(dr, "Add4");
                objDocotrmst.Pin = objBaseSqlManager.GetTextValue(dr, "Pin");
                objDocotrmst.PhoneNo = objBaseSqlManager.GetTextValue(dr, "PhoneNo");
                objDocotrmst.RefrenceNo = objBaseSqlManager.GetTextValue(dr, "RefrenceNo");
                objDocotrmst.CategoryName = objBaseSqlManager.GetTextValue(dr, "CategoryName");
                objDocotrmst.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                objDocotrmst.Dobdt = objBaseSqlManager.GetDateTime(dr, "Dob");
                objDocotrmst.strDob = Convert.ToDateTime(objDocotrmst.Dobdt).ToString("dd/MM/yyyy");
                if (objDocotrmst.Dobdt.ToString("MM/dd/yyyy") == "01/01/0001")
                {
                    objDocotrmst.strDob = " ";
                }
                objDocotrmst.Dowdt = objBaseSqlManager.GetDateTime(dr, "Dow");
                objDocotrmst.strDow = Convert.ToDateTime(objDocotrmst.Dowdt).ToString("dd/MM/yyyy");
                if (objDocotrmst.Dowdt.ToString("MM/dd/yyyy") == "01/01/0001")
                {
                    objDocotrmst.strDow = " ";
                }
                objDocotrmst.Email = objBaseSqlManager.GetTextValue(dr, "Email");
                objDocotrmst.Approved = objBaseSqlManager.GetBoolean(dr, "Approved");
                //objDocotrmst.ApprovedBy = objBaseSqlManager.GetInt32(dr, "ApprovedBy");
                objDocotrmst.FieldStaffName = objBaseSqlManager.GetTextValue(dr, "FieldStaffName");
                objDocotrmst.DivisionName = objBaseSqlManager.GetTextValue(dr, "DivisionName");
                objDocotrmst.DoctorClass = objBaseSqlManager.GetTextValue(dr, "DoctorClass");

                lstdoctor.Add(objDocotrmst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstdoctor;
        }

        public bool ApproveDoctorRSM(int DoctorId, int UserId, int Status)
        {
            try
            {
                SqlCommand cmdGet = new SqlCommand();
                BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                cmdGet.CommandType = CommandType.StoredProcedure;
                cmdGet.CommandText = "ApproveDoctorRSM";
                cmdGet.Parameters.AddWithValue("@DoctorId", DoctorId);
                cmdGet.Parameters.AddWithValue("@UserId", UserId);
                cmdGet.Parameters.AddWithValue("@Status", Status);
                object dr = objBaseSqlManager.ExecuteNonQuery(cmdGet);
                objBaseSqlManager.ForceCloseConnection();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Doctor_MstModel> GetAllDoctorList(long GroupID = 0, long GroupTypeID = 0)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllDoctorAssignmentList";
            cmdGet.Parameters.AddWithValue("@GroupID", GroupID);
            cmdGet.Parameters.AddWithValue("@GroupTypeID", GroupTypeID);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Doctor_MstModel> lstDoctor = new List<Doctor_MstModel>();
            while (dr.Read())
            {
                Doctor_MstModel objDocotrmst = new Doctor_MstModel();
                objDocotrmst.DoctorId = objBaseSqlManager.GetInt32(dr, "DoctorId");
                objDocotrmst.DoctorCode = objBaseSqlManager.GetTextValue(dr, "DoctorCode");
                objDocotrmst.DoctorName = objBaseSqlManager.GetTextValue(dr, "DoctorName");
                objDocotrmst.GradeName = objBaseSqlManager.GetInt32(dr, "GradeName");
                objDocotrmst.Qualification = objBaseSqlManager.GetTextValue(dr, "Qualification");
                objDocotrmst.Add1 = objBaseSqlManager.GetTextValue(dr, "Add1");
                objDocotrmst.Add2 = objBaseSqlManager.GetTextValue(dr, "Add2");
                objDocotrmst.Add3 = objBaseSqlManager.GetTextValue(dr, "Add3");
                objDocotrmst.Add4 = objBaseSqlManager.GetTextValue(dr, "Add4");
                objDocotrmst.Pin = objBaseSqlManager.GetTextValue(dr, "Pin");
                objDocotrmst.PhoneNo = objBaseSqlManager.GetTextValue(dr, "PhoneNo");
                objDocotrmst.RefrenceNo = objBaseSqlManager.GetTextValue(dr, "RefrenceNo");
                objDocotrmst.CategoryName = objBaseSqlManager.GetTextValue(dr, "CategoryName");
                objDocotrmst.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                string strDob = Convert.ToDateTime(objDocotrmst.Dob).ToString("dd/MM/yyyy");
                string strDow = Convert.ToDateTime(objDocotrmst.Dow).ToString("dd/MM/yyyy");
                objDocotrmst.Dob = strDob;
                objDocotrmst.Dow = strDow;
                objDocotrmst.Email = objBaseSqlManager.GetTextValue(dr, "Email");
                objDocotrmst.FieldStaffName = objBaseSqlManager.GetTextValue(dr, "FieldStaffName");
                objDocotrmst.DivisionName = objBaseSqlManager.GetTextValue(dr, "DivisionName");
                objDocotrmst.DoctorClass = objBaseSqlManager.GetTextValue(dr, "DoctorClass");
                lstDoctor.Add(objDocotrmst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstDoctor;
        }

        public List<Doctor_MstModel> GetAllDoctorList()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllDoctorList";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Doctor_MstModel> lstDoctor = new List<Doctor_MstModel>();
            while (dr.Read())
            {
                Doctor_MstModel objDocotrmst = new Doctor_MstModel();
                objDocotrmst.DoctorId = objBaseSqlManager.GetInt32(dr, "DoctorId");
                objDocotrmst.DoctorCode = objBaseSqlManager.GetTextValue(dr, "DoctorCode");
                objDocotrmst.DoctorName = objBaseSqlManager.GetTextValue(dr, "DoctorName");
                objDocotrmst.GradeName = objBaseSqlManager.GetInt32(dr, "GradeName");
                objDocotrmst.Qualification = objBaseSqlManager.GetTextValue(dr, "Qualification");
                objDocotrmst.Add1 = objBaseSqlManager.GetTextValue(dr, "Add1");
                objDocotrmst.Add2 = objBaseSqlManager.GetTextValue(dr, "Add2");
                objDocotrmst.Add3 = objBaseSqlManager.GetTextValue(dr, "Add3");
                objDocotrmst.Add4 = objBaseSqlManager.GetTextValue(dr, "Add4");
                objDocotrmst.Pin = objBaseSqlManager.GetTextValue(dr, "Pin");
                objDocotrmst.PhoneNo = objBaseSqlManager.GetTextValue(dr, "PhoneNo");
                objDocotrmst.RefrenceNo = objBaseSqlManager.GetTextValue(dr, "RefrenceNo");
                objDocotrmst.CategoryName = objBaseSqlManager.GetTextValue(dr, "CategoryName");
                objDocotrmst.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                try
                {
                    objDocotrmst.Dobdt = objBaseSqlManager.GetDateTime(dr, "Dob");
                    objDocotrmst.strDob = Convert.ToDateTime(objDocotrmst.Dobdt).ToString("dd/MM/yyyy");
                    if (objDocotrmst.Dobdt.ToString("MM/dd/yyyy") == "01/01/0001")
                    {
                        objDocotrmst.strDob = " ";
                    }
                    objDocotrmst.Dowdt = objBaseSqlManager.GetDateTime(dr, "Dow");
                    objDocotrmst.strDow = Convert.ToDateTime(objDocotrmst.Dowdt).ToString("dd/MM/yyyy");
                    if (objDocotrmst.Dowdt.ToString("MM/dd/yyyy") == "01/01/0001")
                    {
                        objDocotrmst.strDow = " ";
                    }
                }

                catch
                {
                }
                objDocotrmst.Email = objBaseSqlManager.GetTextValue(dr, "Email");
                objDocotrmst.FieldStaffName = objBaseSqlManager.GetTextValue(dr, "FieldStaffName");
                if (objDocotrmst.FieldStaffName == "1")
                {
                    objDocotrmst.FieldStaffName = "Chemist";
                }
                else if (objDocotrmst.FieldStaffName == "2")
                {
                    objDocotrmst.FieldStaffName = "Stockist";
                }
                else
                {
                    objDocotrmst.FieldStaffName = " ";
                }

                objDocotrmst.DivisionName = objBaseSqlManager.GetTextValue(dr, "DivisionName");
                if (objDocotrmst.DivisionName == "1")
                {
                    objDocotrmst.DivisionName = "Blaze";
                }
                else if (objDocotrmst.DivisionName == "2")
                {
                    objDocotrmst.DivisionName = "Magnar";
                }
                else
                {
                    objDocotrmst.DivisionName = " ";
                }

                objDocotrmst.DoctorClass = objBaseSqlManager.GetTextValue(dr, "DoctorClass");
                objDocotrmst.CertificateId = objBaseSqlManager.GetTextValue(dr, "CertificateId");
                objDocotrmst.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                lstDoctor.Add(objDocotrmst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstDoctor;
        }

        public List<Doctor_MstModel> GetAllDoctorList(long GroupID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllDoctorListForalloction";
            cmdGet.Parameters.AddWithValue("GroupID", GroupID);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Doctor_MstModel> lstDoctor = new List<Doctor_MstModel>();
            while (dr.Read())
            {
                Doctor_MstModel objDocotrmst = new Doctor_MstModel();
                objDocotrmst.DoctorId = objBaseSqlManager.GetInt32(dr, "DoctorId");
                objDocotrmst.DoctorCode = objBaseSqlManager.GetTextValue(dr, "DoctorCode");
                objDocotrmst.DoctorName = objBaseSqlManager.GetTextValue(dr, "DoctorName");
                objDocotrmst.GradeName = objBaseSqlManager.GetInt32(dr, "GradeName");
                objDocotrmst.Qualification = objBaseSqlManager.GetTextValue(dr, "Qualification");
                objDocotrmst.Add1 = objBaseSqlManager.GetTextValue(dr, "Add1");
                objDocotrmst.Add2 = objBaseSqlManager.GetTextValue(dr, "Add2");
                objDocotrmst.Add3 = objBaseSqlManager.GetTextValue(dr, "Add3");
                objDocotrmst.Add4 = objBaseSqlManager.GetTextValue(dr, "Add4");
                objDocotrmst.Pin = objBaseSqlManager.GetTextValue(dr, "Pin");
                objDocotrmst.PhoneNo = objBaseSqlManager.GetTextValue(dr, "PhoneNo");
                objDocotrmst.RefrenceNo = objBaseSqlManager.GetTextValue(dr, "RefrenceNo");
                objDocotrmst.CategoryName = objBaseSqlManager.GetTextValue(dr, "CategoryName");
                objDocotrmst.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                string strDob = Convert.ToDateTime(objDocotrmst.Dob).ToString("dd/MM/yyyy");
                string strDow = Convert.ToDateTime(objDocotrmst.Dow).ToString("dd/MM/yyyy");
                objDocotrmst.Dob = strDob;
                objDocotrmst.Dow = strDow;
                objDocotrmst.Email = objBaseSqlManager.GetTextValue(dr, "Email");
                objDocotrmst.FieldStaffName = objBaseSqlManager.GetTextValue(dr, "FieldStaffName");
                objDocotrmst.DivisionName = objBaseSqlManager.GetTextValue(dr, "DivisionName");
                objDocotrmst.DoctorClass = objBaseSqlManager.GetTextValue(dr, "DoctorClass");
                lstDoctor.Add(objDocotrmst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstDoctor;
        }

        public long GetLastDoctorCode()
        {
            long DocId = 0;
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetLastDoctorCode";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);          
            while (dr.Read())
            {
                DocId = objBaseSqlManager.GetInt64(dr, "DoctorId");
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return DocId;
        }

        public List<Doctor_MstModel> GetAllDoctorListForFilter(Doctor_MstModel model)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllDoctorListForFilter";
            cmdGet.Parameters.AddWithValue("@Add4", model.Add4);
            cmdGet.Parameters.AddWithValue("@SubArea", model.SubAreaName);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Doctor_MstModel> lstDoctor = new List<Doctor_MstModel>();
            while (dr.Read())
            {
                Doctor_MstModel objDocotrmst = new Doctor_MstModel();
                objDocotrmst.DoctorId = objBaseSqlManager.GetInt32(dr, "DoctorId");
                objDocotrmst.DoctorCode = objBaseSqlManager.GetTextValue(dr, "DoctorCode");
                objDocotrmst.DoctorName = objBaseSqlManager.GetTextValue(dr, "DoctorName");
                objDocotrmst.GradeName = objBaseSqlManager.GetInt32(dr, "GradeName");
                objDocotrmst.Qualification = objBaseSqlManager.GetTextValue(dr, "Qualification");
                objDocotrmst.Add1 = objBaseSqlManager.GetTextValue(dr, "Add1");
                objDocotrmst.Add2 = objBaseSqlManager.GetTextValue(dr, "Add2");
                objDocotrmst.Add3 = objBaseSqlManager.GetTextValue(dr, "Add3");
                objDocotrmst.Add4 = objBaseSqlManager.GetTextValue(dr, "Add4");
                objDocotrmst.Pin = objBaseSqlManager.GetTextValue(dr, "Pin");
                objDocotrmst.PhoneNo = objBaseSqlManager.GetTextValue(dr, "PhoneNo");
                objDocotrmst.RefrenceNo = objBaseSqlManager.GetTextValue(dr, "RefrenceNo");
                objDocotrmst.CategoryName = objBaseSqlManager.GetTextValue(dr, "CategoryName");
                objDocotrmst.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                try
                {
                    objDocotrmst.Dobdt = objBaseSqlManager.GetDateTime(dr, "Dob");
                    objDocotrmst.strDob = Convert.ToDateTime(objDocotrmst.Dobdt).ToString("dd/MM/yyyy");
                    if (objDocotrmst.Dobdt.ToString("MM/dd/yyyy") == "01/01/0001")
                    {
                        objDocotrmst.strDob = " ";
                    }
                    objDocotrmst.Dowdt = objBaseSqlManager.GetDateTime(dr, "Dow");
                    objDocotrmst.strDow = Convert.ToDateTime(objDocotrmst.Dowdt).ToString("dd/MM/yyyy");
                    if (objDocotrmst.Dowdt.ToString("MM/dd/yyyy") == "01/01/0001")
                    {
                        objDocotrmst.strDow = " ";
                    }
                }

                catch
                {
                }
                objDocotrmst.Email = objBaseSqlManager.GetTextValue(dr, "Email");
                objDocotrmst.FieldStaffName = objBaseSqlManager.GetTextValue(dr, "FieldStaffName");
                if (objDocotrmst.FieldStaffName == "1")
                {
                    objDocotrmst.FieldStaffName = "Chemist";
                }
                else if (objDocotrmst.FieldStaffName == "2")
                {
                    objDocotrmst.FieldStaffName = "Stockist";
                }
                else
                {
                    objDocotrmst.FieldStaffName = " ";
                }

                objDocotrmst.DivisionName = objBaseSqlManager.GetTextValue(dr, "DivisionName");
                if (objDocotrmst.DivisionName == "1")
                {
                    objDocotrmst.DivisionName = "Blaze";
                }
                else if (objDocotrmst.DivisionName == "2")
                {
                    objDocotrmst.DivisionName = "Magnar";
                }
                else
                {
                    objDocotrmst.DivisionName = " ";
                }

                objDocotrmst.DoctorClass = objBaseSqlManager.GetTextValue(dr, "DoctorClass");
                objDocotrmst.CertificateId = objBaseSqlManager.GetTextValue(dr, "CertificateId");
                objDocotrmst.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                lstDoctor.Add(objDocotrmst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstDoctor;
        }

        public List<MeetingTypeModel> GetAvailableMeeting(int UserId, int DoctorId)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAvailableMeeting";
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            cmdGet.Parameters.AddWithValue("@DoctorId", DoctorId);           
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<MeetingTypeModel> lstmeeting = new List<MeetingTypeModel>();
            while (dr.Read())
            {
                MeetingTypeModel objmeeting = new MeetingTypeModel();
                objmeeting.MeetingTypeId = objBaseSqlManager.GetInt32(dr, "MeetingTypeId");
                objmeeting.MeetingType = objBaseSqlManager.GetTextValue(dr, "MeetingType");
                lstmeeting.Add(objmeeting);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstmeeting;
        }
    }
}
