using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Data
{
    public class NotificationInfo
    {
        public long NotificationID { get; set; }
        public Nullable<long> UserID { get; set; }
        public string DeviceTokenID { get; set; }
        public string Message { get; set; }
        public Nullable<bool> Isread { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public System.DateTime UpdateOn { get; set; }
        public long UpdateBy { get; set; }
        public Nullable<int> NotificationType { get; set; }
        //public string UserFullName { get; set; }
        //public string OrderNumber { get; set; }


    }

    public class NotificationResponse
    {
        public long NotificationID { get; set; }
        public Nullable<long> UserID { get; set; }
        public string Message { get; set; }
        public Nullable<bool> Isread { get; set; }
        public Nullable<int> NotificationType { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
    public class NotificationResponseData
    {
        public Nullable<long> UID { get; set; }
        public string DeviceTokenID { get; set; }
        public Nullable<int> NotificationType { get; set; }
        public System.DateTime InstallationDate { get; set; }
    }
}
