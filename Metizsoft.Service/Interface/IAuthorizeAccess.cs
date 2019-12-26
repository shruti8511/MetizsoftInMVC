

namespace Metizsoft.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAuthorizeAccess
    {
       int AuthorizeMaster_CheckAuthorizeAccess(string RoleName,string controllerName,string actionName);
    }
}
