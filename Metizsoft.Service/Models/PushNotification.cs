using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Script.Serialization;

namespace Metiz
{
    public class PushNotificationTitle
    {

    }
    public class PushNotification : System.Web.Http.AuthorizeAttribute
    {
        public static void pushnotification(string UserInstanceID, string title, string body, string NotificationType)
        {
            PushNotification result = new PushNotification();
            try
            {
                var requestUri = "https://fcm.googleapis.com/fcm/send";

                WebRequest webRequest = WebRequest.Create(requestUri);
                webRequest.Method = "POST";
                webRequest.Headers.Add(string.Format("Authorization: key={0}", "AIzaSyBK3jW-Oxv5YFAOcQtfTBqaNK-Kknx6yRc"));
                webRequest.Headers.Add(string.Format("Sender: id={0}", "360634529680"));
                webRequest.ContentType = "application/json";
                var data = new object();

                data = new
                {
                    to = UserInstanceID, // user instance id Uncoment this if you want to test for single device
                    //to = "/topics/" + ObjEntity.InstanceId, // this is for topic 
                    //notification = new
                    //{
                        
                    //    //icon="myicon"
                    //},
                    data = new
                    {
                        NotificationType = NotificationType,
                        title = title,
                        body = body
                        //title = "brunchy offers available",
                        //body = "redeem your offer from brunchy"
                        //NotificationType = ObjEntity.NotificationType,
                        //UserId = ObjEntity.UserId,
                        //DataId = ObjEntity.BlogId,
                        //IsPostImage = ObjEntity.IsPostImage
                    }
                };


                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);

                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                webRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse webResponse = webRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = webResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                //result.Response = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch
            {
                //result.Successful = false;
                //result.Response = null;
                //result.Error = ex;
            }
            // return result;
        }
    }

}