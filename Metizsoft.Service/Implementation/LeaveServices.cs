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


namespace Metizsoft.Service
{
    public class LeaveServices : ILeave
    {
        #region ADD Leave
        public long AddLeave(LeaveMaster obj)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                var data = context.LeaveMasters.FirstOrDefault(x => x.LeaveId == obj.LeaveId);

                if (data == null)
                {
                    context.LeaveMasters.Add(obj);
                    context.SaveChanges();
                    return obj.LeaveId;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        public List<LeaveResponse> GetLeaveReport()
        {
            List<LeaveResponse> objects = new List<LeaveResponse>();
            return objects;
        }

        public ResponseLeaveCountModel GetallLeavesByUserId(string userId, long roleId, DateTime StartDate, DateTime EndDate)
        {
            string DDate = Convert.ToDateTime(StartDate).ToString("yyyy/MM/dd");
            string DDate1 = Convert.ToDateTime(EndDate).ToString("yyyy/MM/dd");
            if (DDate == "0001/01/01" && DDate1 == "0001/01/01" || DDate1 == "0001/01/02")
            {
                DDate = null;
                DDate1 = null;
            }            
            //ResponseLeaveCountModel lstleave = new ResponseLeaveCountModel();
            SqlCommand cmdGet = new SqlCommand();
            ResponseLeaveCountModel lstmain = new ResponseLeaveCountModel();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllLeavesByUser";
            cmdGet.Parameters.AddWithValue("@userId", userId);
            cmdGet.Parameters.AddWithValue("@roleId", roleId);
            cmdGet.Parameters.AddWithValue("@StartDate", DDate);
            cmdGet.Parameters.AddWithValue("@EndDate", DDate1);
         
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);

            List<ResponseLeaveModel> lstProduct = new List<ResponseLeaveModel>();
            while (dr.Read())
            {
                ResponseLeaveModel objProductresponse = new ResponseLeaveModel();
                objProductresponse.UserName = objBaseSqlManager.GetTextValue(dr, "UserName");
               objProductresponse.RoleId = objBaseSqlManager.GetInt32(dr, "RoleId");
                objProductresponse.LeaveId = objBaseSqlManager.GetInt32(dr, "LeaveId");
                objProductresponse.CurrentUserId = objBaseSqlManager.GetInt64(dr, "UserId");
                objProductresponse.LeaveType = objBaseSqlManager.GetInt32(dr, "LeaveType");               
                DateTime DateFrom = objBaseSqlManager.GetDateTime(dr, "DateFrom");
                DateTime DateTo = objBaseSqlManager.GetDateTime(dr, "DateTo");

                string datfr = DateFrom.ToString();
                objProductresponse.DateFrom = Convert.ToDateTime(datfr).ToString("yyyy-MM-dd");

                string datto = DateTo.ToString();
                objProductresponse.DateTo = Convert.ToDateTime(datto).ToString("yyyy-MM-dd");

                objProductresponse.Reason = objBaseSqlManager.GetTextValue(dr, "Reason");
                objProductresponse.LeaveStatus = objBaseSqlManager.GetTextValue(dr, "LeaveStatus");
                objProductresponse.LeaveStatusByVP = objBaseSqlManager.GetTextValue(dr, "LeaveStatusByVP");
                lstmain.TotalLeave = objBaseSqlManager.GetInt32(dr, "TotalLeaves");
                lstmain.ApprovedLeave = objBaseSqlManager.GetInt32(dr, "ApprovedLeave");
                lstmain.InProgress = objBaseSqlManager.GetInt32(dr, "InProgress");
                lstProduct.Add(objProductresponse);
            }
            foreach (var item in lstProduct)
            {
                List<ResponseLeaveModel> lsts = lstProduct.ToList();
                lstmain.lstResponseLeaveModel = lsts;
            }

            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
           
            SqlCommand cmdGetLeaveType = new SqlCommand();
            BaseSqlManager objBaseSqlManagerLeave = new BaseSqlManager();
            cmdGetLeaveType.CommandType = CommandType.StoredProcedure;
            cmdGetLeaveType.CommandText = "GetAllLeaveTypeName";

            SqlDataReader drleave = objBaseSqlManagerLeave.ExecuteDataReader(cmdGetLeaveType);
            List<LeaveTypeResponse> lstLeaveType = new List<LeaveTypeResponse>();
            while (drleave.Read())
            {
                LeaveTypeResponse objleavetype = new LeaveTypeResponse();
                objleavetype.LeaveTypeId = objBaseSqlManagerLeave.GetInt32(drleave, "LeaveTypeId");
                objleavetype.LeaveType = objBaseSqlManagerLeave.GetTextValue(drleave, "LeaveType");
                lstLeaveType.Add(objleavetype);
            }
            drleave.Close();
            objBaseSqlManagerLeave.ForceCloseConnection();
            lstmain.LeaveTypeList = lstLeaveType;
            return lstmain;
           
        }
        
        public List<Register> GetUnapprovedForASM()
        {
            try
            {
                SqlCommand cmdGet = new SqlCommand();
                BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                cmdGet.CommandType = CommandType.StoredProcedure;
                cmdGet.CommandText = "GetUnapprovedForASM";

                SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
                List<Register> lstUser = new List<Register>();
                while (dr.Read())
                {
                    Register objEntity = new Register();
                    objEntity.LeaveId = objBaseSqlManager.GetInt64(dr, "LeaveId");
                    objEntity.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                    objEntity.FirstName = objBaseSqlManager.GetTextValue(dr, "FirstName");
                    objEntity.LastName = objBaseSqlManager.GetTextValue(dr, "LastName");
                    objEntity.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");
                    objEntity.username = objBaseSqlManager.GetTextValue(dr, "UserName");

                    objEntity.FromDatedt = objBaseSqlManager.GetDateTime(dr, "DateFrom");
                    string dat = objEntity.FromDatedt.ToString();
                    objEntity.FromDate = Convert.ToDateTime(dat).ToString("MM/dd/yyyy");

                    objEntity.ToDatedt = objBaseSqlManager.GetDateTime(dr, "DateTo");
                    string dat1 = objEntity.ToDatedt.ToString();
                    objEntity.ToDate = Convert.ToDateTime(dat1).ToString("MM/dd/yyyy");

                    objEntity.LeaveReason = objBaseSqlManager.GetTextValue(dr, "Reason");
                    objEntity.LeaveStatus = objBaseSqlManager.GetTextValue(dr, "LeaveStatus");
                    objEntity.RoleName = objBaseSqlManager.GetTextValue(dr, "RoleDisplayName");
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

        public List<Register> GetUnapprovedForVP()
        {
            try
            {
                SqlCommand cmdGet = new SqlCommand();
                BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                cmdGet.CommandType = CommandType.StoredProcedure;
                cmdGet.CommandText = "GetUnapprovedForVP";

                SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
                List<Register> lstUser = new List<Register>();
                while (dr.Read())
                {
                    Register objEntity = new Register();
                    objEntity.LeaveId = objBaseSqlManager.GetInt64(dr, "LeaveId");
                    objEntity.UserID = objBaseSqlManager.GetInt32(dr, "Id");
                    objEntity.FirstName = objBaseSqlManager.GetTextValue(dr, "FirstName");
                    objEntity.LastName = objBaseSqlManager.GetTextValue(dr, "LastName");
                    objEntity.UserFullName = objBaseSqlManager.GetTextValue(dr, "UserFullName");
                    objEntity.username = objBaseSqlManager.GetTextValue(dr, "UserName");

                    objEntity.FromDatedt = objBaseSqlManager.GetDateTime(dr, "DateFrom");
                    string dat = objEntity.FromDatedt.ToString();
                    objEntity.FromDate = Convert.ToDateTime(dat).ToString("dd-MM-yyyy");

                    objEntity.ToDatedt = objBaseSqlManager.GetDateTime(dr, "DateTo");
                    string dat1 = objEntity.ToDatedt.ToString();
                    objEntity.ToDate = Convert.ToDateTime(dat1).ToString("dd-MM-yyyy");

                    objEntity.LeaveReason = objBaseSqlManager.GetTextValue(dr, "Reason");
                    objEntity.LeaveStatus = objBaseSqlManager.GetTextValue(dr, "LeaveStatus");
                    objEntity.LeaveStatusByVP = objBaseSqlManager.GetTextValue(dr, "LeaveStatusByVP");

                    objEntity.RoleName = objBaseSqlManager.GetTextValue(dr, "RoleDisplayName");

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

        public bool ApproveLeaveASM(long LeaveId,string Status)
        {
            try
            {
                SqlCommand cmdGet = new SqlCommand();
                BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                cmdGet.CommandType = CommandType.StoredProcedure;
                cmdGet.CommandText = "ApproveLeaveASM";
                cmdGet.Parameters.AddWithValue("@LeaveId", LeaveId);
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

        public bool ApproveLeaveVP(long LeaveId,string Status)
        {

            try
            {
                SqlCommand cmdGet = new SqlCommand();
                BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                cmdGet.CommandType = CommandType.StoredProcedure;
                cmdGet.CommandText = "ApproveLeaveVP";
                cmdGet.Parameters.AddWithValue("@LeaveId", LeaveId);
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

        public List<LeaveTypeResponse> GetAllLeaveTypeName()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllLeaveTypeName";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<LeaveTypeResponse> lstLeaveType = new List<LeaveTypeResponse>();
            while (dr.Read())
            {
                LeaveTypeResponse objleavetype = new LeaveTypeResponse();
                objleavetype.LeaveTypeId = objBaseSqlManager.GetInt32(dr, "LeaveTypeId");
                objleavetype.LeaveType = objBaseSqlManager.GetTextValue(dr, "LeaveType");
                lstLeaveType.Add(objleavetype);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstLeaveType;
        }
    }
}
