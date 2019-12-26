using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Metizsoft.Service.Models
{

    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

    }

    public enum ProductName
    {
        Sample = 1,
        Product = 2,
        Gift = 3,
        Promotion = 4
    }

    public class DoctorView1
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
    }
    public class DoctorViewModel
    {
        public List<DoctorModelList> lstdoctorview { get; set; }
        public List<MeetingTypeModelList> lstmeetingview { get; set; }
    }

    public class DoctorModelList
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
    }

    public class MeetingTypeModelList
    {
        public int MeetingTypeId { get; set; }
        public string MeetingType { get; set; }
    }

    public class DoctorReportViewModel
    {
        public Nullable<int> DoctorId { get; set; }
        public int ProductId { get; set; }
        public long DCRId { get; set; }
        public int ProductCaseId { get; set; }
        public string ProductName { get; set; }
        public string CaseName { get; set; }
        public string VisitSection { get; set; }
        public string RxType { get; set; }
        public string Quantity { get; set; }
        public string ValueType { get; set; }
        public string WeigtageType { get; set; }
        public string DiscussionPad { get; set; }
        public string Medicine { get; set; }
        public string Prescription { get; set; }
        public string PrescriptionText { get; set; }
        public long CurrentUserId { get; set; }
        public int MeetingTypeId { get; set; }
    }

    public class MultipleProductDoctorReport
    {
        public Nullable<int> DoctorId { get; set; }
        public long DCRId { get; set; }       
        // public int ProductId { get; set; }       
        // public int ProductCaseId { get; set; }
        // public string ProductName { get; set; }
        //  public string CaseName { get; set; }          
        // public string RxType { get; set; }
        //   public string Quantity { get; set; }
        //   public string ValueType { get; set; }
        //  public string WeigtageType { get; set; }
        public string VisitSection { get; set; }
        public string DiscussionPad { get; set; }
        //public string Medicine { get; set; }
        public string Prescription { get; set; }
        public string PrescriptionText { get; set; }
        public long CurrentUserId { get; set; }
        public int MeetingTypeId { get; set; }
        public int ProductTypeId { get; set; }
        public List<MultipleProduct> products { get; set; }        
    }

    public class MultipleProduct
    {
        public long ProductId { get; set; }
        public long ProductCaseId { get; set; }
        public string ProductName { get; set; }
        public string CaseName { get; set; }
        public long Quantity { get; set; }
        public string RxType { get; set; }
        public string ValueType { get; set; }
        public string WeigtageType { get; set; }        
    }

    public class MultipleRecords
    {
        public List<DoctorReportData> records { get; set; }   
    }

    public class DoctorReportData
    {
        public Nullable<int> DoctorId { get; set; }
        public long DCRId { get; set; }        
        public string VisitSection { get; set; }
        public string DiscussionPad { get; set; }        
        public string Prescription { get; set; }
        public string PrescriptionText { get; set; }
        public long CurrentUserId { get; set; }
        public int MeetingTypeId { get; set; }
        public int ProductTypeId { get; set; }
        public int CreatedBy { get; set; }
    }

    public class ProductListViewModel
    {
        public int ProductCaseId { get; set; }
        public string ProductName { get; set; }
        public string CaseName { get; set; }
        public string CaseSize { get; set; }
        public string RatePerNo { get; set; }
        public string RatePerLTKG { get; set; }
        public string CaseValue { get; set; }
        public string MRPPerNo { get; set; }
      
    }

    public class DoctorSummaryReport
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string VisitSection { get; set; }
        public DateTime ReportDate { get; set; }
        public string Quantity { get; set; }
        public string MeetingType { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public string WorkWithIds { get; set; }
        public string WorkWithUserId { get; set; }
        public string FullName { get; set; }
        public string WorkWithUserName { get; set; }
        public string ReportDateInDate { get; set; }
    }

    public class RCPA
    {

        public int ReatailerId { get; set; }
        public string FullName { get; set; }
        public float Quantity { get; set; }
        public string CaseName { get; set; }
        public string Name { get; set; }
        public string MeasureIn { get; set; }
        public string StrangthIn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedOnInDate { get; set; }
        // public long CurrentUserId { get; set; }
        //public string ProductName { get; set; }
        //public string DiscussionPad { get; set; }
        //public int DoctorId { get; set; }
        //public string DoctorName { get; set; }
        //public string Prescription { get; set; }
        //public string Medicine { get; set; }
        //public string VisitSection { get; set; }

    }

    public partial class Doctor_MstModel
    {
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
        public string Dob { get; set; }
        public string Dow { get; set; }
        public string Email { get; set; }
        public string FieldStaffName { get; set; }
        public string DivisionName { get; set; }
        public string DoctorClass { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime UpdateOn { get; set; }       
        public DateTime Dobdt { get; set; }
        public string strDob { get; set; }
        public DateTime Dowdt { get; set; }
        public string strDow { get; set; }
        public bool Approved { get; set; }
        public int ApprovedBy { get; set; }
        public string CertificateId { get; set; }
        public bool IsActive { get; set; }
    }

    public class Group_MstModel
    {
        public long? ID { get; set; }
        public long? GroupID { get; set; }
        public long? GroupTypeID { get; set; }
        public long? AllocationID { get; set; }
        public string Name { get; set; }
        public string Prescription { get; set; }
        public bool? IsActive { get; set; }
        public string GroupName { get; set; }
        public string GroupTypeName { get; set; }
        public string UpdateUserName { get; set; }
        public DateTime? UpdateOnstr { get; set; }
        public string UpdateOnstring { get; set; }
        public string CreatedOnstring { get; set; }
        public string CreateUserName { get; set; }
    }

    public class MultipleGroup_MstModel
    {
        public int Id { get; set; }

        public long? GroupID { get; set; }
        public long ToAllocateID { get; set; }

        public string UserName { get; set; }

        public long? GroupTypeID { get; set; }
        public long? AllocationID { get; set; }
        public string Name { get; set; }
        public string Prescription { get; set; }
        public bool? IsActive { get; set; }
        public string GroupName { get; set; }
        public string GroupTypeName { get; set; }
        public string UpdateUserName { get; set; }
        public DateTime? UpdateOnstr { get; set; }
        public string UpdateOnstring { get; set; }
        public string CreatedOnstring { get; set; }
        public string CreateUserName { get; set; }
    }

    public class MeetingTypeModel
    {
        public int MeetingTypeId { get; set; }
        public string MeetingType { get; set; }
    }
}

