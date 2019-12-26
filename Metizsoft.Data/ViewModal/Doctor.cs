using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Data.ViewModal
{
    public class DoctorResponse
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int DoctorId { get; set; }
        public int AreaId { get; set; }
        public string DoctorName { get; set; }

        public int DoctorReportId { get; set; }
        public string DoctorCode { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public string ProductName { get; set; }
        public int CaseName { get; set; }
        public string DiscussionPad { get; set; }
        public string VisitSection { get; set; }
        public string Prescription { get; set; }
        public string PhoneNo { get; set; }
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public string CategoryName { get; set; }
        public string GradeName { get; set; }
        public string DivisionName { get; set; }
        public DateTime Dob { get; set; }
        public string DobNull { get; set; }
        public DateTime Dow { get; set; }
        public string DowNull { get; set; }
        public string Email { get; set; }
        public string CertificateId { get; set; } 
    }
    public class PreCallAnalysisReport
    {
        //public int AreaID { get; set; }
        public string AreaName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime ReportDate { get; set; }
        public string ReportDateInDate { get; set; }
    }

    public class ProductViewModel1
    {
        public List<RxTypevalue> lstRxtype { get; set; }
        public List<ProductModel> lstProductModel { get; set; }
    }

    public class RxTypevalue
    {
        public long RxTypeId { get; set; }
        public Nullable<int> RxType { get; set; }
        public string Name { get; set; }
    }

    public class ProductModel
    {
        public int ProductCaseId { get; set; }
        public string CaseName { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string MeasureIn { get; set; }
        public string StrangthIn { get; set; }
        public string Pack { get; set; }
        public string PTR { get; set; }
        public string MRP { get; set; }
        public string STDRate { get; set; }
        public string BrandName { get; set; }

    }

 
}
