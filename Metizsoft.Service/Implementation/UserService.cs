namespace Metizsoft.Service
{
    using Metizsoft.Data;
    using Metizsoft.Data.Modal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Metizsoft.Service.Models;

    public class UserService : IUser
    {

        public List<NotificationResponseData> GetNotificationID(string DeviceTokenID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetNotificationID";
            cmdGet.Parameters.AddWithValue("@DeviceTokenID", DeviceTokenID);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<NotificationResponseData> lstProduct = new List<NotificationResponseData>();
            while (dr.Read())
            {
                NotificationResponseData objresponse = new NotificationResponseData();


                DateTime twoweeksago = DateTime.Today.AddDays(-14);
                DateTime oneweekago = DateTime.Today.AddDays(-7);
                DateTime onedayago = DateTime.Today.AddDays(-1);
                DateTime today = DateTime.Now;
                DateTime afteroneday = DateTime.Today.AddDays(1);
                DateTime afterthreedays = DateTime.Today.AddDays(3);
                DateTime aftertwoweek = DateTime.Today.AddDays(14);
                DateTime afterfourweek = DateTime.Today.AddDays(30);
                DateTime aftersixweek = DateTime.Today.AddDays(42);
                DateTime aftereightweek = DateTime.Today.AddDays(56);
                DateTime aftertwelveweek = DateTime.Today.AddDays(84);
                DateTime afterfifteenweek = DateTime.Today.AddDays(109);
                DateTime aftersixteenweek = DateTime.Today.AddDays(112);
                DateTime aftertwofourweek = DateTime.Today.AddDays(168);


                DateTime InsDate = objBaseSqlManager.GetDateTime(dr, "InstallationDate");
                if (InsDate.Date == twoweeksago.Date)
                {
                    objresponse.UID = objBaseSqlManager.GetInt64(dr, "UID");
                    objresponse.DeviceTokenID = objBaseSqlManager.GetTextValue(dr, "DeviceTokenID");
                    objresponse.InstallationDate = objBaseSqlManager.GetDateTime(dr, "InstallationDate");
                    objresponse.NotificationType = 1;
                    lstProduct.Add(objresponse);
                }
                else if (InsDate.Date == oneweekago.Date)
                {
                    objresponse.UID = objBaseSqlManager.GetInt64(dr, "UID");
                    objresponse.DeviceTokenID = objBaseSqlManager.GetTextValue(dr, "DeviceTokenID");
                    objresponse.InstallationDate = objBaseSqlManager.GetDateTime(dr, "InstallationDate");
                    objresponse.NotificationType = 2;
                    lstProduct.Add(objresponse);
                }
                else if (InsDate.Date == onedayago.Date)
                {
                    objresponse.UID = objBaseSqlManager.GetInt64(dr, "UID");
                    objresponse.DeviceTokenID = objBaseSqlManager.GetTextValue(dr, "DeviceTokenID");
                    objresponse.InstallationDate = objBaseSqlManager.GetDateTime(dr, "InstallationDate");
                    objresponse.NotificationType = 3;
                    lstProduct.Add(objresponse);
                }
                else if (InsDate.Date == today.Date)
                {
                    objresponse.UID = objBaseSqlManager.GetInt64(dr, "UID");
                    objresponse.DeviceTokenID = objBaseSqlManager.GetTextValue(dr, "DeviceTokenID");
                    objresponse.InstallationDate = objBaseSqlManager.GetDateTime(dr, "InstallationDate");
                    objresponse.NotificationType = 4;
                    lstProduct.Add(objresponse);
                }
                else if (InsDate.Date == afteroneday.Date)
                {
                    objresponse.UID = objBaseSqlManager.GetInt64(dr, "UID");
                    objresponse.DeviceTokenID = objBaseSqlManager.GetTextValue(dr, "DeviceTokenID");
                    objresponse.InstallationDate = objBaseSqlManager.GetDateTime(dr, "InstallationDate");
                    objresponse.NotificationType = 5;
                    lstProduct.Add(objresponse);
                }
                else if (InsDate.Date == afterthreedays.Date)
                {
                    objresponse.UID = objBaseSqlManager.GetInt64(dr, "UID");
                    objresponse.DeviceTokenID = objBaseSqlManager.GetTextValue(dr, "DeviceTokenID");
                    objresponse.InstallationDate = objBaseSqlManager.GetDateTime(dr, "InstallationDate");
                    objresponse.NotificationType = 6;
                    lstProduct.Add(objresponse);
                }
                else if (InsDate.Date == aftertwoweek.Date)
                {
                    objresponse.UID = objBaseSqlManager.GetInt64(dr, "UID");
                    objresponse.DeviceTokenID = objBaseSqlManager.GetTextValue(dr, "DeviceTokenID");
                    objresponse.InstallationDate = objBaseSqlManager.GetDateTime(dr, "InstallationDate");
                    objresponse.NotificationType = 7;
                    lstProduct.Add(objresponse);
                }
                else if (InsDate.Date == afterfourweek.Date)
                {
                    objresponse.UID = objBaseSqlManager.GetInt64(dr, "UID");
                    objresponse.DeviceTokenID = objBaseSqlManager.GetTextValue(dr, "DeviceTokenID");
                    objresponse.InstallationDate = objBaseSqlManager.GetDateTime(dr, "InstallationDate");
                    objresponse.NotificationType = 8;
                    lstProduct.Add(objresponse);
                }
                else if (InsDate.Date == aftersixweek.Date)
                {
                    objresponse.UID = objBaseSqlManager.GetInt64(dr, "UID");
                    objresponse.DeviceTokenID = objBaseSqlManager.GetTextValue(dr, "DeviceTokenID");
                    objresponse.InstallationDate = objBaseSqlManager.GetDateTime(dr, "InstallationDate");
                    objresponse.NotificationType = 9;
                    lstProduct.Add(objresponse);
                }
                else if (InsDate.Date == aftereightweek.Date)
                {
                    objresponse.UID = objBaseSqlManager.GetInt64(dr, "UID");
                    objresponse.DeviceTokenID = objBaseSqlManager.GetTextValue(dr, "DeviceTokenID");
                    objresponse.InstallationDate = objBaseSqlManager.GetDateTime(dr, "InstallationDate");
                    objresponse.NotificationType = 10;
                    lstProduct.Add(objresponse);
                }
                else if (InsDate.Date == aftertwelveweek.Date)
                {
                    objresponse.UID = objBaseSqlManager.GetInt64(dr, "UID");
                    objresponse.DeviceTokenID = objBaseSqlManager.GetTextValue(dr, "DeviceTokenID");
                    objresponse.InstallationDate = objBaseSqlManager.GetDateTime(dr, "InstallationDate");
                    objresponse.NotificationType = 11;
                    lstProduct.Add(objresponse);
                }
                else if (InsDate.Date == afterfifteenweek.Date)
                {
                    objresponse.UID = objBaseSqlManager.GetInt64(dr, "UID");
                    objresponse.DeviceTokenID = objBaseSqlManager.GetTextValue(dr, "DeviceTokenID");
                    objresponse.InstallationDate = objBaseSqlManager.GetDateTime(dr, "InstallationDate");
                    objresponse.NotificationType = 12;
                    lstProduct.Add(objresponse);
                }
                else if (InsDate.Date == aftersixteenweek.Date)
                {
                    objresponse.UID = objBaseSqlManager.GetInt64(dr, "UID");
                    objresponse.DeviceTokenID = objBaseSqlManager.GetTextValue(dr, "DeviceTokenID");
                    objresponse.InstallationDate = objBaseSqlManager.GetDateTime(dr, "InstallationDate");
                    objresponse.NotificationType = 13;
                    lstProduct.Add(objresponse);
                }
                else if (InsDate.Date == aftertwofourweek.Date)
                {
                    objresponse.UID = objBaseSqlManager.GetInt64(dr, "UID");
                    objresponse.DeviceTokenID = objBaseSqlManager.GetTextValue(dr, "DeviceTokenID");
                    objresponse.InstallationDate = objBaseSqlManager.GetDateTime(dr, "InstallationDate");
                    objresponse.NotificationType = 14;
                    lstProduct.Add(objresponse);
                }
                else { }


            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProduct;
        }

        public UsersModel checklogin(RequestLogin Data)
        {
            UsersModel lstlogin = new UsersModel();
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "Userlogin";
            cmdGet.Parameters.AddWithValue("@username", Data.UserName.ToString());
            cmdGet.Parameters.AddWithValue("@password", Data.Password.ToString());

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            UsersModel objuser = new UsersModel();
            while (dr.Read())
            {
                objuser.Id = objBaseSqlManager.GetInt32(dr, "Id");
                objuser.UserName = objBaseSqlManager.GetTextValue(dr, "UserName");
                objuser.UserRoleID = objBaseSqlManager.GetInt64(dr, "UserRoleID");
                objuser.MobileNumber = objBaseSqlManager.GetTextValue(dr, "MobileNumber");
                objuser.EmailID = objBaseSqlManager.GetTextValue(dr, "EmailID");
                objuser.FirstName = objBaseSqlManager.GetTextValue(dr, "FirstName");
                objuser.LastName = objBaseSqlManager.GetTextValue(dr, "LastName");
                objuser.Address = objBaseSqlManager.GetTextValue(dr, "Address");
                // objuser.InstanceID = objBaseSqlManager.GetTextValue(dr, "InstanceID");
                objuser.DirectorID = objBaseSqlManager.GetInt64(dr, "DirectorID");
                objuser.ExpensesLimit = objBaseSqlManager.GetInt64(dr, "ExpensesLimit");
                objuser.SalesTarget = objBaseSqlManager.GetInt64(dr, "SalesTarget");
                objuser.VPID = objBaseSqlManager.GetInt64(dr, "VPID");
                objuser.NSMID = objBaseSqlManager.GetInt64(dr, "NSMID");
                objuser.HOID = objBaseSqlManager.GetInt64(dr, "HOID");
                objuser.ZSMID = objBaseSqlManager.GetInt64(dr, "ZSMID");
                objuser.RSMID = objBaseSqlManager.GetInt64(dr, "RSMID");
                objuser.ASMID = objBaseSqlManager.GetInt64(dr, "ASMID");
                objuser.MRID = objBaseSqlManager.GetInt64(dr, "MRID");
                objuser.City = objBaseSqlManager.GetInt32(dr, "City");
                objuser.State = objBaseSqlManager.GetInt64(dr, "State");
                objuser.PostCode = objBaseSqlManager.GetTextValue(dr, "PostCode");
                objuser.CaseName = objBaseSqlManager.GetInt32(dr, "CaseName");
                objuser.DCRBlocking = objBaseSqlManager.GetInt32(dr, "DCRBlocking");

                if (Data.InstanceID != null)
                {
                    SqlCommand cmdGet1 = new SqlCommand();
                    BaseSqlManager objBaseSqlManager1 = new BaseSqlManager();
                    cmdGet1.CommandType = CommandType.StoredProcedure;
                    cmdGet1.CommandText = "UpdateInstanceID";
                    cmdGet1.Parameters.AddWithValue("@Id", objuser.Id);
                    cmdGet1.Parameters.AddWithValue("@InstanceID", Data.InstanceID);
                    cmdGet1.Parameters.AddWithValue("@DeviceID", Data.DeviceID);
                    objBaseSqlManager1.ExecuteNonQuery(cmdGet1);
                    objBaseSqlManager1.ForceCloseConnection();
                }

            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            SqlCommand cmdGetUserRole = new SqlCommand();
            BaseSqlManager objBaseSqlManagerUserRole = new BaseSqlManager();
            cmdGetUserRole.CommandType = CommandType.StoredProcedure;
            cmdGetUserRole.CommandText = "GetUserByRoleLogin";
            cmdGetUserRole.Parameters.AddWithValue("@RoleID", objuser.UserRoleID);
            cmdGetUserRole.Parameters.AddWithValue("@UserID", objuser.Id);

            SqlDataReader drUserRole = objBaseSqlManagerUserRole.ExecuteDataReader(cmdGetUserRole);
            List<UserViewModel1> lstUserByRole1 = new List<UserViewModel1>();
            while (drUserRole.Read())
            {
                UserViewModel1 objresponseUser = new UserViewModel1();
                objresponseUser.Id = objBaseSqlManagerUserRole.GetInt32(drUserRole, "Id");
                objresponseUser.UserRoleID = objBaseSqlManagerUserRole.GetInt64(drUserRole, "UserRoleID");
                objresponseUser.FirstName = objBaseSqlManagerUserRole.GetTextValue(drUserRole, "FirstName");
                objresponseUser.LastName = objBaseSqlManagerUserRole.GetTextValue(drUserRole, "LastName");
                objresponseUser.ParentId = objBaseSqlManagerUserRole.GetInt64(drUserRole, "ParentIdLs");
                objresponseUser.UserName = objresponseUser.FirstName + ' ' + objresponseUser.LastName;
                objresponseUser.ZSMID = objBaseSqlManagerUserRole.GetInt64(drUserRole, "ZSMID");
                objresponseUser.RSMID = objBaseSqlManagerUserRole.GetInt64(drUserRole, "RSMID");
                objresponseUser.ASMID = objBaseSqlManagerUserRole.GetInt64(drUserRole, "ASMID");
                objresponseUser.MRID = objBaseSqlManagerUserRole.GetInt64(drUserRole, "MRID");
                objresponseUser.SalesTarget = objBaseSqlManagerUserRole.GetInt64(drUserRole, "SalesTarget");
               // objresponseUser.AchievedTarget = objBaseSqlManager.GetInt64(drUserRole, "AchievedTarget");
                lstUserByRole1.Add(objresponseUser);
            }
            drUserRole.Close();
            objBaseSqlManager.ForceCloseConnection();

            SqlCommand cmdGetUserR = new SqlCommand();
            BaseSqlManager objBaseSqlManagerUserR = new BaseSqlManager();
            cmdGetUserR.CommandType = CommandType.StoredProcedure;
            cmdGetUserR.CommandText = "GetUserByRole";
            cmdGetUserR.Parameters.AddWithValue("@RoleID", 4);
            if (objuser.UserRoleID == 4)
            {
                cmdGetUserR.Parameters.AddWithValue("@UserID", objuser.Id);
            }
            else
            {
                cmdGetUserR.Parameters.AddWithValue("@UserID", objuser.NSMID);
            }

            SqlDataReader drUserR = objBaseSqlManagerUserR.ExecuteDataReader(cmdGetUserR);
            List<UserViewModel1> lstUserByRoleList = new List<UserViewModel1>();
            while (drUserR.Read())
            {
                UserViewModel1 objresponseUser = new UserViewModel1();
                objresponseUser.Id = objBaseSqlManagerUserR.GetInt32(drUserR, "Id");
                objresponseUser.UserRoleID = objBaseSqlManagerUserR.GetInt64(drUserR, "UserRoleID");
                objresponseUser.FirstName = objBaseSqlManagerUserR.GetTextValue(drUserR, "FirstName");
                objresponseUser.LastName = objBaseSqlManagerUserR.GetTextValue(drUserR, "LastName");
                objresponseUser.ParentId = objBaseSqlManagerUserR.GetInt64(drUserR, "ParentId");
                objresponseUser.UserName = objresponseUser.FirstName + ' ' + objresponseUser.LastName;
                objresponseUser.ZSMID = objBaseSqlManagerUserR.GetInt64(drUserR, "ZSMID");
                objresponseUser.RSMID = objBaseSqlManagerUserR.GetInt64(drUserR, "RSMID");
                objresponseUser.ASMID = objBaseSqlManagerUserR.GetInt64(drUserR, "ASMID");
                objresponseUser.MRID = objBaseSqlManagerUserR.GetInt64(drUserR, "MRID");
                objresponseUser.SalesTarget = objBaseSqlManagerUserR.GetInt64(drUserR, "SalesTarget");
                // objresponseUser.AchievedTarget = objBaseSqlManager.GetInt64(drUserRole, "AchievedTarget");
                lstUserByRoleList.Add(objresponseUser);
            }
            drUserR.Close();
            objBaseSqlManager.ForceCloseConnection();
            objuser.lstUserRoleList = lstUserByRoleList;
            objuser.lstUserList = lstUserByRole1;
            return objuser;
        }
        
        public bool Addtrackerinfo(Tracker_Mst Data)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                try
                {
                    context.Tracker_Mst.Add(Data);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<UserViewModel> GetUserRoleUserList(long RoleId, long CurrentUserId)
        {
            //using (AllurionEntities context = new AllurionEntities())
            //{
            //    try
            //    {
            //        #region MyRegion
            //        //var lstdata = (from emp in context.Users
            //        //               select new usermodel()
            //        //               {
            //        //                   UserRoleID = emp.UserRoleID,
            //        //                   Id = emp.Id,
            //        //                   DirectorID = emp.DirectorID,
            //        //                   VPID = emp.VPID,
            //        //                   ZSMID = emp.ZSMID,
            //        //                   RSMID = emp.RSMID,
            //        //                   ASMID = emp.ASMID,
            //        //                   MRID = emp.MRID,
            //        //               }).Where(i => i.UserRoleID == RoleId && i.Id == CurrentUserId).ToList();


            //        //int DirectorID = Convert.ToInt32(lstdata[0].DirectorID);
            //        //int VPID = Convert.ToInt32(lstdata[0].VPID);
            //        //int ZSMID = Convert.ToInt32(lstdata[0].ZSMID);
            //        //int RSMID = Convert.ToInt32(lstdata[0].RSMID);
            //        //int ASMID = Convert.ToInt32(lstdata[0].ASMID);
            //        //int MRID = Convert.ToInt32(lstdata[0].MRID); 
            //        //#endregion

            //        var lstdata1 = context.Users.Where(s => s.ASMID == CurrentUserId).ToList();

            //        return lstdata1;
            //    }
            //    catch (Exception ex)
            //    {
            //        return null;
            //    }

            //}
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetUserByRole";
            cmdGet.Parameters.AddWithValue("@RoleID", RoleId);
            cmdGet.Parameters.AddWithValue("@UserID", CurrentUserId);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<UserViewModel> lstProduct = new List<UserViewModel>();
            while (dr.Read())
            {
                UserViewModel objresponse = new UserViewModel();
                objresponse.Id = objBaseSqlManager.GetInt32(dr, "Id");
                objresponse.UserRoleID = objBaseSqlManager.GetInt64(dr, "UserRoleID");
                objresponse.FirstName = objBaseSqlManager.GetTextValue(dr, "FirstName");
                objresponse.LastName = objBaseSqlManager.GetTextValue(dr, "LastName");
               objresponse.ParentId = objBaseSqlManager.GetInt64(dr, "ParentId");
               objresponse.UserName = objresponse.FirstName + ' ' + objresponse.LastName;
               objresponse.ZSMID = objBaseSqlManager.GetInt64(dr, "ZSMID");
               objresponse.RSMID = objBaseSqlManager.GetInt64(dr, "RSMID");
               objresponse.ASMID = objBaseSqlManager.GetInt64(dr, "ASMID");
               objresponse.MRID = objBaseSqlManager.GetInt64(dr, "MRID");

                lstProduct.Add(objresponse);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProduct;

        }

        public List<GetUserTargetdataByRole> GetUserTargetdataByRole(GetForTargate Data)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetUserTargetdataByRoleFortest";
            cmdGet.Parameters.AddWithValue("@RoleID", Data.RoleId);
            cmdGet.Parameters.AddWithValue("@UserID", Data.CurrentUserId);
            cmdGet.Parameters.AddWithValue("@CaseID", Data.ProductCaseId);
            cmdGet.Parameters.AddWithValue("@year", Data.Year);
            cmdGet.Parameters.AddWithValue("@Month", Data.Month);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<GetUserTargetdataByRole> lstProduct = new List<GetUserTargetdataByRole>();
            while (dr.Read())
            {
                GetUserTargetdataByRole objresponse = new GetUserTargetdataByRole();
                objresponse.Id = objBaseSqlManager.GetInt32(dr, "Id");
                objresponse.UserRoleID = objBaseSqlManager.GetInt64(dr, "UserRoleID");
                objresponse.FirstName = objBaseSqlManager.GetTextValue(dr, "FirstName");
                objresponse.LastName = objBaseSqlManager.GetTextValue(dr, "LastName");
                objresponse.ParentId = objBaseSqlManager.GetInt64(dr, "ParentId");
                objresponse.UserName = objresponse.FirstName + ' ' + objresponse.LastName;
                objresponse.ZSMID = objBaseSqlManager.GetInt64(dr, "ZSMID");
                objresponse.RSMID = objBaseSqlManager.GetInt64(dr, "RSMID");
                objresponse.ASMID = objBaseSqlManager.GetInt64(dr, "ASMID");
                objresponse.MRID = objBaseSqlManager.GetInt64(dr, "MRID");
                //objresponse.SalesTarget = objBaseSqlManager.GetInt64(dr, "SalesTarget");
                objresponse.SalesTarget = objBaseSqlManager.GetDecimal(dr, "TotaoSalesTargate");
                string kk = objBaseSqlManager.GetTextValue(dr, "AchievedTarget");
                objresponse.AchievedTarget = objBaseSqlManager.GetDecimal(dr, "AchievedTarget");
                decimal Total = Convert.ToDecimal(objBaseSqlManager.GetDecimal(dr, "TotalDoctorAchivment")) + Convert.ToDecimal(objBaseSqlManager.GetFloat(dr, "TotalRetailerAchivment"));
                objresponse.AchievedTarget = Total;
                //objresponse.AchievedTarget = objBaseSqlManager.GetInt64(dr, "AchievedTarget");

                lstProduct.Add(objresponse);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProduct;

        }

        public List<Tracker_MstModel> GetTrackListByUserID(string UserId)
        {  SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetTrackListByUserID";
            cmdGet.Parameters.AddWithValue("@AllUserId", UserId);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Tracker_MstModel> lstobjuser = new List<Tracker_MstModel>();
            while (dr.Read())
            {
                Tracker_MstModel objuser = new Tracker_MstModel();
                objuser.UserID = objBaseSqlManager.GetInt64(dr, "UserID");
                objuser.Lattitude = objBaseSqlManager.GetTextValue(dr, "Lattitude");
                objuser.longitude = objBaseSqlManager.GetTextValue(dr, "longitude");
                lstobjuser.Add(objuser);
            }    
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();

            return lstobjuser;
        }

        public List<ProductCaseMst_mstForAPI> GetAllProductlistOnAllocationFroDoctor(long CurrentUserId)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllProductlistOnAllocation";
            cmdGet.Parameters.AddWithValue("@CreatedBy", CurrentUserId);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<ProductCaseMst_mstForAPI> lstProduct = new List<ProductCaseMst_mstForAPI>();
            while (dr.Read())
            {
                ProductCaseMst_mstForAPI objProductemst = new ProductCaseMst_mstForAPI();
                objProductemst.ProductId = objBaseSqlManager.GetInt32(dr, "ProductCaseId"); 
                objProductemst.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                //objProductemst.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                //objProductemst.jan = objBaseSqlManager.GetFloat(dr, "jan");
                //objProductemst.Mar = objBaseSqlManager.GetFloat(dr, "Mar");
                //objProductemst.Feb = objBaseSqlManager.GetFloat(dr, "Feb");
                //objProductemst.Apr = objBaseSqlManager.GetFloat(dr, "Apr");
                //objProductemst.May = objBaseSqlManager.GetFloat(dr, "May");
                //objProductemst.Jun = objBaseSqlManager.GetFloat(dr, "Jun");
                //objProductemst.July = objBaseSqlManager.GetFloat(dr, "July");
                //objProductemst.Sep = objBaseSqlManager.GetFloat(dr, "Sep");
                //objProductemst.Oct = objBaseSqlManager.GetFloat(dr, "Oct");
                //objProductemst.Nov = objBaseSqlManager.GetFloat(dr, "Nov");
                //objProductemst.Dec = objBaseSqlManager.GetFloat(dr, "Dec");
                //objProductemst.year = objBaseSqlManager.GetTextValue(dr, "year");
                //  objProductemst.Active = objBaseSqlManager.GetBoolean(dr, "Active");
                lstProduct.Add(objProductemst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProduct;
        }
     
    }


    public class usermodel
    {
        public Nullable<long> DirectorID { get; set; }
        public Nullable<long> VPID { get; set; }
        public Nullable<long> ZSMID { get; set; }
        public Nullable<long> RSMID { get; set; }
        public Nullable<long> ASMID { get; set; }
        public Nullable<long> MRID { get; set; }
        public int Id { get; set; }
        public Nullable<long> UserRoleID { get; set; }
    

    }

}
