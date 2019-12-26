namespace Metizsoft.Service
{
    using Metizsoft.Data;
    using Metizsoft.Data.Modal;
    using Metizsoft.Data.ViewModal;
    using Metizsoft.Model;
    using Metizsoft.Service.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class CommonService : ICommon
    {
        public DynamicMenuModel DynamicMenuMaster_RoleWiseMenuList(int RoleId)
        {
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            DynamicMenuModel objMenu = new DynamicMenuModel();
            SqlCommand cmdGet = new SqlCommand();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "DynamicMenuMaster_RoleWiseMenuList";
            cmdGet.Parameters.AddWithValue("@RoleId", RoleId);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<MainTier> lstMain = new List<MainTier>();
            List<SubTier> lstSub = new List<SubTier>();
            List<ThirdTier> lstThird = new List<ThirdTier>();
            while (dr.Read())
            {
                if (objBaseSqlManager.GetInt32(dr, "MainTier") == 0)
                {
                    MainTier objMainMenu = new MainTier();
                    objMainMenu.MainTierID = objBaseSqlManager.GetInt32(dr, "MainTier");
                    objMainMenu.MenuID = objBaseSqlManager.GetInt64(dr, "MenuID");
                    objMainMenu.DisplayName = objBaseSqlManager.GetTextValue(dr, "DisplayName");
                    objMainMenu.Controller = objBaseSqlManager.GetTextValue(dr, "Controller");
                    objMainMenu.Iconname = objBaseSqlManager.GetTextValue(dr, "Iconname");
                    objMainMenu.SortNo = objBaseSqlManager.GetInt32(dr, "SortNo");
                    objMainMenu.Action = objBaseSqlManager.GetTextValue(dr, "Action");
                    lstMain.Add(objMainMenu);
                }
                else if (objBaseSqlManager.GetInt32(dr, "SubTier") == 1)
                {
                    SubTier objSubTier = new SubTier();
                    objSubTier.MainTierID = objBaseSqlManager.GetInt32(dr, "MainTier");
                    objSubTier.SubTierID = objBaseSqlManager.GetInt32(dr, "SubTier");
                    objSubTier.MenuID = objBaseSqlManager.GetInt64(dr, "MenuID");
                    objSubTier.DisplayName = objBaseSqlManager.GetTextValue(dr, "DisplayName");
                    objSubTier.Controller = objBaseSqlManager.GetTextValue(dr, "Controller");
                    objSubTier.SortNo = objBaseSqlManager.GetInt32(dr, "SortNo");
                    objSubTier.Action = objBaseSqlManager.GetTextValue(dr, "Action");
                    lstSub.Add(objSubTier);

                }
                else if (objBaseSqlManager.GetInt32(dr, "SubTier") == 2)
                {
                    ThirdTier objThirdTier = new ThirdTier();
                    objThirdTier.MainTierID = objBaseSqlManager.GetInt32(dr, "MainTier");
                    objThirdTier.SubTierID = objBaseSqlManager.GetInt32(dr, "SubTier");
                    objThirdTier.MenuID = objBaseSqlManager.GetInt64(dr, "MenuID");
                    objThirdTier.DisplayName = objBaseSqlManager.GetTextValue(dr, "DisplayName");
                    objThirdTier.Controller = objBaseSqlManager.GetTextValue(dr, "Controller");
                    objThirdTier.SortNo = objBaseSqlManager.GetInt32(dr, "SortNo");
                    objThirdTier.Action = objBaseSqlManager.GetTextValue(dr, "Action");
                    lstThird.Add(objThirdTier);
                }
            }

            foreach (var item in lstSub)
            {
                List<ThirdTier> lstt = lstThird.Where(i => i.MainTierID == item.MenuID).ToList();
                item.ThirdTier = lstt;
            }

            foreach (var item in lstMain)
            {
                List<SubTier> lsts = lstSub.Where(i => i.MainTierID == item.MenuID).ToList();
                item.SubTier = lsts;
            }
            objMenu.MainTier = lstMain;

            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objMenu;
        }

        public List<Role> webpages_Roles_RoleList()
        {
            try
            {
                SqlCommand cmdGet = new SqlCommand();
                BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                cmdGet.CommandType = CommandType.StoredProcedure;
                cmdGet.CommandText = "webpages_Roles_RoleList";

                SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
                List<Role> lstRole = new List<Role>();
                while (dr.Read())
                {
                    Role objEntity = new Role();
                    objEntity.RoleId = objBaseSqlManager.GetInt32(dr, "RoleId");
                    objEntity.RoleName = objBaseSqlManager.GetTextValue(dr, "RoleName");
                    lstRole.Add(objEntity);
                }
                dr.Close();
                objBaseSqlManager.ForceCloseConnection();
                return lstRole;
            }
            catch
            {
                return new List<Role>();
            }
        }

        public List<Role> RoleList()
        {
            try
            {
                SqlCommand cmdGet = new SqlCommand();
                BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                cmdGet.CommandType = CommandType.StoredProcedure;
                cmdGet.CommandText = "GetRoleList";

                SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
                List<Role> lstRole = new List<Role>();
                while (dr.Read())
                {
                    Role objEntity = new Role();
                    objEntity.RoleId = objBaseSqlManager.GetInt32(dr, "RoleId");
                    objEntity.RoleName = objBaseSqlManager.GetTextValue(dr, "RoleName");
                    lstRole.Add(objEntity);
                }
                dr.Close();
                objBaseSqlManager.ForceCloseConnection();
                return lstRole;
            }
            catch
            {
                return new List<Role>();
            }
        }
        
        public List<Register> UsersListFilter(Register model)
        {
            try
            {
                SqlCommand cmdGet = new SqlCommand();
                BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                cmdGet.CommandType = CommandType.StoredProcedure;
                cmdGet.CommandText = "UsersListFilter";
                cmdGet.Parameters.AddWithValue("@AreaID", model.AreaID);
                cmdGet.Parameters.AddWithValue("@SubAreaID", model.SubAreaID);
                SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
                List<Register> lstUser = new List<Register>();
                while (dr.Read())
                {
                    Register objEntity = new Register();
                    objEntity.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                    objEntity.FirstName = objBaseSqlManager.GetTextValue(dr, "FirstName");
                    objEntity.LastName = objBaseSqlManager.GetTextValue(dr, "LastName");
                    objEntity.username = objBaseSqlManager.GetTextValue(dr, "UserName");
                    objEntity.InstanceID = objBaseSqlManager.GetTextValue(dr, "InstanceID");
                    objEntity.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                    lstUser.Add(objEntity);
                }
                dr.Close();
                objBaseSqlManager.ForceCloseConnection();
                return lstUser;
            }
            catch
            {
                return new List<Register>();
            }
        }

        public List<Register> UsersList()
        {
            try
            {
                SqlCommand cmdGet = new SqlCommand();
                BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                cmdGet.CommandType = CommandType.StoredProcedure;
                cmdGet.CommandText = "UsersList";                
                SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
                List<Register> lstUser = new List<Register>();
                while (dr.Read())
                {
                    Register objEntity = new Register();
                    objEntity.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                    objEntity.FirstName = objBaseSqlManager.GetTextValue(dr, "FirstName");
                    objEntity.LastName = objBaseSqlManager.GetTextValue(dr, "LastName");
                    objEntity.username = objBaseSqlManager.GetTextValue(dr, "UserName");
                    objEntity.InstanceID = objBaseSqlManager.GetTextValue(dr, "InstanceID");
                    objEntity.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                    lstUser.Add(objEntity);
                }
                dr.Close();
                objBaseSqlManager.ForceCloseConnection();
                return lstUser;
            }
            catch
            {
                return new List<Register>();
            }
        }

        public bool EditUser(User register)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                if (register.Id == 0)
                {
                    context.Users.Add(register);
                    context.SaveChanges();
                }
                else
                {
                    context.Entry(register).State = EntityState.Modified;
                    context.SaveChanges();
                }
                if (register.Id > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public RegisterResponse GetUserByUserId(int Id)
        {
            RegisterResponse objregister = new RegisterResponse();
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetUserByUserId";
            cmdGet.Parameters.AddWithValue("@UserId", Id);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            while (dr.Read())
            {

                objregister.Id = objBaseSqlManager.GetInt32(dr, "Id");
                objregister.FirstName = objBaseSqlManager.GetTextValue(dr, "FirstName");
                objregister.LastName = objBaseSqlManager.GetTextValue(dr, "LastName");
                objregister.MobileNumber = objBaseSqlManager.GetTextValue(dr, "MobileNumber");
                objregister.EmailID = objBaseSqlManager.GetTextValue(dr, "EmailID");
                objregister.Address = objBaseSqlManager.GetTextValue(dr, "Address");
                objregister.AreaID = objBaseSqlManager.GetInt32(dr, "AreaID");
                objregister.SubAreaID = objBaseSqlManager.GetInt32(dr, "SubAreaID");
                objregister.Address = objBaseSqlManager.GetTextValue(dr, "Address");              
                objregister.PostCode = objBaseSqlManager.GetTextValue(dr, "PostCode");
                objregister.UserName = objBaseSqlManager.GetTextValue(dr, "UserName");
                objregister.Password = objBaseSqlManager.GetTextValue(dr, "Password");
                objregister.UserPosition = objBaseSqlManager.GetInt32(dr, "UserPosition");
                objregister.DirectorID = objBaseSqlManager.GetInt32(dr, "DirectorID");
                objregister.HOID = objBaseSqlManager.GetInt32(dr, "HOID");
                objregister.VPID = objBaseSqlManager.GetInt32(dr, "VPID");
                objregister.NSMID = objBaseSqlManager.GetInt32(dr, "NSMID");
                objregister.ZSMID = objBaseSqlManager.GetInt32(dr, "ZSMID");
                objregister.RSMID = objBaseSqlManager.GetInt32(dr, "RSMID");
                objregister.ASMID = objBaseSqlManager.GetInt32(dr, "ASMID");
                objregister.MRID = objBaseSqlManager.GetInt32(dr, "MRID");
               // objregister.DOB = objBaseSqlManager.GetDateTime(dr, "DOB");
                objregister.DOBdt = Convert.ToDateTime(objBaseSqlManager.GetTextValue(dr, "DOB")).ToString("MM/dd/yyyy");
                //objregister.DOW = objBaseSqlManager.GetDateTime(dr, "DOW");
                objregister.DOWdt = Convert.ToDateTime(objBaseSqlManager.GetTextValue(dr, "DOW")).ToString("MM/dd/yyyy");
                //objregister.DOJ = objBaseSqlManager.GetDateTime(dr, "DOJ");
                objregister.DOJdt = Convert.ToDateTime(objBaseSqlManager.GetTextValue(dr, "DOJ")).ToString("MM/dd/yyyy");
                objregister.EmployeeCode = objBaseSqlManager.GetTextValue(dr, "EmployeeCode");
                objregister.CaseName = objBaseSqlManager.GetInt32(dr, "CaseName");
                objregister.DCRBlocking = objBaseSqlManager.GetInt32(dr, "DCRBlocking");
                objregister.ManualEmployeeCode = objBaseSqlManager.GetTextValue(dr, "ManualEmployeeCode");
                                  
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();

            return objregister;
        }

        //public bool CreateNotification(NotificationInfo ObjEntity)
        //{
        //    try
        //    {
        //        using (AllurionEntities  context = new AllurionEntities())
        //        {
        //            Notification obj = new Notification();
        //            obj.UserID = ObjEntity.UserID;
        //            obj.Message = ObjEntity.Message;
        //            obj.IsRead = ObjEntity.Isread;
        //            obj.CreatedBy = ObjEntity.CreatedBy;
        //            obj.UpdatedBy = ObjEntity.UpdateBy;
        //            obj.CreatedOn = DateTime.UtcNow;
        //            obj.UpdatedOn = DateTime.UtcNow;
        //            context.Notifications.Add(obj);
        //            context.SaveChanges();
        //        }
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}          

        public List<WorkTypeViewModel> GetWorkTypeListForDropDown()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetWorkTypeListForDropDown";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<WorkTypeViewModel> objlst = new List<WorkTypeViewModel>();

            while (dr.Read())
            {
                WorkTypeViewModel objAreaList = new WorkTypeViewModel();
                objAreaList.WorkTypeId = objBaseSqlManager.GetInt32(dr, "WorkTypeId");
                objAreaList.WorkType = objBaseSqlManager.GetTextValue(dr, "WorkType");
                objlst.Add(objAreaList);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<PromotionalViewModel> GetPromotionalListForDropDown()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetPromotionalListForDropDown";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<PromotionalViewModel> objlst = new List<PromotionalViewModel>();

            while (dr.Read())
            {
                PromotionalViewModel objPromoList = new PromotionalViewModel();
                objPromoList.PromotionalId = objBaseSqlManager.GetInt32(dr, "PromotionalId");
                objPromoList.PromotionalName = objBaseSqlManager.GetTextValue(dr, "PromotionalName");
                objlst.Add(objPromoList);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<DoctorView1> GetDoctorListForDropDown()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetDoctorListForDropDown";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<DoctorView1> objlst = new List<DoctorView1>();

            while (dr.Read())
            {
                DoctorView1 objAreaList = new DoctorView1();
                objAreaList.DoctorId = objBaseSqlManager.GetInt32(dr, "DoctorId");
                objAreaList.DoctorName = objBaseSqlManager.GetTextValue(dr, "Doctor");
                objlst.Add(objAreaList);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<AreaViewModel> GetAreaListForDropDown()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAreaListForDropDown";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<AreaViewModel> objlst = new List<AreaViewModel>();

            while (dr.Read())
            {
                AreaViewModel objRetailerList = new AreaViewModel();
                objRetailerList.AreaID = objBaseSqlManager.GetInt32(dr, "AreaID");
                objRetailerList.AreaName = objBaseSqlManager.GetTextValue(dr, "AreaName");
                int a = objlst.Where(x => x.AreaName == objRetailerList.AreaName).Count();
                if (a == 0)
                {
                    objlst.Add(objRetailerList);
                }
               
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<SubAreaViewModels> GetSubAreaListForDropDown()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetallSubAreaListDropDown";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<SubAreaViewModels> objlst = new List<SubAreaViewModels>();

            while (dr.Read())
            {
                SubAreaViewModels objRetailerList = new SubAreaViewModels();
                objRetailerList.AreaID = objBaseSqlManager.GetInt32(dr, "AreaID");
                objRetailerList.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                int a = objlst.Where(x => x.SubAreaName == objRetailerList.SubAreaName).Count();
                if (a == 0)
                {
                    objlst.Add(objRetailerList);
                }

            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<RetailerViewModel> GetRetailerListForDropDown()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetRetailerListForDropDown";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<RetailerViewModel> objlst = new List<RetailerViewModel>();

            while (dr.Read())
            {
                RetailerViewModel objRetailerList = new RetailerViewModel();
                objRetailerList.ReatailerId = objBaseSqlManager.GetInt32(dr, "ReatailerId");
                objRetailerList.FullName = objBaseSqlManager.GetTextValue(dr, "FullName");
                objlst.Add(objRetailerList);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<ProductViewModel> GetProductListForDropDown()
        {  
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetProductListForDropDown";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<ProductViewModel> objlst = new List<ProductViewModel>();

            while (dr.Read())
            {
                ProductViewModel objproduct = new ProductViewModel();
                objproduct.ProductId = objBaseSqlManager.GetInt32(dr, "ProductId");
                objproduct.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objlst.Add(objproduct);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<ProductCaseResponse> GetProductCaseListForDropDown()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetProductCaseListForDropDown";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<ProductCaseResponse> objlst = new List<ProductCaseResponse>();

            while (dr.Read())
            {
                ProductCaseResponse objproduct = new ProductCaseResponse();
                objproduct.ProductCaseId = objBaseSqlManager.GetInt32(dr, "ProductCaseId");
                objproduct.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objlst.Add(objproduct);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<UserListViewModel> GetUserListForGroup()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetUserListForGroup";           
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<UserListViewModel> objlst = new List<UserListViewModel>();

            while (dr.Read())
            {
                UserListViewModel objuser = new UserListViewModel();
                objuser.Id = objBaseSqlManager.GetInt32(dr, "Id");
                objuser.FullName = objBaseSqlManager.GetTextValue(dr, "FullName");
                objlst.Add(objuser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<UserListViewModel> GetUserListForDropDown(int RoleId)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetUserListForDropDown";
            cmdGet.Parameters.AddWithValue("@RoleId", RoleId);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<UserListViewModel> objlst = new List<UserListViewModel>();

            while (dr.Read())
            {
                UserListViewModel objuser = new UserListViewModel();
                objuser.Id = objBaseSqlManager.GetInt32(dr, "Id");
                objuser.FullName = objBaseSqlManager.GetTextValue(dr, "FullName");
                objlst.Add(objuser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<LeaveTypeResponse> GetLeaveListForDropDown()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetLeaveListForDropDown";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<LeaveTypeResponse> objlst = new List<LeaveTypeResponse>();

            while (dr.Read())
            {
                LeaveTypeResponse objuser = new LeaveTypeResponse();
                objuser.LeaveTypeId = objBaseSqlManager.GetInt32(dr, "LeaveTypeId");
                objuser.LeaveType = objBaseSqlManager.GetTextValue(dr, "LeaveType");
                objlst.Add(objuser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<GroupNameViewModel> GetGroupListForDropDown()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetGroupListForDropDown";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<GroupNameViewModel> objlst = new List<GroupNameViewModel>();

            while (dr.Read())
            {
                GroupNameViewModel objuser = new GroupNameViewModel();
                objuser.GroupID = objBaseSqlManager.GetInt32(dr, "GroupID");
                objuser.GroupName = objBaseSqlManager.GetTextValue(dr, "GroupName");
                objlst.Add(objuser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<ProductTypeViewModel> GetProductTyepeListForDropDown()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetProductTyepeListForDropDown";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<ProductTypeViewModel> objlst = new List<ProductTypeViewModel>();

            while (dr.Read())
            { 
                ProductTypeViewModel objuser = new ProductTypeViewModel();
                objuser.ProductTypeId = objBaseSqlManager.GetInt32(dr, "ProductTypeId");
                objuser.ProductType = objBaseSqlManager.GetTextValue(dr, "ProductType");
                objlst.Add(objuser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public List<ExpenseViewModel> GetExpenseListForDropDown()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetExpenseListForDropDown";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<ExpenseViewModel> objlst = new List<ExpenseViewModel>();

            while (dr.Read())
            {
                ExpenseViewModel objuser = new ExpenseViewModel();
                objuser.ExpenserId = objBaseSqlManager.GetInt32(dr, "ExpenserId");
                objuser.ExpenserName = objBaseSqlManager.GetTextValue(dr, "ExpenserName");
                objlst.Add(objuser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        public LoginResponse GetUserDetails(string UserId)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetUserDetails";
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            LoginResponse obj = new LoginResponse();

            while (dr.Read())
            {

                obj.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                obj.UserName = objBaseSqlManager.GetTextValue(dr, "UserName");
                obj.UserEmail = objBaseSqlManager.GetTextValue(dr, "EmailID");
                obj.RoleName = objBaseSqlManager.GetTextValue(dr, "RoleName");
                obj.UserFullName = objBaseSqlManager.GetTextValue(dr, "FullName");
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return obj;
        }

        public LoginResponse GetUserDetailsForReports(string UserId)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetUserDetailsForReports";
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            LoginResponse obj = new LoginResponse();

            while (dr.Read())
            {

                obj.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                obj.UserName = objBaseSqlManager.GetTextValue(dr, "UserName");
                obj.UserEmail = objBaseSqlManager.GetTextValue(dr, "EmailID");
                obj.RoleName = objBaseSqlManager.GetTextValue(dr, "RoleName");
                obj.DirectorID = objBaseSqlManager.GetInt32(dr, "DirectorID");
                obj.HOID = objBaseSqlManager.GetInt32(dr, "HOID");
                obj.VPID = objBaseSqlManager.GetInt32(dr, "VPID");
                obj.NSMID = objBaseSqlManager.GetInt32(dr, "NSMID");
                obj.ZSMID = objBaseSqlManager.GetInt32(dr, "ZSMID");
                obj.RSMID = objBaseSqlManager.GetInt32(dr, "RSMID");
                obj.ASMID = objBaseSqlManager.GetInt32(dr, "ASMID");
                obj.MRID = objBaseSqlManager.GetInt32(dr, "MRID");                
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return obj;
        }

        public List<SelectListItem> GetUserPosition()
        {
            Array values = Enum.GetValues(typeof(UserPosition));
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var i in values)
            {
                if (Enum.GetName(typeof(UserPosition), i) != "FARMER")
                {
                    SelectListItem listItem = new SelectListItem
                    {
                        Text = Enum.GetName(typeof(UserPosition), i),
                        Value = ((int)i).ToString(),
                    };
                    items.Add(listItem);
                }
            }

            return items;
        }

        public List<Register> GetUsersByRoleID(long RoleID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetUsersByRoleID";
            cmdGet.Parameters.AddWithValue("@RoleID", RoleID);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Register> objlstDealer = new List<Register>();
            while (dr.Read())
            {
                Register objUser = new Register();
                objUser.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                objUser.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");
                objlstDealer.Add(objUser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlstDealer;
        }

        public List<Register> GetUsersByParentID(long RoleID,long DirectorID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetUsersByDirectorID";
            cmdGet.Parameters.AddWithValue("@RoleID", RoleID);
            cmdGet.Parameters.AddWithValue("@UserID", DirectorID);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Register> objlstDealer = new List<Register>();
            while (dr.Read())
            {
                Register objUser = new Register();
                objUser.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                objUser.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");
                objlstDealer.Add(objUser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlstDealer;
        }
        
        //public List<Register> GetVPByDirectorID(long DirectorID, long RoleID)
        //{
        //    SqlCommand cmdGet = new SqlCommand();
        //    BaseSqlManager objBaseSqlManager = new BaseSqlManager();
        //    cmdGet.CommandType = CommandType.StoredProcedure;
        //    cmdGet.CommandText = "GetVPByDirectorID";
        //    cmdGet.Parameters.AddWithValue("@DirectorID", DirectorID);
        //    cmdGet.Parameters.AddWithValue("@RoleID", RoleID);
        //    SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
        //    List<Register> objlstDealer = new List<Register>();
        //    while (dr.Read())
        //    {
        //        Register objUser = new Register();
        //        objUser.UserID = objBaseSqlManager.GetInt32(dr, "Id");
        //        objUser.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");

        //        objlstDealer.Add(objUser);
        //    }
        //    dr.Close();
        //    objBaseSqlManager.ForceCloseConnection();
        //    return objlstDealer;
        //}

       
        //public List<Register> GetZSMByVPID(long DirectorID, long VPID, long RoleID)
        //{
        //    SqlCommand cmdGet = new SqlCommand();
        //    BaseSqlManager objBaseSqlManager = new BaseSqlManager();
        //    cmdGet.CommandType = CommandType.StoredProcedure;
        //    cmdGet.CommandText = "GetZSMByVPID";
        //    cmdGet.Parameters.AddWithValue("@DirectorID", DirectorID);
        //    cmdGet.Parameters.AddWithValue("@VPID", VPID);
        //    cmdGet.Parameters.AddWithValue("@RoleID", RoleID);
        //    SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
        //    List<Register> objlstDealer = new List<Register>();
        //    while (dr.Read())
        //    {
        //        Register objUser = new Register();
        //        objUser.UserID = objBaseSqlManager.GetInt32(dr, "Id");
        //        objUser.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");

        //        objlstDealer.Add(objUser);
        //    }
        //    dr.Close();
        //    objBaseSqlManager.ForceCloseConnection();
        //    return objlstDealer;
        //}




        //public List<Register> GetRSMByZSMID(long DirectorID, long VPID, long ZSMID, long RoleID)
        //{
        //    SqlCommand cmdGet = new SqlCommand();
        //    BaseSqlManager objBaseSqlManager = new BaseSqlManager();
        //    cmdGet.CommandType = CommandType.StoredProcedure;
        //    cmdGet.CommandText = "GetRSMByZSMID";
        //    cmdGet.Parameters.AddWithValue("@DirectorID", DirectorID);
        //    cmdGet.Parameters.AddWithValue("@VPID", VPID);
        //    cmdGet.Parameters.AddWithValue("@ZSMID", ZSMID);
        //    cmdGet.Parameters.AddWithValue("@RoleID", RoleID);
        //    SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
        //    List<Register> objlstDealer = new List<Register>();
        //    while (dr.Read())
        //    {
        //        Register objUser = new Register();
        //        objUser.UserID = objBaseSqlManager.GetInt32(dr, "Id");
        //        objUser.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");

        //        objlstDealer.Add(objUser);
        //    }
        //    dr.Close();
        //    objBaseSqlManager.ForceCloseConnection();
        //    return objlstDealer;
        //}


        //public List<Register> GetASMByRSMID(long DirectorID, long VPID, long ZSMID, long RSMID, long RoleID)
        //{
        //    SqlCommand cmdGet = new SqlCommand();
        //    BaseSqlManager objBaseSqlManager = new BaseSqlManager();
        //    cmdGet.CommandType = CommandType.StoredProcedure;
        //    cmdGet.CommandText = "GetASMByRSMID";
        //    cmdGet.Parameters.AddWithValue("@DirectorID", DirectorID);
        //    cmdGet.Parameters.AddWithValue("@VPID", VPID);
        //    cmdGet.Parameters.AddWithValue("@ZSMID", ZSMID);
        //    cmdGet.Parameters.AddWithValue("@RSMID", RSMID);
        //    cmdGet.Parameters.AddWithValue("@RoleID", RoleID);
        //    SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
        //    List<Register> objlstDealer = new List<Register>();
        //    while (dr.Read())
        //    {
        //        Register objUser = new Register();
        //        objUser.UserID = objBaseSqlManager.GetInt32(dr, "Id");
        //        objUser.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");

        //        objlstDealer.Add(objUser);
        //    }
        //    dr.Close();
        //    objBaseSqlManager.ForceCloseConnection();
        //    return objlstDealer;
        //}

        public List<Register> GetHOByDirectorID(long DirectorID, long RoleID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetHOByDirectorID";
            cmdGet.Parameters.AddWithValue("@DirectorID", DirectorID);
            cmdGet.Parameters.AddWithValue("@RoleID", RoleID);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Register> objlstDealer = new List<Register>();
            while (dr.Read())
            {
                Register objUser = new Register();
                objUser.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                objUser.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");

                objlstDealer.Add(objUser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlstDealer;
        }

        public List<Register> GetVPByHOID(long DirectorID, long HOID, long RoleID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetVPByHOID";
            cmdGet.Parameters.AddWithValue("@DirectorID", DirectorID);
            cmdGet.Parameters.AddWithValue("@HOID", HOID);
            cmdGet.Parameters.AddWithValue("@RoleID", RoleID);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Register> objlstDealer = new List<Register>();
            while (dr.Read())
            {
                Register objUser = new Register();
                objUser.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                objUser.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");

                objlstDealer.Add(objUser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlstDealer;
        }

        public List<Register> GetNSMByVPID(long DirectorID, long HOID, long VPID, long RoleID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetNSMByVPID";
            cmdGet.Parameters.AddWithValue("@DirectorID", DirectorID);
            cmdGet.Parameters.AddWithValue("@HOID", HOID);
            cmdGet.Parameters.AddWithValue("@VPID", VPID);
            cmdGet.Parameters.AddWithValue("@RoleID", RoleID);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Register> objlstDealer = new List<Register>();
            while (dr.Read())
            {
                Register objUser = new Register();
                objUser.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                objUser.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");

                objlstDealer.Add(objUser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlstDealer;
        }

        public List<Register> GetZSMByNSMID(long DirectorID, long HOID, long VPID, long NSMID, long RoleID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetZSMByNSMID";
            cmdGet.Parameters.AddWithValue("@DirectorID", DirectorID);
            cmdGet.Parameters.AddWithValue("@HOID", HOID);
            cmdGet.Parameters.AddWithValue("@VPID", VPID);
            cmdGet.Parameters.AddWithValue("@NSMID", NSMID);
            cmdGet.Parameters.AddWithValue("@RoleID", RoleID);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Register> objlstDealer = new List<Register>();
            while (dr.Read())
            {
                Register objUser = new Register();
                objUser.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                objUser.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");

                objlstDealer.Add(objUser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlstDealer;
        }

        public List<Register> GetRSMByZSMID(long DirectorID, long HOID, long VPID, long NSMID, long ZSMID, long RoleID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetRSMByZSMID";
            cmdGet.Parameters.AddWithValue("@DirectorID", DirectorID);
            cmdGet.Parameters.AddWithValue("@HOID", HOID);
            cmdGet.Parameters.AddWithValue("@VPID", VPID);
            cmdGet.Parameters.AddWithValue("@NSMID", NSMID);
            cmdGet.Parameters.AddWithValue("@ZSMID", ZSMID);
            cmdGet.Parameters.AddWithValue("@RoleID", RoleID);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Register> objlstDealer = new List<Register>();
            while (dr.Read())
            {
                Register objUser = new Register();
                objUser.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                objUser.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");

                objlstDealer.Add(objUser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlstDealer;
        }

        public List<Register> GetASMByRSMID(long DirectorID, long HOID, long VPID, long NSMID, long ZSMID, long RSMID, long RoleID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetASMByRSMID";
            cmdGet.Parameters.AddWithValue("@DirectorID", DirectorID);
            cmdGet.Parameters.AddWithValue("@HOID", HOID);
            cmdGet.Parameters.AddWithValue("@VPID", VPID);
            cmdGet.Parameters.AddWithValue("@NSMID", NSMID);
            cmdGet.Parameters.AddWithValue("@ZSMID", ZSMID);
            cmdGet.Parameters.AddWithValue("@RSMID", RSMID);
            cmdGet.Parameters.AddWithValue("@RoleID", RoleID);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Register> objlstDealer = new List<Register>();
            while (dr.Read())
            {
                Register objUser = new Register();
                objUser.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                objUser.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");

                objlstDealer.Add(objUser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlstDealer;
        }

        #region Save & Update Notification
        public static bool SaveNotification(NotificationMaster obj)
        {
            using (AllurionEntities db = new AllurionEntities())
            {
                try
                {
                    if (obj.NotificationID == 0)
                    {

                        obj.CreatedDate = DateTime.Now;
                        obj.IsActive = true;
                        obj.IsRead = false;
                        db.NotificationMasters.Add(obj);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        NotificationMaster objnot = db.NotificationMasters.Where(i => i.NotificationID == obj.NotificationID).FirstOrDefault();
                        if (objnot != null)
                        {
                            objnot.NotificationTitle = obj.NotificationTitle;
                            objnot.NotificationMessage = obj.NotificationMessage;

                            db.Entry(objnot).State = EntityState.Modified;
                            db.SaveChanges();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                }
                catch
                {
                    return false;
                }
            }
        }
        #endregion

        #region Get User List
        public static List<User> GetUserList()
        {
            List<User> Users = new List<User>();
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "SelectAllUser";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<User> lstUSer = new List<User>();
            //Users objuser = new Users();

            while (dr.Read())
            {
                User obj = new User();
                obj.UserRoleID = objBaseSqlManager.GetInt64(dr, "UserRoleID");
                obj.InstanceID = objBaseSqlManager.GetTextValue(dr, "InstanceID");

                Users.Add(obj);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstUSer;
            //ForceCloseConnection();
            //return Users;
        }
        #endregion

        #region Get ExpenseLimit List
        public List<ExpenseLimit> ExpenseLimitList(int RoleId)
        {

            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetExpenseLimitList";
            cmdGet.Parameters.AddWithValue("@RoleId", RoleId);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<ExpenseLimit> lstExpense = new List<ExpenseLimit>();
            while (dr.Read())
            {
                ExpenseLimit obj = new ExpenseLimit();
                obj.Id = objBaseSqlManager.GetInt32(dr, "Id");
                //obj.UserRoleID = objBaseSqlManager.GetInt32(dr, "UserRoleID");
                obj.FirstName = objBaseSqlManager.GetTextValue(dr, "FirstName");
                obj.LastName = objBaseSqlManager.GetTextValue(dr, "LastName");
                obj.City = objBaseSqlManager.GetTextValue(dr, "City");
                obj.State = objBaseSqlManager.GetTextValue(dr, "State");
                obj.ExpensesLimit = objBaseSqlManager.GetInt32(dr, "ExpensesLimit");
                lstExpense.Add(obj);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstExpense;

        }
        #endregion

        public void UpadateExpenseLimit(List<ExpenseLimit> data)
        {
            foreach (var item in data)
            {
                try
                {
                    SqlCommand cmdGet = new SqlCommand();
                    BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                    cmdGet.CommandType = CommandType.StoredProcedure;
                    cmdGet.CommandText = "UpdateExpenses";
                    cmdGet.Parameters.AddWithValue("@UserID", item.Id);
                    cmdGet.Parameters.AddWithValue("@ExpensesLimit", item.ExpensesLimit);

                    int dr = objBaseSqlManager.ExecuteNonQuery(cmdGet);
                    objBaseSqlManager.ForceCloseConnection();
                }
                catch
                {

                }
            }
        }

        public void UpadateDCRBlockng(List<Register> data)
        {
            foreach (var item in data)
            {
                try
                {
                    SqlCommand cmdGet = new SqlCommand();
                    BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                    cmdGet.CommandType = CommandType.StoredProcedure;
                    cmdGet.CommandText = "UpdateDCRBlocking";
                    cmdGet.Parameters.AddWithValue("@UserID", item.Id);
                    cmdGet.Parameters.AddWithValue("@DCRBlocking", item.DCRBlocking);

                    int dr = objBaseSqlManager.ExecuteNonQuery(cmdGet);
                    objBaseSqlManager.ForceCloseConnection();
                }
                catch
                {

                }
            }
        }

        #region Get Target
        public List<Target> TargetList(Target model)
        {

            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetSalesTarget";
            cmdGet.Parameters.AddWithValue("@RoleId", model.UserRoleID);
            cmdGet.Parameters.AddWithValue("@AreaID", model.AreaID);
            cmdGet.Parameters.AddWithValue("@SubAreaID", model.SubAreaID);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Target> lstTarget = new List<Target>();
            while (dr.Read())
            {
                Target obj = new Target();
                obj.Id = objBaseSqlManager.GetInt32(dr, "Id");
                //obj.UserRoleID = objBaseSqlManager.GetInt32(dr, "UserRoleID");
                obj.FirstName = objBaseSqlManager.GetTextValue(dr, "FirstName");
                obj.LastName = objBaseSqlManager.GetTextValue(dr, "LastName");
                obj.AreaName = objBaseSqlManager.GetTextValue(dr, "AreaName");
                obj.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                obj.City = objBaseSqlManager.GetTextValue(dr, "City");
                obj.State = objBaseSqlManager.GetTextValue(dr, "State");
                obj.SalesTarget = objBaseSqlManager.GetInt32(dr, "SalesTarget");
                lstTarget.Add(obj);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstTarget;

        }
        #endregion

        public void UpadateTarget(List<Target> data)
        {
            foreach (var item in data)
            {
                try
                {
                    SqlCommand cmdGet = new SqlCommand();
                    BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                    cmdGet.CommandType = CommandType.StoredProcedure;
                    cmdGet.CommandText = "UpdateTarget";
                    cmdGet.Parameters.AddWithValue("@UserID", item.Id);
                    cmdGet.Parameters.AddWithValue("@SalesTarget", item.SalesTarget);

                    int dr = objBaseSqlManager.ExecuteNonQuery(cmdGet);
                    objBaseSqlManager.ForceCloseConnection();
                }
                catch
                {

                }
            }
        }

        public void UpadateUserData(UploadUserData ObjUploaddata)
        {
            try
            {
                SqlCommand cmdGet = new SqlCommand();
                BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                cmdGet.CommandType = CommandType.StoredProcedure;
                cmdGet.CommandText = "UpadateDoctorData";
                cmdGet.Parameters.AddWithValue("@DoctorCode", ObjUploaddata.DoctorCode);
                cmdGet.Parameters.AddWithValue("@Dob", ObjUploaddata.BirthDate);
                cmdGet.Parameters.AddWithValue("@Anniversary", ObjUploaddata.DOW);
                int dr = objBaseSqlManager.ExecuteNonQuery(cmdGet);
                objBaseSqlManager.ForceCloseConnection();
            }
            catch
            {

            }
        }

        public void UpadateRetailerData(UploadRetailerData objuplaoadRetailer)
        {
            try
            {
                SqlCommand cmdGet = new SqlCommand();
                BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                cmdGet.CommandType = CommandType.StoredProcedure;
                cmdGet.CommandText = "UpadateRetailerData";
                cmdGet.Parameters.AddWithValue("@FullName", objuplaoadRetailer.FullName);
                cmdGet.Parameters.AddWithValue("@Dob", objuplaoadRetailer.Dob);
                cmdGet.Parameters.AddWithValue("@Anniversary", objuplaoadRetailer.Anniversary);
                int dr = objBaseSqlManager.ExecuteNonQuery(cmdGet);
                objBaseSqlManager.ForceCloseConnection();
            }
            catch
            {

            }
        }

        public List<SalesTargetResponse> GetSalesReport()
        {
            List<SalesTargetResponse> objects = new List<SalesTargetResponse>();
            return objects;
        }
        public long GetProductID(long CaseName, string ProDuctName)
        {
            using (AllurionEntities Context = new AllurionEntities())
            {
                var data = Context.ProductCase_Mst.Where(i => CaseName == CaseName && i.ProductName == ProDuctName).ToList();
                return data[0].ProductCaseId;
            }
        }


        public long InsertTargate(Productallocation data)
        {
            using (AllurionEntities Context = new AllurionEntities())
            {
                var Exist = Context.Productallocations.Where(i => i.UserID == data.UserID && i.ProductCaseId == data.ProductCaseId && i.CaseID == data.CaseID).FirstOrDefault();
                if (Exist == null)
                {
                    Context.Productallocations.Add(data);
                    Context.SaveChanges();
                    return data.Id;
                }
                else
                {
                    Exist.Jan = data.Jan == 0 ? Exist.Jan : data.Jan;
                    Exist.Feb = data.Feb == 0 ? Exist.Feb : data.Feb;
                    Exist.Mar = data.Mar == 0 ? Exist.Mar : data.Mar;
                    Exist.Apr = data.Apr == 0 ? Exist.Apr : data.Apr;
                    Exist.May = data.May == 0 ? Exist.May : data.May;
                    Exist.Jun = data.Jun == 0 ? Exist.Jun : data.Jun;
                    Exist.July = data.July == 0 ? Exist.July : data.July;
                    Exist.Aug = data.Aug == 0 ? Exist.Aug : data.Aug;
                    Exist.Sep = data.Sep == 0 ? Exist.Sep : data.Sep;
                    Exist.Oct = data.Oct == 0 ? Exist.Oct : data.Oct;
                    Exist.Nov = data.Nov == 0 ? Exist.Nov : data.Nov;
                    Exist.Dec = data.Dec == 0 ? Exist.Dec : data.Dec;
                    Context.Entry(Exist).State = EntityState.Modified;
                    Context.SaveChanges();
                    return data.Id;

                }

            }
        }

        public List<ProductCaseMst_mst> GetAllProductallocationList(string Rxtype)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllProductallocationList";
            cmdGet.Parameters.AddWithValue("@CaseName", Rxtype);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<ProductCaseMst_mst> objlst = new List<ProductCaseMst_mst>();

            int Srno = 1;
            while (dr.Read())
            {
                ProductCaseMst_mst objProduct = new ProductCaseMst_mst();
                objProduct.ProductId = objBaseSqlManager.GetInt32(dr, "ProductId");
                objProduct.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objProduct.MeasureIn = objBaseSqlManager.GetTextValue(dr, "MeasureIn");
                objProduct.StrangthIn = objBaseSqlManager.GetTextValue(dr, "StrangthIn");
                objProduct.PTS = objBaseSqlManager.GetDecimal(dr, "PTS");
                objProduct.year = System.DateTime.Now.Year.ToString();
                objProduct.SrNo = Srno;
                Srno++;
                #region MyRegion

                //objProduct.CategoryID = objBaseSqlManager.GetInt64(dr, "CategoryID");
                //objProduct.CategoryName = objBaseSqlManager.GetTextValue(dr, "CategoryName");
                //objProduct.GodownID = objBaseSqlManager.GetInt64(dr, "GodownID");
                //objProduct.GodownName = objBaseSqlManager.GetTextValue(dr, "GodownName");
                //objProduct.ProductPrice = objBaseSqlManager.GetDecimal(dr, "ProductPrice");
                //objProduct.UnitID = objBaseSqlManager.GetInt64(dr, "UnitID");
                //objProduct.PouchID = objBaseSqlManager.GetInt64(dr, "PouchID");
                //objProduct.UnitCode = objBaseSqlManager.GetTextValue(dr, "UnitCode");
                //objProduct.ProductDescription = objBaseSqlManager.GetTextValue(dr, "ProductDescription");
                //objProduct.HSNNumber = objBaseSqlManager.GetTextValue(dr, "HSNNumber");
                //objProduct.SGST = objBaseSqlManager.GetDecimal(dr, "SGST");
                //objProduct.CGST = objBaseSqlManager.GetDecimal(dr, "CGST");
                //objProduct.IGST = objBaseSqlManager.GetDecimal(dr, "IGST");
                //objProduct.HFor = objBaseSqlManager.GetDecimal(dr, "HFor");
                //objProduct.IsDelete = objBaseSqlManager.GetBoolean(dr, "IsDelete"); 
                #endregion
                objlst.Add(objProduct);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }

        #region Notification List
        public List<NotificationMaster> GetNotificationList()
        {
            List<NotificationMaster> Nationalitylst = new List<NotificationMaster>();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();

            SqlCommand cmdGet = new SqlCommand();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetallNotificationListfornotification";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);

            while (dr.Read())
            {
                NotificationMaster obj = new NotificationMaster();
                obj.NotificationID = objBaseSqlManager.GetInt64(dr, "NotificationID");
                obj.NotificationTitle = objBaseSqlManager.GetTextValue(dr, "NotificationTitle");
                obj.NotificationMessage = objBaseSqlManager.GetTextValue(dr, "NotificationMessage");
                obj.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                Nationalitylst.Add(obj);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return Nationalitylst;
        }
        #endregion

        public DashboardCounts DashboardCount(long UserID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "DachboardCount";
            cmdGet.Parameters.AddWithValue("@UserID", UserID);
            DashboardCounts objcount = new DashboardCounts();
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            while (dr.Read())
            {
                objcount.DCR = objBaseSqlManager.GetInt32(dr, "DCR");
                objcount.Doctors = objBaseSqlManager.GetInt32(dr, "Doctor");
                objcount.Retailer = objBaseSqlManager.GetInt32(dr, "Retailer");
                objcount.Product = objBaseSqlManager.GetInt32(dr, "Product");
                objcount.DCRMonthly = objBaseSqlManager.GetInt32(dr, "DCRMonthly");
                objcount.DoctorsMonthly = objBaseSqlManager.GetInt32(dr, "DoctorMonthly");
                objcount.RetailerMonthly = objBaseSqlManager.GetInt32(dr, "RetailerMonthly");
                objcount.ProductMonthly = objBaseSqlManager.GetInt32(dr, "ProductMonthly");

                objcount.Target = objBaseSqlManager.GetInt32(dr, "Target");
                objcount.Achieved = objBaseSqlManager.GetInt32(dr, "Achieved");
                if (objcount.Achieved == null)
                {
                    objcount.Achieved = 0;
                }
                if (objcount.Target == null)
                {
                    objcount.Target = 0;
                }
                objcount.RejectedLeave = objBaseSqlManager.GetInt32(dr, "RejectedLeaves");
                objcount.ApprovedLeave = objBaseSqlManager.GetInt32(dr, "ApprovedLeaves");
                objcount.PendingLeave = objBaseSqlManager.GetInt32(dr, "PendingLeaves");
                objcount.TotalLeaves = objcount.RejectedLeave + objcount.ApprovedLeave + objcount.PendingLeave;
                objcount.TotalSales = objcount.Achieved + objcount.Target;
                objcount.TotalUsers = objcount.DCR + objcount.Doctors + objcount.Retailer;
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objcount;
        }

        public List<ProductCount> DashboardProductCount(long UserID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "DashboardProductCount";
            cmdGet.Parameters.AddWithValue("@UserID", UserID);

            List<ProductCount> lstCount = new List<ProductCount>();

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            while (dr.Read())
            {
                ProductCount objcount = new ProductCount();
                objcount.total = objBaseSqlManager.GetInt32(dr, "total");
                objcount.ProductCaseId = objBaseSqlManager.GetInt64(dr, "ProductCaseId");
                objcount.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objcount.months = objBaseSqlManager.GetTextValue(dr, "months");
                lstCount.Add(objcount);
            }

            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstCount;
        }

        public TarGetDetails GetTotalProductCountDashboard(long UserID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetTotalProductCountDashboard";
            cmdGet.Parameters.AddWithValue("@UserID", UserID);
            string currentYear = DateTime.Now.Year.ToString();
            cmdGet.Parameters.AddWithValue("@year", currentYear);


            TarGetDetails objcount = new TarGetDetails();

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            while (dr.Read())
            {
                objcount.TotalAchieved = objBaseSqlManager.GetInt32(dr, "Achievedtarget");
                objcount.TotalTarget = objBaseSqlManager.GetInt32(dr, "TotalAchiveMent");
            }

            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objcount;
        }

        public List<FieldUserTargetViewModel> GetFieldUserTargetReport()
        {
            List<FieldUserTargetViewModel> objects = new List<FieldUserTargetViewModel>();
            return objects;
        }

        public List<FieldUserViewModel> GetFieldUserReport()
        {
            List<FieldUserViewModel> objects = new List<FieldUserViewModel>();
            return objects;
        }

        public long GetLastEmployeeCode()
        {
            long EmpId = 0;
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetLastEmployeeCode";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            while (dr.Read())
            {
                EmpId = objBaseSqlManager.GetInt64(dr, "Id");
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return EmpId;
        }

    }
}
    

