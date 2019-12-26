using Metizsoft.Data.Modal;
using Metizsoft.Data.ViewModal;
using Metizsoft.Service;
using Metizsoft.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Metizsoft.API.Controllers
{
    public class TourProgramController : ApiController
    {
        #region Constructor Call
        private ITourProgram _ITourProgramService;

        public TourProgramController(ITourProgram TourProgramService)
        {
            _ITourProgramService = TourProgramService;

        }
        #endregion

        #region  ADD TOUR
        [Route("api/TourProgram/AddTourProgram")]
        [HttpPost]
        public HttpResponseMessage AddTourProgram(List<TourProgramArea_Mst> Data)
        {
            bool responsedata = false;
            try
            {
                foreach (var item in Data)
                {
                    TourProgram_Mst objdcrmaster = new TourProgram_Mst();
                    //objdcrmaster.TourID = item.TourID;
                    objdcrmaster.AreaID = item.AreaID;
                    objdcrmaster.SubareaID = item.SubareaID;
                    objdcrmaster.Day = item.Day;
                    if (item.TourDate != null)
                        objdcrmaster.Date = Convert.ToDateTime(item.TourDate);
                    else
                        objdcrmaster.Date = null;

                    objdcrmaster.HQRS = item.HQRS;
                    objdcrmaster.ContactPoint = item.ContactPoint;
                    objdcrmaster.IsApproved = "Pending";
                    //  objdcrmaster.ApprovedBy = "";
                    objdcrmaster.CreatedBy = item.UserID;
                    objdcrmaster.UpdatedBy = item.UserID;
                    objdcrmaster.CreatedOn = DateTime.Now;
                    objdcrmaster.UpdatedOn = DateTime.Now;
                    objdcrmaster.IsActive = true;

                    responsedata = _ITourProgramService.AddTourProgram(objdcrmaster);
                }
                if (responsedata != false)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<bool>.Success(responsedata));
                    HttpRequestHeaders headers = Request.Headers;
                    return TokenHelper.Addresponse(response, headers);
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<string>.Error("Something Went Wrong", "Tour Program Add Fail"));
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

        #region GetDCRFilterlist
        [Route("api/TourProgram/GetTourProgramMonthlylist")]
        [HttpPost]
        public HttpResponseMessage TourProgramFilterlist(string Date, long CurrentUserId)
        {
            try
            {
                var data = Date.Split('-');

                List<TourProgram_MstModel> responsedata = _ITourProgramService.GetTourprogramMonthlylist(data[0], data[1], CurrentUserId);
                if (responsedata.Count != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<TourProgram_MstModel>>.Success(responsedata));
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

        #region GET VARIANCE LIST
        [Route("api/TourProgram/GetVarianceList")]
        [HttpPost]
        public HttpResponseMessage TourProgramFilterlist(string CurrentUserId)
        {
            try
            {
                List<TourProgram_Varianceresponce> responsedata = _ITourProgramService.GetVarianceList(CurrentUserId);
                if (responsedata.Count != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<TourProgram_Varianceresponce>>.Success(responsedata));
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

        #region Change TourStaus
        [Route("api/TourProgram/ApproveTour")]
        [HttpPost]
        public HttpResponseMessage ApproveTour(long TourId, long ApprovedBy, string Status)
        {
            try
            {
                bool responsedata = _ITourProgramService.ApproveTour(TourId, ApprovedBy, Status);
                if (responsedata == true)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<string>.Success("Tour Status Changed to " + Status));
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

        #region DELETE Tour
        [Route("api/TourProgram/DeleteTourProgram")]
        [HttpPost]
        public HttpResponseMessage DeleteTourProgram(long TourID)
        {            
            try
            {
                bool responsedata = _ITourProgramService.DeleteTourProgram(TourID);
                if (responsedata == true)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<bool>.Success(responsedata));                 
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

        #region GET Tour List
        [Route("api/TourProgram/TourPlanList")]
        [HttpPost]
        public HttpResponseMessage TourPlanList(string UserId)
        {
            try
            {
                List<TourPlanModel> responsedata = _ITourProgramService.GetTourList(UserId);
                if (responsedata.Count != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<TourPlanModel>>.Success(responsedata));
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
    }
}
