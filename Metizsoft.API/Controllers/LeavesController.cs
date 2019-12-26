using Metizsoft.Data;
using Metizsoft.Data.Modal;
using Metizsoft.Data.ViewModal;
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
    public class LeavesController : ApiController
    {
          private ILeave _ILeaveService;

          public LeavesController(ILeave LeaveServices)
        {
            _ILeaveService = LeaveServices;
            
        }

          [Route("api/Leaves/GetallLeavesByUserId")]
          [HttpPost]
          public HttpResponseMessage GetallLeavesByUserId(string UserID, long roleId, string StartDate, string EndDate)
          {
              try
              {
                  DateTime dd = Convert.ToDateTime(StartDate);
                  DateTime dd1 = Convert.ToDateTime(EndDate);
                  string DDate = Convert.ToDateTime(StartDate).ToString("yyyy/MM/dd");
                  string DDate1 = Convert.ToDateTime(EndDate).ToString("yyyy/MM/dd");
                  //DateTime ConvertedStartDate = DateTime.ParseExact(dd.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                 // DateTime ConvertedEndDate = DateTime.ParseExact(dd1.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                  if (dd == dd1)
                  {
                      dd1 = dd1.AddDays(1);
                  }
                  ResponseLeaveCountModel responsedata = _ILeaveService.GetallLeavesByUserId(UserID, roleId, dd, dd1);
                  if (responsedata != null)
                  {
                      HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<ResponseLeaveCountModel>.Success(responsedata));
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

          [Route("api/Leaves/AddLeave")]
          [HttpPost]
          public HttpResponseMessage AddLeave(AddLeaveModel obj)
          {
              try
              {
                  LeaveMaster objmaster = new LeaveMaster();
                  //objmaster.LeaveId = obj.LeaveId;
                  objmaster.UserId = obj.CurrentUserId;
                  objmaster.LeaveType = obj.LeaveType;
                  objmaster.DateFrom = obj.DateFrom;
                  objmaster.DateTo = obj.DateTo;
                  objmaster.Reason = obj.Reason;
                  objmaster.RoleId = obj.RoleId;
                  objmaster.LeaveStatus = "Pending";
                  objmaster.LeaveStatusByVP = "Pending";
                  objmaster.CreatedOn = DateTime.Now;
                  objmaster.UpdatedOn = DateTime.Now;

                  long responsedata = _ILeaveService.AddLeave(objmaster);
                  if (responsedata != 0)
                  {
                      HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<long>.Success(responsedata));
                      HttpRequestHeaders headers = Request.Headers;
                      return TokenHelper.Addresponse(response, headers);
                  }
                  else
                  {
                      HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<string>.Error("Something Went Wrong", "Leave Application Fail"));
                      HttpRequestHeaders headers = Request.Headers;
                      return TokenHelper.Addresponse(response, headers);
                  }

              }
              catch (Exception ex)
              {
                  ResponseFailed obj1 = new ResponseFailed();
                  HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, customresponse<ResponseFailed>.Error(ex.Message, obj1.Response));
                  HttpRequestHeaders headers = Request.Headers;
                  return TokenHelper.Addresponse(response, headers);
              }
          }


          //[Route("api/Leaves/GetMRByASM")]
          //[HttpPost]
          //public HttpResponseMessage GetMRByASM(int UserType)
          //{
          //    try
          //    {
          //        List<UserInfo> responsedata = _ILeaveService.GetMRByASM(UserType);
          //        if (responsedata.Count != 0)
          //        {
          //            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<UserInfo>>.Success(responsedata));
          //            HttpRequestHeaders headers = Request.Headers;
          //            return TokenHelper.Addresponse(response, headers);
          //        }
          //        else
          //        {
          //            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<string>.Error("Data Not found", "Data Not found"));
          //            HttpRequestHeaders headers = Request.Headers;
          //            return TokenHelper.Addresponse(response, headers);
          //        }
          //    }
          //    catch (Exception ex)
          //    {
          //        ResponseFailed obj = new ResponseFailed();
          //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, customresponse<ResponseFailed>.Error(ex.Message, obj.Response));
          //        HttpRequestHeaders headers = Request.Headers;
          //        return TokenHelper.Addresponse(response, headers);
          //    }
          //}

          [Route("api/Leaves/ApproveLeave")]
          [HttpPost]
          public HttpResponseMessage ApproveLeave(int LeaveID, string Status)
          {
              try
              {
                  bool responsedata = _ILeaveService.ApproveLeaveASM(LeaveID, Status);
                  if (responsedata == true)
                  {
                      HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<string>.Success("Leave is " + Status));
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


          [Route("api/Leaves/GetallLeaveTypeList")]
          [HttpPost]
          public HttpResponseMessage GetAllLeaveTypeName()
          {
              try
              {
                  List<LeaveTypeResponse> responsedata = _ILeaveService.GetAllLeaveTypeName();
                  if (responsedata.Count != 0)
                  {
                      HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<LeaveTypeResponse>>.Success(responsedata));
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
    }
}
