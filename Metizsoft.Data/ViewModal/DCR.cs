using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Data.ViewModal
{
    public class DCRResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int WorkTypeId { get; set; }
        public int AreaID { get; set; }
        public int SubAreaId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int WorkWithId { get; set; }
        //public int CaseName { get; set; }

        public int DCRId { get; set; }
        public DateTime Date { get; set; }
        public string CreatedBy { get; set; }
        public string WorkType { get; set; }
        public string RoleName { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        
       
    }
}
