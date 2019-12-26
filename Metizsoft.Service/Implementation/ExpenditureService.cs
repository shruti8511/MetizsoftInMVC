using System;
using Metizsoft.Data;
using Metizsoft.Data.Modal;
using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metizsoft.Service.Models;
using Metizsoft.Data.ViewModal;
using System.IO;
using System.Configuration;

namespace Metizsoft.Service.Implementation
{
    public class ExpenditureService : IExpenditure
    {
        public bool AddExpenditure(Expenser_Mst objexpen)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                if (objexpen.ExpenserId == 0)
                {
                    context.Expenser_Mst.Add(objexpen);
                    context.SaveChanges();
                }
                else
                {
                    context.Entry(objexpen).State = EntityState.Modified;
                    context.SaveChanges();
                }
                if (objexpen.ExpenserId > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<Expenser_Mst> GetAllExpenditureList()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetallExpenserlist";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Expenser_Mst> lstExpensers = new List<Expenser_Mst>();
            while (dr.Read())
            {
                Expenser_Mst objExpensermst = new Expenser_Mst();
                objExpensermst.ExpenserId = objBaseSqlManager.GetInt32(dr, "ExpenserId");
                objExpensermst.ExpenserName = objBaseSqlManager.GetTextValue(dr, "ExpenserName");
                //objExpensermst.IsActive = objBaseSqlManager.GetBoolean(dr, "Active");
                lstExpensers.Add(objExpensermst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstExpensers;
        }

        public bool DeleteExpenditure(long ExpenserId)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                Expenser_Mst data = context.Expenser_Mst.FirstOrDefault(x => x.ExpenserId == ExpenserId);
                if (data != null)
                {
                    context.Expenser_Mst.Remove(data);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public List<ExpenditureResponse> GetExpenditureReport()
        {
            List<ExpenditureResponse> objects = new List<ExpenditureResponse>();
            return objects;
        }

        #region Api
        public bool AddExpenditureReport(ExpenserReport_Mst objExpenditurereportmst)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                var data = context.ExpenserReport_Mst.FirstOrDefault(x => x.ExpenserReportId == objExpenditurereportmst.ExpenserReportId);
                if (data == null)
                {
                    context.ExpenserReport_Mst.Add(objExpenditurereportmst);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<ExpenditureViewModel> GetallExpenditureList(int CreatedBy, DateTime? StartDate, DateTime? EndDate)
       {
           string DDate = Convert.ToDateTime(StartDate).ToString("yyyy/MM/dd");
           string DDate1 = Convert.ToDateTime(EndDate).ToString("yyyy/MM/dd");
           if (DDate == "0001/01/01" && DDate1 == "0001/01/01" || DDate1 == "0001/01/02")
           {
               DDate = null;
               DDate1 = null;
           }  
            string path = ConfigurationManager.AppSettings["PromotionalImage"];
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllExpenditureList";
            cmdGet.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmdGet.Parameters.AddWithValue("@StartDate", DDate);
            cmdGet.Parameters.AddWithValue("@EndDate", DDate1);            
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<ExpenditureViewModel> lstProduct = new List<ExpenditureViewModel>();
            while (dr.Read())
            {
                ExpenditureViewModel objProductresponse = new ExpenditureViewModel();
                objProductresponse.ExpenserReportId = objBaseSqlManager.GetInt32(dr, "ExpenserReportId");
                objProductresponse.Amount = objBaseSqlManager.GetInt32(dr, "Amount");
                objProductresponse.Note = objBaseSqlManager.GetTextValue(dr, "Note");
                objProductresponse.Reason = objBaseSqlManager.GetTextValue(dr, "Reason");

                DateTime DateCreatedOn = objBaseSqlManager.GetDateTime(dr, "CreatedOn");
                objProductresponse.CreatedOn = Convert.ToDateTime(DateCreatedOn).ToString("yyyy-MM-dd");

                DateTime DateUpdatedOn = objBaseSqlManager.GetDateTime(dr, "UpdatedOn");
                objProductresponse.UpdatedOn = Convert.ToDateTime(DateUpdatedOn).ToString("yyyy-MM-dd");

                objProductresponse.ExpenserImage1 = objBaseSqlManager.GetTextValue(dr, "ExpenserImage");
                if (objProductresponse.ExpenserImage1 == "")
                {
                    objProductresponse.ExpenserImage1 = null;
                }
                else
                {
                    objProductresponse.ExpenserImage = path + objProductresponse.ExpenserImage1;
                    //string ImageNames = objProductresponse.ExpenserImage;
                    //string SavedPath = HttpContext.Current.Server.MapPath("~/PromotionalImage/" + ImageNames);
                    //objProductresponse.ExpenserImage = SavedPath;
                }
                lstProduct.Add(objProductresponse);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProduct;
        }       
        #endregion
    }
}
