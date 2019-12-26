namespace Metizsoft.Service
{
    using Metizsoft.Data;
    using Metizsoft.Data.ViewModal;
    using Metizsoft.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ReportService : IReport
    {
        public List<DCRResponse> GetDCRReportList(DateTime? StartDate, DateTime? EndDate, int WorkTypeId, int UserId, int AreaID, int SubAreaId, int WorkWithId)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetDCRReportList";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@WorkTypeId", WorkTypeId);
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            cmdGet.Parameters.AddWithValue("@AreaID", AreaID);
            cmdGet.Parameters.AddWithValue("@SubAreaId", SubAreaId);
            cmdGet.Parameters.AddWithValue("@WorkWithId", WorkWithId);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<DCRResponse> objlst = new List<DCRResponse>();

            while (dr.Read())
            {
                DCRResponse objdcr = new DCRResponse();
                objdcr.DCRId = objBaseSqlManager.GetInt32(dr, "DCRId");
                objdcr.WorkType = objBaseSqlManager.GetTextValue(dr, "WorkType");
                objdcr.Date = objBaseSqlManager.GetDateTime(dr, "SubmissionDate");
                objdcr.CreatedBy = objBaseSqlManager.GetTextValue(dr, "CreatedBy");
                objdcr.RoleName = objBaseSqlManager.GetTextValue(dr, "RoleName");
                objdcr.AreaName = objBaseSqlManager.GetTextValue(dr, "AreaName");
                objdcr.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                int RoleId = objBaseSqlManager.GetInt32(dr, "RoleId");

                var data = objBaseSqlManager.GetTextValue(dr, "WorkWithIds");

                long isexist = data.IndexOf(WorkWithId.ToString());

                if (isexist != WorkWithId && RoleId == WorkWithId)
                {
                    objlst.Add(objdcr);
                }
                else if (WorkWithId == 0)
                {
                    objlst.Add(objdcr);
                }

            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<DoctorResponse> GetDoctorReportList(DateTime? StartDate, DateTime? EndDate, int DoctorId, string DoctorCode, string VisitSection, string CategoryName, string GradeName, string PhoneNo, int CaseName, string AreaName, string SubAreaName)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetDoctorReportList";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@DoctorId", DoctorId);
            cmdGet.Parameters.AddWithValue("@DoctorCode", DoctorCode);
            cmdGet.Parameters.AddWithValue("@VisitSection", VisitSection);
            cmdGet.Parameters.AddWithValue("@CategoryName", CategoryName);
            cmdGet.Parameters.AddWithValue("@GradeName", GradeName);
            cmdGet.Parameters.AddWithValue("@PhoneNo", PhoneNo);
            cmdGet.Parameters.AddWithValue("@CaseName", CaseName);
            cmdGet.Parameters.AddWithValue("@AreaName", AreaName);
            cmdGet.Parameters.AddWithValue("@SubAreaName", SubAreaName);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<DoctorResponse> objlst = new List<DoctorResponse>();

            while (dr.Read())
            {
                DoctorResponse objPayment = new DoctorResponse();
                objPayment.DoctorReportId = objBaseSqlManager.GetInt32(dr, "DoctorReportId");
                objPayment.DoctorCode = objBaseSqlManager.GetTextValue(dr, "DoctorCode");
                objPayment.AreaName = objBaseSqlManager.GetTextValue(dr, "Add4");
                objPayment.DoctorName = objBaseSqlManager.GetTextValue(dr, "Doctor");
                objPayment.SubAreaName = objBaseSqlManager.GetTextValue(dr, "Add3");
                objPayment.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objPayment.CaseName = objBaseSqlManager.GetInt32(dr, "CaseName");
                objPayment.VisitSection = objBaseSqlManager.GetTextValue(dr, "VisitSection");
                objPayment.PhoneNo = objBaseSqlManager.GetTextValue(dr, "PhoneNo");
                objPayment.CertificateId = objBaseSqlManager.GetTextValue(dr, "CertificateId");
                objPayment.Add1 = objBaseSqlManager.GetTextValue(dr, "Add1");
                objPayment.Add2 = objBaseSqlManager.GetTextValue(dr, "Add2");
                objPayment.CategoryName = objBaseSqlManager.GetTextValue(dr, "CategoryName");
                objPayment.GradeName = objBaseSqlManager.GetTextValue(dr, "GradeName");
                objPayment.DivisionName = objBaseSqlManager.GetTextValue(dr, "DivisionName");
                objPayment.Dob = objBaseSqlManager.GetDateTime(dr, "Dob");
                objPayment.DobNull = (objBaseSqlManager.GetDateTime(dr, "Dob")).ToString("MM/dd/yyyy");
                if (objPayment.Dob.ToString("MM/dd/yyyy") == "01/01/0001")
                {
                    objPayment.DobNull = " ";
                }
                objPayment.Dow = objBaseSqlManager.GetDateTime(dr, "Dow");
                objPayment.DowNull = (objBaseSqlManager.GetDateTime(dr, "Dow")).ToString("MM/dd/yyyy");
                if (objPayment.Dow.ToString("MM/dd/yyyy") == "01/01/0001")
                {
                    objPayment.DowNull = " ";
                }
                objPayment.Email = objBaseSqlManager.GetTextValue(dr, "Email");
                objlst.Add(objPayment);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<RetailerResponse> GetRetailerReportList(DateTime? StartDate, DateTime? EndDate, int ReatailerId, string AreaName, string SubAreaName, int UserType, DateTime DOB, DateTime Anniversary, int CaseName, string RetailerCode,string ContactName)
        {
            string DDate = Convert.ToDateTime(DOB).ToString("yyyy/MM/dd");
            string DDate1 = Convert.ToDateTime(Anniversary).ToString("yyyy/MM/dd");
            if (DDate == "0001/01/01")
            {
                DDate = null;
            }
            if (DDate1 == "0001/01/01")
            {
                DDate1 = null;
            }
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetRetailerReportList";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@ReatailerId", ReatailerId);
            cmdGet.Parameters.AddWithValue("@AreaName", AreaName);
            cmdGet.Parameters.AddWithValue("@SubAreaName", SubAreaName);
            cmdGet.Parameters.AddWithValue("@UserType", UserType);
            cmdGet.Parameters.AddWithValue("@DOB", DDate);
            cmdGet.Parameters.AddWithValue("@Anniversary", DDate1);
            cmdGet.Parameters.AddWithValue("@CaseName", CaseName);
            cmdGet.Parameters.AddWithValue("@RetailerCode", RetailerCode);
            cmdGet.Parameters.AddWithValue("@ContactName", ContactName);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<RetailerResponse> objlst = new List<RetailerResponse>();

            while (dr.Read())
            {
                RetailerResponse objPayment = new RetailerResponse();
                objPayment.ReatailerReportId = objBaseSqlManager.GetInt32(dr, "ReatailerReportId");
                objPayment.FullName = objBaseSqlManager.GetTextValue(dr, "FullName");
                objPayment.Address = objBaseSqlManager.GetTextValue(dr, "Address");
                objPayment.AreaName = objBaseSqlManager.GetTextValue(dr, "State");
                objPayment.SubAreaName = objBaseSqlManager.GetTextValue(dr, "City");
                objPayment.Zip = objBaseSqlManager.GetTextValue(dr, "Zip");
                objPayment.Mobile = objBaseSqlManager.GetTextValue(dr, "Mobile");
                objPayment.OfficePhone = objBaseSqlManager.GetTextValue(dr, "OfficePhone");
                objPayment.Email = objBaseSqlManager.GetTextValue(dr, "Email");
                objPayment.DOB = objBaseSqlManager.GetDateTime(dr, "DOB");
                objPayment.DOBNull = (objBaseSqlManager.GetDateTime(dr, "DOB")).ToString("MM/dd/yyyy");
                if (objPayment.DOB.ToString("MM/dd/yyyy") == "01/01/0001")
                {
                    objPayment.DOBNull = " ";
                }
                objPayment.Anniversary = objBaseSqlManager.GetDateTime(dr, "Anniversary");
                objPayment.AnniversaryNull = (objBaseSqlManager.GetDateTime(dr, "Anniversary")).ToString("MM/dd/yyyy");
                if (objPayment.DOB.ToString("MM/dd/yyyy") == "01/01/0001")
                {
                    objPayment.AnniversaryNull = " ";
                }
                objPayment.CaseName = objBaseSqlManager.GetInt32(dr, "CaseName");
                objPayment.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objPayment.RetailerCode = objBaseSqlManager.GetTextValue(dr, "RetailerCode");
                objPayment.ContactName = objBaseSqlManager.GetTextValue(dr, "ContactName");
                objPayment.Refrence = objBaseSqlManager.GetTextValue(dr, "Refrence");
                objPayment.ShortName = objBaseSqlManager.GetTextValue(dr, "ShortName");
                objPayment.GradeName = objBaseSqlManager.GetInt32(dr, "GradeName");
                objPayment.UserType = objBaseSqlManager.GetInt32(dr, "UserType");
                objlst.Add(objPayment);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<PromotionalResponse> GetPromotionalReportList(DateTime? StartDate, DateTime? EndDate, int PromotionalId, int AreaId, int UserId, DateTime Date, int manager, string managerName)
        {
            string DDate = Convert.ToDateTime(Date).ToString("yyyy/MM/dd");
            if (DDate == "0001/01/01")
            {
                DDate = null;

            }
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetPromotionalReportList";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@PromotionalId", PromotionalId);
            cmdGet.Parameters.AddWithValue("@AreaId", AreaId);
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            cmdGet.Parameters.AddWithValue("@Date", DDate);
            cmdGet.Parameters.AddWithValue("@manager", manager);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<PromotionalResponse> objlst = new List<PromotionalResponse>();
            while (dr.Read())
            {
                PromotionalResponse objpromo = new PromotionalResponse();
                objpromo.CreatedBy = objBaseSqlManager.GetTextValue(dr, "UserName");
                objpromo.PromotionalImage = objBaseSqlManager.GetTextValue(dr, "PromotionalImage");
                objpromo.AreaName = objBaseSqlManager.GetTextValue(dr, "AreaName");
                objpromo.PromotionalName = objBaseSqlManager.GetTextValue(dr, "PromotionalName");
                // objPayment.RoleName = objBaseSqlManager.GetTextValue(dr, "RoleName");
                objpromo.Date = objBaseSqlManager.GetDateTime(dr, "Date");
                objpromo.RoleId = objBaseSqlManager.GetInt32(dr, "UserRoleID");
                objpromo.VPID = objBaseSqlManager.GetInt32(dr, "VPID");
                objpromo.NSMID = objBaseSqlManager.GetInt32(dr, "NSMID");
                objpromo.ZSMID = objBaseSqlManager.GetInt32(dr, "ZSMID");
                objpromo.RSMID = objBaseSqlManager.GetInt32(dr, "RSMID");
                objpromo.ASMID = objBaseSqlManager.GetInt32(dr, "ASMID");

                switch (objpromo.RoleId)
                {
                    case 4:
                        if (manager == objpromo.VPID)
                        {
                            objpromo.manager = objBaseSqlManager.GetInt32(dr, "VPID");
                            objpromo.managerName = managerName;
                            //objlst.Add(objpromo);
                        }
                        break;
                    case 5:
                        if (manager == objpromo.NSMID)
                        {
                            objpromo.manager = objBaseSqlManager.GetInt32(dr, "NSMID");
                            objpromo.managerName = managerName;
                            //objlst.Add(objpromo);
                        }
                        break;
                    case 6:
                        if (manager == objpromo.ZSMID)
                        {
                            objpromo.manager = objBaseSqlManager.GetInt32(dr, "ZSMID");
                            objpromo.managerName = managerName;
                           // objlst.Add(objpromo);
                        }
                        break;
                    case 7:
                        if (manager == objpromo.RSMID)
                        {
                            objpromo.manager = objBaseSqlManager.GetInt32(dr, "RSMID");
                            objpromo.managerName = managerName;
                            //objlst.Add(objpromo);
                        }
                        break;
                    case 8:
                        if (manager == objpromo.ASMID)
                        {
                            objpromo.manager = objBaseSqlManager.GetInt32(dr, "ASMID");
                            objpromo.managerName = managerName;
                           // objlst.Add(objpromo);
                        }
                        break;
                }
                objlst.Add(objpromo);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<SalesTargetResponse> GetSalesReportList(DateTime? StartDate, DateTime? EndDate, int Id, string UserName, int ProductId, int CaseName, int AreaID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetSalesReportList";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@Id", Id);
            cmdGet.Parameters.AddWithValue("@ProductId", ProductId);
            cmdGet.Parameters.AddWithValue("@CaseName", CaseName);
            cmdGet.Parameters.AddWithValue("@AreaID", AreaID);          

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<SalesTargetResponse> objlst = new List<SalesTargetResponse>();

            while (dr.Read())
            {
                SalesTargetResponse objsales = new SalesTargetResponse();
                objsales.Id = objBaseSqlManager.GetInt32(dr, "Id");
                objsales.DirectorID = objBaseSqlManager.GetInt32(dr, "DirectorID");
                objsales.VPID = objBaseSqlManager.GetInt32(dr, "VPID");
                objsales.NSMID = objBaseSqlManager.GetInt32(dr, "NSMID");
                objsales.RSMID = objBaseSqlManager.GetInt32(dr, "RSMID");
                objsales.ZSMID = objBaseSqlManager.GetInt32(dr, "ZSMID");
                objsales.ASMID = objBaseSqlManager.GetInt32(dr, "ASMID");
                objsales.MRID = objBaseSqlManager.GetInt32(dr, "MRID");
                objsales.UserName = UserName;
                objsales.ProductId = objBaseSqlManager.GetInt32(dr, "ProductId");
                objsales.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objsales.CaseName = objBaseSqlManager.GetInt32(dr, "CaseName");
                objsales.AreaID = objBaseSqlManager.GetInt32(dr, "AreaID");
                objsales.AreaName = objBaseSqlManager.GetTextValue(dr, "AreaName");
                objsales.Jan = objBaseSqlManager.GetInt64(dr, "Jan");
                objsales.Feb = objBaseSqlManager.GetInt64(dr, "Feb");
                objsales.Mar = objBaseSqlManager.GetInt64(dr, "Mar");
                objsales.Apr = objBaseSqlManager.GetInt64(dr, "Apr");
                objsales.May = objBaseSqlManager.GetInt64(dr, "May");
                objsales.Jun = objBaseSqlManager.GetInt64(dr, "Jun");
                objsales.July = objBaseSqlManager.GetInt64(dr, "July");
                objsales.Aug = objBaseSqlManager.GetInt64(dr, "Aug");
                objsales.Sep = objBaseSqlManager.GetInt64(dr, "Sep");
                objsales.Oct = objBaseSqlManager.GetInt64(dr, "Oct");
                objsales.Nov = objBaseSqlManager.GetInt64(dr, "Nov");
                objsales.Dec = objBaseSqlManager.GetInt64(dr, "Dec");
                objsales.Year = objBaseSqlManager.GetTextValue(dr, "Year");                     
                objlst.Add(objsales);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<Register> GetVPByDirectorReport(long DirectorID, long RoleID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetVPByDirectorReport";
            cmdGet.Parameters.AddWithValue("@DirectorID", DirectorID);
            cmdGet.Parameters.AddWithValue("@RoleID", RoleID);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Register> objlstDealer = new List<Register>();
            while (dr.Read())
            {
                Register objUser = new Register();
                objUser.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                objUser.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");

                objlstDealer.Add(objUser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlstDealer;
        }

        public List<Register> GetNSMByVPIDReport(long DirectorID, long VPID, long RoleID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetNSMByVPIDReport";
            cmdGet.Parameters.AddWithValue("@DirectorID", DirectorID);           
            cmdGet.Parameters.AddWithValue("@VPID", VPID);
            cmdGet.Parameters.AddWithValue("@RoleID", RoleID);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Register> objlstDealer = new List<Register>();
            while (dr.Read())
            {
                Register objUser = new Register();
                objUser.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                objUser.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");

                objlstDealer.Add(objUser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlstDealer;
        }

        public List<Register> GetZSMByNSMIDReport(long DirectorID, long VPID, long NSMID, long RoleID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetZSMByNSMIDReport";
            cmdGet.Parameters.AddWithValue("@DirectorID", DirectorID);           
            cmdGet.Parameters.AddWithValue("@VPID", VPID);
            cmdGet.Parameters.AddWithValue("@NSMID", NSMID);
            cmdGet.Parameters.AddWithValue("@RoleID", RoleID);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Register> objlstDealer = new List<Register>();
            while (dr.Read())
            {
                Register objUser = new Register();
                objUser.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                objUser.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");

                objlstDealer.Add(objUser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlstDealer;
        }

        public List<Register> GetRSMByZSMIDReport(long DirectorID, long VPID, long NSMID, long ZSMID, long RoleID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetRSMByZSMIDReport";
            cmdGet.Parameters.AddWithValue("@DirectorID", DirectorID);
            cmdGet.Parameters.AddWithValue("@VPID", VPID);
            cmdGet.Parameters.AddWithValue("@NSMID", NSMID);
            cmdGet.Parameters.AddWithValue("@ZSMID", ZSMID);
            cmdGet.Parameters.AddWithValue("@RoleID", RoleID);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Register> objlstDealer = new List<Register>();
            while (dr.Read())
            {
                Register objUser = new Register();
                objUser.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                objUser.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");

                objlstDealer.Add(objUser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlstDealer;
        }

        public List<Register> GetASMByRSMIDReport(long DirectorID, long VPID, long NSMID, long ZSMID, long RSMID, long RoleID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetASMByRSMIDReport";
            cmdGet.Parameters.AddWithValue("@DirectorID", DirectorID);
            cmdGet.Parameters.AddWithValue("@VPID", VPID);
            cmdGet.Parameters.AddWithValue("@NSMID", NSMID);
            cmdGet.Parameters.AddWithValue("@ZSMID", ZSMID);
            cmdGet.Parameters.AddWithValue("@RSMID", RSMID);
            cmdGet.Parameters.AddWithValue("@RoleID", RoleID);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Register> objlstDealer = new List<Register>();
            while (dr.Read())
            {
                Register objUser = new Register();
                objUser.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                objUser.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");

                objlstDealer.Add(objUser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlstDealer;
        }

        public List<Register> GetMRByASMIDReport(long DirectorID, long VPID, long NSMID, long ZSMID, long RSMID, long ASMID, long RoleID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetMRByASMIDReport";
            cmdGet.Parameters.AddWithValue("@DirectorID", DirectorID);
            cmdGet.Parameters.AddWithValue("@VPID", VPID);
            cmdGet.Parameters.AddWithValue("@NSMID", NSMID);
            cmdGet.Parameters.AddWithValue("@ZSMID", ZSMID);
            cmdGet.Parameters.AddWithValue("@RSMID", RSMID);
            cmdGet.Parameters.AddWithValue("@ASMID", ASMID);
            cmdGet.Parameters.AddWithValue("@RoleID", RoleID);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Register> objlstDealer = new List<Register>();
            while (dr.Read())
            {
                Register objUser = new Register();
                objUser.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                objUser.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");

                objlstDealer.Add(objUser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlstDealer;
        }

        public List<RCPAResponse> GetRCPAReportList(DateTime? StartDate, DateTime? EndDate, int ReatailerId, int ProductId, int CaseName, int NameID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();

            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "Report_RCPA";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@ReatailerId", ReatailerId);
            //cmdGet.Parameters.AddWithValue("@DoctorId", DoctorId);
            cmdGet.Parameters.AddWithValue("@ProductId", ProductId);
            cmdGet.Parameters.AddWithValue("@CaseName", CaseName);
            cmdGet.Parameters.AddWithValue("@Name", NameID);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<RCPAResponse> objlst = new List<RCPAResponse>();
            while (dr.Read())
            {
                RCPAResponse objrcpa = new RCPAResponse();
                //objrcpa.DoctorName = objBaseSqlManager.GetTextValue(dr, "DoctorName");
                objrcpa.ReatailerReportId = objBaseSqlManager.GetInt32(dr, "ReatailerReportId");
                objrcpa.FullName = objBaseSqlManager.GetTextValue(dr, "FullName");
                objrcpa.Quantity = objBaseSqlManager.GetTextValue(dr, "Quantity");
                objrcpa.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objrcpa.CaseName = objBaseSqlManager.GetInt32(dr, "CaseName");
                objrcpa.Name = objBaseSqlManager.GetTextValue(dr, "Name");
                objrcpa.Date = objBaseSqlManager.GetDateTime(dr, "CreatedOn");
                //objrcpa.Medicine = objBaseSqlManager.GetTextValue(dr, "Medicine");
                //objrcpa.VisitSection = objBaseSqlManager.GetTextValue(dr, "VisitSection");             

                objlst.Add(objrcpa);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        //public SalesTargetResponse GetUserDetailsForsale(string UserId)
        //{
        //    SqlCommand cmdGet = new SqlCommand();
        //    BaseSqlManager objBaseSqlManager = new BaseSqlManager();
        //    cmdGet.CommandType = CommandType.StoredProcedure;
        //    cmdGet.CommandText = "GetUserDetails";
        //    cmdGet.Parameters.AddWithValue("@UserId", UserId);
        //    SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
        //    SalesTargetResponse obj = new SalesTargetResponse();

        //    while (dr.Read())
        //    {

        //        obj.UserID = objBaseSqlManager.GetInt32(dr, "Id");
        //        obj.UserName = objBaseSqlManager.GetTextValue(dr, "UserName");
        //        obj.UserEmail = objBaseSqlManager.GetTextValue(dr, "EmailID");
        //        obj.RoleId = objBaseSqlManager.GetInt32(dr, "UserRoleID");
        //        obj.DirectorID = objBaseSqlManager.GetInt32(dr, "DirectorID");
        //    }
        //    dr.Close();
        //    objBaseSqlManager.ForceCloseConnection();
        //    return obj;
        //}

        public List<TourPlanResponse> TourPlanVarianceHistory(DateTime? StartDate, DateTime? EndDate, int UserId, int AreaId, int SubAreaId, DateTime Date)
        {
            string DDate = Convert.ToDateTime(Date).ToString("yyyy/MM/dd");
            if (DDate == "0001/01/01")
            {
                DDate = null;
            }
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "Report_TourPlanVarianceHistory";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            cmdGet.Parameters.AddWithValue("@AreaId", AreaId);
            cmdGet.Parameters.AddWithValue("@SubAreaId", SubAreaId);
            cmdGet.Parameters.AddWithValue("@Date", DDate);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<TourPlanResponse> objlst = new List<TourPlanResponse>();

            while (dr.Read())
            {
                TourPlanResponse objtourplan = new TourPlanResponse();
                objtourplan.DCRId = objBaseSqlManager.GetInt32(dr, "DCRId");
                objtourplan.UserName = objBaseSqlManager.GetTextValue(dr, "UserName");
                objtourplan.TourDate = objBaseSqlManager.GetDateTime(dr, "Date");
                objtourplan.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                objtourplan.AreaName = objBaseSqlManager.GetTextValue(dr, "AreaName");
                objtourplan.VarianceArea = objBaseSqlManager.GetTextValue(dr, "varianceAreaName");
                objtourplan.VarianceSubArea = objBaseSqlManager.GetTextValue(dr, "varianceSubAreaName");
                objtourplan.Reason = objBaseSqlManager.GetTextValue(dr, "TourPlanVariation");
                objtourplan.HQRS = objBaseSqlManager.GetTextValue(dr, "HQRS");
                //objtourplan.IsApproved = objBaseSqlManager.GetTextValue(dr, "IsApproved");
                objlst.Add(objtourplan);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<PrecallResponse> GetPreCallAnalysisReportList(DateTime? StartDate, DateTime? EndDate, int SelectType)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            if (SelectType == 1)
            {
                cmdGet.CommandType = CommandType.StoredProcedure;
                cmdGet.CommandText = "Report_PreCallDoctorAnalysisReport";
                cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
                cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
                //cmdGet.Parameters.AddWithValue("@DoctorId", SelectType);
            }
            else
            {
                cmdGet.CommandType = CommandType.StoredProcedure;
                cmdGet.CommandText = "Report_PreCallRetailerAnalysisReport";
                cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
                cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
                //cmdGet.Parameters.AddWithValue("@ReatailerId", SelectType);
            }

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<PrecallResponse> objlst = new List<PrecallResponse>();

            while (dr.Read())
            {
                PrecallResponse objprecall = new PrecallResponse();
                if (SelectType == 1)
                {
                    objprecall.DoctorId = objBaseSqlManager.GetInt32(dr, "DoctorId");
                    objprecall.DoctorName = objBaseSqlManager.GetTextValue(dr, "DoctorName");
                    objprecall.AreaNameDoctor = objBaseSqlManager.GetTextValue(dr, "Add4");
                    objprecall.SubAreaNameDoctor = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                }
                else
                {
                    objprecall.ReatailerId = objBaseSqlManager.GetInt32(dr, "ReatailerId");
                    objprecall.Usertype = objBaseSqlManager.GetTextValue(dr, "Usertype");
                    objprecall.FirstName = objBaseSqlManager.GetTextValue(dr, "FirstName");
                    objprecall.AreaName = objBaseSqlManager.GetTextValue(dr, "State");
                    objprecall.SubAreaName = objBaseSqlManager.GetTextValue(dr, "Address");
                }
                objlst.Add(objprecall);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<DoctorDetailsResponce> GetDoctorDetailsReportList()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "Report_DoctorDetail";
            //cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            //cmdGet.Parameters.AddWithValue("@EndDate", EndDate);        

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<DoctorDetailsResponce> objlst = new List<DoctorDetailsResponce>();

            while (dr.Read())
            {
                DoctorDetailsResponce objPayment = new DoctorDetailsResponce();
                objPayment.DoctorId = objBaseSqlManager.GetInt32(dr, "DoctorId");
                objPayment.DoctorName = objBaseSqlManager.GetTextValue(dr, "Doctor");
                objPayment.DoctorCode = objBaseSqlManager.GetTextValue(dr, "DoctorCode");
                objPayment.GradeName = objBaseSqlManager.GetInt32(dr, "GradeName");
                objPayment.CategoryName = objBaseSqlManager.GetTextValue(dr, "CategoryName");
                objPayment.PhoneNo = objBaseSqlManager.GetTextValue(dr, "PhoneNo");
                objPayment.Add1 = objBaseSqlManager.GetTextValue(dr, "Add1");
                objPayment.Add2 = objBaseSqlManager.GetTextValue(dr, "Add2");
                objPayment.Add3 = objBaseSqlManager.GetTextValue(dr, "Add3");
                objPayment.Add4 = objBaseSqlManager.GetTextValue(dr, "Add4");
                objlst.Add(objPayment);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<ExpenditureResponse> GetExpenditureReportList(DateTime? StartDate, DateTime? EndDate, int UserId, int ExpenserId, long Amount)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "Report_Expenser";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            cmdGet.Parameters.AddWithValue("@ExpenserId", ExpenserId);
            cmdGet.Parameters.AddWithValue("@Amount", Amount);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<ExpenditureResponse> objlst = new List<ExpenditureResponse>();

            while (dr.Read())
            {
                ExpenditureResponse objexpe = new ExpenditureResponse();
                objexpe.UserName = objBaseSqlManager.GetTextValue(dr, "UserName");
                objexpe.ExpenserName = objBaseSqlManager.GetTextValue(dr, "ExpenserName");
                objexpe.Amount = objBaseSqlManager.GetInt64(dr, "Amount");
                objexpe.ExpenserImage = objBaseSqlManager.GetTextValue(dr, "ExpenserImage");
                objexpe.Date = objBaseSqlManager.GetDateTime(dr, "Date");

                //objexpe.Reason = objBaseSqlManager.GetTextValue(dr, "Reason");
                objlst.Add(objexpe);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<LeaveResponse> GetLeaveReportList(DateTime? StartDate, DateTime? EndDate, int UserId, int LeaveId, string Reason, int DaysOfLeave, int manager ,string managerName)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "Report_Leave";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            cmdGet.Parameters.AddWithValue("@LeaveId", LeaveId);
            cmdGet.Parameters.AddWithValue("@DaysOfLeave", DaysOfLeave);
            cmdGet.Parameters.AddWithValue("@Reason", Reason);
            cmdGet.Parameters.AddWithValue("@manager", manager);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<LeaveResponse> objlst = new List<LeaveResponse>();

            while (dr.Read())
            {
                LeaveResponse objleave = new LeaveResponse();
                objleave.UserName = objBaseSqlManager.GetTextValue(dr, "UserName");
                objleave.DateFrom = objBaseSqlManager.GetDateTime(dr, "DateFrom");
                objleave.DateTo = objBaseSqlManager.GetDateTime(dr, "DateTo");
                objleave.DaysOfLeave = objBaseSqlManager.GetInt32(dr, "LeaveDays");
                objleave.Reason = objBaseSqlManager.GetTextValue(dr, "Reason");
                //objleave.LeaveStatus = objBaseSqlManager.GetTextValue(dr, "LeaveStatus");
                objleave.LeaveType = objBaseSqlManager.GetTextValue(dr, "LeaveType");
                //objleave.manager = objBaseSqlManager.GetInt32(dr, "manager");     
                objleave.RoleId = objBaseSqlManager.GetInt32(dr, "UserRoleID");
                objleave.VPID = objBaseSqlManager.GetInt32(dr, "VPID");
                objleave.NSMID = objBaseSqlManager.GetInt32(dr, "NSMID");
                objleave.ZSMID = objBaseSqlManager.GetInt32(dr, "ZSMID");
                objleave.RSMID = objBaseSqlManager.GetInt32(dr, "RSMID");
                objleave.ASMID = objBaseSqlManager.GetInt32(dr, "ASMID");
              
                switch (objleave.RoleId)
                {
                    case 4:
                        if (manager == objleave.VPID)
                        {
                            objleave.manager = objBaseSqlManager.GetInt32(dr, "VPID");
                            objleave.managerName = managerName;
                            //objlst.Add(objleave);
                        }
                        break;
                    case 5:
                        if (manager == objleave.NSMID)
                        {
                            objleave.manager = objBaseSqlManager.GetInt32(dr, "NSMID");
                            objleave.managerName = managerName;
                            //objlst.Add(objleave);
                        }
                        break;
                    case 6:
                        if (manager == objleave.ZSMID)
                        {
                            objleave.manager = objBaseSqlManager.GetInt32(dr, "ZSMID");
                            objleave.managerName = managerName;
                            //objlst.Add(objleave);
                        }
                        break;
                    case 7:
                        if (manager == objleave.RSMID)
                        {
                            objleave.manager = objBaseSqlManager.GetInt32(dr, "RSMID");
                            objleave.managerName = managerName;
                           // objlst.Add(objleave);
                        }
                        break;
                    case 8:
                        if (manager == objleave.ASMID)
                        {
                            objleave.manager = objBaseSqlManager.GetInt32(dr, "ASMID");
                            objleave.managerName = managerName;
                          // objlst.Add(objleave);
                        }
                        break;
                }
               objlst.Add(objleave);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<ProductListResponse> GetProductReportList(DateTime? StartDate, DateTime? EndDate, int ProductId, string StrangthIn, string MeasureIn, string ProductCode, string PTS, int CaseName, int NameID, string MRP, string STDRate, string BrandName, int ProductTypeId)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetProductReportList";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@ProductId", ProductId);
            cmdGet.Parameters.AddWithValue("@StrangthIn", StrangthIn);
            cmdGet.Parameters.AddWithValue("@MeasureIn", MeasureIn);
            cmdGet.Parameters.AddWithValue("@ProductCode", ProductCode);
            cmdGet.Parameters.AddWithValue("@ProductTypeId", ProductTypeId);
            cmdGet.Parameters.AddWithValue("@PTS", PTS);
            cmdGet.Parameters.AddWithValue("@CaseName", CaseName);
            cmdGet.Parameters.AddWithValue("@NameID", NameID);
            cmdGet.Parameters.AddWithValue("@MRP", MRP);
            cmdGet.Parameters.AddWithValue("@STDRate", STDRate);
            cmdGet.Parameters.AddWithValue("@BrandName", BrandName);           
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<ProductListResponse> objlst = new List<ProductListResponse>();

            while (dr.Read())
            {
                ProductListResponse objproduct = new ProductListResponse();
                objproduct.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objproduct.ProductCode = objBaseSqlManager.GetTextValue(dr, "ProductCode");
                objproduct.CaseName = objBaseSqlManager.GetInt32(dr, "CaseName");
                objproduct.Name = objBaseSqlManager.GetTextValue(dr, "Name");
                objproduct.ProductType = objBaseSqlManager.GetTextValue(dr, "ProductType");
                objproduct.MeasureIn = objBaseSqlManager.GetTextValue(dr, "MeasureIn");
                objproduct.StrangthIn = objBaseSqlManager.GetTextValue(dr, "StrangthIn");
                objproduct.PTS = objBaseSqlManager.GetTextValue(dr, "PTS");
                objproduct.ProductType = objBaseSqlManager.GetTextValue(dr, "ProductType");
                objproduct.Pack = objBaseSqlManager.GetTextValue(dr, "Pack");
                objproduct.PTR = objBaseSqlManager.GetTextValue(dr, "PTR");
                objproduct.MRP = objBaseSqlManager.GetTextValue(dr, "MRP");
                objproduct.STDRate = objBaseSqlManager.GetTextValue(dr, "STDRate");
                objproduct.BrandName = objBaseSqlManager.GetTextValue(dr, "BrandName");                
                objlst.Add(objproduct);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<DoctorFieldUserResponse> GetDoctorFieldUserReportList(DateTime? StartDate, DateTime? EndDate, string DoctorCode, int ProductId, int UserId, string CategoryName, int GradeName, int CaseName, int DoctorId, int ProductTypeId, string AreaName, string SubAreaName)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetDoctorFieldUserReportList";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@DoctorCode", DoctorCode);
            cmdGet.Parameters.AddWithValue("@ProductId", ProductId);
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            cmdGet.Parameters.AddWithValue("@CategoryName", CategoryName);
            cmdGet.Parameters.AddWithValue("@GradeName", GradeName);
            cmdGet.Parameters.AddWithValue("@CaseName", CaseName);
            cmdGet.Parameters.AddWithValue("@DoctorId", DoctorId);
            cmdGet.Parameters.AddWithValue("@ProductTypeId", ProductTypeId);
            cmdGet.Parameters.AddWithValue("@AreaName", AreaName);
            cmdGet.Parameters.AddWithValue("@SubAreaName", SubAreaName);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<DoctorFieldUserResponse> objlst = new List<DoctorFieldUserResponse>();

            while (dr.Read())
            {
                DoctorFieldUserResponse objdoctor = new DoctorFieldUserResponse();
                objdoctor.DoctorReportId = objBaseSqlManager.GetInt32(dr, "DoctorReportId");
                objdoctor.DoctorCode = objBaseSqlManager.GetTextValue(dr, "DoctorCode");
                objdoctor.DoctorName = objBaseSqlManager.GetTextValue(dr, "Doctor");
                objdoctor.GradeName = objBaseSqlManager.GetInt32(dr, "GradeName");
                objdoctor.CreatedBy = objBaseSqlManager.GetTextValue(dr, "UserName");
                objdoctor.AreaName = objBaseSqlManager.GetTextValue(dr, "Add4");
                objdoctor.SubAreaName = objBaseSqlManager.GetTextValue(dr, "Add3");
                objdoctor.CategoryName = objBaseSqlManager.GetTextValue(dr, "CategoryName");
                objdoctor.VisitSection = objBaseSqlManager.GetTextValue(dr, "VisitSection");
                objdoctor.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objdoctor.ProductType = objBaseSqlManager.GetTextValue(dr, "ProductType");
                objdoctor.CaseName = objBaseSqlManager.GetInt32(dr, "CaseName");
                objdoctor.Name = objBaseSqlManager.GetTextValue(dr, "Name");
                objdoctor.MeasureIn = objBaseSqlManager.GetTextValue(dr, "ValueType");
                objdoctor.StrangthIn = objBaseSqlManager.GetTextValue(dr, "WeigtageType");


                objlst.Add(objdoctor);
            }
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<DoctorListResponse> GetDoctorListReportList(DateTime? StartDate, DateTime? EndDate, int GroupID, DateTime Dob, DateTime Dow, int CaseName, string AreaName, string SubAreaName, string GroupCode)
        {
            string DDate = Convert.ToDateTime(Dob).ToString("yyyy/MM/dd");
            string DDate1 = Convert.ToDateTime(Dow).ToString("yyyy/MM/dd");
            if (DDate == "0001/01/01")
            {
                DDate = null;
            } 
            if (DDate1 == "0001/01/01")
            {
                DDate1 = null;
            }
            
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetDoctorListReportList";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@GroupID", GroupID);
            cmdGet.Parameters.AddWithValue("@Dob", DDate);
            cmdGet.Parameters.AddWithValue("@Dow", DDate1);
            cmdGet.Parameters.AddWithValue("@CaseName", CaseName);
            cmdGet.Parameters.AddWithValue("@AreaName", AreaName);
            cmdGet.Parameters.AddWithValue("@SubAreaName", SubAreaName);
            cmdGet.Parameters.AddWithValue("@GroupCode", GroupCode);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<DoctorListResponse> objlst = new List<DoctorListResponse>();

            while (dr.Read())
            {
                DoctorListResponse objdoctor = new DoctorListResponse();
                objdoctor.DoctorReportId = objBaseSqlManager.GetInt32(dr, "DoctorReportId");
                objdoctor.DoctorCode = objBaseSqlManager.GetTextValue(dr, "DoctorCode");
                objdoctor.DoctorName = objBaseSqlManager.GetTextValue(dr, "Doctor");
                objdoctor.GradeName = objBaseSqlManager.GetInt32(dr, "GradeName");
                objdoctor.Add1 = objBaseSqlManager.GetTextValue(dr, "Add1");
                objdoctor.Add2 = objBaseSqlManager.GetTextValue(dr, "Add2");
                objdoctor.SubAreaName = objBaseSqlManager.GetTextValue(dr, "Add3");
                objdoctor.AreaName = objBaseSqlManager.GetTextValue(dr, "Add4");
                objdoctor.CategoryName = objBaseSqlManager.GetTextValue(dr, "CategoryName");
                objdoctor.VisitSection = objBaseSqlManager.GetTextValue(dr, "VisitSection");
                objdoctor.Email = objBaseSqlManager.GetTextValue(dr, "Email");
                objdoctor.PhoneNo = objBaseSqlManager.GetTextValue(dr, "PhoneNo");
                objdoctor.CertificateId = objBaseSqlManager.GetTextValue(dr, "CertificateId");
                objdoctor.CaseName = objBaseSqlManager.GetInt32(dr, "CaseName");
                objdoctor.Dob = objBaseSqlManager.GetDateTime(dr, "Dob");
                objdoctor.DobNull = (objBaseSqlManager.GetDateTime(dr, "Dob")).ToString("MM/dd/yyyy");
                if (objdoctor.Dob.ToString("MM/dd/yyyy") == "01/01/0001")
                {
                    objdoctor.DobNull = " ";
                }
                objdoctor.Dow = objBaseSqlManager.GetDateTime(dr, "Dow");
                objdoctor.DowNull = (objBaseSqlManager.GetDateTime(dr, "Dow")).ToString("MM/dd/yyyy");
                if (objdoctor.Dow.ToString("MM/dd/yyyy") == "01/01/0001")
                {
                    objdoctor.DowNull = " ";
                }
                objdoctor.GroupName = objBaseSqlManager.GetTextValue(dr, "GroupName");
                objdoctor.GroupCode = objBaseSqlManager.GetTextValue(dr, "GroupCode");
                objlst.Add(objdoctor);
            }
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<DoctorProductResponse> GetDoctorProductReportList(DateTime? StartDate, DateTime? EndDate, string DoctorCode, int DoctorId, int ProductId, int GroupID, int GradeName, int CaseName, string CategoryName, int ProductTypeId, string GroupCode)
        {

            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetDoctorProductReportList";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@DoctorCode", DoctorCode);
            cmdGet.Parameters.AddWithValue("@DoctorId", DoctorId);
            cmdGet.Parameters.AddWithValue("@ProductId", ProductId);
            cmdGet.Parameters.AddWithValue("@GroupID", GroupID);
            cmdGet.Parameters.AddWithValue("@GradeName", GradeName);
            cmdGet.Parameters.AddWithValue("@CaseName", CaseName);
            cmdGet.Parameters.AddWithValue("@ProductTypeId", ProductTypeId);
            cmdGet.Parameters.AddWithValue("@CategoryName", CategoryName);
            cmdGet.Parameters.AddWithValue("@GroupCode", GroupCode);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<DoctorProductResponse> objlst = new List<DoctorProductResponse>();

            while (dr.Read())
            {
                DoctorProductResponse objdoctor = new DoctorProductResponse();
                objdoctor.DoctorReportId = objBaseSqlManager.GetInt32(dr, "DoctorReportId");
                objdoctor.DoctorCode = objBaseSqlManager.GetTextValue(dr, "DoctorCode");
                objdoctor.DoctorName = objBaseSqlManager.GetTextValue(dr, "Doctor");
                objdoctor.CategoryName = objBaseSqlManager.GetTextValue(dr, "CategoryName");
                objdoctor.GradeName = objBaseSqlManager.GetInt32(dr, "GradeName");
                objdoctor.Add1 = objBaseSqlManager.GetTextValue(dr, "Add4");
                objdoctor.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                objdoctor.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objdoctor.ProductType = objBaseSqlManager.GetTextValue(dr, "ProductType");
                objdoctor.GroupName = objBaseSqlManager.GetTextValue(dr, "GroupName");
                objdoctor.CaseName = objBaseSqlManager.GetInt32(dr, "CaseName");
                objdoctor.GroupCode = objBaseSqlManager.GetTextValue(dr, "GroupCode");
                objlst.Add(objdoctor);
            }
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<DoctorRetailerResponse> GetDoctorRetailerReportList(DateTime? StartDate, DateTime? EndDate, int RetailerId, int GroupID, int ProductId, int CaseName, int ProductTypeId, string RetailerCode, string GroupCode)
        {

            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetDoctorRetailerReportList";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@RetailerId", RetailerId);
            cmdGet.Parameters.AddWithValue("@GroupID", GroupID);
            cmdGet.Parameters.AddWithValue("@ProductId", ProductId);
            cmdGet.Parameters.AddWithValue("@CaseName", CaseName);
            cmdGet.Parameters.AddWithValue("@ProductTypeId", ProductTypeId);
            cmdGet.Parameters.AddWithValue("@RetailerCode", RetailerCode);
            cmdGet.Parameters.AddWithValue("@GroupCode", GroupCode);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<DoctorRetailerResponse> objlst = new List<DoctorRetailerResponse>();

            while (dr.Read())
            {
                DoctorRetailerResponse objdoctor = new DoctorRetailerResponse();
                objdoctor.DoctorCode = objBaseSqlManager.GetTextValue(dr, "DoctorCode");
                objdoctor.DoctorName = objBaseSqlManager.GetTextValue(dr, "Doctor");
                objdoctor.CategoryName = objBaseSqlManager.GetTextValue(dr, "CategoryName");
                objdoctor.GradeName = objBaseSqlManager.GetInt32(dr, "GradeName");
                objdoctor.Add1 = objBaseSqlManager.GetTextValue(dr, "Add4");
                objdoctor.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                objdoctor.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objdoctor.RetailerCode = objBaseSqlManager.GetTextValue(dr, "RetailerCode");
                objdoctor.RetailerName = objBaseSqlManager.GetTextValue(dr, "RetailerName");
                objdoctor.GroupCode = objBaseSqlManager.GetTextValue(dr, "GroupCode");
                objdoctor.GroupName = objBaseSqlManager.GetTextValue(dr, "GroupName");
                objdoctor.CaseName = objBaseSqlManager.GetInt32(dr, "CaseName");
                objdoctor.ProductType = objBaseSqlManager.GetTextValue(dr, "ProductType");
                objlst.Add(objdoctor);
            }
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<GroupreportResponse> GetGroupReportList(DateTime? StartDate, DateTime? EndDate, int GroupID, int UserId, int CaseName, string GroupCode, int manager, string managerName)
        {

            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetGroupReportList";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            cmdGet.Parameters.AddWithValue("@GroupID", GroupID);
            cmdGet.Parameters.AddWithValue("@CaseName", CaseName);
            cmdGet.Parameters.AddWithValue("@GroupCode", GroupCode);
            cmdGet.Parameters.AddWithValue("@manager", manager);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<GroupreportResponse> objlst = new List<GroupreportResponse>();

            while (dr.Read())
            {
                GroupreportResponse objdoctor = new GroupreportResponse();
                objdoctor.ID = objBaseSqlManager.GetInt32(dr, "ID");
                objdoctor.GroupName = objBaseSqlManager.GetTextValue(dr, "GroupName");
                objdoctor.CaseName = objBaseSqlManager.GetInt32(dr, "CaseName");
                objdoctor.FieldUser = objBaseSqlManager.GetTextValue(dr, "UserName");
                objdoctor.GroupCode = objBaseSqlManager.GetTextValue(dr, "GroupCode");
                objdoctor.FieldStaffName = objBaseSqlManager.GetTextValue(dr, "FieldStaffName");               
                objdoctor.RoleId = objBaseSqlManager.GetInt32(dr, "UserRoleID");
                objdoctor.VPID = objBaseSqlManager.GetInt32(dr, "VPID");
                objdoctor.NSMID = objBaseSqlManager.GetInt32(dr, "NSMID");
                objdoctor.ZSMID = objBaseSqlManager.GetInt32(dr, "ZSMID");
                objdoctor.RSMID = objBaseSqlManager.GetInt32(dr, "RSMID");
                objdoctor.ASMID = objBaseSqlManager.GetInt32(dr, "ASMID");
                switch (objdoctor.RoleId)
                {
                    case 4:
                        if (manager == objdoctor.VPID)
                        {
                            objdoctor.manager = objBaseSqlManager.GetInt32(dr, "VPID");
                            objdoctor.managerName = managerName;
                            // objlst.Add(objdoctor);
                        }
                        break;
                    case 5:
                        if (manager == objdoctor.NSMID)
                        {
                            objdoctor.manager = objBaseSqlManager.GetInt32(dr, "NSMID");
                            objdoctor.managerName = managerName;
                            // objlst.Add(objdoctor);
                        }
                        break;
                    case 6:
                        if (manager == objdoctor.ZSMID)
                        {
                            objdoctor.manager = objBaseSqlManager.GetInt32(dr, "ZSMID");
                            objdoctor.managerName = managerName;
                            // objlst.Add(objdoctor);
                        }
                        break;
                    case 7:
                        if (manager == objdoctor.RSMID)
                        {
                            objdoctor.manager = objBaseSqlManager.GetInt32(dr, "RSMID");
                            objdoctor.managerName = managerName;
                            // objlst.Add(objdoctor);
                        }
                        break;
                    case 8:
                        if (manager == objdoctor.ASMID)
                        {
                            objdoctor.manager = objBaseSqlManager.GetInt32(dr, "ASMID");
                            objdoctor.managerName = managerName;
                            // objlst.Add(objdoctor);
                        }
                        break;
                }
                objlst.Add(objdoctor);
            }
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<GroupDetailsResponse> GetGroupDetailsReportList(DateTime? StartDate, DateTime? EndDate, int GroupID, int UserId, int ProductId, int CaseName,int NameID, string GroupCode, int manager, string managerName)
        {

            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetGroupDetailsReportList";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            cmdGet.Parameters.AddWithValue("@ProductId", ProductId);
            cmdGet.Parameters.AddWithValue("@GroupID", GroupID);
            cmdGet.Parameters.AddWithValue("@Division", CaseName);
            cmdGet.Parameters.AddWithValue("@NameID", NameID);
            cmdGet.Parameters.AddWithValue("@GroupCode", GroupCode);
            cmdGet.Parameters.AddWithValue("@manager", manager);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<GroupDetailsResponse> objlst = new List<GroupDetailsResponse>();

            while (dr.Read())
            {
                GroupDetailsResponse objdoctor = new GroupDetailsResponse();
                objdoctor.Id = objBaseSqlManager.GetInt32(dr, "ID");
                objdoctor.GroupName = objBaseSqlManager.GetTextValue(dr, "GroupName");
                objdoctor.CaseName = objBaseSqlManager.GetInt32(dr, "CaseName");
                objdoctor.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objdoctor.Name = objBaseSqlManager.GetTextValue(dr, "Name");
                objdoctor.StrangthIn = objBaseSqlManager.GetTextValue(dr, "WeigtageType");
                objdoctor.MeasureIn = objBaseSqlManager.GetTextValue(dr, "ValueType");
                objdoctor.FieldUser = objBaseSqlManager.GetTextValue(dr, "UserName");
                objdoctor.FieldStaffName = objBaseSqlManager.GetTextValue(dr, "FieldStaffName");
                objdoctor.GroupCode = objBaseSqlManager.GetTextValue(dr, "GroupCode");                
                objdoctor.RoleId = objBaseSqlManager.GetInt32(dr, "UserRoleID");
                objdoctor.VPID = objBaseSqlManager.GetInt32(dr, "VPID");
                objdoctor.NSMID = objBaseSqlManager.GetInt32(dr, "NSMID");
                objdoctor.ZSMID = objBaseSqlManager.GetInt32(dr, "ZSMID");
                objdoctor.RSMID = objBaseSqlManager.GetInt32(dr, "RSMID");
                objdoctor.ASMID = objBaseSqlManager.GetInt32(dr, "ASMID");

                switch (objdoctor.RoleId)
                {
                    case 4:
                        if (manager == objdoctor.VPID)
                        {
                            objdoctor.manager = objBaseSqlManager.GetInt32(dr, "VPID");
                            objdoctor.managerName = managerName;
                            //objlst.Add(objleave);
                        }
                        break;
                    case 5:
                        if (manager == objdoctor.NSMID)
                        {
                            objdoctor.manager = objBaseSqlManager.GetInt32(dr, "NSMID");
                            objdoctor.managerName = managerName;
                            //objlst.Add(objleave);
                        }
                        break;
                    case 6:
                        if (manager == objdoctor.ZSMID)
                        {
                            objdoctor.manager = objBaseSqlManager.GetInt32(dr, "ZSMID");
                            objdoctor.managerName = managerName;
                            //objlst.Add(objleave);
                        }
                        break;
                    case 7:
                        if (manager == objdoctor.RSMID)
                        {
                            objdoctor.manager = objBaseSqlManager.GetInt32(dr, "RSMID");
                            objdoctor.managerName = managerName;
                            // objlst.Add(objleave);
                        }
                        break;
                    case 8:
                        if (manager == objdoctor.ASMID)
                        {
                            objdoctor.manager = objBaseSqlManager.GetInt32(dr, "ASMID");
                            objdoctor.managerName = managerName;
                            // objlst.Add(objleave);
                        }
                        break;
                }
                objlst.Add(objdoctor);
            }
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<ProductTargetViewModel> GetProductTargetReportList(DateTime? StartDate, DateTime? EndDate, int ProductId, int CaseName, int NameID, int ProductGroupId, int SubAreaId)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetProductTargetReportList";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@ProductId", ProductId);
            cmdGet.Parameters.AddWithValue("@CaseName", CaseName);
            cmdGet.Parameters.AddWithValue("@SubAreaId", SubAreaId);
            cmdGet.Parameters.AddWithValue("@Name", NameID);
            cmdGet.Parameters.AddWithValue("@ProductGroupId", ProductGroupId);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<ProductTargetViewModel> objlst = new List<ProductTargetViewModel>();

            while (dr.Read())
            {
                ProductTargetViewModel objleave = new ProductTargetViewModel();
                objleave.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objleave.CaseName = objBaseSqlManager.GetInt32(dr, "CaseName");
                objleave.Name = objBaseSqlManager.GetTextValue(dr, "Name");
                objleave.PTS = objBaseSqlManager.GetTextValue(dr, "PTS");
                objleave.AreaName = objBaseSqlManager.GetTextValue(dr, "AreaName");
                objleave.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                objleave.Date = objBaseSqlManager.GetDateTime(dr, "CreatedOn");
                objleave.ProductGroupName = objBaseSqlManager.GetTextValue(dr, "GroupName");
                objleave.Pack = objBaseSqlManager.GetTextValue(dr, "Pack");
                objlst.Add(objleave);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<DoctorDCRViewModel> GetDoctorDCRReportList(DateTime? StartDate, DateTime? EndDate, int UserId, int AreaID, int SubAreaId, int ProductId, int ProductTypeId, int CaseName, string VisitSection, string Prescription, int WorkTypeId, int WorkWithId)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetDoctorDCRReportList";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@WorkTypeId", WorkTypeId);
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            cmdGet.Parameters.AddWithValue("@AreaID", AreaID);
            cmdGet.Parameters.AddWithValue("@SubAreaId", SubAreaId);
            cmdGet.Parameters.AddWithValue("@ProductId", ProductId);
            cmdGet.Parameters.AddWithValue("@ProductTypeId", ProductTypeId);
            cmdGet.Parameters.AddWithValue("@CaseName", CaseName);
            cmdGet.Parameters.AddWithValue("@VisitSection", VisitSection);
            cmdGet.Parameters.AddWithValue("@Prescription", Prescription);
            cmdGet.Parameters.AddWithValue("@WorkWithId", WorkWithId);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<DoctorDCRViewModel> objlst = new List<DoctorDCRViewModel>();

            while (dr.Read())
            {
                DoctorDCRViewModel objdoctor = new DoctorDCRViewModel();
                objdoctor.DoctorReportId = objBaseSqlManager.GetInt32(dr, "DoctorReportId");
                objdoctor.DoctorCode = objBaseSqlManager.GetTextValue(dr, "DoctorCode");
                objdoctor.DoctorName = objBaseSqlManager.GetTextValue(dr, "Doctor");
                objdoctor.AreaName = objBaseSqlManager.GetTextValue(dr, "AreaName");
                objdoctor.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                objdoctor.VisitSection = objBaseSqlManager.GetTextValue(dr, "VisitSection");
                objdoctor.Medicine = objBaseSqlManager.GetTextValue(dr, "Medicine");
                objdoctor.Prescription = objBaseSqlManager.GetTextValue(dr, "Prescription");
                objdoctor.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objdoctor.CaseName = objBaseSqlManager.GetInt32(dr, "CaseName");
                objdoctor.Name = objBaseSqlManager.GetTextValue(dr, "Name");
                objdoctor.MeasureIn = objBaseSqlManager.GetTextValue(dr, "ValueType");
                objdoctor.StrangthIn = objBaseSqlManager.GetTextValue(dr, "WeigtageType");
                objdoctor.Quantity = objBaseSqlManager.GetInt32(dr, "Quantity");
                objdoctor.ProductType = objBaseSqlManager.GetTextValue(dr, "ProductType");
                objdoctor.WorkType = objBaseSqlManager.GetTextValue(dr, "WorkType");
                objdoctor.Date = objBaseSqlManager.GetDateTime(dr, "CreatedOn");
                objdoctor.UserName = objBaseSqlManager.GetTextValue(dr, "Fullname");
                objdoctor.RoleName = objBaseSqlManager.GetTextValue(dr, "RoleName");
                int RoleId = objBaseSqlManager.GetInt32(dr, "RoleId");

                var data = objBaseSqlManager.GetTextValue(dr, "WorkWithIds");

                long isexist = data.IndexOf(WorkWithId.ToString());

                if (isexist != WorkWithId && RoleId == WorkWithId)
                {
                    objlst.Add(objdoctor);
                }
                else if (WorkWithId == 0)
                {
                    objlst.Add(objdoctor);
                }
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<RetailerDCRViewModel> GetRetailerDCRReportList(DateTime? StartDate, DateTime? EndDate, int UserId, int AreaID, int SubAreaId, int ProductId, int ProductTypeId, int CaseName, string VisitSection, int WorkTypeId, int WorkWithId)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetRetailerDCRReportList";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@WorkTypeId", WorkTypeId);
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            cmdGet.Parameters.AddWithValue("@AreaID", AreaID);
            cmdGet.Parameters.AddWithValue("@SubAreaId", SubAreaId);
            cmdGet.Parameters.AddWithValue("@ProductId", ProductId);
            cmdGet.Parameters.AddWithValue("@ProductTypeId", ProductTypeId);
            cmdGet.Parameters.AddWithValue("@CaseName", CaseName);
            cmdGet.Parameters.AddWithValue("@VisitSection", VisitSection);
            cmdGet.Parameters.AddWithValue("@WorkWithId", WorkWithId);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<RetailerDCRViewModel> objlst = new List<RetailerDCRViewModel>();

            while (dr.Read())
            {
                RetailerDCRViewModel objretailer = new RetailerDCRViewModel();
                objretailer.FullName = objBaseSqlManager.GetTextValue(dr, "FirstName");
                objretailer.DCRId = objBaseSqlManager.GetInt32(dr, "DCRId");
                objretailer.AreaName = objBaseSqlManager.GetTextValue(dr, "AreaName");
                objretailer.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                objretailer.VisitSection = objBaseSqlManager.GetTextValue(dr, "VisitSection");
                objretailer.Medicine = objBaseSqlManager.GetTextValue(dr, "Medicine");
                objretailer.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objretailer.CaseName = objBaseSqlManager.GetInt32(dr, "CaseName");
                objretailer.Name = objBaseSqlManager.GetTextValue(dr, "Name");
                objretailer.MeasureIn = objBaseSqlManager.GetTextValue(dr, "ValueType");
                objretailer.StrangthIn = objBaseSqlManager.GetTextValue(dr, "WeigtageType");
                objretailer.ProductType = objBaseSqlManager.GetTextValue(dr, "ProductType");
                objretailer.WorkType = objBaseSqlManager.GetTextValue(dr, "WorkType");
                objretailer.Date = objBaseSqlManager.GetDateTime(dr, "SubmissionDate");
                objretailer.UserName = objBaseSqlManager.GetTextValue(dr, "FullName");
                objretailer.RoleName = objBaseSqlManager.GetTextValue(dr, "RoleName");
                int RoleId = objBaseSqlManager.GetInt32(dr, "RoleId");

                var data = objBaseSqlManager.GetTextValue(dr, "WorkWithIds");

                long isexist = data.IndexOf(WorkWithId.ToString());

                if (isexist != WorkWithId && RoleId == WorkWithId)
                {
                    objlst.Add(objretailer);
                }
                else if (WorkWithId == 0)
                {
                    objlst.Add(objretailer);
                }
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<FieldUserTargetViewModel> GetFieldUserTargetReportList(DateTime? StartDate, DateTime? EndDate, int UserId, int ProductId, int CaseName, int NameID, int AreaId, int SubAreaId, int manager, string managerName)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetFieldUserReportList";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            cmdGet.Parameters.AddWithValue("@ProductId", ProductId);
            cmdGet.Parameters.AddWithValue("@CaseName", CaseName);
            cmdGet.Parameters.AddWithValue("@NameID", NameID);
            cmdGet.Parameters.AddWithValue("@AreaId", AreaId);
            cmdGet.Parameters.AddWithValue("@SubAreaId", SubAreaId);
            cmdGet.Parameters.AddWithValue("@manager", manager);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<FieldUserTargetViewModel> objlst = new List<FieldUserTargetViewModel>();

            while (dr.Read())
            {
                FieldUserTargetViewModel objfielduser = new FieldUserTargetViewModel();
                objfielduser.ProductCaseId = objBaseSqlManager.GetInt32(dr, "ProductCaseId");
                objfielduser.UserName = objBaseSqlManager.GetTextValue(dr, "FullName");
                objfielduser.AreaName = objBaseSqlManager.GetTextValue(dr, "AreaName");
                objfielduser.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                objfielduser.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objfielduser.CaseName = objBaseSqlManager.GetInt32(dr, "CaseName");
                objfielduser.Name = objBaseSqlManager.GetTextValue(dr, "Name");
                objfielduser.PTS = objBaseSqlManager.GetTextValue(dr, "PTS");
                objfielduser.pack = objBaseSqlManager.GetTextValue(dr, "Pack");
                objfielduser.Year = objBaseSqlManager.GetTextValue(dr, "Year");
                objfielduser.Jan = objBaseSqlManager.GetDecimal(dr, "Jan");
                objfielduser.Feb = objBaseSqlManager.GetDecimal(dr, "Feb");
                objfielduser.Mar = objBaseSqlManager.GetDecimal(dr, "Mar");
                objfielduser.Apr = objBaseSqlManager.GetDecimal(dr, "Apr");
                objfielduser.May = objBaseSqlManager.GetDecimal(dr, "May");
                objfielduser.Jun = objBaseSqlManager.GetDecimal(dr, "Jun");
                objfielduser.July = objBaseSqlManager.GetDecimal(dr, "July");
                objfielduser.Aug = objBaseSqlManager.GetDecimal(dr, "Aug");
                objfielduser.Sep = objBaseSqlManager.GetDecimal(dr, "Sep");
                objfielduser.Oct = objBaseSqlManager.GetDecimal(dr, "Oct");
                objfielduser.Nov = objBaseSqlManager.GetDecimal(dr, "Nov");    
                objfielduser.Dec = objBaseSqlManager.GetDecimal(dr, "Dec");                  
                objfielduser.RoleId = objBaseSqlManager.GetInt32(dr, "UserRoleID");
                objfielduser.DirectorID = objBaseSqlManager.GetInt32(dr, "DirectorID");
                objfielduser.VPID = objBaseSqlManager.GetInt32(dr, "VPID");
                objfielduser.NSMID = objBaseSqlManager.GetInt32(dr, "NSMID");
                objfielduser.ZSMID = objBaseSqlManager.GetInt32(dr, "ZSMID");
                objfielduser.RSMID = objBaseSqlManager.GetInt32(dr, "RSMID");
                objfielduser.ASMID = objBaseSqlManager.GetInt32(dr, "ASMID");
                
                switch (objfielduser.RoleId)
                {
                    case 4:
                        if (manager == objfielduser.VPID)
                        {
                            objfielduser.manager = objBaseSqlManager.GetInt32(dr, "VPID");
                            objfielduser.managerName = managerName;                           
                            //objlst.Add(objfielduser);
                        }
                        break;
                    case 5:
                        if (manager == objfielduser.NSMID)
                        {
                            objfielduser.manager = objBaseSqlManager.GetInt32(dr, "NSMID");
                            objfielduser.managerName = managerName;                           
                            //objlst.Add(objfielduser);
                        }
                        break;
                    case 6:
                        if (manager == objfielduser.ZSMID)
                        {
                            objfielduser.manager = objBaseSqlManager.GetInt32(dr, "ZSMID");
                            objfielduser.managerName = managerName;                           
                            //objlst.Add(objfielduser);
                        }
                        break;
                    case 7:
                        if (manager == objfielduser.RSMID)
                        {
                            objfielduser.manager = objBaseSqlManager.GetInt32(dr, "RSMID");
                            objfielduser.managerName = managerName;                           
                            //objlst.Add(objfielduser);
                        }
                        break;
                    case 8:
                        if (manager == objfielduser.ASMID)
                        {
                            objfielduser.manager = objBaseSqlManager.GetInt32(dr, "ASMID");
                            objfielduser.managerName = managerName;                           
                            //objlst.Add(objfielduser);
                        }
                        break;
                }
                objlst.Add(objfielduser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<FieldUserViewModel> GetFieldUserReportList(DateTime? StartDate, DateTime? EndDate, int UserId, int RoleId, int AreaId, DateTime DOB,DateTime DOW,DateTime DOJ,string EmployeeCode, int manager,string managerName,int Reportingto,string ReportingName, int CaseName)
        {
            string DDate = Convert.ToDateTime(DOB).ToString("yyyy/MM/dd");
            string DDate1 = Convert.ToDateTime(DOW).ToString("yyyy/MM/dd");
            string DDate2 = Convert.ToDateTime(DOJ).ToString("yyyy/MM/dd");
            if (DDate == "0001/01/01")
            {
                DDate = null;
            }
            if (DDate1 == "0001/01/01")
            {
                DDate1 = null;
            }
            if (DDate2 == "0001/01/01")
            {
                DDate2 = null;
            }

            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetFieldUserListReportList";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            cmdGet.Parameters.AddWithValue("@UserPosition", RoleId);
            cmdGet.Parameters.AddWithValue("@AreaId", AreaId);
            cmdGet.Parameters.AddWithValue("@DOB", DDate);
            cmdGet.Parameters.AddWithValue("@DOW", DDate1);
            cmdGet.Parameters.AddWithValue("@DOJ", DDate2);
            cmdGet.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
            cmdGet.Parameters.AddWithValue("@manager", manager);
            cmdGet.Parameters.AddWithValue("@Reportingto", Reportingto);
            cmdGet.Parameters.AddWithValue("@CaseName", CaseName);            
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<FieldUserViewModel> objlst = new List<FieldUserViewModel>();

            while (dr.Read())
            {
                FieldUserViewModel objfielduser = new FieldUserViewModel();
                objfielduser.Id = objBaseSqlManager.GetInt32(dr, "Id");
                objfielduser.EmployeeCode = objBaseSqlManager.GetTextValue(dr, "EmployeeCode");
                objfielduser.UserName = objBaseSqlManager.GetTextValue(dr, "FullName");
                objfielduser.Desigantion = objBaseSqlManager.GetTextValue(dr, "designation");
                objfielduser.AreaName = objBaseSqlManager.GetTextValue(dr, "AreaName");
                objfielduser.Add1 = objBaseSqlManager.GetTextValue(dr, "Address");
                objfielduser.Pin = objBaseSqlManager.GetInt32(dr, "PostCode");
                objfielduser.MobileNumber = objBaseSqlManager.GetTextValue(dr, "MobileNumber");
                objfielduser.Email = objBaseSqlManager.GetTextValue(dr, "EmailID");
                objfielduser.DOB = objBaseSqlManager.GetDateTime(dr, "DOB");
                objfielduser.DOBNull = (objBaseSqlManager.GetDateTime(dr, "DOB")).ToString("dd-MM-yyyy");
                if (objfielduser.DOB.ToString("MM/dd/yyyy") == "01/01/0001")
                {
                    objfielduser.DOBNull = " ";
                }
                objfielduser.DOW = objBaseSqlManager.GetDateTime(dr, "DOW");
                objfielduser.DOWNull = (objBaseSqlManager.GetDateTime(dr, "DOW")).ToString("dd-MM-yyyy");
                if (objfielduser.DOW.ToString("MM/dd/yyyy") == "01/01/0001")
                {
                    objfielduser.DOWNull = " ";
                }
                objfielduser.DOJ = objBaseSqlManager.GetDateTime(dr, "DOJ");
                objfielduser.DOJNull = (objBaseSqlManager.GetDateTime(dr, "DOJ")).ToString("dd-MM-yyyy");
                if (objfielduser.DOJ.ToString("MM/dd/yyyy") == "01/01/0001")
                {
                    objfielduser.DOJNull = " ";
                }
                objfielduser.CaseName = objBaseSqlManager.GetInt32(dr, "CaseName");                
                //objfielduser.manager = objBaseSqlManager.GetInt32(dr, "manager");
                //objfielduser.Reportingto = objBaseSqlManager.GetInt32(dr, "Reportingto");                            
                objfielduser.RoleId = objBaseSqlManager.GetInt32(dr, "UserRoleID");
                objfielduser.DirectorID = objBaseSqlManager.GetInt32(dr, "DirectorID");
                objfielduser.VPID = objBaseSqlManager.GetInt32(dr, "VPID");
                objfielduser.NSMID = objBaseSqlManager.GetInt32(dr, "NSMID");
                objfielduser.ZSMID = objBaseSqlManager.GetInt32(dr, "ZSMID");
                objfielduser.RSMID = objBaseSqlManager.GetInt32(dr, "RSMID");
                objfielduser.ASMID = objBaseSqlManager.GetInt32(dr, "ASMID");

                switch (objfielduser.RoleId)
                {
                    case 4:
                        if (manager == objfielduser.VPID)
                        {
                            objfielduser.manager = objBaseSqlManager.GetInt32(dr, "VPID");
                            objfielduser.managerName = managerName;
                            objfielduser.Reportingto = objBaseSqlManager.GetInt32(dr, "DirectorID");
                            objfielduser.ReportingName = ReportingName;
                            //objlst.Add(objfielduser);
                        }
                        break;
                    case 5:
                        if (manager == objfielduser.NSMID)
                        {
                            objfielduser.manager = objBaseSqlManager.GetInt32(dr, "NSMID");
                            objfielduser.managerName = managerName;
                            objfielduser.Reportingto = objBaseSqlManager.GetInt32(dr, "VPID");
                            objfielduser.ReportingName = ReportingName;
                            //objlst.Add(objfielduser);
                        }
                        break;
                    case 6:
                        if (manager == objfielduser.ZSMID)
                        {
                            objfielduser.manager = objBaseSqlManager.GetInt32(dr, "ZSMID");
                            objfielduser.managerName = managerName;
                            objfielduser.Reportingto = objBaseSqlManager.GetInt32(dr, "NSMID");
                            objfielduser.ReportingName = ReportingName;
                            //objlst.Add(objfielduser);
                        }
                        break;
                    case 7:
                        if (manager == objfielduser.RSMID)
                        {
                            objfielduser.manager = objBaseSqlManager.GetInt32(dr, "RSMID");
                            objfielduser.managerName = managerName;
                            objfielduser.Reportingto = objBaseSqlManager.GetInt32(dr, "NSMID");
                            objfielduser.ReportingName = ReportingName;
                            //objlst.Add(objfielduser);
                        }
                        break;
                    case 8:
                        if (manager == objfielduser.ASMID)
                        {
                            objfielduser.manager = objBaseSqlManager.GetInt32(dr, "ASMID");
                            objfielduser.managerName = managerName;
                            objfielduser.Reportingto = objBaseSqlManager.GetInt32(dr, "NSMID");
                            objfielduser.ReportingName = managerName;
                            //objlst.Add(objfielduser);
                        }
                        break;
                }
                objlst.Add(objfielduser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }
    }
}
