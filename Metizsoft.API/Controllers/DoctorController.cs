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
using Metizsoft.Data.ViewModal;
using System.Data.SqlClient;
using System.Data;

namespace Metizsoft.API.Controllers
{
    public class DoctorController : ApiController
    {
        #region CONSTRUCTOR CALL
        private IDoctor _IDoctorService;
        public DoctorController(IDoctor DoctorService)
        {
            _IDoctorService = DoctorService;

        }
        #endregion

        #region GetAll Product Name
        [Route("api/DCR/GetallProductName")]
        [HttpPost]
        public HttpResponseMessage GetallProductName()
        {
            try
            {
                ProductViewModel1 responsedata = _IDoctorService.GetallProductName();
                if (responsedata != null)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<ProductViewModel1>.Success(responsedata));
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

        #region GET ALL DOCTOR NAME
        [Route("api/DCR/GetallDoctorName")]
        [HttpPost]
        public HttpResponseMessage GetallDoctorName(int UserId = 0, string Area = null, string SubArea = null)
        {
            try
            {
                DoctorViewModel responsedata = _IDoctorService.GetallDoctorName(UserId, Area, SubArea);
                if (responsedata != null)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<DoctorViewModel>.Success(responsedata));
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

        #region Add DOCTOR REPORT
        [Route("api/DCR/AddDoctorReport")]
        [HttpPost]
        public HttpResponseMessage AddDoctorReport(MultipleProductDoctorReport objdoctorreport)
        {
            try
            {
                string result = "";
                MultipleProductDoctorReport objmilt = new MultipleProductDoctorReport();              
                DoctorReport_Mst objdoctorreportmst = new DoctorReport_Mst();
                objdoctorreportmst.DoctorId = objdoctorreport.DoctorId;
                objdoctorreportmst.DCRId = objdoctorreport.DCRId;
                objdoctorreportmst.DiscussionPad = objdoctorreport.DiscussionPad;
                objdoctorreportmst.Prescription = objdoctorreport.Prescription;
                objdoctorreportmst.PrescriptionText = objdoctorreport.PrescriptionText;
                //objdoctorreportmst.Medicine = objdoctorreport.Medicine;
                objdoctorreportmst.VisitSection = objdoctorreport.VisitSection;
                objdoctorreportmst.MeetingTypeId = objdoctorreport.MeetingTypeId;
                objdoctorreportmst.ProductTypeId = objdoctorreport.ProductTypeId;

                objdoctorreportmst.CreatedBy = objdoctorreport.CurrentUserId;
                objdoctorreportmst.UpdatedBy = objdoctorreport.CurrentUserId;

                objdoctorreportmst.CreatedOn = DateTime.Now;
                objdoctorreportmst.UpdatedOn = DateTime.Now;

                #region MYregion
                //try
                //{
                //    SqlCommand cmdGet = new SqlCommand();
                //    BaseSqlManager objBaseSqlManager = new BaseSqlManager();
                //    cmdGet.CommandType = CommandType.StoredProcedure;
                //    cmdGet.CommandText = "CheckMeetingType";
                //    cmdGet.Parameters.AddWithValue("@DoctorId", objdoctorreportmst.DoctorId);
                //    cmdGet.Parameters.AddWithValue("@CreatedBy", objdoctorreportmst.CreatedBy);
                //    SqlDataReader dr = objBaseSqlManager.ExecuteDataReader(cmdGet);
                //    while (dr.Read())
                //    {
                //        objdoctorreportmst.MeetingTypeId = objBaseSqlManager.GetInt32(dr, "MeetingTypeId");
                //        result = "your meeting " + objdoctorreportmst.MeetingTypeId + " is completed so your next meeting selected.";
                //        objdoctorreportmst.MeetingTypeId = objdoctorreportmst.MeetingTypeId + 1;
                //    }
                //    if(objdoctorreportmst.MeetingTypeId == null )
                //    {
                //        objdoctorreportmst.MeetingTypeId = 1;
                //    }
                //    dr.Close();
                //    objBaseSqlManager.ForceCloseConnection();
                //}
                //catch (Exception ex)
                //{
                //    return null;
                //}
                #endregion

                List<DoctorProductReport_Mst> lstproduct = new List<DoctorProductReport_Mst>();
                           
                foreach (var item in objdoctorreport.products)
                {
                    DoctorProductReport_Mst objproduct = new DoctorProductReport_Mst();
                    objproduct.ProductId = item.ProductId;
                    objproduct.ProductCaseId = item.ProductCaseId;
                    objproduct.ProductName = item.ProductName;
                    objproduct.CaseName = item.CaseName;
                    objproduct.RxType = item.RxType;
                    objproduct.ValueType = item.ValueType;
                    objproduct.WeigtageType = item.WeigtageType;
                    objproduct.Quantity = item.Quantity;
                    lstproduct.Add(objproduct);

                }
                bool responsedata = _IDoctorService.AddDoctorReport(objdoctorreportmst, lstproduct);
                if (responsedata)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<bool>.Success(responsedata));
                    HttpRequestHeaders headers = Request.Headers;
                    return TokenHelper.Addresponse(response, headers);
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<string>.Error("Something Went Wrong", "Doctor Add Fail"));
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

        #region GET ALL PRODUCT NAME LIST
        [Route("api/DCR/GetallProductNameList")]
        [HttpPost]
        public HttpResponseMessage GetallProductNameList(int ProductId)
        {
            try
            {
                List<ProductListViewModel> responsedata = _IDoctorService.GetallProductNameList(ProductId);
                if (responsedata.Count != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<ProductListViewModel>>.Success(responsedata));
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

        #region DOCTOR SUMMRY REPORT
        [Route("api/Doctor/DoctorSummaryReport")]
        [HttpPost]
        public HttpResponseMessage DoctorSummaryReport(int CurrentUserId, String StartDate, String EndDate)
        {
            try
            {
                DateTime ConvertedStartDate = DateTime.ParseExact(StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime ConvertedEndDate = DateTime.ParseExact(EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                if (ConvertedStartDate == ConvertedEndDate)
                {
                    ConvertedEndDate = ConvertedEndDate.AddDays(1);
                }
                List<DoctorSummaryReport> responsedata = _IDoctorService.DoctorSummaryReport(CurrentUserId, ConvertedStartDate, ConvertedEndDate);
                if (responsedata.Count != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<DoctorSummaryReport>>.Success(responsedata));
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

        #region RCPA
        [Route("api/Doctor/RCPA")]
        [HttpPost]
        public HttpResponseMessage RCPA(int CurrentUserId, string StartDate, string EndDate)
        {
            try
            {
                DateTime dd = Convert.ToDateTime(StartDate);
                DateTime dd1 = Convert.ToDateTime(EndDate);
                string DDate = Convert.ToDateTime(StartDate).ToString("yyyy/MM/dd");
                string DDate1 = Convert.ToDateTime(EndDate).ToString("yyyy/MM/dd");
                // DateTime ConvertedStartDate = DateTime.ParseExact(dd.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                //// DateTime ConvertedEndDate = DateTime.ParseExact(dd1.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                if (dd == dd1)
                {
                    dd1 = dd1.AddDays(1);
                }
                List<RCPA> responsedata = _IDoctorService.RCPA(CurrentUserId, dd, dd1);
                if (responsedata.Count != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<RCPA>>.Success(responsedata));
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

        #region PreCallAnalysisReport
        [Route("api/Doctor/PreCallAnalysisReport")]
        [HttpPost]
        public HttpResponseMessage PreCallAnalysisReport(int CurrentUserId, String StartDate, String EndDate)
        {
            try
            {
                DateTime dd = Convert.ToDateTime(StartDate);
                DateTime dd1 = Convert.ToDateTime(EndDate);
                string DDate = Convert.ToDateTime(StartDate).ToString("yyyy/MM/dd");
                string DDate1 = Convert.ToDateTime(EndDate).ToString("yyyy/MM/dd");
                //DateTime ConvertedStartDate = DateTime.ParseExact(StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                //DateTime ConvertedEndDate = DateTime.ParseExact(EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                if (dd == dd1)
                {
                    dd1 = dd1.AddDays(1);
                }
                List<PreCallAnalysisReport> responsedata = _IDoctorService.PreCallAnalysisReport(CurrentUserId, dd, dd1);
                if (responsedata.Count != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<PreCallAnalysisReport>>.Success(responsedata));
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

        #region GET WORK TYPE LIST
        [Route("api/DCR/GetAvailableMeeting")]
        [HttpPost]
        public HttpResponseMessage GetAvailableMeeting(int UserId, int DoctorId)
        {
            try
            {
                List<MeetingTypeModel> responsedata = _IDoctorService.GetAvailableMeeting(UserId, DoctorId);
                if (responsedata.Count != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<MeetingTypeModel>>.Success(responsedata));
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
