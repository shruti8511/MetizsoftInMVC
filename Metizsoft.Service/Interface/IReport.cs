
namespace Metizsoft.Service
{
    using Metizsoft.Data.ViewModal;
    using Metizsoft.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

   public interface IReport
    {
       List<DCRResponse> GetDCRReportList(DateTime? StartDate, DateTime? EndDate, int WorkTypeId, int UserId, int AreaID, int SubAreaId, int WorkWithId);

       List<DoctorResponse> GetDoctorReportList(DateTime? StartDate, DateTime? EndDate, int DoctorId, string DoctorCode, string VisitSection, string CategoryName, string GradeName, string PhoneNo, int CaseName, string AreaName, string SubAreaName);

       List<RetailerResponse> GetRetailerReportList(DateTime? StartDate, DateTime? EndDate, int ReatailerId, string AreaName, string SubAreaName, int UserType, DateTime DOB, DateTime Anniversary, int CaseName, string RetailerCode, string ContactName);

       List<PromotionalResponse> GetPromotionalReportList(DateTime? StartDate, DateTime? EndDate, int PromotionalId, int AreaId, int UserId, DateTime Date, int manager, string managerName);

       List<SalesTargetResponse> GetSalesReportList(DateTime? StartDate, DateTime? EndDate, int Id, string UserName, int ProductId, int CaseName, int AreaID);

       List<Register> GetVPByDirectorReport(long DirectorID, long RoleID);
       List<Register> GetNSMByVPIDReport(long DirectorID, long VPID, long RoleID);
       List<Register> GetZSMByNSMIDReport(long DirectorID, long VPID, long NSMID, long RoleID);
       List<Register> GetRSMByZSMIDReport(long DirectorID, long VPID, long NSMID, long ZSMID, long RoleID);
       List<Register> GetASMByRSMIDReport(long DirectorID, long VPID, long NSMID, long ZSMID, long RSMID, long RoleID);
       List<Register> GetMRByASMIDReport(long DirectorID, long VPID, long NSMID, long ZSMID, long RSMID, long ASMID, long RoleID);

       List<RCPAResponse> GetRCPAReportList(DateTime? StartDate, DateTime? EndDate, int ReatailerId, int ProductId, int CaseName, int NameID);
       // SalesTargetResponse GetUserDetailsForsale(string UserId);

       List<TourPlanResponse> TourPlanVarianceHistory(DateTime? StartDate, DateTime? EndDate, int UserId, int AreaId, int SubAreaId, DateTime Date);

       List<PrecallResponse> GetPreCallAnalysisReportList(DateTime? StartDate, DateTime? EndDate, int SelectType);

       List<DoctorDetailsResponce> GetDoctorDetailsReportList();

       List<ExpenditureResponse> GetExpenditureReportList(DateTime? StartDate, DateTime? EndDate, int UserId, int ExpenserId, long Amount);

       List<LeaveResponse> GetLeaveReportList(DateTime? StartDate, DateTime? EndDate, int UserId, int LeaveId, string Reason, int DaysOfLeave, int manager,string managerName);

       List<ProductListResponse> GetProductReportList(DateTime? StartDate, DateTime? EndDate, int ProductId, string StrangthIn, string MeasureIn, string ProductCode, string PTS, int CaseName, int NameID, string MRP, string STDRate, string BrandName, int ProductTypeId);

       List<DoctorFieldUserResponse> GetDoctorFieldUserReportList(DateTime? StartDate, DateTime? EndDate, string DoctorCode, int ProductId, int UserId, string CategoryName, int GradeName, int CaseName, int DoctorId, int ProductTypeId, string AreaName, string SubAreaName);

       List<DoctorListResponse> GetDoctorListReportList(DateTime? StartDate, DateTime? EndDate, int GroupID, DateTime Dob, DateTime Dow, int CaseName, string AreaName, string SubAreaName, string GroupCode);

       List<DoctorProductResponse> GetDoctorProductReportList(DateTime? StartDate, DateTime? EndDate, string DoctorCode, int DoctorId, int ProductId, int GroupID, int GradeName, int CaseName, string CategoryName, int ProductTypeId, string GroupCode);

       List<DoctorRetailerResponse> GetDoctorRetailerReportList(DateTime? StartDate, DateTime? EndDate, int RetailerId, int GroupID, int ProductId, int CaseName, int ProductTypeId, string RetailerCode, string GroupCode);

       List<GroupreportResponse> GetGroupReportList(DateTime? StartDate, DateTime? EndDate, int GroupID, int UserId, int CaseName, string GroupCode, int manager, string managerName);

       List<GroupDetailsResponse> GetGroupDetailsReportList(DateTime? StartDate, DateTime? EndDate, int GroupID, int UserId, int ProductId, int CaseName, int NameID, string GroupCode, int manager, string managerName);

       List<ProductTargetViewModel> GetProductTargetReportList(DateTime? StartDate, DateTime? EndDate, int ProductId, int CaseName, int NameID, int ProductGroupId, int SubAreaId);

       List<DoctorDCRViewModel> GetDoctorDCRReportList(DateTime? StartDate, DateTime? EndDate, int UserId, int AreaID, int SubAreaId, int ProductId, int ProductTypeId, int CaseName, string VisitSection, string Prescription,int WorkTypeId, int WorkWithId);

       List<RetailerDCRViewModel> GetRetailerDCRReportList(DateTime? StartDate, DateTime? EndDate, int UserId, int AreaID, int SubAreaId, int ProductId, int ProductTypeId, int CaseName, string VisitSection, int WorkTypeId, int WorkWithId);

       List<FieldUserTargetViewModel> GetFieldUserTargetReportList(DateTime? StartDate, DateTime? EndDate, int UserId, int ProductId, int CaseName, int NameID, int AreaId, int SubAreaId, int manager, string managerName);

       List<FieldUserViewModel> GetFieldUserReportList(DateTime? StartDate, DateTime? EndDate, int UserId, int RoleId, int AreaId, DateTime DOB, DateTime DOW, DateTime DOJ, string EmployeeCode, int manager, string managerName, int Reportingto, string ReportingName, int CaseName);
    }
}
