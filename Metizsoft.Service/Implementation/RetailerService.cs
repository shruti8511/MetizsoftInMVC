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

namespace Metizsoft.Service.Implementation
{
    public class RetailerService : IRetail
    {
        public bool AddRetailer(Retailer_mst ObjRetailer)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                if (ObjRetailer.ReatailerId == 0)
                {
                    context.Retailer_mst.Add(ObjRetailer);
                    context.SaveChanges();
                }
                else
                {
                    context.Entry(ObjRetailer).State = EntityState.Modified;
                    context.SaveChanges();
                }
                if (ObjRetailer.ReatailerId > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //public List<RetailerMst_mst> GetAllRetailerList()
        //{
        //    SqlCommand cmdGet = new SqlCommand();
        //    BaseSqlManager objBaseSqlManager = new BaseSqlManager();
        //    cmdGet.CommandType = CommandType.StoredProcedure;
        //    cmdGet.CommandText = "GetAllRetailerList";

        //    SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
        //    List<RetailerMst_mst> lstRetailer = new List<RetailerMst_mst>();
        //    while (dr.Read())
        //    {
        //        RetailerMst_mst objRetailermst = new RetailerMst_mst();
        //        objRetailermst.ReatailerId = objBaseSqlManager.GetInt64(dr, "ReatailerId");
        //        objRetailermst.Usertype = objBaseSqlManager.GetInt32(dr, "Usertype");
        //        objRetailermst.FirstName = objBaseSqlManager.GetTextValue(dr, "FirstName");
        //        objRetailermst.LastName = objBaseSqlManager.GetTextValue(dr, "LastName");
        //        objRetailermst.Address = objBaseSqlManager.GetTextValue(dr, "Address");
        //        objRetailermst.State = objBaseSqlManager.GetTextValue(dr, "State");
        //        objRetailermst.City = objBaseSqlManager.GetTextValue(dr, "City");
        //        objRetailermst.Zip = objBaseSqlManager.GetTextValue(dr, "Zip");
        //        objRetailermst.OfficePhone = objBaseSqlManager.GetTextValue(dr, "OfficePhone");
        //        objRetailermst.Mobile = objBaseSqlManager.GetTextValue(dr, "Mobile");
        //        objRetailermst.Email = objBaseSqlManager.GetTextValue(dr, "Email");
        //        objRetailermst.Fax = objBaseSqlManager.GetTextValue(dr, "Fax");
        //        objRetailermst.Dob = objBaseSqlManager.GetDateTime(dr, "Dob");
        //        objRetailermst.strDob = Convert.ToDateTime(objRetailermst.Dob).ToString("dd/MM/yyyy");
        //        objRetailermst.Anniversary = objBaseSqlManager.GetDateTime(dr, "Anniversary");
        //        objRetailermst.strAnniversary = Convert.ToDateTime(objRetailermst.Anniversary).ToString("dd/MM/yyyy");
        //        lstRetailer.Add(objRetailermst);
        //    }
        //    dr.Close();
        //    objBaseSqlManager.ForceCloseConnection();
        //    return lstRetailer;
        //}

        public bool DeleteRetailer(long ReatailerId , bool IsActive)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                //Retailer_mst data = context.Retailer_mst.FirstOrDefault(x => x.ReatailerId == ReatailerId);
                var exist = context.Retailer_mst.FirstOrDefault(x => x.ReatailerId == ReatailerId && x.IsActive == IsActive);
                if (exist != null)
                {
                    exist.IsActive = false;
                    context.Entry(exist).State = EntityState.Modified;
                   // context.Retailer_mst.Remove(exist);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public RetailerMst_mst GetRetailerByRetailerId(int ReatailerId)
        {
            RetailerMst_mst objuser = new RetailerMst_mst();
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetRetailerByRetailerId";
            cmdGet.Parameters.AddWithValue("@ReatailerId", ReatailerId);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            while (dr.Read())
            {

                objuser.ReatailerId = objBaseSqlManager.GetInt64(dr, "ReatailerId");
                objuser.Usertype = objBaseSqlManager.GetInt32(dr, "Usertype");
                objuser.FirstName = objBaseSqlManager.GetTextValue(dr, "FirstName");
                objuser.LastName = objBaseSqlManager.GetTextValue(dr, "LastName");
                objuser.Address = objBaseSqlManager.GetTextValue(dr, "Address");
                objuser.State = objBaseSqlManager.GetTextValue(dr, "State");
                //objuser.CityId = objBaseSqlManager.GetInt32(dr, "");
                objuser.City = objBaseSqlManager.GetTextValue(dr, "City");
                objuser.Zip = objBaseSqlManager.GetTextValue(dr, "Zip");
                objuser.OfficePhone = objBaseSqlManager.GetTextValue(dr, "OfficePhone");
                objuser.Mobile = objBaseSqlManager.GetTextValue(dr, "Mobile");
                objuser.Email = objBaseSqlManager.GetTextValue(dr, "Email");
                objuser.Fax = objBaseSqlManager.GetTextValue(dr, "Fax");
                objuser.Dob = objBaseSqlManager.GetDateTime(dr, "Dob");
                objuser.strDob = Convert.ToDateTime(objuser.Dob).ToString("dd/MM/yyyy");
                objuser.Anniversary = objBaseSqlManager.GetDateTime(dr, "Anniversary");
                objuser.strAnniversary = Convert.ToDateTime(objuser.Anniversary).ToString("dd/MM/yyyy");
                objuser.ContactName = objBaseSqlManager.GetTextValue(dr, "ContactName");
                objuser.Refrence = objBaseSqlManager.GetTextValue(dr, "Refrence");
                objuser.GradeName = objBaseSqlManager.GetInt32(dr, "GradeName");
                objuser.ShortName = objBaseSqlManager.GetTextValue(dr, "ShortName");
                objuser.RetailerCode = objBaseSqlManager.GetTextValue(dr, "RetailerCode");
                objuser.DivName = objBaseSqlManager.GetTextValue(dr, "DivName");
                objuser.DivNamein =Convert.ToInt32(objBaseSqlManager.GetTextValue(dr, "DivName"));
                if (objuser.DivName == "1")
                {
                    objuser.DivName = "Blaze";
                }
                else if (objuser.DivName == "2")
                {
                    objuser.DivName = "Magnar";
                }
                else
                {
                    objuser.DivName = " ";

                }
                objuser.SubArea = objBaseSqlManager.GetTextValue(dr, "SubArea");
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();

            return objuser;
        }

        public List<RetailerMst_mst> GetAllRetailerListUnapprovedByRSM(int UserId)
        {

            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllRetailerListUnapprovedByRSM";
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<RetailerMst_mst> lstretailer = new List<RetailerMst_mst>();
            while (dr.Read())
            {
                RetailerMst_mst objRetailermst = new RetailerMst_mst();
                objRetailermst.ReatailerId = objBaseSqlManager.GetInt64(dr, "ReatailerId");
                objRetailermst.Usertype = objBaseSqlManager.GetInt32(dr, "Usertype");
                objRetailermst.FirstName = objBaseSqlManager.GetTextValue(dr, "FirstName");
                objRetailermst.LastName = objBaseSqlManager.GetTextValue(dr, "LastName");
                objRetailermst.Address = objBaseSqlManager.GetTextValue(dr, "Address");
                objRetailermst.State = objBaseSqlManager.GetTextValue(dr, "State");
                objRetailermst.City = objBaseSqlManager.GetTextValue(dr, "City");
                objRetailermst.Zip = objBaseSqlManager.GetTextValue(dr, "Zip");
                objRetailermst.OfficePhone = objBaseSqlManager.GetTextValue(dr, "OfficePhone");
                objRetailermst.Mobile = objBaseSqlManager.GetTextValue(dr, "Mobile");
                objRetailermst.Email = objBaseSqlManager.GetTextValue(dr, "Email");
                objRetailermst.Fax = objBaseSqlManager.GetTextValue(dr, "Fax");
                objRetailermst.Approved = objBaseSqlManager.GetBoolean(dr, "Approved");
                //objDocotrmst.ApprovedBy = objBaseSqlManager.GetInt32(dr, "ApprovedBy");
                objRetailermst.Dob = objBaseSqlManager.GetDateTime(dr, "Dob");
                objRetailermst.strDob = Convert.ToDateTime(objRetailermst.Dob).ToString("dd/MM/yyyy");
                if (objRetailermst.Dob.ToString("MM/dd/yyyy") == "01/01/0001")
                {
                    objRetailermst.strDob = " ";
                }
                objRetailermst.Anniversary = objBaseSqlManager.GetDateTime(dr, "Anniversary");
                objRetailermst.strAnniversary = Convert.ToDateTime(objRetailermst.Anniversary).ToString("dd/MM/yyyy");
                if (objRetailermst.Anniversary.ToString("MM/dd/yyyy") == "01/01/0001")
                {
                    objRetailermst.strAnniversary = " ";
                }
                lstretailer.Add(objRetailermst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstretailer;
        }

        public int GetApprovedByRollId(int UserID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetApprovedByRollId";
            cmdGet.Parameters.AddWithValue("@UserID", UserID);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            int RoleID = 0;
            while (dr.Read())
            {
                RoleID = objBaseSqlManager.GetInt32(dr, "RoleId");
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return RoleID;
        }

        public bool ApproveRetailerRSM(int ReatailerId, int UserId, int Status)
        {
            try
            {
                SqlCommand cmdGet = new SqlCommand();
                BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                cmdGet.CommandType = CommandType.StoredProcedure;
                cmdGet.CommandText = "ApproveRetailerRSM";
                cmdGet.Parameters.AddWithValue("@ReatailerId", ReatailerId);
                cmdGet.Parameters.AddWithValue("@UserId", UserId);
                cmdGet.Parameters.AddWithValue("@Status", Status);
                object dr = objBaseSqlManager.ExecuteNonQuery(cmdGet);
                objBaseSqlManager.ForceCloseConnection();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Retailer_mst> GetAllRetailerList(long GroupID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllRetailerListForAllocation";
            cmdGet.Parameters.AddWithValue("GroupID", GroupID);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Retailer_mst> lstRetailer = new List<Retailer_mst>();
            while (dr.Read())
            {
                Retailer_mst objRetailermst = new Retailer_mst();
                objRetailermst.ReatailerId = objBaseSqlManager.GetInt64(dr, "ReatailerId");
                objRetailermst.Usertype = objBaseSqlManager.GetInt32(dr, "Usertype");
                objRetailermst.FirstName = objBaseSqlManager.GetTextValue(dr, "FirstName");
                objRetailermst.LastName = objBaseSqlManager.GetTextValue(dr, "LastName");
                objRetailermst.Address = objBaseSqlManager.GetTextValue(dr, "Address");
                objRetailermst.State = objBaseSqlManager.GetTextValue(dr, "State");
                objRetailermst.City = objBaseSqlManager.GetTextValue(dr, "City");
                objRetailermst.Zip = objBaseSqlManager.GetTextValue(dr, "Zip");
                objRetailermst.OfficePhone = objBaseSqlManager.GetTextValue(dr, "OfficePhone");
                objRetailermst.Mobile = objBaseSqlManager.GetTextValue(dr, "Mobile");
                objRetailermst.Email = objBaseSqlManager.GetTextValue(dr, "Email");
                objRetailermst.Fax = objBaseSqlManager.GetTextValue(dr, "Fax");
                lstRetailer.Add(objRetailermst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstRetailer;
        }

        public List<RetailerMst_mst> GetAllRetailerList()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllRetailerLists";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<RetailerMst_mst> lstRetailer = new List<RetailerMst_mst>();
            while (dr.Read())
            {
                RetailerMst_mst objRetailermst = new RetailerMst_mst();
                objRetailermst.ReatailerId = objBaseSqlManager.GetInt64(dr, "ReatailerId");
                objRetailermst.Usertype = objBaseSqlManager.GetInt32(dr, "Usertype");
                objRetailermst.FirstName = objBaseSqlManager.GetTextValue(dr, "FirstName");
                objRetailermst.LastName = objBaseSqlManager.GetTextValue(dr, "LastName");
                objRetailermst.Address = objBaseSqlManager.GetTextValue(dr, "Address");
                objRetailermst.State = objBaseSqlManager.GetTextValue(dr, "State");
                objRetailermst.City = objBaseSqlManager.GetTextValue(dr, "City");
                objRetailermst.Zip = objBaseSqlManager.GetTextValue(dr, "Zip");
                objRetailermst.OfficePhone = objBaseSqlManager.GetTextValue(dr, "OfficePhone");
                objRetailermst.Mobile = objBaseSqlManager.GetTextValue(dr, "Mobile");
                objRetailermst.Email = objBaseSqlManager.GetTextValue(dr, "Email");
                objRetailermst.Fax = objBaseSqlManager.GetTextValue(dr, "Fax");
                objRetailermst.Dob = objBaseSqlManager.GetDateTime(dr, "Dob");
                objRetailermst.strDob = Convert.ToDateTime(objRetailermst.Dob).ToString("dd/MM/yyyy");
                if (objRetailermst.Dob.ToString("MM/dd/yyyy") == "01/01/0001")
                {
                    objRetailermst.strDob = " ";
                }
                objRetailermst.Anniversary = objBaseSqlManager.GetDateTime(dr, "Anniversary");
                objRetailermst.strAnniversary = Convert.ToDateTime(objRetailermst.Anniversary).ToString("dd/MM/yyyy");
                if (objRetailermst.Anniversary.ToString("MM/dd/yyyy") == "01/01/0001")
                {
                    objRetailermst.strAnniversary = " ";
                }
                objRetailermst.ContactName = objBaseSqlManager.GetTextValue(dr, "ContactName");
                objRetailermst.Refrence = objBaseSqlManager.GetTextValue(dr, "Refrence");
                objRetailermst.GradeName = objBaseSqlManager.GetInt32(dr, "GradeName");
                objRetailermst.ShortName = objBaseSqlManager.GetTextValue(dr, "ShortName");
                objRetailermst.RetailerCode = objBaseSqlManager.GetTextValue(dr, "RetailerCode");
                objRetailermst.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                objRetailermst.DivName = objBaseSqlManager.GetTextValue(dr, "DivName");
                if (objRetailermst.DivName == "1")
                {
                    objRetailermst.DivName = "Blaze";
                }
                else if (objRetailermst.DivName == "2")
                {
                    objRetailermst.DivName = "Magnar";
                }
                else
                {
                    objRetailermst.DivName = " ";

                }
                objRetailermst.SubArea = objBaseSqlManager.GetTextValue(dr, "SubArea");

                lstRetailer.Add(objRetailermst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstRetailer;
        }

        public List<RetailerMst_mst> GetAllRetailerListForFilter(RetailerMst_mst model)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllRetailerListForFilter";
            cmdGet.Parameters.AddWithValue("@City", model.City);
            cmdGet.Parameters.AddWithValue("@SubArea", model.SubArea);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<RetailerMst_mst> lstRetailer = new List<RetailerMst_mst>();
            while (dr.Read())
            {
                RetailerMst_mst objRetailermst = new RetailerMst_mst();
                objRetailermst.ReatailerId = objBaseSqlManager.GetInt64(dr, "ReatailerId");
                objRetailermst.Usertype = objBaseSqlManager.GetInt32(dr, "Usertype");
                objRetailermst.FirstName = objBaseSqlManager.GetTextValue(dr, "FirstName");
                objRetailermst.LastName = objBaseSqlManager.GetTextValue(dr, "LastName");
                objRetailermst.Address = objBaseSqlManager.GetTextValue(dr, "Address");
                objRetailermst.State = objBaseSqlManager.GetTextValue(dr, "State");
                objRetailermst.City = objBaseSqlManager.GetTextValue(dr, "City");
                objRetailermst.Zip = objBaseSqlManager.GetTextValue(dr, "Zip");
                objRetailermst.OfficePhone = objBaseSqlManager.GetTextValue(dr, "OfficePhone");
                objRetailermst.Mobile = objBaseSqlManager.GetTextValue(dr, "Mobile");
                objRetailermst.Email = objBaseSqlManager.GetTextValue(dr, "Email");
                objRetailermst.Fax = objBaseSqlManager.GetTextValue(dr, "Fax");
                objRetailermst.Dob = objBaseSqlManager.GetDateTime(dr, "Dob");
                objRetailermst.strDob = Convert.ToDateTime(objRetailermst.Dob).ToString("dd/MM/yyyy");
                if (objRetailermst.Dob.ToString("MM/dd/yyyy") == "01/01/0001")
                {
                    objRetailermst.strDob = " ";
                }
                objRetailermst.Anniversary = objBaseSqlManager.GetDateTime(dr, "Anniversary");
                objRetailermst.strAnniversary = Convert.ToDateTime(objRetailermst.Anniversary).ToString("dd/MM/yyyy");
                if (objRetailermst.Anniversary.ToString("MM/dd/yyyy") == "01/01/0001")
                {
                    objRetailermst.strAnniversary = " ";
                }
                objRetailermst.ContactName = objBaseSqlManager.GetTextValue(dr, "ContactName");
                objRetailermst.Refrence = objBaseSqlManager.GetTextValue(dr, "Refrence");
                objRetailermst.GradeName = objBaseSqlManager.GetInt32(dr, "GradeName");
                objRetailermst.ShortName = objBaseSqlManager.GetTextValue(dr, "ShortName");
                objRetailermst.RetailerCode = objBaseSqlManager.GetTextValue(dr, "RetailerCode");
                objRetailermst.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");

                objRetailermst.DivName = objBaseSqlManager.GetTextValue(dr, "DivName");
                if (objRetailermst.DivName == "1")
                {
                    objRetailermst.DivName = "Blaze";
                }
                else if (objRetailermst.DivName == "2")
                {
                    objRetailermst.DivName = "Magnar";
                }
                else
                {
                    objRetailermst.DivName = " ";

                }
                objRetailermst.SubArea = objBaseSqlManager.GetTextValue(dr, "SubArea");
                lstRetailer.Add(objRetailermst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstRetailer;
        }

        public List<Retailer_mst> GetAllRetailerList(long GroupID = 0, long GroupTypeID = 0)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllRetailerAssignmentList";
            cmdGet.Parameters.AddWithValue("@GroupID", GroupID);
            cmdGet.Parameters.AddWithValue("@GroupTypeID", GroupTypeID);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Retailer_mst> lstDoctor = new List<Retailer_mst>();
            while (dr.Read())
            {
                Retailer_mst objDocotrmst = new Retailer_mst();
                objDocotrmst.ReatailerId = objBaseSqlManager.GetInt32(dr, "ReatailerId");
                objDocotrmst.FirstName = objBaseSqlManager.GetTextValue(dr, "FirstName");
                objDocotrmst.LastName = objBaseSqlManager.GetTextValue(dr, "LastName");
                objDocotrmst.FirstName = objDocotrmst.FirstName + " " + objDocotrmst.LastName;

                lstDoctor.Add(objDocotrmst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstDoctor;
        }

        public long GetLastRetailerCode()
        {
            long RetId = 0;
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetLastRetailerCode";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            while (dr.Read())
            {
                RetId = objBaseSqlManager.GetInt64(dr, "ReatailerId");
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return RetId;
        }

        #region api
        public List<RetailerViewModel> GetAllRetailerListforApi(int UserId, string City, string SubArea)
        {
           // string.IsNullOrEmpty(value)
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllRetailerList";
            cmdGet.Parameters.AddWithValue("@UserId", UserId);
            cmdGet.Parameters.AddWithValue("@City", City);
            cmdGet.Parameters.AddWithValue("@SubArea", SubArea);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<RetailerViewModel> lstRetailer = new List<RetailerViewModel>();
            while (dr.Read())
            {
                RetailerViewModel objRetailermst = new RetailerViewModel();
                objRetailermst.ReatailerId = objBaseSqlManager.GetInt64(dr, "ReatailerId");
                objRetailermst.ReatailerTypeId = objBaseSqlManager.GetInt32(dr, "Usertype");
                objRetailermst.FullName = objBaseSqlManager.GetTextValue(dr, "FullName");

                lstRetailer.Add(objRetailermst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstRetailer;
        }

        public List<RetailerDetailViewModel> GetallRetailerDetail(int UserType)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetallRetailerDetail";
            cmdGet.Parameters.AddWithValue("@Usertype", UserType);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<RetailerDetailViewModel> lstProduct = new List<RetailerDetailViewModel>();
            while (dr.Read())
            {
                RetailerDetailViewModel objProductresponse = new RetailerDetailViewModel();
                objProductresponse.ReatailerId = objBaseSqlManager.GetInt32(dr, "ReatailerId");
                objProductresponse.FullName = objBaseSqlManager.GetTextValue(dr, "FullName");

                lstProduct.Add(objProductresponse);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProduct;
        }

        public bool AddRetailerReport(RetailerReport_Mst objretailrreportmst, List<DoctorProductReport_Mst> lstdpr)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                var data = context.RetailerReport_Mst.FirstOrDefault(x => x.ReatailerReportId == objretailrreportmst.ReatailerReportId);
                if (data == null)
                {
                    context.RetailerReport_Mst.Add(objretailrreportmst);
                    context.SaveChanges();

                    long RetailerReportId = objretailrreportmst.ReatailerReportId;
                    long UserId = Convert.ToInt16(objretailrreportmst.ReatailerId);
                    long Createdby = Convert.ToInt16(objretailrreportmst.CreatedBy);
                    long Updatedby = Convert.ToInt16(objretailrreportmst.UpdatedBy);
                    DateTime CreatedOn = Convert.ToDateTime(objretailrreportmst.CreatedOn);
                    DateTime UpdatedOn = Convert.ToDateTime(objretailrreportmst.UpdatedOn);
                    
                    foreach (var item in lstdpr)
                    {
                        item.ReportId = RetailerReportId;
                        item.UserId = UserId;
                        item.GrouptypeID = 2; // 2 retailer grouptypeId
                        item.CreatedBy = Createdby;
                        item.UpdateBy = Updatedby;
                        item.CreatedOn = CreatedOn;
                        item.UpdateOn = UpdatedOn;
                        context.DoctorProductReport_Mst.Add(item);
                        context.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        public List<RCPAResponse> GetRCPAReport()
        {
            List<RCPAResponse> objects = new List<RCPAResponse>();
            return objects;
        }

        public List<RetailerDCRViewModel> GetRetailerDCRReport()
        {
            List<RetailerDCRViewModel> objects = new List<RetailerDCRViewModel>();
            return objects;
        }

    }
}
