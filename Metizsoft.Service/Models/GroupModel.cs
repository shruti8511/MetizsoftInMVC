using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Service
{
    public class GroupModel
    {
        public long GroupID { get; set; }
        public Nullable<long> GroupTypeID { get; set; }
        public string GroupName { get; set; }
        public string GroupTypeName { get; set; }
        public string GroupCode { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdateOn { get; set; }
        public Nullable<bool> IsActive { get; set; }

    }

    public class GroupNameViewModel
    {
        public long GroupID { get; set; }        
        public string GroupName { get; set; }
    }

    public class GroupViewModel
    {
        public int GrouptypeID { get; set; }
        public string GroupTypeName { get; set; }
    }

    public class ALlocationModel
    {
        public long ID { get; set; }
        public long GroupID { get; set; }
        public long GroupTypeID { get; set; }
        public string GroupName { get; set; }
        public string GroupTypeName { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdateOn { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public long UserRoleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long ParentId { get; set; }
        public long ZSMID { get; set; }
        public long RSMID { get; set; }
        public long ASMID { get; set; }
        public long MRID { get; set; }
        public float SalesTarget { get; set; }
        public float AchievedTarget { get; set; }

    }
    public class UserViewModel1
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public long UserRoleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public long ZSMID { get; set; }
        public long RSMID { get; set; }
        public long ASMID { get; set; }
        public long MRID { get; set; }
        public long ParentId { get; set; }
        public float SalesTarget { get; set; }
        public float AchievedTarget { get; set; }

    }
   

    public class GetUserTargetdataByRole
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public long UserRoleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long ParentId { get; set; }
        public long ZSMID { get; set; }
        public long RSMID { get; set; }
        public long ASMID { get; set; }
        public long MRID { get; set; }
        public decimal SalesTarget { get; set; }
        public decimal AchievedTarget { get; set; }


    }

    public class MultipleAllocation
    {
        public long? ID { get; set; }
        public long UserID { get; set; }
        public long GroupID { get; set; }
        public long GroupTypeID { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdateOn { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }

    public class GetForTargate
    {
        public long RoleId { get; set; }
        public long CurrentUserId { get; set; }
        public int ProductCaseId { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
    }

    public class GetUserByPosition
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }

    public class GroupToFieldUserModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int UserPositionId { get; set; }
        public string UserPosition { get; set; }
        public int GrouptypeID { get; set; }
        public int GroupID { get; set; }
        public string GroupTypeName { get; set; }
        public string GroupName { get; set; }
        public bool IsActive { get; set; }
        public string UpdateOnstr { get; set; }       
        public string UpdateUserName { get; set; }
        public string CreateUserName { get; set; }
        
    }
}
