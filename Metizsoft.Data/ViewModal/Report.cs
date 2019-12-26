using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Data.ViewModal
{
    public class PrecallResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SelectType { get; set; }
        public int DoctorId { get; set; }
        public int ReatailerId { get; set; }

        public string DoctorName { get; set; }
        public string AreaNameDoctor { get; set; }
        public string SubAreaNameDoctor { get; set; }

        public string FirstName { get; set; }
        public string Usertype { get; set; }       
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
    }

    public class DoctorDetailsResponce
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int DoctorId { get; set; }
        public string DoctorCode { get; set; }
        public string DoctorName { get; set; }
        public int GradeName { get; set; }
        public string Qualification { get; set; }
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public string Add3 { get; set; }
        public string Add4 { get; set; }
        public string Pin { get; set; }
        public string PhoneNo { get; set; }
        public string RefrenceNo { get; set; }
        public string CategoryName { get; set; }
        public string SubAreaName { get; set; }
        public Nullable<System.DateTime> Dob { get; set; }
        public Nullable<System.DateTime> Dow { get; set; }
        public string Email { get; set; }
       
    }

    public class ExpenditureResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public int ExpenserId { get; set; }

        public int ExpenserReportId { get; set; }
        public string UserName { get; set; }
        public string ExpenserName { get; set; }
        public string ExpenserImage { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }
        //public string Note { get; set; }
       // public string Reason { get; set; }
       
    }

    public class LeaveResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public int LeaveId { get; set; }
        public int RoleId { get; set; }
        public int VPID { get; set; }
        public int NSMID { get; set; }
        public int ZSMID { get; set; }
        public int RSMID { get; set; }
        public int ASMID { get; set; }

        public string UserName { get; set; }
        public long CurrentUserId { get; set; }
        public string LeaveType { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int DaysOfLeave { get; set; }
        public string Reason { get; set; }
        public int manager { get; set; }
        public string managerName { get; set; }
        //public string LeaveStatus { get; set; }
        //public string LeaveStatusByVP { get; set; }
    }

    public class ProductListResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProductId { get; set; }
        public int CaseName { get; set; }
        public string ProductCode { get; set; }
        public int NameID { get; set; }

        public int ProductCaseId { get; set; }        
        public string ProductName { get; set; }        
        public string Name { get; set; }
        public string MeasureIn { get; set; }
        public string StrangthIn { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductType { get; set; }
        public string PTS { get; set; }
        public string Pack { get; set; }
        public string PTR { get; set; }
        public string MRP { get; set; }
        public string STDRate { get; set; }
        public string BrandName { get; set; }
    }

    public class DoctorFieldUserResponse
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProductId { get; set; }
        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public int GradeName { get; set; }
        public int ProductTypeId { get; set; }
        public int CaseName { get; set; }

        public int DoctorReportId { get; set; }
        public string DoctorCode { get; set; }
        public string DoctorName { get; set; }       
        public string CreatedBy { get; set; }
        public string Add1 { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public string CategoryName { get; set; }       
        public string VisitSection { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string Name { get; set; }         
        public string MeasureIn { get; set; }
        public string StrangthIn { get; set; }
    }

    public class DoctorListResponse
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GroupID { get; set; }
        public int GradeName { get; set; }
        public DateTime Dob { get; set; }
        public string DobNull { get; set; }
        public DateTime Dow { get; set; }
        public string DowNull { get; set; }
        public int CaseName { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public string GroupCode { get; set; }

        public int DoctorReportId { get; set; }
        public string DoctorCode { get; set; }
        public string DoctorName { get; set; }
        public string CategoryName { get; set; }        
        public string VisitSection { get; set; }
        public int CreatedOn { get; set; }
        public string Add1 { get; set; }
        public string Add2 { get; set; }       
        public string PhoneNo { get; set; }
        public string GroupName { get; set; }
        public string Email { get; set; }
        public string CertificateId { get; set; }
       
        
    }

    public class DoctorProductResponse
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GroupID { get; set; }
        public int ProductId { get; set; }
        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public int GradeName { get; set; }
        public int CaseName { get; set; }
        public string GroupCode { get; set; }

        public int DoctorReportId { get; set; }
        public string DoctorCode { get; set; }
        public string DoctorName { get; set; }
        public string CategoryName { get; set; }
        public string CreatedBy { get; set; }
        public string Add1 { get; set; }
        public string SubAreaName { get; set; }
        public string GroupName { get; set; }       
        public string ProductName { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductType { get; set; }

    }

    public class DoctorRetailerResponse
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GroupID { get; set; }
        public int RetailerId { get; set; }
        public int ProductId { get; set; }      
        public int CaseName { get; set; }
        public string RetailerCode { get; set; }
        public string GroupCode { get; set; }

        public int DoctorReportId { get; set; }
        public string DoctorCode { get; set; }
        public string DoctorName { get; set; }
        public string CategoryName { get; set; }
        public int GradeName { get; set; }       
        public string Add1 { get; set; }
        public string SubAreaName { get; set; }       
        public string ProductName { get; set; }
        public string RetailerName { get; set; }
        public string GroupName { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductType { get; set; }
       
    }

    public class GroupreportResponse
    {
        public int ID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int GroupID { get; set; }
        public int DoctorId { get; set; }
        public int CaseName { get; set; }
        public string FieldStaffName { get; set; }
        public int UserId { get; set; }
        public string GroupName { get; set; }
        public string GroupCode { get; set; }
        public string FieldUser { get; set; }
        public int manager { get; set; }
        public string managerName { get; set; }
        public int RoleId { get; set; }
        public int VPID { get; set; }
        public int NSMID { get; set; }
        public int ZSMID { get; set; }
        public int RSMID { get; set; }
        public int ASMID { get; set; }
        public decimal ContributeToPool { get; set; }
    }

    public class GroupDetailsResponse
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GroupID { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string GroupCode { get; set; }
        public int CaseName { get; set; }
        public int NameID { get; set; }
        public int RoleId { get; set; }
        public int VPID { get; set; }
        public int NSMID { get; set; }
        public int ZSMID { get; set; }
        public int RSMID { get; set; }
        public int ASMID { get; set; }

        public string DoctorId { get; set; }
        public string Division { get; set; }
        public string FieldStaffName { get; set; }
        public string ProductName { get; set; }
        public string Name { get; set; }
        public string MeasureIn { get; set; }
        public string StrangthIn { get; set; }
        public string GroupName { get; set; }
        public string FieldUser { get; set; }
        public string DivisionName { get; set; }
        public int manager { get; set; }
        public string managerName { get; set; }
        public decimal ContributeToPool { get; set; }
    }

    public class ProductTargetViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ProductId { get; set; }
        public int CaseName { get; set; }
        public int ProductGroupId { get; set; }
        public int SubAreaId { get; set; }
        public int NameID { get; set; }

        public string ProductName { get; set; }     
        public int ProductCaseId { get; set; }                 
        public string PTS { get; set; }
        public string Name { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public DateTime Date { get; set; }
        public string Pack { get; set; }
        public string ProductGroupName { get; set; }
    }

    public class DoctorDCRViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int WorkWithId { get; set; }
        public int UserId { get; set; }
        public int WorkTypeId { get; set; }
        public int AreaID { get; set; }
        public int SubAreaId { get; set; }
        public int ProductId { get; set; }           
        public int CaseName { get; set; }
        public int ProductTypeId { get; set; }
        public string VisitSection { get; set; }
        public string Prescription { get; set; }

        public int DCRId { get; set; }
        public int DoctorReportId { get; set; }
        public string DoctorCode { get; set; }
        public string DoctorName { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public string ProductType { get; set; }
        public string Medicine { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string WorkType { get; set; }
        public string RoleName { get; set; }
        public int CategoryName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string MeasureIn { get; set; }
        public string StrangthIn { get; set; }  
        
    }

    public class RetailerDCRViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int WorkWithId { get; set; }
        public int UserId { get; set; }
        public int WorkTypeId { get; set; }
        public int AreaID { get; set; }
        public int SubAreaId { get; set; }
        public int ProductId { get; set; }
        public int ProductTypeId { get; set; }
        public int CaseName { get; set; }       
        public string VisitSection { get; set; }
        

        public int DCRId { get; set; }
        public string FullName { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }        
        public string Medicine { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string WorkType { get; set; }
        public string RoleName { get; set; }
        public int CategoryName { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string MeasureIn { get; set; }
        public string StrangthIn { get; set; }

    }

    public class FieldUserTargetViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public int AreaId { get; set; }
        public int SubAreaId { get; set; }
        public int ProductId { get; set; }       
        public int CaseName { get; set; }
        public int NameID { get; set; }
        public int RoleId { get; set; }
        public int VPID { get; set; }
        public int NSMID { get; set; }
        public int ZSMID { get; set; }
        public int RSMID { get; set; }
        public int ASMID { get; set; }
        public int DirectorID { get; set; }

        public int ProductCaseId { get; set; }
        public string UserName { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public string ProductName { get; set; }
        public string Name { get; set; }
        public string PTS { get; set; }
        public int manager { get; set; }
        public string managerName { get; set; }
        public string pack { get; set; }
        public string Year { get; set; }
        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal Mar { get; set; }
        public decimal Apr { get; set; }
        public decimal May { get; set; }
        public decimal Jun { get; set; }
        public decimal July { get; set; }
        public decimal Aug { get; set; }
        public decimal Sep { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dec { get; set; }
       
    }

    public class FieldUserViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public int AreaId { get; set; }
        public int RoleId { get; set; }
        public DateTime DOB { get; set; }
        public string DOBNull { get; set; }
        public DateTime DOW { get; set; }
        public string DOWNull { get; set; }
        public DateTime DOJ { get; set; }
        public string DOJNull { get; set; }
        public int VPID { get; set; }
        public int NSMID { get; set; }
        public int ZSMID { get; set; }
        public int RSMID { get; set; }
        public int ASMID { get; set; }
        public int DirectorID { get; set; }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string AreaName { get; set; }
        public string Desigantion { get; set; }
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public int Pin { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public int manager { get; set; }
        public string managerName { get; set; }
        public int Reportingto { get; set; }
        public string ReportingName { get; set; }
        public int CaseName { get; set; }
        public string EmployeeCode { get; set; }
    }
}
