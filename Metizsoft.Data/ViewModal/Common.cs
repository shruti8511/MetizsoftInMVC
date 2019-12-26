using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Data.ViewModal
{
    public enum UserPosition
    {
        Director = 1,
        HO = 2,
        VP = 3,
        NSM = 4,
        ZSM = 5,
        RSM = 6,
        ASM = 7,
        MR = 8,
        //Admin = 1,
        //Employee = 2
    };
    public class LoginResponse
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int DirectorID { get; set; }
        public int HOID { get; set; }
        public int VPID { get; set; }
        public int NSMID { get; set; }
        public int ZSMID { get; set; }
        public int RSMID { get; set; }
        public int ASMID { get; set; }
        public int MRID { get; set; }
        public string UserFullName { get; set; }

    }

    public class UserListViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

    public partial class TourProgram_Varianceresponce
    {
        public long TourID { get; set; }
        public string TourDate { get; set; }
        public string Day { get; set; }
        public string Status { get; set; }
        public Nullable<long> AreaID { get; set; }
        public Nullable<long> SubareaID { get; set; }
        public Nullable<long> VarianceAreaID { get; set; }
        public Nullable<long> VarianceSubareaID { get; set; }

        public string UserName { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public string VarianceArea { get; set; }
        public string VarianceSubarea { get; set; }
        public string AreaNote { get; set; }


    }
    public class DashboardCounts
    {
        public int DCR { get; set; }
        public int Doctors { get; set; }
        public int Retailer { get; set; }
        public int Product { get; set; }
        public int DCRMonthly { get; set; }
        public int DoctorsMonthly { get; set; }
        public int RetailerMonthly { get; set; }
        public int ProductMonthly { get; set; }
        public int ApprovedLeave { get; set; }
        public int RejectedLeave { get; set; }
        public int PendingLeave { get; set; }
        public int Target { get; set; }
        public int Achieved { get; set; }
        public int TotalUsers { get; set; }
        public int TotalLeaves { get; set; }
        public int TotalSales { get; set; }

    }

    public class ProductCount
    {
        public int total { get; set; }
        public long ProductCaseId { get; set; }
        public string ProductName { get; set; }
        public string months { get; set; }

        public int TotalTarget { get; set; }
        public int TotalAchieved { get; set; }
        public float Goal { get; set; }

    }
    public class TarGetDetails
    {
        public int TotalTarget { get; set; }
        public int TotalAchieved { get; set; }
    }



}