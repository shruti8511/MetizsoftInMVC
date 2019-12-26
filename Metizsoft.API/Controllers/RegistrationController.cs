using Metizsoft.Data;
using Metizsoft.Data.Modal;
using Metizsoft.Data.ViewModal;
using Metizsoft.Service;
using Metizsoft.Service.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;

namespace Metizsoft.API.Controllers
{
    public class RegistrationController : ApiController
    {
        #region CONSTRUCTOR CALL
        private IUser _userservice;
        private ILeave _ILeaveService;
        public RegistrationController(IUser userservic, ILeave leaveservice)
        {
            _userservice = userservic;
            _ILeaveService = leaveservice;

        }
        #endregion

        #region CHECK LOGIN
        [Route("api/Registration/checklogin")]
        [HttpPost]
        public HttpResponseMessage checklogin(RequestLogin data)
        {
            bool success = WebSecurity.Login(data.UserName, data.Password, false);
            try
            {
                if (success)
                {

                    UsersModel responsedata = _userservice.checklogin(data);
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<UsersModel>.Success(responsedata));
                    HttpRequestHeaders headers = Request.Headers;
                    return TokenHelper.Addresponse(response, headers);
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<string>.Error("User Not found", "User Not found"));
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

        #region ADD TRACKER INFO
        [Route("api/AddTrackerInfo")]
        [HttpPost]
        public HttpResponseMessage AddDCR(Tracker_MstModel Data)
        {
            try
            {
                Tracker_Mst ObjTracker = new Tracker_Mst();
                ObjTracker.Lattitude = Data.Lattitude;
                ObjTracker.longitude = Data.longitude;
                ObjTracker.UserID = Data.UserID;
                ObjTracker.CreatedOn = DateTime.Now;

                bool responsedata = _userservice.Addtrackerinfo(ObjTracker);
                if (responsedata)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<bool>.Success(responsedata));
                    HttpRequestHeaders headers = Request.Headers;
                    return TokenHelper.Addresponse(response, headers);
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<string>.Error("Something Went Wrong", "Tracking Add Fail"));
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

        #region Get Tracking Info
        [Route("api/GetTrackingInfo")]
        [HttpPost]
        public HttpResponseMessage GetTrackingInfo(string UserIds)
        {
            try
            {
                //List<UserViewModel> responsedata = _userservice.GetUderRoleUserList(RoleId, CurrentUserId);
                //List<long> Userlist = responsedata.Select(x => x.Id).ToList();
                //string joined = string.Join(",", Userlist);
                List<Tracker_MstModel> data = _userservice.GetTrackListByUserID(UserIds);
                var groupdata = data.GroupBy(x => x.UserID);
                List<Tracker_MstList> lstobject = new List<Tracker_MstList>();
                Random r = new Random();
                foreach (var item in groupdata)
                {
                    Tracker_MstList objlst = new Tracker_MstList();
                    string grncolor = r.Next(0, 256) +","+ r.Next(0, 256)+","+ r.Next(0, 256);
                    objlst.Color = grncolor;
                    objlst.UserID = item.Key;
                    objlst.lstTracker = item.ToList();
                    lstobject.Add(objlst);
                }
                if (data.Count != 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<Tracker_MstList>>.Success(lstobject));
                    HttpRequestHeaders headers = Request.Headers;
                    return TokenHelper.Addresponse(response, headers);
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<string>.Error("Something Went Wrong", "Tracking Add Fail"));
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

        //[Route("api/Registration/NewUserRegistration")]
        //[HttpPost]
        //public HttpResponseMessage NewUserRegistration(RegistrationRequest model)
        //{
        //    try
        //    {
        //        User result = _commonservice.NewUserRegistration(model);
        //        if (result != null)
        //        {
        //            //Reg.ForceClose = ForcetoClose;

        //            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<User>.Success(result));
        //            return TokenHelper.Firstresponse(response, "");
        //        }
        //        else
        //        {
        //            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<ResponseFailed>.Error("Something went wrong!!", "try again!"));
        //            return response;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ResponseFailed obj = new ResponseFailed();
        //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, customresponse<ResponseFailed>.Error(ex.Message, obj.Response));
        //        return response;
        //    }
        //}


        //[Route("api/Registration/LoginVerify")]
        //[HttpGet]
        //public HttpResponseMessage LoginVerify(RequestLogin obj)
        //{
        //    try
        //    {
        //        User result = _commonservice.LoginVerify(obj.UserName, obj.Password);

        //        if (result != null)
        //        {
        //            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<User>.Success(result));
        //            return TokenHelper.Firstresponse(response, "");
        //        }
        //        else
        //        {
        //            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<ResponseFailed>.Error("Something went wrong!!", "Please check Username and Password"));
        //            return TokenHelper.Firstresponse(response, string.Empty);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ResponseFailed objres = new ResponseFailed();
        //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, customresponse<ResponseFailed>.Error(ex.Message + " " + ex.InnerException, objres.Response));
        //        return response;
        //    }


        //    //HttpRequestHeaders headers1 = Request.Headers;
        //    //string deviceType = "";
        //    //string appVersion = "";
        //    //if (headers1.Contains("deviceType") && headers1.Contains("appVersion"))
        //    //{
        //    //    try
        //    //    {
        //    //        deviceType = headers1.GetValues("deviceType").First();
        //    //        appVersion = headers1.GetValues("appVersion").First();
        //    //    }
        //    //    catch { }
        //    //}

        //    //if (deviceType == "1")
        //    //{
        //    //    long UserAppVersion = Convert.ToInt64(appVersion.Replace(".", string.Empty));
        //    //    long StableAppVersion = Convert.ToInt64(ConfigurationManager.AppSettings["AndroidApp"]);

        //    //    if (UserAppVersion >= StableAppVersion)
        //    //    {
        //    //        Token.ForceClose = false;
        //    //    }
        //    //    else
        //    //    {
        //    //        Token.ForceClose = true;
        //    //    }

        //    //}
        //    //else if (deviceType == "2")
        //    //{
        //    //    long UserAppVersion = Convert.ToInt64(appVersion.Replace(".", string.Empty));
        //    //    long StableAppVersion = Convert.ToInt64(ConfigurationManager.AppSettings["IOSApp"]);

        //    //    if (UserAppVersion >= StableAppVersion)
        //    //    {
        //    //        Token.ForceClose = false;
        //    //    }
        //    //    else
        //    //    {
        //    //        Token.ForceClose = true;
        //    //    }
        //    //}
        //}

        #region GetUserRoleUserList
        [Route("api/User/GetUserRoleUserList")]
        [HttpPost]
        public HttpResponseMessage GetUserRoleUserList(long RoleId, long CurrentUserId)
        {
            try
            {
                List<UserViewModel> responsedata = _userservice.GetUserRoleUserList(RoleId, CurrentUserId);
                if (responsedata != null)
                {
                    var result = responsedata.Select(x => new { x.Id, x.UserName, x.UserRoleID, x.FirstName, x.LastName, x.ParentId, x.RSMID,x.ASMID,x.ZSMID,x.MRID, x.SalesTarget, x.AchievedTarget});

                    List<cascaderesponce> returnobject = new List<cascaderesponce>();
                    cascaderesponce singleobject1 = new cascaderesponce();
                    foreach (var item in result)
                    {
                        cascaderesponce singleobject = new cascaderesponce();
                        singleobject.Id = item.Id;
                        singleobject.UserRoleID = item.UserRoleID;
                        singleobject.FirstName = item.FirstName;
                        singleobject.LastName = item.LastName; 
                        singleobject.UserName = singleobject.FirstName + " " + singleobject.LastName;
                        singleobject.ParentId = item.ParentId;
                        singleobject1 = singleobject;


                        returnobject.Add(singleobject1);
                    }

                    if (responsedata.Count != 0)
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<cascaderesponce>>.Success(returnobject));
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

        #region GetUserTargetdataByRole
        [Route("api/User/GetUserTargetdataByRole")]
        [HttpPost]
        public HttpResponseMessage GetUserTargetdataByRole(GetForTargate Data)
        {
            try
            {
                List<GetUserTargetdataByRole> responsedata = _userservice.GetUserTargetdataByRole(Data);
                if (responsedata != null)
                {
                    var result = responsedata.Select(x => new { x.Id, x.UserName, x.UserRoleID, x.FirstName, x.LastName, x.ParentId, x.RSMID, x.ASMID, x.ZSMID, x.MRID, x.SalesTarget, x.AchievedTarget });

                    List<cascaderesponce1> returnobject = new List<cascaderesponce1>();
                    cascaderesponce1 singleobject1 = new cascaderesponce1();
                    foreach (var item in result)
                    {
                        cascaderesponce1 singleobject = new cascaderesponce1();
                        singleobject.Id = item.Id;
                        singleobject.UserRoleID = item.UserRoleID;
                        singleobject.FirstName = item.FirstName;
                        singleobject.LastName = item.LastName;
                        singleobject.UserName = singleobject.FirstName + " " + singleobject.LastName;
                        singleobject.ParentId = item.ParentId;
                        //singleobject.RSMID = item.RSMID;
                        //singleobject.ZSMID = item.ZSMID;
                        //singleobject.ASMID = item.ASMID;
                        //singleobject.MRID = item.MRID;
                        singleobject.SalesTarget = item.SalesTarget;
                        singleobject.AchievedTarget = item.AchievedTarget;
                        singleobject1 = singleobject;


                        returnobject.Add(singleobject1);
                    }

                    if (responsedata.Count != 0)
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<cascaderesponce1>>.Success(returnobject));
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

        #region GET Product ON Target Allocation
        [Route("api/GetAllProductListBaseOnTarget")]
        [HttpGet]
        public HttpResponseMessage GetAllProductListOnTargateForDoctor(long CurrentUserId)
        {
            try
            {
                List<ProductCaseMst_mstForAPI> responsedata = _userservice.GetAllProductlistOnAllocationFroDoctor(CurrentUserId);
                if (responsedata != null)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, customresponse<List<ProductCaseMst_mstForAPI>>.Success(responsedata));
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

    
 

 
    public class cascaderesponce
    {
        public int Id { get; set; }
        public Nullable<long> UserRoleID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long ParentId { get; set; }
        //public long ZSMID { get; set; }
        //public long RSMID { get; set; }
        //public long ASMID { get; set; }
        //public long MRID { get; set; }
        //public float SalesTarget { get; set; }
        //public float AchievedTarget { get; set; }
    }
    public class cascaderesponce1
    {
        public int Id { get; set; }
        public Nullable<long> UserRoleID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long ParentId { get; set; }
        //public long ZSMID { get; set; }
        //public long RSMID { get; set; }
        //public long ASMID { get; set; }
        //public long MRID { get; set; }
        public decimal SalesTarget { get; set; }
        public decimal AchievedTarget { get; set; }
    }

}
