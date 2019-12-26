

namespace Metizsoft.Service
{
    using Metizsoft.Data;
    using Metizsoft.Data.Modal;
    using Metizsoft.Service.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public interface IUser
    {
        List<NotificationResponseData> GetNotificationID(string DeviceNotificationID);

        UsersModel checklogin(RequestLogin Data);

        bool Addtrackerinfo(Tracker_Mst Data);

        List<UserViewModel> GetUserRoleUserList(long RoleId, long CurrentUserId);
        List<Tracker_MstModel> GetTrackListByUserID(string Joind);

        List<GetUserTargetdataByRole> GetUserTargetdataByRole(GetForTargate Data);

        List<ProductCaseMst_mstForAPI> GetAllProductlistOnAllocationFroDoctor(long CurrentUserId);
    }
}
