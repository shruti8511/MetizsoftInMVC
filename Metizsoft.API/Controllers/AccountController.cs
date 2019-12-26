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
using WebMatrix.WebData;

namespace Metizsoft.API.Controllers
{

    public class AccountController : ApiController
    {
        #region CONSTRUCTOR
        private IAccount _IAccountService;

        public AccountController(IAccount AccountService)
        {
            _IAccountService = AccountService;

        }
        #endregion

        #region Reset Password
        [Route("api/Account/ResetPassword")]
        [HttpPost]
        public HttpResponseMessage ResetPassword(long Id,string NewPasword)
        {
            try
            {
               
                string UserName = _IAccountService.ResetPassword(Id);

                if (!string.IsNullOrEmpty(UserName))
                {

                    string token = WebSecurity.GeneratePasswordResetToken(UserName, 10);
                    bool result = WebSecurity.ResetPassword(token, NewPasword);

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<bool>.Success(result));
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
