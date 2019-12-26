using Metizsoft.Service.Models;
using Metizsoft.Data.Modal;
using Metizsoft.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Metizsoft.API.Controllers
{
    public class RetailerController : ApiController
    {
        #region CONSTRUCTOR CALL
        private IRetail _IRetailService;


        public RetailerController(IRetail IRetailService)
        {
            _IRetailService = IRetailService;

        } 
        #endregion
        
        #region GET ALL RETAILER DETAIL
        [Route("api/DCR/GetallRetailerDetail")]
        [HttpPost]
        public HttpResponseMessage GetallRetailerDetail(int UserType)
        {
            try
            {
                List<RetailerDetailViewModel> responsedata = _IRetailService.GetallRetailerDetail(UserType);
                if (responsedata.Count != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<RetailerDetailViewModel>>.Success(responsedata));
                    HttpRequestHeaders headers = Request.Headers;
                    return TokenHelper.Addresponse(response, headers);
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<string>.Error("Data Not found", "Data Not found"));
                    HttpRequestHeaders headers = Request.Headers;
                    return TokenHelper.Addresponse(response, headers);
                }
            }
            catch (Exception ex)
            {
                ResponseFailed obj = new ResponseFailed();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, customresponse<ResponseFailed>.Error(ex.Message, obj.Response));
                HttpRequestHeaders headers = Request.Headers;
                return TokenHelper.Addresponse(response, headers);
            }
        } 
        #endregion
        
        #region GET ALL RETAILER LIST
        [Route("api/DCR/GetallRetailerList")]
        [HttpPost]
        public HttpResponseMessage GetallRetailerList(int UserId = 0, string City = null, string SubArea = null)
        {
            try
            {
                List<RetailerViewModel> responsedata = _IRetailService.GetAllRetailerListforApi(UserId, City, SubArea);
                if (responsedata.Count != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<RetailerViewModel>>.Success(responsedata));
                    HttpRequestHeaders headers = Request.Headers;
                    return TokenHelper.Addresponse(response, headers);
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<string>.Error("Data Not found", "Data Not found"));
                    HttpRequestHeaders headers = Request.Headers;
                    return TokenHelper.Addresponse(response, headers);
                }
            }
            catch (Exception ex)
            {
                ResponseFailed obj = new ResponseFailed();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, customresponse<ResponseFailed>.Error(ex.Message, obj.Response));
                HttpRequestHeaders headers = Request.Headers;
                return TokenHelper.Addresponse(response, headers);
            }
        } 
        #endregion
        
        #region ADD RETAILE
        [Route("api/DCR/AddRetailer")]
        [HttpPost]
        public HttpResponseMessage AddRetailerReport(MultipleProductRetailerReport objretadd)
        {
            try
            {
                MultipleProductRetailerReport objmlt = new MultipleProductRetailerReport();
                RetailerReport_Mst objretreport = new RetailerReport_Mst();
                objretreport.ReatailerId = objretadd.ReatailerId;
                objretreport.AreaId = objretadd.AreaId;
                objretreport.DCRId = objretadd.DCRId;
                objretreport.SubAreaId = objretadd.SubAreaId;
                objretreport.ProductTypeId = objretadd.ProductTypeId;            

                objretreport.DiscussionPad = objretadd.DiscussionPad;            
                objretreport.CreatedBy = objretadd.CurrentUserId;
                objretreport.UpdatedBy = objretadd.CurrentUserId;

                objretreport.CreatedOn = DateTime.Now;
                objretreport.UpdatedOn = DateTime.Now;

                List<DoctorProductReport_Mst> lstdpr = new List<DoctorProductReport_Mst>();

                foreach (var item in objretadd.products)
                {
                    DoctorProductReport_Mst objdocpro = new DoctorProductReport_Mst();
                    objdocpro.ProductId = item.ProductId;
                    objdocpro.ProductCaseId = item.ProductCaseId;
                    objdocpro.ProductName = item.ProductName;
                    objdocpro.CaseName = item.CaseName;
                    objdocpro.RxType = item.RxType;
                    objdocpro.ValueType = item.ValueType;
                    objdocpro.WeigtageType = item.WeigtageType;
                    objdocpro.Quantity = item.Quantity;
                    lstdpr.Add(objdocpro);
                }

                bool responsedata = _IRetailService.AddRetailerReport(objretreport, lstdpr);
                if (responsedata)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<bool>.Success(responsedata));
                    HttpRequestHeaders headers = Request.Headers;
                    return TokenHelper.Addresponse(response, headers);
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<string>.Error("Something Went Wrong", "DCR Add Fail"));
                    HttpRequestHeaders headers = Request.Headers;
                    return TokenHelper.Addresponse(response, headers);
                }

            }
            catch (Exception ex)
            {
                ResponseFailed obj = new ResponseFailed();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, customresponse<ResponseFailed>.Error(ex.Message, obj.Response));
                HttpRequestHeaders headers = Request.Headers;
                return TokenHelper.Addresponse(response, headers);
            }

        } 
        #endregion
    }
}
