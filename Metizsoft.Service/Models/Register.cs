using Metizsoft.Data.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Service.Models
{
    
        public class UsersModel
        {
            public int Id { get; set; }
            public Nullable<long> UserRoleID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string MobileNumber { get; set; }
            public string EmailID { get; set; }
            public string Address { get; set; }
            public Nullable<long> City { get; set; }
            public Nullable<long> State { get; set; }
            public string PostCode { get; set; }
            public string Password { get; set; }
            public Nullable<long> UserPosition { get; set; }
            public Nullable<long> DirectorID { get; set; }
            public Nullable<long> VPID { get; set; }
            public Nullable<long> ZSMID { get; set; }
            public Nullable<long> RSMID { get; set; }
            public Nullable<long> ASMID { get; set; }
            public Nullable<long> MRID { get; set; }
            public Nullable<long> HOID { get; set; }
            public Nullable<long> NSMID { get; set; }
            public string DeviceID { get; set; }
            public string InstanceID { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public Nullable<long> CreatedBy { get; set; }
            public Nullable<System.DateTime> UpdateOn { get; set; }
            public Nullable<long> UpdateBy { get; set; }
            public Nullable<bool> IsActive { get; set; }
            public Nullable<double> ExpensesLimit { get; set; }
            public Nullable<double> SalesTarget { get; set; }
            public virtual ICollection<webpages_Roles> webpages_Roles { get; set; }
            public int CaseName { get; set; }
            public int DCRBlocking { get; set; }

            public List<UserViewModel1> lstUserList { get; set; }
            public List<UserViewModel1> lstUserRoleList { get; set; }
        }
    
}
