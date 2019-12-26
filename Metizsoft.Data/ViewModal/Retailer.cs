using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Data.ViewModal
{
    public class RetailerNameresponse
    {
        public int RetailerId { get; set; }
        public int Usertype { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string OfficePhone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }

    }

    public class RetailerResponse
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ReatailerId { get; set; }
        public int AreaId { get; set; }
        public int SubAreaId { get; set; }
        public int UserType { get; set; }
        public string ContactName { get; set; }
        public string RetailerCode { get; set; }

        public int ReatailerReportId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public string Zip { get; set; }
        public string Mobile { get; set; }
        public string OfficePhone { get; set; }
        public DateTime DOB { get; set; }
        public string DOBNull { get; set; }
        public DateTime Anniversary { get; set; }
        public string AnniversaryNull { get; set; }
        public int CaseName { get; set; }
        public string ProductName { get; set; }
        public string Email { get; set; }
        public string Refrence { get; set; }
        public string ShortName { get; set; }
        public int GradeName { get; set; }
       // public int DiscussionPad { get; set; }
        //public string AreaName { get; set; }
        //public string SubAreaName { get; set; }
        //public float Quantity { get; set; }
    }

    public class RCPAResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ReatailerId { get; set; }
        public int DoctorId { get; set; }
        public int ProductId { get; set; }
        public int CaseName { get; set; }
        public int NameID { get; set; }
                
        public string DoctorName { get; set; }
        public string Quantity { get; set; }
        public string Medicine { get; set; }
        public string VisitSection { get; set; }

        public string ProductName { get; set; }       
        public int ReatailerReportId { get; set; }        
        public string FullName { get; set; }
        public int Usertype { get; set; }
        //public int Quantity { get; set; }    
        public string Name { get; set; }
        public string MeasureIn { get; set; }
        public string StrangthIn { get; set; }
        public DateTime Date { get; set; }
    }
}
