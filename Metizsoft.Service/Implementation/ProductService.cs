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
using Metizsoft.Data.ViewModal;

namespace Metizsoft.Service.Implementation
{
    public class ProductService : IProduct
    {
        public bool AddProduct(Product_Mst objproduct)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                if (objproduct.ProductId == 0)
                {
                    context.Product_Mst.Add(objproduct);
                    context.SaveChanges();
                }
                else
                {
                    context.Entry(objproduct).State = EntityState.Modified;
                    context.SaveChanges();
                }
                if (objproduct.ProductId > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<Product_Mst> GetAllProductList()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllProductlist";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Product_Mst> lstProduct = new List<Product_Mst>();
            while (dr.Read())
            {
                Product_Mst objProductemst = new Product_Mst();
                objProductemst.ProductId = objBaseSqlManager.GetInt32(dr, "ProductId");
                objProductemst.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objProductemst.Active = objBaseSqlManager.GetBoolean(dr, "Active");
                objProductemst.ProductCode = objBaseSqlManager.GetTextValue(dr, "ProductCode");
                objProductemst.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                lstProduct.Add(objProductemst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProduct;
        }

        public bool DeleteProduct(long ProductId , bool IsActive)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                var exist = context.Product_Mst.FirstOrDefault(x => x.ProductId == ProductId && x.IsActive == IsActive);
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

        public bool AddProductcase(ProductCase_Mst objProductcase)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                if (objProductcase.ProductCaseId == 0)
                {
                    context.ProductCase_Mst.Add(objProductcase);
                    context.SaveChanges();
                }
                else
                {
                    context.Entry(objProductcase).State = EntityState.Modified;
                    context.SaveChanges();
                }
                if (objProductcase.ProductCaseId > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<ProductCase_Mst> GetAllProductName()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllProductName";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<ProductCase_Mst> lstProductcase = new List<ProductCase_Mst>();
            while (dr.Read())
            {
                ProductCase_Mst objProductcasemst = new ProductCase_Mst();
                objProductcasemst.ProductId = objBaseSqlManager.GetInt32(dr, "ProductId");
                objProductcasemst.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                lstProductcase.Add(objProductcasemst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProductcase;
        }

        public List<ProductCase_Mst> GetAllProductCaseList()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllProductCaseList";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<ProductCase_Mst> lstProductcase = new List<ProductCase_Mst>();
            while (dr.Read())
            {
                ProductCase_Mst objProductCasemst = new ProductCase_Mst();
                objProductCasemst.ProductCaseId = objBaseSqlManager.GetInt32(dr, "ProductCaseId");
                objProductCasemst.ProductId = objBaseSqlManager.GetInt32(dr, "ProductId");
                objProductCasemst.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objProductCasemst.CaseName = objBaseSqlManager.GetTextValue(dr, "CaseName");
                objProductCasemst.Name = objBaseSqlManager.GetTextValue(dr, "Name");
                objProductCasemst.PTS = objBaseSqlManager.GetTextValue(dr, "PTS");
                objProductCasemst.MeasureIn = objBaseSqlManager.GetTextValue(dr, "MeasureIn");
                objProductCasemst.StrangthIn = objBaseSqlManager.GetTextValue(dr, "StrangthIn");
                objProductCasemst.Pack = objBaseSqlManager.GetTextValue(dr, "Pack");
                objProductCasemst.PTR = objBaseSqlManager.GetTextValue(dr, "PTR");
                objProductCasemst.MRP = objBaseSqlManager.GetTextValue(dr, "MRP");
                objProductCasemst.STDRate = objBaseSqlManager.GetTextValue(dr, "STDRate");
                objProductCasemst.BrandName = objBaseSqlManager.GetTextValue(dr, "BrandName");
                objProductCasemst.ProductTypeId = objBaseSqlManager.GetTextValue(dr, "ProductTypeId");
                objProductCasemst.IsActive = objBaseSqlManager.GetBoolean(dr, "IsActive");
                lstProductcase.Add(objProductCasemst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProductcase;
        }

        public bool DeleteProductCase(long ProductCaseId , bool IsActive)
        {
            using (AllurionEntities context = new AllurionEntities())
            {
                var exist = context.ProductCase_Mst.FirstOrDefault(x => x.ProductCaseId == ProductCaseId && x.IsActive == IsActive);
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

        public ProductMst_mst GetProductByProductId(int ProductId)
        {
            ProductMst_mst objproduct = new ProductMst_mst();
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetProductByProductId";
            cmdGet.Parameters.AddWithValue("@ProductId", ProductId);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            while (dr.Read())
            {

                objproduct.ProductId = objBaseSqlManager.GetInt32(dr, "ProductId");
                objproduct.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objproduct.ProductCode = objBaseSqlManager.GetTextValue(dr, "ProductCode");
                objproduct.Active = objBaseSqlManager.GetBoolean(dr, "Active");
                
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objproduct;
        }

        public List<RxTypemodel_mst> GetNameByRxType(int RxType)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetNameByRxType";
            cmdGet.Parameters.AddWithValue("@RxType", RxType);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<RxTypemodel_mst> objlstDealer = new List<RxTypemodel_mst>();
            while (dr.Read())
            {
                RxTypemodel_mst objUser = new RxTypemodel_mst();
                objUser.RxTypeId = objBaseSqlManager.GetInt32(dr, "RxTypeId");
                objUser.Name = objBaseSqlManager.GetTextValue(dr, "Name");

                objlstDealer.Add(objUser);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return objlstDealer;
        }

        public ProductCaseMst_mst GetProductCaseByProductCaseId(int ProductCaseId)
        {
            ProductCaseMst_mst objproductcase = new ProductCaseMst_mst();
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetProductCaseByProductCaseId";
            cmdGet.Parameters.AddWithValue("@ProductCaseId", ProductCaseId);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            while (dr.Read())
            {
                objproductcase.ProductCaseId = objBaseSqlManager.GetInt32(dr, "ProductCaseId");
                objproductcase.ProductId = objBaseSqlManager.GetInt32(dr, "ProductId");
                objproductcase.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objproductcase.CaseName = objBaseSqlManager.GetTextValue(dr, "CaseName");
                objproductcase.Name = objBaseSqlManager.GetTextValue(dr, "Name");
                objproductcase.PTS = objBaseSqlManager.GetDecimal(dr, "PTS");
                objproductcase.MeasureIn = objBaseSqlManager.GetTextValue(dr, "MeasureIn");
                objproductcase.StrangthIn = objBaseSqlManager.GetTextValue(dr, "StrangthIn");
                objproductcase.Pack = objBaseSqlManager.GetTextValue(dr, "Pack");
                objproductcase.PTR = objBaseSqlManager.GetTextValue(dr, "PTR");
                objproductcase.MRP = objBaseSqlManager.GetTextValue(dr, "MRP");
                objproductcase.STDRate = objBaseSqlManager.GetTextValue(dr, "STDRate");
                objproductcase.BrandName = objBaseSqlManager.GetTextValue(dr, "BrandName");
                objproductcase.ProductTypeId = objBaseSqlManager.GetTextValue(dr, "ProductTypeId");

            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();

            return objproductcase;
        }

        public List<RxTypemodel_mst> GetAllSpecialityName()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllSpecialityName";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<RxTypemodel_mst> lstProductcase = new List<RxTypemodel_mst>();
            while (dr.Read())
            {
                RxTypemodel_mst objProductcasemst = new RxTypemodel_mst();
                objProductcasemst.RxTypeId = objBaseSqlManager.GetInt32(dr, "RxTypeId");
                objProductcasemst.Name = objBaseSqlManager.GetTextValue(dr, "Name");
                lstProductcase.Add(objProductcasemst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProductcase;
        }

        public List<Product_Mst> GetAllProductList(long GroupID = 0, long GroupTypeID = 0)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllProductAssignmentList";
            cmdGet.Parameters.AddWithValue("@GroupID", GroupID);
            cmdGet.Parameters.AddWithValue("@GroupTypeID", GroupTypeID);

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Product_Mst> lstDoctor = new List<Product_Mst>();
            while (dr.Read())
            {
                Product_Mst objDocotrmst = new Product_Mst();
                objDocotrmst.ProductId = objBaseSqlManager.GetInt32(dr, "ProductId");
                objDocotrmst.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                lstDoctor.Add(objDocotrmst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstDoctor;
        }

        public List<Product_Mst> GetAllProductList(long GroupID)
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllProductlistForAllocation";
            cmdGet.Parameters.AddWithValue("@GroupID", GroupID);
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<Product_Mst> lstProduct = new List<Product_Mst>();
            while (dr.Read())
            {
                Product_Mst objProductemst = new Product_Mst();
                objProductemst.ProductId = objBaseSqlManager.GetInt32(dr, "ProductId");
                objProductemst.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                objProductemst.Active = objBaseSqlManager.GetBoolean(dr, "Active");
                lstProduct.Add(objProductemst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProduct;
        }

        public List<ProductCase_Mst> GetAllProductNameDropDown()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllProductNameDropDown";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<ProductCase_Mst> lstProductcase = new List<ProductCase_Mst>();
            while (dr.Read())
            {
                ProductCase_Mst objProductcasemst = new ProductCase_Mst();
                objProductcasemst.ProductId = objBaseSqlManager.GetInt32(dr, "ProductId");
                objProductcasemst.ProductName = objBaseSqlManager.GetTextValue(dr, "ProductName");
                lstProductcase.Add(objProductcasemst);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProductcase;
        }

        public List<ProductListResponse> GetProductReport()
        {
            List<ProductListResponse> objects = new List<ProductListResponse>();
            return objects;
        }

        public List<ProductTargetViewModel> GetProductTargetReport()
        {
            List<ProductTargetViewModel> objects = new List<ProductTargetViewModel>();
            return objects;
        }

        public long GetLastProductCode()
        {
            long ProId = 0;
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetLastProductCode";
            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            while (dr.Read())
            {
                ProId = objBaseSqlManager.GetInt64(dr, "ProductId");
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return ProId;
        }

        #region Api
        public List<ProductTypeResponse> GetAllProductTypeName()
        {
            SqlCommand cmdGet = new SqlCommand();
            BaseSqlManager objBaseSqlManager = new BaseSqlManager();
            cmdGet.CommandType = CommandType.StoredProcedure;
            cmdGet.CommandText = "GetAllProductTypeName";

            SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
            List<ProductTypeResponse> lstProductType = new List<ProductTypeResponse>();
            while (dr.Read())
            {
                ProductTypeResponse objProducttype = new ProductTypeResponse();
                objProducttype.ProductTypeId = objBaseSqlManager.GetInt32(dr, "ProductTypeId");
                objProducttype.ProductType = objBaseSqlManager.GetTextValue(dr, "ProductType");
                lstProductType.Add(objProducttype);
            }
            dr.Close();
            objBaseSqlManager.ForceCloseConnection();
            return lstProductType;
        }
        #endregion
    }
}
