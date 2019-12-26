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
using System.Globalization;
using System.Configuration;

namespace Metizsoft.API.Controllers
{
    public class ExpenditureController : ApiController
    {
        private IExpenditure _IExpenditureservice;

        public ExpenditureController(IExpenditure IExpenditureservice)
        {
            _IExpenditureservice = IExpenditureservice;
        }


        [Route("api/Expenditure/AddExpenditureReport")]
        [HttpPost]
        public HttpResponseMessage AddExpenditureReport(ExpenditureReportViewModel objExpenditurereport)
        {
            try
            {
                ExpenserReport_Mst objExpenditurereportmst = new ExpenserReport_Mst();
             // objExpenditurereportmst.ExpenserId = objExpenditurereport.ExpenserId;
                objExpenditurereportmst.Amount = objExpenditurereport.Amount;
                objExpenditurereportmst.Reason = objExpenditurereport.Reason;
                //objExpenditurereportmst.History = objExpenditurereport.History;
                objExpenditurereportmst.Note = objExpenditurereport.Note;
               // objExpenditurereportmst.ExpenserImage = objExpenditurereport.ExpenserImage;
                objExpenditurereportmst.CreatedBy = objExpenditurereport.CurrentUserId;
                objExpenditurereportmst.UpdatedBy = objExpenditurereport.CurrentUserId;

                objExpenditurereportmst.CreatedOn = DateTime.Now;
                objExpenditurereportmst.UpdatedOn = DateTime.Now;

                string ImageNames = "Promotional_Image" + DateTime.Now.Ticks.ToString() + ".jpg";
                string SavedPath = HttpContext.Current.Server.MapPath("/PromotionalImage/" + ImageNames);
                try
                {
                    if (!string.IsNullOrEmpty(objExpenditurereport.ExpenserImage))
                    {
                        File.WriteAllBytes(SavedPath, Convert.FromBase64String(objExpenditurereport.ExpenserImage));
                    }
                    objExpenditurereportmst.ExpenserImage = ImageNames;
                }
                catch (Exception ex)
                {
                    objExpenditurereport.ExpenserImage = "";
                }

                bool responsedata = _IExpenditureservice.AddExpenditureReport(objExpenditurereportmst);
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

        #region History LIST

        [Route("api/Expenditure/GetallExpenditureList")]
        [HttpPost]
        public HttpResponseMessage GetallExpenditureList(int CreatedBy, String StartDate, String EndDate)
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
                List<ExpenditureViewModel> responsedata = _IExpenditureservice.GetallExpenditureList(CreatedBy,dd, dd1);
                if (responsedata.Count != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<ExpenditureViewModel>>.Success(responsedata));
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
