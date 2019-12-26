using Metizsoft.Data.Modal;
using Metizsoft.Service.Interface;
using Metizsoft.Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Service.Implementation
{
    public class NotificationService : INotification
    {

        #region API
        public List<NotificationModel> GetNotificationList(int UserId)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetallNotificationList";
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<NotificationModel> lstNotification = new List<NotificationModel>();
            while (dr.Read())
            {
                NotificationModel objnotificationresponse = new NotificationModel();
                objnotificationresponse.NotificationID = objBaseSqlManager.GetInt32(dr, "NotificationID");
                objnotificationresponse.UserId = objBaseSqlManager.GetInt32(dr, "UserId");
                objnotificationresponse.NotificationTitle = objBaseSqlManager.GetTextValue(dr, "NotificationTitle");
                objnotificationresponse.NotificationMessage = objBaseSqlManager.GetTextValue(dr, "NotificationMessage");
                objnotificationresponse.CreatedDate = objBaseSqlManager.GetDateTime(dr, "CreatedDate");
                lstNotification.Add(objnotificationresponse);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstNotification;
        }
        #endregion

        #region Save & Update Notification
        public bool SaveNotification(NotificationMaster obj)
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

    }
}
