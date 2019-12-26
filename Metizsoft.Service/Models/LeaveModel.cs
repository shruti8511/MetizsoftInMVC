using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Data.ViewModal
{
    public class LeaveModel
    {
        public long LeaveId { get; set; }
        //  public long UserId { get; set; }
        public long CurrentUserId { get; set; }
        public int LeaveType { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Reason { get; set; }
        public string LeaveStatus { get; set; }
        public string LeaveStatusByVP { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public string CreatedOnst { get; set; }
        public string UpdatedOnst { get; set; }
        public bool IsActive { get; set; }
    }
    public class AddLeaveModel
    {
        public long LeaveId { get; set; }
        public long RoleId { get; set; }
        public long CurrentUserId { get; set; }
        public int LeaveType { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
    }
    public class ResponseLeaveModel
    {
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public long LeaveId { get; set; }
        public long CurrentUserId { get; set; }       
        public int LeaveType { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Reason { get; set; }
        public string LeaveStatus { get; set; }
        public string LeaveStatusByVP { get; set; }


    }
    public class ResponseLeaveCountModel
    {
        public int TotalLeave { get; set; }
        //  public int AssignedLeave { get; set; }
        public int ApprovedLeave { get; set; }
        public int InProgress { get; set; }
        public List<ResponseLeaveModel> lstResponseLeaveModel { get; set; }

        public List<LeaveTypeResponse> LeaveTypeList { get; set; }
    }


     public class LeaveTypeResponse
     {
         public int LeaveTypeId { get; set; }
         public string LeaveType { get; set; }
    }

     
}