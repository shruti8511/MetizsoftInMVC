using Metizsoft.Data.Modal;
using Metizsoft.Data.ViewModal;
using Metizsoft.Service.Interface;
using Metizsoft.Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Service
{
    public class TourProgramService : ITourProgram
    {

        public List<TourPlanResponse> GetTourPlanVarianceHistory()
        {
            List<TourPlanResponse> objects = new List<TourPlanResponse>();
            return objects;
        }

        #region ADD Tour Program
        public bool AddTourProgram(TourProgram_Mst obj)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                try
                {
                    var data = context.TourProgram_Mst.FirstOrDefault(x => x.TourID == obj.TourID);

                    if (data == null)
                    {
                        context.TourProgram_Mst.Add(obj);
                        context.SaveChanges();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
        }
        #endregion

        #region Get Tourprogram Monthly list
        public List<TourProgram_MstModel> GetTourprogramMonthlylist(string Month, string Year, long CurrentUserId)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetTourprogramMonthlylist";
            cmdGet.Parameters.AddWithValue("@Createdby", CurrentUserId);
            cmdGet.Parameters.AddWithValue("@Month", Month);
            cmdGet.Parameters.AddWithValue("@Year", Year);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<TourProgram_MstModel> lstProduct = new List<TourProgram_MstModel>();
            while (dr.Read())
            {
                TourProgram_MstModel objProductresponse = new TourProgram_MstModel();
                objProductresponse.DCRID = objBaseSqlManager.GetInt32(dr, "DCRId");
                objProductresponse.TourID = objBaseSqlManager.GetInt64(dr, "TourID");
                objProductresponse.AreaID = objBaseSqlManager.GetInt64(dr, "AreaID");
                objProductresponse.Day = objBaseSqlManager.GetTextValue(dr, "Day");
                objProductresponse.SubareaID = objBaseSqlManager.GetInt64(dr, "SubareaID");
                objProductresponse.HQRS = objBaseSqlManager.GetTextValue(dr, "HQRS");
                objProductresponse.ContactPoint = objBaseSqlManager.GetTextValue(dr, "ContactPoint");
                objProductresponse.UserID = objBaseSqlManager.GetInt64(dr, "UserID");
                objProductresponse.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                objProductresponse.TourDate = Convert.ToDateTime(objBaseSqlManager.GetDateTime(dr, "Date")).ToString("yyyy-MM-dd");

                lstProduct.Add(objProductresponse);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProduct;
        }

        #endregion

        #region Get Tourprogram GetVarianceList list
        public List<TourProgram_Varianceresponce> GetVarianceList(string CurrentUserId)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetVarianceList";
            cmdGet.Parameters.AddWithValue("@CurrentUserId", CurrentUserId);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<TourProgram_Varianceresponce> lstProduct = new List<TourProgram_Varianceresponce>();
            while (dr.Read())
            {
                TourProgram_Varianceresponce objProductresponse = new TourProgram_Varianceresponce();
                objProductresponse.TourID = objBaseSqlManager.GetInt64(dr, "TourID");
                objProductresponse.AreaID = objBaseSqlManager.GetInt64(dr, "AreaID");
                objProductresponse.Day = objBaseSqlManager.GetTextValue(dr, "Day");
                objProductresponse.VarianceAreaID = objBaseSqlManager.GetInt64(dr, "VarianceAreaID");
                objProductresponse.VarianceSubareaID = objBaseSqlManager.GetInt64(dr, "VarianceSubareaID");
                objProductresponse.SubareaID = objBaseSqlManager.GetInt64(dr, "SubareaID");
                objProductresponse.TourDate = Convert.ToDateTime(objBaseSqlManager.GetDateTime(dr, "Date")).ToString("yyyy-MM-dd");
                objProductresponse.Status = objBaseSqlManager.GetTextValue(dr, "Status");
                objProductresponse.UserName = objBaseSqlManager.GetTextValue(dr, "UserName");
                objProductresponse.AreaName = objBaseSqlManager.GetTextValue(dr, "AreaName");
                objProductresponse.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                objProductresponse.VarianceArea = objBaseSqlManager.GetTextValue(dr, "VarianceArea");
                objProductresponse.VarianceSubarea = objBaseSqlManager.GetTextValue(dr, "VarianceSubarea");
                objProductresponse.AreaNote = objBaseSqlManager.GetTextValue(dr, "AreaNote");
                lstProduct.Add(objProductresponse);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProduct;
        }

        #endregion

        #region Approve Tour
        public bool ApproveTour(long TourId, long ApprovedBy, string Status)
        {
            try
            {
                SqlCommand cmdGet = new SqlCommand();
                BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                cmdGet.CommandType = CommandType.StoredProcedure;
                cmdGet.CommandText = "ApproveTour";
                cmdGet.Parameters.AddWithValue("@TourId", TourId);
                cmdGet.Parameters.AddWithValue("@ApprovedBy", ApprovedBy);
                cmdGet.Parameters.AddWithValue("@Status", Status);
                object dr = objBaseSqlManager.ExecuteNonQuery(cmdGet);
                objBaseSqlManager.ForceCloseConnection();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Delete Tour
        public bool DeleteTourProgram(long TourID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "DeleteTourProgram";
            cmdGet.Parameters.AddWithValue("@TourID", TourID);
            object dr = objBaseSqlManager.ExecuteNonQuery(cmdGet);
            objBaseSqlManager.ForceCloseConnection();
            return true;
        }
        #endregion

        #region Get all UserByManagerId
        public List<UserListViewModel> GetUserByManagerId(int UserId, int UserRoleId)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetUserByManagerId";
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            cmdGet.Parameters.AddWithValue("@UserRoleId", UserRoleId);
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
        #endregion

        #region GetAllTourList
        public List<TourPlanResponse> GetAllTourList(TourPlanResponse model)
        {
            string DDate = Convert.ToDateTime(model.StartDate).ToString("yyyy-MM-dd");
            string DDate1 = Convert.ToDateTime(model.EndDate).ToString("yyyy-MM-dd");
            if (DDate == "0001-01-01" && DDate1 == "0001-01-01" || DDate1 == "0001-01-02")
            {
                DDate = null;
                DDate1 = null;
            }
            try
            {
                SqlCommand cmdGet = new SqlCommand();
                BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                cmdGet.CommandType = CommandType.StoredProcedure;
                cmdGet.CommandText = "GetTourList";
                cmdGet.Parameters.AddWithValue("@UserId", model.UserId);
                cmdGet.Parameters.AddWithValue("@StartDate", DDate);
                cmdGet.Parameters.AddWithValue("@EndDate", DDate1);
                SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
                List<TourPlanResponse> lsttour = new List<TourPlanResponse>();
                while (dr.Read())
                {
                    TourPlanResponse objEntity = new TourPlanResponse();
                    objEntity.TourID = objBaseSqlManager.GetInt32(dr, "TourID");
                    objEntity.UserName = objBaseSqlManager.GetTextValue(dr, "FullName");
                    objEntity.Day = objBaseSqlManager.GetTextValue(dr, "Day");
                    objEntity.Date = objBaseSqlManager.GetDateTime(dr, "Date");
                    objEntity.AreaName = objBaseSqlManager.GetTextValue(dr, "AreaName");
                    objEntity.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                    objEntity.IsApproved = objBaseSqlManager.GetTextValue(dr, "IsApproved");
                    objEntity.ApprovedBy = objBaseSqlManager.GetInt32(dr, "ApprovedBy");
                    lsttour.Add(objEntity);
                }
                dr.Close();
                objBaseSqlManager.ForceCloseConnection();
                return lsttour;
            }
            catch
            {
                return new List<TourPlanResponse>();
            }
        }
        #endregion

        #region ApproveMR
        public bool ApproveMR(long TourID, string IsApproved, int ApprovedBy)
        {
            try
            {
                SqlCommand cmdGet = new SqlCommand();
                BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                cmdGet.CommandType = CommandType.StoredProcedure;
                cmdGet.CommandText = "ApproveMRforTourplan";
                cmdGet.Parameters.AddWithValue("@TourID", TourID);
                cmdGet.Parameters.AddWithValue("@IsApproved", IsApproved);
                cmdGet.Parameters.AddWithValue("@ApprovedBy", ApprovedBy);
                object dr = objBaseSqlManager.ExecuteNonQuery(cmdGet);
                objBaseSqlManager.ForceCloseConnection();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region rejectMR
        public bool RejectMR(long TourID, string IsApproved, int ApprovedBy)
        {
            try
            {
                SqlCommand cmdGet = new SqlCommand();
                BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                cmdGet.CommandType = CommandType.StoredProcedure;
                cmdGet.CommandText = "RejectMRforTourplan";
                cmdGet.Parameters.AddWithValue("@TourID", TourID);
                cmdGet.Parameters.AddWithValue("@IsApproved", IsApproved);
                cmdGet.Parameters.AddWithValue("@ApprovedBy", ApprovedBy);
                object dr = objBaseSqlManager.ExecuteNonQuery(cmdGet);
                objBaseSqlManager.ForceCloseConnection();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region UpdateAllTour Approve and rejected
        public bool UpdateAllTourApprove(List<TourPlanResponse> data)
        {
            foreach (var item in data)
            {
                try
                {
                    SqlCommand cmdGet = new SqlCommand();
                    BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                    cmdGet.CommandType = CommandType.StoredProcedure;
                    cmdGet.CommandText = "UpdateAllTourApprove";
                    cmdGet.Parameters.AddWithValue("@TourID", item.TourID);
                    cmdGet.Parameters.AddWithValue("@IsApproved", item.IsApproved);
                    cmdGet.Parameters.AddWithValue("@ApprovedBy", item.ApprovedBy);
                    int dr = objBaseSqlManager.ExecuteNonQuery(cmdGet);
                    objBaseSqlManager.ForceCloseConnection();
                }
                catch
                {

                }
            }
            return true;
        }

        public bool UpdateAllTourRejected(List<TourPlanResponse> data)
        {
            foreach (var item in data)
            {
                try
                {
                    SqlCommand cmdGet = new SqlCommand();
                    BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                    cmdGet.CommandType = CommandType.StoredProcedure;
                    cmdGet.CommandText = "UpdateAllTourRejected";
                    cmdGet.Parameters.AddWithValue("@TourID", item.TourID);
                    cmdGet.Parameters.AddWithValue("@IsApproved", item.IsApproved);
                    cmdGet.Parameters.AddWithValue("@ApprovedBy", item.ApprovedBy);
                    int dr = objBaseSqlManager.ExecuteNonQuery(cmdGet);
                    objBaseSqlManager.ForceCloseConnection();
                }
                catch
                {

                }
            }
            return true;
        }
        #endregion

        #region GetTourplanList api
        public List<TourPlanModel> GetTourList(string UserId)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetTourListforapi";
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<TourPlanModel> objlst = new List<TourPlanModel>();

            while (dr.Read())
            {
                TourPlanModel objuser = new TourPlanModel();
                objuser.TourID = objBaseSqlManager.GetInt32(dr, "TourID");
                objuser.UserName = objBaseSqlManager.GetTextValue(dr, "FullName");
                objuser.Day = objBaseSqlManager.GetTextValue(dr, "Day");
                objuser.Date = objBaseSqlManager.GetDateTime(dr, "Date");
                objuser.Datestr = Convert.ToDateTime(objBaseSqlManager.GetDateTime(dr, "Date")).ToString("yyyy-MM-dd");
                objuser.AreaName = objBaseSqlManager.GetTextValue(dr, "AreaName");
                objuser.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                objuser.Status = objBaseSqlManager.GetTextValue(dr, "IsApproved");
                objuser.UserId = objBaseSqlManager.GetInt32(dr, "CreatedBy");               
                objlst.Add(objuser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlst;
        }
        #endregion
    }
}
