using BL;
using BL.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UI.Enums;

namespace UI.Controllers
{
    public abstract class BaseController : Controller
    {
        protected JsonResult Error(string message)
        {
            return Json(new { Status = ActionResultStatus.Error, Message = message }, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult Error(List<string> messages)
        {
            return Json(new { Status = ActionResultStatus.Error, Message = messages }, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult Warning(string message)
        {
            return Json(new { Status = ActionResultStatus.Warning, Message = message }, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult Success(string message)
        {
            return Json(new { Status = ActionResultStatus.Success, Message = message }, JsonRequestBehavior.AllowGet);
        }

        protected void ServiceTransaction<TService>(Action<TService> action) where TService : IService, new()
        {
            Transaction.Service(action);
        }
    }
}