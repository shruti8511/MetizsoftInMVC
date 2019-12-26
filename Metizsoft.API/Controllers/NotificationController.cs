using Metizsoft.API.Models;
using Metizsoft.Data;
using Metizsoft.Data.Modal;
using Metizsoft.Data.ViewModal;
using Metizsoft.Service;
using Metizsoft.Service.Interface;
using Metizsoft.Service.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;


namespace Metizsoft.API.Controllers
{
    public class NotificationController : ApiController
    {
        private INotification _INotificationService;

        public NotificationController(INotification INotificationService)
        {
            _INotificationService = INotificationService;

        }

        [Route("api/Notification/GetNotificationList")]
        [HttpPost]
        public HttpResponseMessage GetNotificationList(int UserId)
        {
            try
            {
                List<NotificationModel> obj = _INotificationService.GetNotificationList(UserId);

                if (obj.Count > 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<NotificationModel>>.Success(obj));
                    HttpRequestHeaders headers = Request.Headers;
                    return TokenHelper.Addresponse(response, headers);
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<string>.NotFound());
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


        //[Route("api/Notification/GetNotificationID")]
        //[HttpPost]
        //public HttpResponseMessage GetNotificationID(string DeviceNotificationID)
        //{
        //    try
        //    {
        //        List<NotificationResponseData> responsedata = _userservice.GetNotificationID(string DeviceNotificationID);
        //        if (responsedata.Count != 0)
        //        {
        //            #region notification

        //            try
        //            {
        //                for (int i = 0; i <= responsedata.Count; i++)
        //                {
        //                    NotificationInfo obj = new NotificationInfo();

        //                    obj.Isread = false;
        //                    obj.NotificationType = responsedata[i].NotificationType;
        //                    // obj.CreatedBy = Data.UserID;
        //                    // obj.UpdateBy = Data.UserID;
        //                    //   obj.InstanceID = obj1[0].InstanceID;
        //                    //  obj.UserID = obj1[0].UserID;
        //                    new PushNotification().pushnotification(obj);
        //                }
        //            }
        //            catch
        //            {
        //            }

        //            #endregion
        //            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<NotificationResponseData>>.Success(responsedata));
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



    }
}
