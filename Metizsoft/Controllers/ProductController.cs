using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Metizsoft.Data;
using Metizsoft.Service;
using Metizsoft.Data.Modal;
using System.Web.Mvc;
using MapYourMeds.App_Start;
using Metizsoft.Service.Models;

namespace MapYourMeds.Controllers
{
    public class ProductController : Controller
    {

        private IProduct _IProductservice;
        private ICommon _ICommonservice;

        public ProductController(IProduct IProductservice, ICommon ICommonService)
        {
            _IProductservice = IProductservice;
            _ICommonservice = ICommonService;
        }

        #region Product
        [SessionAuthentication]
        public ActionResult ProductList()
        {
            List<Product_Mst> ObjProduct = _IProductservice.GetAllProductList();
            return View(ObjProduct);
        }

        [SessionAuthentication]
        public ActionResult ProductCreate()
        {
            return View();
        }

        [SessionAuthentication]
        [HttpPost]
        public ActionResult ProductCreate(Product_Mst ObjProduct)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    if (ObjProduct.ProductId == 0)
                    {
                        ObjProduct.ProductCode = GetLastProductCode();
                    }
                    ObjProduct.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    ObjProduct.UpdateBy = Convert.ToInt32(Session["UserID"]);
                }
                ObjProduct.CreatedOn = DateTime.UtcNow;
                ObjProduct.UpdateOn = DateTime.UtcNow;
                ObjProduct.IsActive = true;

                bool data = _IProductservice.AddProduct(ObjProduct);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteProduct(long ID, bool IsActive)
        {
            try
            {
                bool data = _IProductservice.DeleteProduct(ID, IsActive);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult EditProduct(int ProductId)
        {
            ProductMst_mst objuser = _IProductservice.GetProductByProductId(ProductId);
            return Json(objuser, JsonRequestBehavior.AllowGet);
        }

        #region auto generated ProductCode
        private string GetLastProductCode()
        {
            long ProId = _IProductservice.GetLastProductCode();
            string ProductCode = "";
            if (ProId != 0)
            {
                string code = ProId.ToString();
                //string number = code.Substring(1, 7);
                long incr = Convert.ToInt64(code) + 1;
                ProductCode = "PRO" + incr.ToString().PadLeft(4, '0');
            }
            else
            {
                ProductCode = "PRO0001";
            }
            return ProductCode;
        }
        #endregion

        #endregion

        #region ProductCase

        #region ProductCaseList
        [SessionAuthentication]
        public ActionResult ProductCaseList()
        {
            List<ProductCase_Mst> objProductcase = _IProductservice.GetAllProductCaseList();
            return View(objProductcase);
        }
        #endregion

        #region ProductCaseCreate
        [SessionAuthentication]
        public ActionResult ProductCaseCreate()
        {
            ViewBag.Product = _IProductservice.GetAllProductName();
            ViewBag.ProductType = _ICommonservice.GetProductTyepeListForDropDown();
            //ViewBag.Speciality = _IProductservice.GetAllSpecialityName();
            return View();
        }

        [SessionAuthentication]
        [HttpPost]
        public ActionResult ProductCaseCreate(ProductCase_Mst objProductcase)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    // if (objProductcase.ProductCaseId == 0)
                    // {
                    objProductcase.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    // }
                    objProductcase.UpdatedBy = Convert.ToInt32(Session["UserID"]);
                }
                objProductcase.CreatedOn = DateTime.UtcNow;
                objProductcase.UpdatedOn = DateTime.UtcNow;
                objProductcase.IsActive = true;
                bool data = _IProductservice.AddProductcase(objProductcase);
                ViewBag.Product = _IProductservice.GetAllProductName();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Delete ProductCase
        public JsonResult DeleteProductCase(long ID, bool IsActive)
        {
            try
            {
                bool data = _IProductservice.DeleteProductCase(ID, IsActive);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Get Name by RxType
        [HttpPost]
        public JsonResult GetNameByRxType(int RxType)
        {
            var lstdata = _IProductservice.GetNameByRxType(RxType);
            return Json(lstdata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Edit ProductCase
        [HttpPost]
        public JsonResult EditProductCase(int ProductCaseId)
        {
            // ViewBag.Name = _IProductservice.GetNameByRxType(RxType);
            ProductCaseMst_mst objproductcase = _IProductservice.GetProductCaseByProductCaseId(ProductCaseId);
            return Json(objproductcase, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #endregion

    }
}