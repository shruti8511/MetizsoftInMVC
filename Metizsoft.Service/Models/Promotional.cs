using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Service.Models
{
    public class PromotionalViewModel
    {
        public int PromotionalId { get; set; }
        public string PromotionalName { get; set; }
    }

    public class PromotionalReportViewModel
    {
        public Nullable<int> PromotionalId { get; set; }
        public Nullable<int> WorkWithId { get; set; }
        public Nullable<int> AreaID { get; set; }
        public Nullable<int> SubAreaId { get; set; }
        public DateTime Date { get; set; }
        public long CurrentUserId { get; set; }
    }

    public class PromotionalReportViewModels
    {
        public int PromotionalId { get; set; }
        public string WorkWithIds { get; set; }
        public string WorkWithUserId { get; set; }
        public int AreaID { get; set; }
        public int SubAreaId { get; set; }
        public string PromotionalImage { get; set; }
        public long CurrentUserId { get; set; }
        public string Date { get; set; }

    }
}
