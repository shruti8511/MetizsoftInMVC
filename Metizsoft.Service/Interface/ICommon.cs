

namespace Metizsoft.Service
{
    using Metizsoft.Data.Modal;
    using Metizsoft.Data.ViewModal;
    using Metizsoft.Model;
    using Metizsoft.Service.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public interface ICommon
    {
       DynamicMenuModel DynamicMenuMaster_RoleWiseMenuList(int RoleId);

       List<Role> webpages_Roles_RoleList();

       List<Role> RoleList();

       RegisterResponse GetUserByUserId(int Id);

       List<Register> UsersListFilter(Register model);

       bool EditUser(User register);

       LoginResponse GetUserDetails(string UserId);
       List<WorkTypeViewModel> GetWorkTypeListForDropDown();
       List<DoctorView1> GetDoctorListForDropDown();
       List<RetailerViewModel> GetRetailerListForDropDown();
       List<PromotionalViewModel> GetPromotionalListForDropDown();
       List<AreaViewModel> GetAreaListForDropDown();
       List<SubAreaViewModels> GetSubAreaListForDropDown();
       List<ProductViewModel> GetProductListForDropDown();
       List<ProductCaseResponse> GetProductCaseListForDropDown();
       List<UserListViewModel> GetUserListForGroup();
       List<UserListViewModel> GetUserListForDropDown(int RoleId);
       List<LeaveTypeResponse> GetLeaveListForDropDown();
       List<GroupNameViewModel> GetGroupListForDropDown();
       List<ProductTypeViewModel> GetProductTyepeListForDropDown();
       List<ExpenseViewModel> GetExpenseListForDropDown();

       List<SelectListItem> GetUserPosition();

       List<Register> GetUsersByRoleID(long RoleID);

       //List<Register> GetVPByDirectorID(long DirectorID, long RoleID);

       //List<Register> GetZSMByVPID(long DirectorID, long VPID, long RoleID);

       //List<Register> GetRSMByZSMID(long DirectorID, long VPID, long ZSMID, long RoleID);

       //List<Register> GetASMByRSMID(long DirectorID, long VPID, long ZSMID, long RSMID, long RoleID);

       List<Register> GetHOByDirectorID(long DirectorID, long RoleID);

       List<Register> GetVPByHOID(long DirectorID, long HOID, long RoleID);

       List<Register> GetNSMByVPID(long DirectorID, long HOID, long VPID, long RoleID);

       List<Register> GetZSMByNSMID(long DirectorID, long HOID, long VPID, long NSMID, long RoleID);

       List<Register> GetRSMByZSMID(long DirectorID, long HOID, long VPID, long NSMID, long ZSMID, long RoleID);

       List<Register> GetASMByRSMID(long DirectorID, long HOID, long VPID, long NSMID, long ZSMID, long RSMID, long RoleID);
       List<ExpenseLimit> ExpenseLimitList(int RoleId);

       void UpadateExpenseLimit(List<ExpenseLimit> data);

       List<Target> TargetList(Target model);
       void UpadateTarget(List<Target> data);


       void UpadateUserData(UploadUserData ObjUploaddata);
       void UpadateRetailerData(UploadRetailerData objuplaoadRetailer);
       List<SalesTargetResponse> GetSalesReport();

       long GetProductID(long CaseName, string ProDuctName);
       long InsertTargate(Productallocation data);
       List<ProductCaseMst_mst> GetAllProductallocationList(string Rxtype);

       List<NotificationMaster> GetNotificationList();

       DashboardCounts DashboardCount(long UserId);
       List<ProductCount> DashboardProductCount(long UserId);
       TarGetDetails GetTotalProductCountDashboard(long UserId);

       List<FieldUserTargetViewModel> GetFieldUserTargetReport();

       List<FieldUserViewModel> GetFieldUserReport();

       LoginResponse GetUserDetailsForReports(string UserId);

       List<Register> GetUsersByParentID(long RoleID, long DirectorID);

       long GetLastEmployeeCode();

       void UpadateDCRBlockng(List<Register> data);

       List<Register> UsersList();
    }
}
