

namespace Metizsoft.Service
{
    using Metizsoft.Data.Modal;
    using Metizsoft.Service.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

     public interface INotification
    {
         List<NotificationModel> GetNotificationList(int UserId);
         bool SaveNotification(NotificationMaster obj);
    }
}
