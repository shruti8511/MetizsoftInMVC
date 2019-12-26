using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Data
{
  
    public class RequestLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string device_type { get; set; }
        public string api_version { get; set; }
        public string app_version { get; set; }
        public string InstanceID { get; set; }
        public string DeviceID { get; set; }

        public long UserRoleId { get; set; }
        public int Id { get; set; }
        
    }
    
    
    public class Tracker_MstModel
    {
        public Nullable<long> UserID { get; set; }
        public string Lattitude { get; set; }
        public string longitude { get; set; }
       
    }

    public class Tracker_MstList
    {
        public Nullable<long> UserID { get; set; }
        public string Color { get; set; }
        public List<Tracker_MstModel> lstTracker { get; set; }
    }
}
