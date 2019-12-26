using MapYourMeds.App_Start;
using Metizsoft.Data.Modal;
using Metizsoft.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MapYourMeds.Controllers
{
    public class GroupController : Controller
    {
        private IGroup _IGroupservice;

        public GroupController(IGroup IGroup)
        {
            _IGroupservice = IGroup;
        }

        [SessionAuthentication]
        public ActionResult Index()
        {
            ViewBag.ddlGroupType = _IGroupservice.SelectGroupType();
            return View();
        }

        public PartialViewResult GroupList()
        {

            List<GroupModel> ObjGroup = _IGroupservice.GetAllGroupList();
            return PartialView(ObjGroup);
        }
        
        [HttpPost]
        public ActionResult GroupCreate(Group_Mst ObjGroup)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    if (ObjGroup.GroupID == 0)
                    {
                        ObjGroup.GroupCode = GetLastGroupCode();
                    }  
                    ObjGroup.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    ObjGroup.UpdateBy = Convert.ToInt32(Session["UserID"]);
                    ObjGroup.UpdateBy = Convert.ToInt32(Session["UserID"]);
                    ObjGroup.CreatedOn = DateTime.Now;
                    ObjGroup.UpdateOn = DateTime.Now;
                }
                bool data = _IGroupservice.AddGroup(ObjGroup);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
                
        public JsonResult DeleteGroup(long ID)
        {
            try
            {
               bool data =  _IGroupservice.DeleteGroup(ID);
               return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index", "Group");
            }
        }

        #region auto generated group Code
        private string GetLastGroupCode()
        {
            long GrpId = _IGroupservice.GetLastGroupCode();
            string GroupCode = "";
            if (GrpId != 0)
            {
                string code = GrpId.ToString();
                //string number = code.Substring(1, 7);
                long incr = Convert.ToInt64(code) + 1;
                GroupCode = "GRP" + incr.ToString().PadLeft(4, '0');
            }
            else
            {
                GroupCode = "GRP0001";
            }
            return GroupCode;
        }
        #endregion
    }
}