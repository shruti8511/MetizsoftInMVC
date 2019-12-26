
namespace Metizsoft.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AuthorizeAccessService : IAuthorizeAccess
    {
        #region AuthorizeMaster_CheckAuthorizeAccess
        public int AuthorizeMaster_CheckAuthorizeAccess(string RoleName, string controllerName, string actionName)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "AuthorizeMaster_CheckAuthorizeAccess";
            cmdGet.Parameters.AddWithValue("@RoleName", RoleName);
            cmdGet.Parameters.AddWithValue("@controllerName", controllerName);
            cmdGet.Parameters.AddWithValue("@actionName", actionName);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            int RoleID = 0;
            while (dr.Read())
            {
                RoleID = objBaseSqlManager.GetInt32(dr, "RoleId");
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return RoleID;
        }
                #endregion              
    }
}
