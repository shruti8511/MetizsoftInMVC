using MapYourMeds.App_Start;
using Metizsoft.Data.Modal;
using Metizsoft.Data.ViewModal;
using Metizsoft.Model;
using Metizsoft.Service;
using Metizsoft.Service.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;


namespace MapYourMeds.Controllers
{
    public class GroupToFieldUserController : Controller
    {

        private IGroup _IGroupService;
        private ICommon _ICommonService;


        public GroupToFieldUserController(IGroup IGroupService, ICommon ICommonService)
        {
            _IGroupService = IGroupService;
            _ICommonService = ICommonService;

        }

        [SessionAuthentication]
        public ActionResult Index()
        {
            ViewBag.RoleName = _ICommonService.RoleList();
           // ViewBag.UserGroup = _IGroupService.SelectGroupTypeForFieldUser();
            return View();
        }

        public ActionResult GroupNameByTypeID(long GroupID)
        {
            List<Group_Mst> lstGroup = _IGroupService.SelectGroup(GroupID);
            return Json(new { lstGroup = lstGroup }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUserNameByPosition(int RoleId)
        {
            var lstdata = _IGroupService.GetUserNameByPosition(RoleId);
            return Json(lstdata, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveGroupToFieldUser(GroupToFieldUser_Mst objgroup)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    objgroup.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    objgroup.UpdatedBy = Convert.ToInt32(Session["UserID"]);
                }

                objgroup.CreatedOn = DateTime.UtcNow;
                objgroup.UpdatedOn = DateTime.UtcNow;
                objgroup.IsActive = true;
                bool data = _IGroupService.AddGroupToFieldUser(objgroup);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            //return RedirectToAction("GroupToFieldUser", "Index");
        }

        public PartialViewResult GroupToFieldUserlist()
        {
            List<GroupToFieldUserModel> objgroup = _IGroupService.GetGroupToFieldUserlist();
            return PartialView(objgroup);

        }

        public JsonResult DeleteGroupToFieldUser(long Id, bool IsActive)
        {
            try
            {
                bool result = _IGroupService.DeleteGroupToFieldUser(Id, IsActive);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}