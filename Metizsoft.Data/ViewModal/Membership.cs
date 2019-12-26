using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Metizsoft.Model
{
    public class AssignRoleVM
    {
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public List<SelectListItem> Userlist { get; set; }
        public List<SelectListItem> RolesList { get; set; }
    }

    public class AllroleandUser
    {
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public List<AllroleandUser> AllDetailsUserlist { get; set; }
    }

    public class Login
    {
        public string password { get; set; }
        public string username { get; set; }
    }

    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class activedeactive
    {
        public int UserID { get; set; }
        public bool flage { get; set; }
    }

    public class Register
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string UserFullName { get; set; }
        public bool IsActive { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public string Confirmpassword { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MobileNumber { get; set; }

        public string EmailID { get; set; }
        public long LeaveId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FromDatedt { get; set; }
        public DateTime ToDatedt { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string LeaveStatus { get; set; }
        public string LeaveStatusByVP { get; set; }
        public string LeaveReason { get; set; }
        public string LeaveByASm { get; set; }

        public string InstanceID { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOW { get; set; }
        public DateTime DOJ { get; set; }
        public string EmployeeCode { get; set; }

        public int AreaID { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public int SubAreaID { get; set; }

        public int CaseName { get; set; }
        public int DCRBlocking { get; set; }
    }

    public enum DCREnums:int
    {
        Zero = 0,
        Three = 3,
        Five = 5,
        Nine = 9,       
    }     

    public class RegisterResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserPosition { get; set; }
        public int DirectorID { get; set; }
        public int HOID { get; set; }
        public int VPID { get; set; }
        public int NSMID { get; set; }
        public int ZSMID { get; set; }
        public int RSMID { get; set; }
        public int ASMID { get; set; }
        public int MRID { get; set; }
        public DateTime DOB { get; set; }
        public string DOBdt { get; set; }
        public DateTime DOW { get; set; }
        public string DOWdt { get; set; }
        public DateTime DOJ { get; set; }
        public string DOJdt { get; set; }
        public string EmployeeCode { get; set; }
        public int AreaID { get; set; }
        public int SubAreaID { get; set; }
        public int CaseName { get; set; }
        public int DCRBlocking { get; set; }
        public string ManualEmployeeCode { get; set; }
    }

    public class ExpenseLimit
    {
        public int Id { get; set; }
        public int UserRoleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ExpensesId { get; set; }
        public float ExpensesLimit { get; set; }
    }

    public class Target
    {
        public int Id { get; set; }
        public int UserRoleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AreaID { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public int SubAreaID { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public float SalesTarget { get; set; }
    }
    public class UploadUserData
    {
        public string DoctorCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DOW { get; set; }

    }
    public class UploadRetailerData
    {
        public string FullName { get; set; }
        public DateTime? Dob { get; set; }
        public DateTime? Anniversary { get; set; }

    }
}
