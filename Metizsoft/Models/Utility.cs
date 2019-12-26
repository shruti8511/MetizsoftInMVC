using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Metizsoft.Models
{
    public class Utility
    {
        public static int GetUserRoleId()
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["RoleId"] != null)
            {
                return Convert.ToInt32(HttpContext.Current.Session["RoleId"].ToString());
            }
            else
            {
                return 0;
            }
        }
    }
}
