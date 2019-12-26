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

    public class AccountService : IAccount
    {

        public string ResetPassword(long Id)
        {
            string UserName = "";
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetResetPassword";
            cmdGet.Parameters.AddWithValue("@UserID", Id);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);

            while (dr.Read())
            {

                UserName = objBaseSqlManager.GetTextValue(dr, "UserName");

            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return UserName;
        }

        public bool ActiveDeactive(activedeactive Data)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                var exist = context.Users.Where(model => model.Id == Data.UserID).FirstOrDefault();

                if (exist != null)
                {
                    exist.IsActive = Data.flage;
                    context.Entry(exist).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
