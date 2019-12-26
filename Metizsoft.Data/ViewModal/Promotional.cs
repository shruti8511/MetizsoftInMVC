using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Data.ViewModal
{
    public class PromotionalResponse
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PromotionalId { get; set; }
        public int AreaId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int VPID { get; set; }
        public int NSMID { get; set; }
        public int ZSMID { get; set; }
        public int RSMID { get; set; }
        public int ASMID { get; set; }

        public string CreatedBy { get; set; }
        public int PromotionalReportId { get; set; }
        public string PromotionalName { get; set; }
        public string PromotionalImage { get; set; }
        public string RoleName { get; set; }       
        public DateTime Date { get; set; }
        public string AreaName { get; set; }
        public int manager { get; set; }
        public string managerName { get; set; }
    }
}
