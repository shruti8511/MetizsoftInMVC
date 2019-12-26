
namespace Metizsoft.Service
{
    using Metizsoft.Data.Modal;
    using System.Collections.Generic;
    using Metizsoft.Service.Interface;
    using Metizsoft.Data;
    using Metizsoft.Service.Models;
    using Metizsoft.Data.ViewModal;
    using System;

    public interface IEvent
    {
        List<Event_Mst> GetAllEventName();

        bool AddEvents(Event_Mst objevent);

        List<Event_Mst> GetAllEventList();

        bool DeleteEvent(long EventID , bool IsActive);

        EventsEventTypeViewModel1 GetallEventsName();
    }
}
