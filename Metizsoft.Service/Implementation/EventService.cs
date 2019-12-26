using System;
using Metizsoft.Data;
using Metizsoft.Data.Modal;
using System;
using Metizsoft.Data;
using Metizsoft.Data.Modal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metizsoft.Service.Models;


namespace Metizsoft.Service.Implementation
{
    public class EventService : IEvent
    {

        public List<Event_Mst> GetAllEventName()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllEventName";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Event_Mst> lstProductcase = new List<Event_Mst>();
            while (dr.Read())
            {
                Event_Mst objProductcasemst = new Event_Mst();
                objProductcasemst.EventTypeID = objBaseSqlManager.GetInt32(dr, "EventTypeID");
                objProductcasemst.EventName = objBaseSqlManager.GetTextValue(dr, "EventName");
                lstProductcase.Add(objProductcasemst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProductcase;
        }

        public bool AddEvents(Event_Mst objevents)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                if (objevents.EventID == 0)
                {
                    context.Event_Mst.Add(objevents);
                    context.SaveChanges();
                }
                else
                {
                    context.Entry(objevents).State = EntityState.Modified;
                    context.SaveChanges();
                }
                if (objevents.EventID > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<Event_Mst> GetAllEventList()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllEventList";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Event_Mst> lstProductcase = new List<Event_Mst>();
            while (dr.Read())
            {
                Event_Mst objEventmst = new Event_Mst();
                objEventmst.EventID = objBaseSqlManager.GetInt32(dr, "EventID");
                objEventmst.EventTypeID = objBaseSqlManager.GetInt32(dr, "EventTypeID");
                objEventmst.EventName = objBaseSqlManager.GetTextValue(dr, "EventName");
                objEventmst.Description = objBaseSqlManager.GetTextValue(dr, "Description");
                objEventmst.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                lstProductcase.Add(objEventmst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProductcase;
        }

        public bool DeleteEvent(long EventID, bool IsActive)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                var exist = context.Event_Mst.FirstOrDefault(x => x.EventID == EventID && x.IsActive == IsActive);
                if (exist != null)
                {
                    exist.IsActive = false;
                    context.Entry(exist).State = EntityState.Modified;   
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        #region API Services

        public EventsEventTypeViewModel1 GetallEventsName()
        {
            EventsEventTypeViewModel1 lstArea = new EventsEventTypeViewModel1();
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetallEventsName";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<EventViewModel> lstEvent = new List<EventViewModel>();
            while (dr.Read())
            {
                EventViewModel objProductresponse = new EventViewModel();
                objProductresponse.EventID = objBaseSqlManager.GetInt32(dr, "EventID");
                objProductresponse.Description = objBaseSqlManager.GetTextValue(dr, "Description");
                objProductresponse.EventType = objBaseSqlManager.GetTextValue(dr, "EventType");
                lstEvent.Add(objProductresponse);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();

            SqlCommand cmdGetsubarea = new SqlCommand();
            BaseSqlManager objBaseSqlManagerSubarea = new BaseSqlManager();
            cmdGetsubarea.CommandType = CommandType.StoredProcedure;
            cmdGetsubarea.CommandText = "GetAllEventTypelist";

            SqlDataReader drsubarea = objBaseSqlManagerSubarea.ExecuteDataReader(cmdGetsubarea);
            List<EventTypesViewModel> lsteventtype = new List<EventTypesViewModel>();
            while (drsubarea.Read())
            {
                EventTypesViewModel objProductresponse = new EventTypesViewModel();
                objProductresponse.EventTypeID = objBaseSqlManager.GetInt32(drsubarea, "EventTypeID");
                objProductresponse.EventType = objBaseSqlManager.GetTextValue(drsubarea, "EventName");
                lsteventtype.Add(objProductresponse);
                
            }
            drsubarea.Close();
            objBaseSqlManager.ForceCloseConnection();
            lstArea.lstEventName = lstEvent;
            lstArea.lstEventType = lsteventtype;
            return lstArea;
        #endregion
        }
    }
}
