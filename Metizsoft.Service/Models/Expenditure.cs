using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Service.Models
{
    public class ExpenditureReportViewModel
    {
        public int Amount { get; set; }
        public string Reason { get; set; }
        public string Note { get; set; }
        public long CurrentUserId { get; set; }
        public string ExpenserImage { get; set; }
    }

    public class ExpenditureViewModel
    {
        public int ExpenserReportId { get; set; }
        public int Amount { get; set; }
        public string Note { get; set; }
        public string Reason { get; set; }
        public long CurrentUserId { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public string Stutas { get; set; }
        public string ExpenserImage1 { get; set; }
        public string ExpenserImage { get; set; }
    }

    public class ExpenseViewModel
    { 
        public int ExpenserId {get;set; }
        public string ExpenserName { get; set; }
    }
}
