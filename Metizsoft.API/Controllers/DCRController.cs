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
using System.Globalization;

namespace Metizsoft.API.Controllers
{

    public class DCRController : ApiController
    {
        #region CONSTRUCTOR
        private IMaster _IMasterService;

        public DCRController(IMaster MasterServices)
        {
            _IMasterService = MasterServices;

        }
        #endregion

      
        [Route("api/DCR/GetAllAreaSubAreaList")]
        [HttpPost]
        public HttpResponseMessage GetAllAreaSubAreaList()
        {
            try
            {
                AreaViewModel1 responsedata = _IMasterService.GetAllAreaSubAreaList();
                if (responsedata != null)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<AreaViewModel1>.Success(responsedata));
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
     

       
        [Route("api/DCR/GetAllWorkAndWorkWith")]
        [HttpPost]
        public HttpResponseMessage GetAllWorkAndWorkWith()
        {
            try
            {
                WorkAndWorkWithModel responsedata = _IMasterService.GetAllWorkAndWorkWith();
                if (responsedata != null)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<WorkAndWorkWithModel>.Success(responsedata));
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
       

        #region GET WORK TYPE LIST
        [Route("api/DCR/GetallWorkTypeList")]
        [HttpPost]
        public HttpResponseMessage GetallWorkTypeName()
        {
            try
            {
                List<WorkTypeViewModel> responsedata = _IMasterService.GetallWorkTypeName();
                if (responsedata.Count != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<WorkTypeViewModel>>.Success(responsedata));
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

        #region GET WORK WITH LIST
        [Route("api/DCR/GetallWorkWithList")]
        [HttpPost]
        public HttpResponseMessage GetallWorkWithName()
        {
            try
            {
                List<WorkWitheViewModel> responsedata = _IMasterService.GetallWorkWithName();
                if (responsedata.Count != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<WorkWitheViewModel>>.Success(responsedata));
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

        #region GET ALL AREA LIST
        [Route("api/DCR/GetallAreaList")]
        [HttpPost]
        public HttpResponseMessage GetallAreaName()
        {
            try
            {
                List<AreaViewModel> responsedata = _IMasterService.GetallAreaName();
                if (responsedata.Count != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<AreaViewModel>>.Success(responsedata));
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

        #region GET ALL SUBAREA LIST
        [Route("api/DCR/GetallSubAreaList")]
        [HttpPost]
        public HttpResponseMessage GetallSubAreaList()
        {
            try
            {
                List<SubAreaViewModel> responsedata = _IMasterService.GetallSubAreaList();
                if (responsedata.Count != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<SubAreaViewModel>>.Success(responsedata));
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

        #region ADD DCR
        [Route("api/DCR/AddDCR")]
        [HttpPost]
        public HttpResponseMessage AddDCR(DCRViewModel objdcr)
        {
            try
            {
                DCR_Mst objdcrmaster = new DCR_Mst();
                objdcrmaster.AreaId = objdcr.AreaID;
                objdcrmaster.TourId = objdcr.TourId;
                objdcrmaster.AreaNote = objdcr.AreaNote;
                objdcrmaster.WorkTypeId = objdcr.WorkTypeId;
               // var WorkTypeIds = objdcr.WorkWithIds.Split(',');
                objdcrmaster.WorkWithIds = objdcr.WorkWithIds;                
                objdcrmaster.SubAreaId = objdcr.SubAreaId;
                objdcrmaster.TourPlanVariation = objdcr.TourPlanVariation;                             
                objdcrmaster.Tourvariance = objdcr.Tourvariance;
                //objdcrmaster.varianceAreaID = objdcr.AreaID;
                //objdcrmaster.varianceSubAreaID = objdcr.SubAreaId;
                objdcrmaster.Latitude = objdcr.Latitude;
                objdcrmaster.longitude = objdcr.longitude;
                objdcrmaster.SubmissionDate = objdcr.SubmissionDate;
                objdcrmaster.CreatedBy = objdcr.CurrentUserId;
                objdcrmaster.UpdatedBy = objdcr.CurrentUserId;
                objdcrmaster.CreatedOn = DateTime.Now;
                objdcrmaster.UpdatedOn = DateTime.Now;
                objdcrmaster.WorkWithUserId = objdcr.WorkWithUserId;

                long responsedata = _IMasterService.AddDCR(objdcrmaster);
                if (responsedata != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<long>.Success(responsedata));
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

        #region GetDCRFilterlist
        [Route("api/DCR/GetDCRFilterlist")]
        [HttpPost]
        public HttpResponseMessage GetDCRFilterlist(DateTime date, long createdby)
        {
            try
            {
                List<TourProgram_MstModel> responsedata = _IMasterService.GetDCRFilterlist(date, createdby);
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
        
        #region DCR SUMMARY REPORT
        [Route("api/DCR/DCRSummaryReport")]
        [HttpPost]
        public HttpResponseMessage DCRSummaryReport(int CurrentUserId, String StartDate, String EndDate)
        {
            try
            {
                DateTime ConvertedStartDate = DateTime.ParseExact(StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime ConvertedEndDate = DateTime.ParseExact(EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                if (ConvertedStartDate == ConvertedEndDate)
                {
                    ConvertedEndDate = ConvertedEndDate.AddDays(1);
                }
                List<DCRSummaryReport> responsedata = _IMasterService.DCRSummaryReport(CurrentUserId, ConvertedStartDate, ConvertedEndDate);
                if (responsedata.Count != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<DCRSummaryReport>>.Success(responsedata));
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
