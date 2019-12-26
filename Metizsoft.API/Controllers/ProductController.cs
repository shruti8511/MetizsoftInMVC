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
using System.IO;
using System.Web;

namespace Metizsoft.API.Controllers
{
    public class ProductController : ApiController
    {
        private IProduct _IProductservice;

        public ProductController(IProduct IProductservice)
        {
            _IProductservice = IProductservice;
        }

        [Route("api/Product/GetallProductTypeList")]
        [HttpPost]
        public HttpResponseMessage GetallProductTypeName()
        {
            try
            {
                List<ProductTypeResponse> responsedata = _IProductservice.GetAllProductTypeName();
                if (responsedata.Count != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<ProductTypeResponse>>.Success(responsedata));
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
