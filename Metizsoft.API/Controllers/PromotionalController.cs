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
    public class PromotionalController : ApiController
    {

        #region CONSTRUCTOR CALL
        private IMaster _IMasterService;
        public PromotionalController(IMaster MasterServices)
        {
            _IMasterService = MasterServices;
        } 
        #endregion

        #region GET ALL PROMOTIONAL LIST
        [Route("api/Promotional/GetallPromotionalList")]
        [HttpPost]
        public HttpResponseMessage GetallPromotionalName()
        {
            try
            {
                List<PromotionalViewModel> responsedata = _IMasterService.GetallPromotionalName();
                if (responsedata.Count != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<PromotionalViewModel>>.Success(responsedata));
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

        #region ADD PROMOTIONAL
        [Route("api/Promotional/AddPromotional")]
        [HttpPost]
        public HttpResponseMessage AddPromotional(PromotionalReportViewModels objPromo)
        {
            try
            {
                PromotionalReport_Mst objpromaster = new PromotionalReport_Mst();
                objpromaster.PromotionalId = objPromo.PromotionalId;
                objpromaster.WorkWithIds = objPromo.WorkWithIds;
                objpromaster.WorkWithUserId = objPromo.WorkWithUserId;
                objpromaster.CreatedBy = objPromo.CurrentUserId;
                objpromaster.UpdatedBy = objPromo.CurrentUserId;
                objpromaster.CreatedOn = DateTime.Now;
                objpromaster.UpdatedOn = DateTime.Now;
                objpromaster.AreaID = objPromo.AreaID;
                objpromaster.SubAreaId = objPromo.SubAreaId;
                if (!string.IsNullOrEmpty(objPromo.Date))
                {
                    objpromaster.Date =Convert.ToDateTime(objPromo.Date);
                }
                else 
                {
                    objpromaster.Date = null;
                }
                string ImageNames = "Promotional_Image" + DateTime.Now.Ticks.ToString() + ".jpg";
                string SavedPath = HttpContext.Current.Server.MapPath("/PromotionalImage/" + ImageNames);
                try
                {
                    if (!string.IsNullOrEmpty(objPromo.PromotionalImage))
                    {
                        File.WriteAllBytes(SavedPath, Convert.FromBase64String(objPromo.PromotionalImage));
                    }
                    objpromaster.PromotionalImage = ImageNames;
                }
                catch (Exception ex)
                {
                    objpromaster.PromotionalImage = "";
                }

                bool responsedata = _IMasterService.AddPromotional(objpromaster);
                if (responsedata)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<bool>.Success(responsedata));
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
    }
}
