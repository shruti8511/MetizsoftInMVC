using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Metizsoft.Service.Models
{
    public class RetailerDetailViewModel
    {
        public int ReatailerId { get; set; }
        public string FullName { get; set; }

    }
    public class RetailerViewModel
    {
        public long ReatailerId { get; set; }
        public int ReatailerTypeId { get; set; }
        public string FullName { get; set; }

    }

    //public enum ProductName
    //{
    //    Sample = 1,
    //    Product = 2,
    //    Gift = 3,
    //    Promotion = 4,
    //}

    public class RetailerAddViewModel
    {
        public Nullable<int> ReatailerId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public long DCRId { get; set; }
        public Nullable<int> ProductCaseId { get; set; }
        public Nullable<int> AreaId { get; set; }
        public Nullable<int> SubAreaId { get; set; }
        public string ProductCaseName { get; set; }
        public string ProductName { get; set; }
        public float Quantity { get; set; }
        public string DiscussionPad { get; set; }
        public long CurrentUserId { get; set; }
        public string CaseName { get; set; }
        public string Name { get; set; }
        public string MeasureIn { get; set; }
        public string StrangthIn { get; set; }           

    }

    public class MultipleProductRetailerReport
    {
        public int ReatailerId { get; set; }
        public long DCRId { get; set; }
        public int AreaId { get; set; }
        public int SubAreaId { get; set; }
        public string DiscussionPad { get; set; }
        public long CurrentUserId { get; set; }
        public int ProductTypeId { get; set; }
        public List<MultipleRetailerProduct> products { get; set; }
        //public int ProductId { get; set; }       
        //public int ProductCaseId { get; set; }      
        //public string ProductCaseName { get; set; }
        //public string ProductName { get; set; }
        //public float Quantity { get; set; }        
        //public string CaseName { get; set; }
        //public string Name { get; set; }
        //public string MeasureIn { get; set; }
        //public string StrangthIn { get; set; }  
    }

    public class MultipleRetailerProduct
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

    public class RetailerMst_mst
    {
        public long ReatailerId { get; set; }
        public Nullable<int> Usertype { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string CityId { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string OfficePhone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public bool Approved { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime Dob { get; set; }
        public string strDob { get; set; }
        public DateTime Anniversary { get; set; }
        public string strAnniversary { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime UpdateOn { get; set; }
        public bool IsActive { get; set; }
        public string ContactName { get; set; }
        public int GradeName { get; set; }
        public string Refrence { get; set; }
        public string ShortName { get; set; }
        public string RetailerCode { get; set; }
        public string DivName { get; set; }
        public int DivNamein { get; set; }        
        public string SubArea { get; set; }
    }

}
