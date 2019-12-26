using Metizsoft.Data;
using Metizsoft.Data.Modal;
using Metizsoft.Data.ViewModal;
using Metizsoft.Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Service
{
    public class MasterServices : IMaster
    {
        public bool AddWorkType(WorkType_mst objworktype)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                if (objworktype.WorkTypeId == 0)
                {
                    context.WorkType_mst.Add(objworktype);
                    context.SaveChanges();
                }
                else
                {
                    context.Entry(objworktype).State = EntityState.Modified;
                    context.SaveChanges();
                }
                if (objworktype.WorkTypeId > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<WorkType_mst> GetAllWorkTypelist()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllWorkTypelist";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<WorkType_mst> lstWorkType = new List<WorkType_mst>();
            while (dr.Read())
            {
                WorkType_mst objWorkTypemst = new WorkType_mst();
                objWorkTypemst.WorkTypeId = objBaseSqlManager.GetInt32(dr, "WorkTypeId");
                objWorkTypemst.WorkType = objBaseSqlManager.GetTextValue(dr, "WorkType");
                objWorkTypemst.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                lstWorkType.Add(objWorkTypemst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstWorkType;
        }

        public bool DeleteWorkType(long WorkTypeId, bool IsActive)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                var exist = context.WorkType_mst.FirstOrDefault(x => x.WorkTypeId == WorkTypeId && x.IsActive == IsActive);
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

        public bool AddArea(Area_Mst objarea)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                if (objarea.AreaID == 0)
                {
                    context.Area_Mst.Add(objarea);
                    context.SaveChanges();
                }
                else
                {
                    context.Entry(objarea).State = EntityState.Modified;
                    context.SaveChanges();
                }
                if (objarea.AreaID > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<Area_Mst> GetAllArealist()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllArealist";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Area_Mst> lstArea = new List<Area_Mst>();
            while (dr.Read())
            {
                Area_Mst objAreamst = new Area_Mst();
                objAreamst.AreaID = objBaseSqlManager.GetInt32(dr, "AreaID");
                objAreamst.AreaName = objBaseSqlManager.GetTextValue(dr, "AreaName");
                objAreamst.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                objAreamst.City = objBaseSqlManager.GetTextValue(dr, "City");
                objAreamst.State = objBaseSqlManager.GetTextValue(dr, "State");
                objAreamst.Country = objBaseSqlManager.GetTextValue(dr, "Country");
                objAreamst.PinCode = objBaseSqlManager.GetTextValue(dr, "PinCode");
                objAreamst.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");

                lstArea.Add(objAreamst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstArea;
        }

        public bool DeleteArea(long AreaID, bool IsActive)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                var exist = context.Area_Mst.FirstOrDefault(x => x.AreaID == AreaID && x.IsActive == IsActive);
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

        public bool AddPromotional(Promotional_Mst objpromst)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                if (objpromst.PromotionalId == 0)
                {
                    context.Promotional_Mst.Add(objpromst);
                    context.SaveChanges();
                }
                else
                {
                    context.Entry(objpromst).State = EntityState.Modified;
                    context.SaveChanges();
                }
                if (objpromst.PromotionalId > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<Promotional_Mst> GetAllPromotionallist()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllPromotionallist";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Promotional_Mst> lstPromotional = new List<Promotional_Mst>();
            while (dr.Read())
            {
                Promotional_Mst objpromst = new Promotional_Mst();
                objpromst.PromotionalId = objBaseSqlManager.GetInt32(dr, "PromotionalId");
                objpromst.PromotionalName = objBaseSqlManager.GetTextValue(dr, "PromotionalName");
                objpromst.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                lstPromotional.Add(objpromst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstPromotional;
        }

        public bool DeletePromotional(long PromotionalId, bool IsActive)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                var exist = context.Promotional_Mst.FirstOrDefault(x => x.PromotionalId == PromotionalId && x.IsActive == IsActive);
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

        public bool AddEvent(EventType_Mst objeventtype)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                if (objeventtype.EventTypeID == 0)
                {
                    context.EventType_Mst.Add(objeventtype);
                    context.SaveChanges();
                }
                else
                {
                    context.Entry(objeventtype).State = EntityState.Modified;
                    context.SaveChanges();
                }
                if (objeventtype.EventTypeID > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<EventType_Mst> GetAllEventTypelist()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllEventTypelist";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<EventType_Mst> lstevent = new List<EventType_Mst>();
            while (dr.Read())
            {
                EventType_Mst objevent = new EventType_Mst();
                objevent.EventTypeID = objBaseSqlManager.GetInt32(dr, "EventTypeID");
                objevent.EventName = objBaseSqlManager.GetTextValue(dr, "EventName");
                objevent.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                //objevent.Description = objBaseSqlManager.GetTextValue(dr, "Description");
                lstevent.Add(objevent);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstevent;
        }

        public bool DeleteEvent(long EventTypeID, bool IsActive)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                var exist = context.EventType_Mst.FirstOrDefault(x => x.EventTypeID == EventTypeID && x.IsActive == IsActive);
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

        public bool AddRxType(RxType_mst objrxtype)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                if (objrxtype.RxTypeId == 0)
                {
                    context.RxType_mst.Add(objrxtype);
                    context.SaveChanges();
                }
                else
                {
                    context.Entry(objrxtype).State = EntityState.Modified;
                    context.SaveChanges();
                }
                if (objrxtype.RxTypeId > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<RxType_mst> GetAllRxTypelist()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllRxTypelistName";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<RxType_mst> lstProductType = new List<RxType_mst>();
            while (dr.Read())
            {
                RxType_mst objproducttype = new RxType_mst();
                objproducttype.RxTypeId = objBaseSqlManager.GetInt32(dr, "RxTypeId");
                objproducttype.RxType = objBaseSqlManager.GetInt32(dr, "RxType");
                objproducttype.Name = objBaseSqlManager.GetTextValue(dr, "Name");
                objproducttype.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                lstProductType.Add(objproducttype);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProductType;
        }

        public bool DeleteRxType(long RxTypeId, bool IsActive)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                var exist = context.RxType_mst.FirstOrDefault(x => x.RxTypeId == RxTypeId && x.IsActive == IsActive);
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

        public bool AddProductType(ProductType_Mst objproducttype)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                if (objproducttype.ProductTypeId == 0)
                {
                    context.ProductType_Mst.Add(objproducttype);
                    context.SaveChanges();
                }
                else
                {
                    context.Entry(objproducttype).State = EntityState.Modified;
                    context.SaveChanges();
                }
                if (objproducttype.ProductTypeId > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<ProductType_Mst> GetAllProductTypelist()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllProductTypelist";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<ProductType_Mst> lstProductType = new List<ProductType_Mst>();
            while (dr.Read())
            {
                ProductType_Mst objproducttype = new ProductType_Mst();
                objproducttype.ProductTypeId = objBaseSqlManager.GetInt32(dr, "ProductTypeId");
                objproducttype.ProductType = objBaseSqlManager.GetTextValue(dr, "ProductType");
                objproducttype.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                lstProductType.Add(objproducttype);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProductType;
        }

        public bool DeleteProductType(long ProductTypeId, bool IsActive)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                var exist = context.ProductType_Mst.FirstOrDefault(x => x.ProductTypeId == ProductTypeId && x.IsActive == IsActive);
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

        public bool AddLeaveType(LeaveType_Mst objleavetype)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                if (objleavetype.LeaveTypeId == 0)
                {
                    context.LeaveType_Mst.Add(objleavetype);
                    context.SaveChanges();
                }
                else
                {
                    context.Entry(objleavetype).State = EntityState.Modified;
                    context.SaveChanges();
                }
                if (objleavetype.LeaveTypeId > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<LeaveType_Mst> GetAllLeaveTypelist()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllLeaveTypelist";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<LeaveType_Mst> lstLeaveType = new List<LeaveType_Mst>();
            while (dr.Read())
            {
                LeaveType_Mst objleavetype = new LeaveType_Mst();
                objleavetype.LeaveTypeId = objBaseSqlManager.GetInt32(dr, "LeaveTypeId");
                objleavetype.LeaveType = objBaseSqlManager.GetTextValue(dr, "LeaveType");
                objleavetype.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                lstLeaveType.Add(objleavetype);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstLeaveType;
        }

        public bool DeleteLeaveType(long LeaveTypeId, bool IsActive)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                var exist = context.LeaveType_Mst.FirstOrDefault(x => x.LeaveTypeId == LeaveTypeId && x.IsActive == IsActive);
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

        public AreaViewModel1 GetAllAreaSubAreaList()
        {
            AreaViewModel1 lstArea = new AreaViewModel1();
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetallAreaName";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<AreaViewModel> lstarea = new List<AreaViewModel>();
            while (dr.Read())
            {
                AreaViewModel objProductresponse = new AreaViewModel();
                objProductresponse.AreaID = objBaseSqlManager.GetInt32(dr, "AreaID");
                objProductresponse.AreaName = objBaseSqlManager.GetTextValue(dr, "AreaName");
                int a = lstarea.Where(x => x.AreaName == objProductresponse.AreaName).Count();
                if (a == 0)
                {
                    lstarea.Add(objProductresponse);
                }
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            SqlCommand cmdGetsubarea = new SqlCommand();
            BaseSqlManager objBaseSqlManagerSubarea = new BaseSqlManager();
            cmdGetsubarea.CommandType = CommandType.StoredProcedure;
            cmdGetsubarea.CommandText = "GetallSubAreaList";

            SqlDataReader drsubarea = objBaseSqlManagerSubarea.ExecuteDataReader(cmdGetsubarea);
            List<SubAreaViewModel> lstsubarea = new List<SubAreaViewModel>();
            while (drsubarea.Read())
            {
                SubAreaViewModel objProductresponse = new SubAreaViewModel();
                objProductresponse.AreaID = objBaseSqlManager.GetInt32(drsubarea, "AreaID");
                objProductresponse.AreaName = objBaseSqlManager.GetTextValue(drsubarea, "SubAreaName");
                int a = lstsubarea.Where(x => x.AreaName == objProductresponse.AreaName).Count();
                if (a == 0)
                {
                    lstsubarea.Add(objProductresponse);
                }
            }
            drsubarea.Close();
            objBaseSqlManager.ForceCloseConnection();
            lstArea.lstAreaName = lstarea;
            lstArea.lstSubArea = lstsubarea;
            return lstArea;
        }

        public WorkAndWorkWithModel GetAllWorkAndWorkWith()
        {
            WorkAndWorkWithModel lstWork1 = new WorkAndWorkWithModel();
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetallWorkTypeName";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<WorkTypeViewModel> lstWorkType = new List<WorkTypeViewModel>();
            while (dr.Read())
            {
                WorkTypeViewModel objProductresponse = new WorkTypeViewModel();
                objProductresponse.WorkTypeId = objBaseSqlManager.GetInt32(dr, "WorkTypeId");
                objProductresponse.WorkType = objBaseSqlManager.GetTextValue(dr, "WorkType");
                lstWorkType.Add(objProductresponse);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            SqlCommand cmdGetWorkWith = new SqlCommand();
            BaseSqlManager objBaseSqlManagerWorkWith = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetallWorkWithName";

            SqlDataReader drWorkWith = objBaseSqlManagerWorkWith.ExecuteDataReader(cmdGet);
            List<WorkWitheViewModel> lstWorkWith = new List<WorkWitheViewModel>();
            while (drWorkWith.Read())
            {
                WorkWitheViewModel objProductresponse = new WorkWitheViewModel();
                objProductresponse.RoleId = objBaseSqlManager.GetInt32(drWorkWith, "RoleId");
                objProductresponse.RoleName = objBaseSqlManager.GetTextValue(drWorkWith, "RoleName");
                objProductresponse.RoleDisplayName = objBaseSqlManager.GetTextValue(drWorkWith, "RoleDisplayName");
                lstWorkWith.Add(objProductresponse);
            }
            drWorkWith.Close();
            objBaseSqlManager.ForceCloseConnection();
            lstWork1.lstWorkTypeModel = lstWorkType;
            lstWork1.lstWorkWithModel = lstWorkWith;
            return lstWork1;
        }

        public List<WorkTypeViewModel> GetallWorkTypeName()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetallWorkTypeName";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<WorkTypeViewModel> lstProduct = new List<WorkTypeViewModel>();
            while (dr.Read())
            {
                WorkTypeViewModel objProductresponse = new WorkTypeViewModel();
                objProductresponse.WorkTypeId = objBaseSqlManager.GetInt32(dr, "WorkTypeId");
                objProductresponse.WorkType = objBaseSqlManager.GetTextValue(dr, "WorkType");
                // objProductresponse.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                lstProduct.Add(objProductresponse);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProduct;
        }

        public List<WorkWitheViewModel> GetallWorkWithName()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetallWorkWithName";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<WorkWitheViewModel> lstProduct = new List<WorkWitheViewModel>();
            while (dr.Read())
            {
                WorkWitheViewModel objProductresponse = new WorkWitheViewModel();
                objProductresponse.RoleId = objBaseSqlManager.GetInt32(dr, "RoleId");
                objProductresponse.RoleName = objBaseSqlManager.GetTextValue(dr, "RoleName");
                objProductresponse.RoleDisplayName = objBaseSqlManager.GetTextValue(dr, "RoleDisplayName");
                lstProduct.Add(objProductresponse);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProduct;
        }

        public List<AreaViewModel> GetallAreaName()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetallAreaName";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<AreaViewModel> lstProduct = new List<AreaViewModel>();
            while (dr.Read())
            {
                AreaViewModel objProductresponse = new AreaViewModel();
                objProductresponse.AreaID = objBaseSqlManager.GetInt32(dr, "AreaID");
                objProductresponse.AreaName = objBaseSqlManager.GetTextValue(dr, "AreaName");
                int a = lstProduct.Where(x => x.AreaName == objProductresponse.AreaName).Count();
                if (a == 0)
                {
                    lstProduct.Add(objProductresponse);
                }
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProduct;
        }


        public List<SubAreaViewModel> GetallSubAreaList()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetallSubAreaList";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<SubAreaViewModel> lstProduct = new List<SubAreaViewModel>();
            while (dr.Read())
            {
                SubAreaViewModel objProductresponse = new SubAreaViewModel();
                objProductresponse.AreaID = objBaseSqlManager.GetInt32(dr, "AreaID");
                objProductresponse.AreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                int a = lstProduct.Where(x => x.AreaName == objProductresponse.AreaName).Count();
                if (a == 0)
                {
                    lstProduct.Add(objProductresponse);
                }

            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProduct;
        }

        #region TOUR FILTER LIST
        public List<TourProgram_MstModel> GetDCRFilterlist(DateTime date, long createdby)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetTourprogramFilterlist";
            string dt;
            if (date.Month != 11 && date.Month != 10 && date.Month != 12 && date.Month != 10)
            {
                dt = "0" + date.Month + "-" + date.Day + "-" + date.Year + " 12:00:00 AM";
            }
            else
            {
                dt = date.Month + "-" + date.Day + "-" + date.Year + " 12:00:00 AM";
            }
            cmdGet.Parameters.AddWithValue("@Createdby", createdby);
            cmdGet.Parameters.AddWithValue("@StartDate", dt);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<TourProgram_MstModel> lstProduct = new List<TourProgram_MstModel>();
            while (dr.Read())
            {
                TourProgram_MstModel objProductresponse = new TourProgram_MstModel();
                objProductresponse.DCRID = objBaseSqlManager.GetInt32(dr, "DCRId");
                objProductresponse.TourID = objBaseSqlManager.GetInt64(dr, "TourID");
                objProductresponse.AreaID = objBaseSqlManager.GetInt64(dr, "AreaID");
                objProductresponse.AreaName = objBaseSqlManager.GetTextValue(dr, "AreaName");
                objProductresponse.Day = objBaseSqlManager.GetTextValue(dr, "Day");
                objProductresponse.SubareaID = objBaseSqlManager.GetInt64(dr, "SubareaID");
                objProductresponse.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                objProductresponse.HQRS = objBaseSqlManager.GetTextValue(dr, "HQRS");
                objProductresponse.ContactPoint = objBaseSqlManager.GetTextValue(dr, "ContactPoint");
                lstProduct.Add(objProductresponse);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProduct;
        }
        #endregion

        #region ADD DCR
        public long AddDCR(DCR_Mst objdcr)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                var exist = context.TourProgram_Mst.Where(i => i.TourID == objdcr.TourId).FirstOrDefault();

                if (objdcr.Tourvariance == true && objdcr.TourId != 0)
                {
                    objdcr.Tourvariance = objdcr.Tourvariance;
                    objdcr.varianceAreaID = objdcr.AreaId;
                    objdcr.varianceSubAreaID = objdcr.SubAreaId;
                    objdcr.AreaId = Convert.ToInt32(exist.AreaID); // Changes according 
                    objdcr.SubAreaId = Convert.ToInt32(exist.SubareaID);
                    objdcr.TourPlanVariation = objdcr.TourPlanVariation;
                }
                else
                {

                }
                //var data = context.DCR_Mst.FirstOrDefault(x => x.TourId == objdcr.TourId);
                if (objdcr.DCRId == 0)
                {

                    context.DCR_Mst.Add(objdcr);
                    context.SaveChanges();
                    return objdcr.DCRId;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        public List<PromotionalViewModel> GetallPromotionalName()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetallPromotionalName";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<PromotionalViewModel> lstProduct = new List<PromotionalViewModel>();
            while (dr.Read())
            {
                PromotionalViewModel objProductresponse = new PromotionalViewModel();
                objProductresponse.PromotionalId = objBaseSqlManager.GetInt32(dr, "PromotionalId");
                objProductresponse.PromotionalName = objBaseSqlManager.GetTextValue(dr, "PromotionalName");
                lstProduct.Add(objProductresponse);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProduct;
        }

        public bool AddPromotional(PromotionalReport_Mst objPromo)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                var data = context.PromotionalReport_Mst.FirstOrDefault(x => x.PromotionalReportId == objPromo.PromotionalReportId);
                if (data == null)
                {
                    context.PromotionalReport_Mst.Add(objPromo);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<PromotionalReportViewModel> GetPromotionalReport()
        {

            List<PromotionalReportViewModel> objects = new List<PromotionalReportViewModel>();
            return objects;
        }

        public List<DCRViewModel> GetDCRReport()
        {
            List<DCRViewModel> objects = new List<DCRViewModel>();
            return objects;
        }

        public List<DCRSummaryReport> DCRSummaryReport(int CurrentUserId, DateTime? StartDate, DateTime? EndDate)
        {
            //string name = string.Empty;
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "DCRSummaryReport";
            cmdGet.Parameters.AddWithValue("@StartDate", StartDate);
            cmdGet.Parameters.AddWithValue("@EndDate", EndDate);
            cmdGet.Parameters.AddWithValue("@CreatedBy", CurrentUserId);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<DCRSummaryReport> lstDCR = new List<DCRSummaryReport>();
            while (dr.Read())
            {
                DCRSummaryReport objdcrsummaryreport = new DCRSummaryReport();
                objdcrsummaryreport.DCRId = objBaseSqlManager.GetInt32(dr, "DCRId");
                objdcrsummaryreport.WorkTypeId = objBaseSqlManager.GetInt32(dr, "WorkTypeId");
                objdcrsummaryreport.WorkType = objBaseSqlManager.GetTextValue(dr, "WorkType");
                objdcrsummaryreport.TourPlanVariation = objBaseSqlManager.GetTextValue(dr, "TourPlanVariation");
                objdcrsummaryreport.AreaID = objBaseSqlManager.GetInt32(dr, "AreaID");
                objdcrsummaryreport.AreaName = objBaseSqlManager.GetTextValue(dr, "AreaName");
                objdcrsummaryreport.SubAreaId = objBaseSqlManager.GetInt32(dr, "SubAreaId");
                objdcrsummaryreport.SubAreaName = objBaseSqlManager.GetTextValue(dr, "SubAreaName");
                objdcrsummaryreport.ReportDate = objBaseSqlManager.GetDateTime(dr, "CreatedOn");
                objdcrsummaryreport.RoleId = objBaseSqlManager.GetTextValue(dr, "WorkWithIds");
                objdcrsummaryreport.WorkWithUserId = objBaseSqlManager.GetTextValue(dr, "WorkWithUserId");
                objdcrsummaryreport.FullName = objBaseSqlManager.GetTextValue(dr, "FullName");
                objdcrsummaryreport.ReportDateInDate = Convert.ToDateTime(objdcrsummaryreport.ReportDate).ToString("dd-MM-yyyy");            
                lstDCR.Add(objdcrsummaryreport);
                //lstDCR.GroupBy(x => new { x.DCRId, x.WorkTypeId, x.WorkType, x.AreaName, x.SubAreaId, x.TourPlanVariation, x.SubAreaName }).FirstOrDefault();
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();

            var data = lstDCR.GroupBy(x => new { x.DCRId, x.RoleId, x.WorkTypeId, x.AreaID, x.WorkWithUserId, x.ReportDate, x.WorkType, x.AreaName, x.SubAreaId, x.TourPlanVariation, x.SubAreaName, x.ReportDateInDate })
                .Select(y => new DCRSummaryReport() { RoleId = y.Key.RoleId, DCRId = y.Key.DCRId, WorkTypeId = y.Key.WorkTypeId, TourPlanVariation= y.Key.TourPlanVariation, WorkType = y.Key.WorkType, AreaID = y.Key.AreaID, AreaName = y.Key.AreaName, SubAreaId = y.Key.SubAreaId, SubAreaName = y.Key.SubAreaName, ReportDate = y.Key.ReportDate, WorkWithUserId = y.Key.WorkWithUserId, ReportDateInDate = y.Key.ReportDateInDate, WorkWithUserName = string.Join(",", y.ToList().Select(z => z.FullName)) });

            return data.ToList();
        }

        #endregion

        public bool DeleteMeetingType(long MeetingTypeId, bool IsActive)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                var exist = context.MeetingType_mst.FirstOrDefault(x => x.MeetingTypeId == MeetingTypeId && x.IsActive == IsActive);
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

        public bool AddMeetingType(MeetingType_mst objMeetingtype)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                if (objMeetingtype.MeetingTypeId == 0)
                {
                    context.MeetingType_mst.Add(objMeetingtype);
                    context.SaveChanges();
                }
                else
                {
                    context.Entry(objMeetingtype).State = EntityState.Modified;
                    context.SaveChanges();
                }
                if (objMeetingtype.MeetingTypeId > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<MeetingType_mst> GetAllMeetingTypelist()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllMeetingTypelist";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<MeetingType_mst> lstWorkType = new List<MeetingType_mst>();
            while (dr.Read())
            {
                MeetingType_mst objWorkTypemst = new MeetingType_mst();
                objWorkTypemst.MeetingTypeId = objBaseSqlManager.GetInt32(dr, "MeetingTypeId");
                objWorkTypemst.MeetingType = objBaseSqlManager.GetTextValue(dr, "MeetingType");
                objWorkTypemst.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                lstWorkType.Add(objWorkTypemst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstWorkType;
        }
    }
}
