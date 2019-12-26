using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Controllers;

namespace Metizsoft.API
{
    public class TokenHelper
    {
        public static HttpResponseMessage Firstresponse(HttpResponseMessage Base, string Token)
        {
            Base.Headers.Add("Authtoken", Token);        
            return Base;
        }
        public static HttpResponseMessage Addresponse(HttpResponseMessage Base, HttpRequestHeaders headers)
        {
            string Token = "";
            if (headers.Contains("Authtoken"))
            {
                Token = headers.GetValues("Authtoken").First();
            }
            Base.Headers.Add("Authtoken", Token);
            return Base;
        }


        //public static HttpResponseMessage Addresponse1(HttpResponseMessage Base, HttpRequestHeaders headers)
        //{
        //    string Token = "";
        //    if (headers.Contains("Authtoken"))
        //    {
        //        Token = headers.GetValues("Authtoken").First();
        //    }
        //    Base.Headers.Add("Authtoken", Token);
        //    return Base;
        //}
       
    }

    public class ResponseFailed
    {
        private string name = "hey, something went wrong please try later";

        // Declare a Name property of type string:
        public string Response
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
    }

    public class ResponseSuccess
    {
        private string name = "Successfully Uploaded";

        // Declare a Name property of type string:
        public string Response
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
    }

    public class customresponse<T>
    {
        public Status ResponseStatus { get; set; }
        public string ErrorMessage { get; set; }
        public string Message { get; set; }
        public DateTime? UpdateDate { get; set; }
        public T Data { get; set; }

        private customresponse() { }

        public static customresponse<T> Success(T data, DateTime? UpdateDatee = null)
        {
            return new customresponse<T> { Data = data, ResponseStatus = Status.Success, Message = "successful", UpdateDate = UpdateDatee };
        }

        public static customresponse<T> NotFound()
        {
            return new customresponse<T> { ResponseStatus = Status.Success, Message = "Not Found" };
        }

        public static customresponse<T> Error(string error , string message )
        {
            return new customresponse<T> { ResponseStatus = Status.Error, ErrorMessage = error, Message = message };
        }
    }

    public enum Status
    {
        Error = 0,
        Success = 1,
        Unknown
    }
}