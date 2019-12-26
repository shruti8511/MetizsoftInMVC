using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Data.ViewModal
{
    public class SalesTargetResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserRoleID { get; set; }
        public string UserName { get; set; }

        public int Id { get; set; }
        public int RoleID { get; set; }
        public int DirectorID { get; set; }
        public int HOID { get; set; }
        public int VPID { get; set; }
        public int NSMID { get; set; }
        public int ZSMID { get; set; }
        public int RSMID { get; set; }
        public int ASMID { get; set; }
        public int MRID { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CaseName { get; set; }
        public int AreaID { get; set; }
        public string AreaName { get; set; }
        public float SalesTarget { get; set; }
        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal Mar { get; set; }
        public decimal Apr { get; set; }
        public decimal May { get; set; }
        public decimal Jun { get; set; }
        public decimal July { get; set; }
        public decimal Aug { get; set; }
        public decimal Sep { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dec { get; set; }
        public string Year { get; set; }       
        
    }

}
