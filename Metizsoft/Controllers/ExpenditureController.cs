using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Metizsoft.Data;
using Metizsoft.Service;
using Metizsoft.Data.Modal;
using System.Web.Mvc;
using MapYourMeds.App_Start;

namespace MapYourMeds.Controllers
{
    public class ExpenditureController : Controller
    {
        private IExpenditure _IExpenditureservice;

        public ExpenditureController(IExpenditure IExpenditureservice)
        {
            _IExpenditureservice = IExpenditureservice;
        }
        //
        // GET: /Expenditure/

        public PartialViewResult ExpenditureList()
        {

            List<Expenser_Mst> objexpen = _IExpenditureservice.GetAllExpenditureList();
            return PartialView(objexpen);
        }

        [SessionAuthentication]
        public ActionResult ExpenditureCreate()
        {
            return View();
        }

        [SessionAuthentication]
        [HttpPost]
        public ActionResult ExpenditureCreate(Expenser_Mst objexpen)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    if (objexpen.ExpenserId == 0)
                    {
                        objexpen.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    }
                    objexpen.UpdatedBy = Convert.ToInt32(Session["UserID"]);
                }

                objexpen.CreatedOn = DateTime.UtcNow;
                objexpen.UpdatedOn = DateTime.UtcNow;
                bool data = _IExpenditureservice.AddExpenditure(objexpen);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [SessionAuthentication]
        public ActionResult DeleteExpenditure(long ID)
        {
            try
            {
                _IExpenditureservice.DeleteExpenditure(ID);
                return RedirectToAction("ExpenditureCreate");
            }
            catch (Exception)
            {

                return RedirectToAction("ExpenditureCreate");
            }
        }
    }
}