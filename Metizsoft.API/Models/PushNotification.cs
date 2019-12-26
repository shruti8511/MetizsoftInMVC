using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Script.Serialization;
using System.IO;
using Metizsoft.Service;
using Metizsoft.Data;

namespace Metizsoft.API.Models
{
    public class PushNotification : System.Web.Http.AuthorizeAttribute
    {
        //public void pushnotification(NotificationInfo ObjEntity)
        //{
        //    CommonService objnew = new CommonService();
        //    try
        //    {
        //        NotificationInfo obj = new NotificationInfo();
        //        obj.UserID = ObjEntity.UserID;
        //        obj.Message = ObjEntity.Message;
        //        obj.DeviceTokenID = ObjEntity.DeviceTokenID;
        //        obj.Isread = false;
        //        obj.CreatedBy = ObjEntity.CreatedBy;
        //        obj.UpdateBy = ObjEntity.UpdateBy;

        //        bool result1 = objnew.CreateNotification(obj);

        //        var requestUri = "https://fcm.googleapis.com/fcm/send";

        //        WebRequest webRequest = WebRequest.Create(requestUri);
        //        webRequest.Method = "POST";
        //        webRequest.Headers.Add(string.Format("Authorization: key={0}", "AIzaSyD2d_NAVZXgvm8HO6szwnr85QK_7K5x9XA"));
        //        webRequest.Headers.Add(string.Format("Sender: id={0}", "175955088607"));
        //        webRequest.ContentType = "application/json";
        //        var data = new object();

        //        data = new
        //        {
        //           // to = ObjEntity.InstanceID, // Uncoment this if you want to test for single device
        //            //to = "/topics/" + ObjEntity.InstanceId, // this is for topic 
        //            notification = new
        //            {
        //                title = "",
        //                body = "",
        //                //icon="myicon"
        //            },
        //            data = new
        //            {
        //                NotificationType = ObjEntity.NotificationType,
        //               // UserId = ObjEntity.UserID
        //                //NotificationType
        //                //OrderNumber = ObjEntity.OrderNumber
        //            }
        //        };
        //        var serializer = new JavaScriptSerializer();
        //        var json = serializer.Serialize(data);

        //        Byte[] byteArray = Encoding.UTF8.GetBytes(json);

        //        webRequest.ContentLength = byteArray.Length;
        //        using (Stream dataStream = webRequest.GetRequestStream())
        //        {
        //            dataStream.Write(byteArray, 0, byteArray.Length);

        //            using (WebResponse webResponse = webRequest.GetResponse())
        //            {
        //                using (Stream dataStreamResponse = webResponse.GetResponseStream())
        //                {
        //                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
        //                    {
        //                        String sResponseFromServer = tReader.ReadToEnd();
        //                        //result.Response = sResponseFromServer;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}
    }
}