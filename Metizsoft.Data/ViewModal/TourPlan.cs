using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Data.ViewModal
{
    public class TourPlanResponse
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int UserId { get; set; }
        public int AreaId { get; set; }
        public int SubAreaId { get; set; }
        public DateTime Date { get; set; }
        public string Day { get; set; }

        public int TourID { get; set; }
        public int DCRId { get; set; }
        public string UserName { get; set; }       
        public DateTime TourDate { get; set; }
        public string  AreaName { get; set; }
        public string SubAreaName { get; set; }
        public string VarianceArea { get; set; }
        public string VarianceSubArea { get; set; }
        public string HQRS { get; set; }
        public string IsApproved { get; set; }
        public int ApprovedBy { get; set; }
        public string Reason { get; set; }
    }

    public class TourPlanModel
    {
        public int TourID { get; set; }        
        public string Day { get; set; }
        public DateTime Date { get; set; }
        public string Datestr { get; set; }
        public string UserName { get; set; }        
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }        
        public string Status { get; set; }
        public int UserId { get; set; }
    }
}
