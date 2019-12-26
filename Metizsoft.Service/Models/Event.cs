using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Service.Models
{
    public class EventViewModel
    {
        public int EventID { get; set; }
        public string Description { get; set; }
        public string EventType { get; set; }
        
    }

    public class EventTypesViewModel
    {
        public int EventTypeID { get; set; }
        public string EventType { get; set; } 
    }
    
    public class EventsEventTypeViewModel1
    {
        public List<EventViewModel> lstEventName { get; set; }
        public List<EventTypesViewModel> lstEventType { get; set; }
    }
}
