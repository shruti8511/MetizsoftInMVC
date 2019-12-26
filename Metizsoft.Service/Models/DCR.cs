using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metizsoft.Service.Models
{
    public class WorkTypeViewModel
    {
        public int WorkTypeId { get; set; }
        public string WorkType { get; set; }
    
    }

    public class WorkWitheViewModel
    {
        public int RoleId { get; set; }
        public string RoleDisplayName { get; set; }
        public string RoleName { get; set; }
    }

    public class AreaViewModel1
    {
        public List<AreaViewModel> lstAreaName { get; set; }
        public List<SubAreaViewModel> lstSubArea { get; set; }
    }

    public class WorkAndWorkWithModel
    {
        public List<WorkTypeViewModel> lstWorkTypeModel { get; set; }
        public List<WorkWitheViewModel> lstWorkWithModel { get; set; }
    }

    public class AreaViewModel
    {
        public int AreaID { get; set; }
        public string AreaName { get; set; }

    }
    public class SubAreaViewModels
    {
        public int AreaID { get; set; }
        public string SubAreaName { get; set; }

    }

   
    public class SubAreaViewModel
    {
        public int AreaID { get; set; }
        public string AreaName { get; set; }

    }
    public class ApproveTourModel
    {
        public int TourId { get; set; }
        public long ApprovedBy { get; set; }
        public string IsApproved { get; set; }

    }
    public class DCRViewModel
    {
        public int WorkTypeId { get; set; }
        public long TourId { get; set; }      
        public string WorkWithIds { get; set; }
        public int AreaID { get; set; }
        public int SubAreaId { get; set; }
        //public int RoleId { get; set; }
        public bool Tourvariance { get; set; }
        public string varianceArea { get; set; }
        public string varianceSubAreaID { get; set; }
        public string Latitude { get; set; }
        public string longitude { get; set; }
        public string AreaNote { get; set; }
        public string TourPlanVariation { get; set; }
        public long CurrentUserId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string WorkWithUserId { get; set; }

        public int ProductId { get; set; }
        public int CaseName { get; set; }
    }

    public partial class TourProgram_MstModel
    {
        public int DCRID { get; set; }
        public long TourID { get; set; }
        public string Day { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string TourDate { get; set; }
        public Nullable<long> AreaID { get; set; }
        public string AreaName { get; set; }
        public Nullable<long> SubareaID { get; set; }       
        public string SubAreaName { get; set; }
        public string HQRS { get; set; }
        public string ContactPoint { get; set; }
        public Nullable<long> UserID { get; set; }
        public bool IsActive { get; set; }
    }

    public class MultipleTourReport
    {
        public List<TourProgramArea_Mst> Tours { get; set; }
    }

    public partial class TourProgramArea_Mst
    {
        //public int DCRID { get; set; }
        //public long TourID { get; set; }
        public string Day { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string TourDate { get; set; }
        public Nullable<long> AreaID { get; set; }
        public Nullable<long> SubareaID { get; set; }
        public string HQRS { get; set; }
        public string ContactPoint { get; set; }
        public Nullable<long> UserID { get; set; }
         
    }    

    public class MultipleArea
    {
        public long AreaID { get; set; }
        public string AreaName { get; set; }       
        public List<MultipleSubArea> SubAreas { get; set; }
    }

    public class MultipleSubArea
    {
        public long SubareaID { get; set; }
        public string SubAreaName { get; set; }  
    }

    public class DCRSummaryReport
    {
        public int DCRId { get; set; }
        public int WorkTypeId { get; set; }
        public string WorkType { get; set; }
        public int AreaID { get; set; }
        public string AreaName { get; set; }
        public int SubAreaId { get; set; }
        public string SubAreaName { get; set; }
        public string RoleId { get; set; }
        public DateTime ReportDate { get; set; }
        public string TourPlanVariation { get; set; }
        public string WorkWithUserId { get; set; }
        public string FullName { get; set; }
        public string WorkWithUserName { get; set; }
        public string ReportDateInDate { get; set; }
       // public List<DCRRoleId> lstRole { get; set; }
        //public int SubAreaId { get; set; }
        //public string SubAreaName { get; set; }
        //public int SubAreaId { get; set; }
        //public string SubAreaName { get; set; }
    }

    public class DCRRoleId
    {
        //public string WorkWithUserId { get; set; }
        public string FullName { get; set; }

    }

}