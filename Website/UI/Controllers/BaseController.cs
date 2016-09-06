using BL;
using BL.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UI.Enums;
using UI.Helpers;

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

        protected JsonResult Success(string message, object data)
        {
            return Json(new { Status = ActionResultStatus.Success, Message = message, Data = data }, JsonRequestBehavior.AllowGet);
        }

        protected int Skip(int per)
        {
            object page = Request.QueryString["page"];
            if (page != null)
            {
                int num = Convert.ToInt32(page);
                return PaginationHelper.Skip(per, num);
            }
            return 0;
        }

        protected void Service<TService>(Action<TService> action) where TService : IService, new()
        {
            Transaction.Service(action);
        }

        protected TOut Service<TService, TOut>(Func<TService, TOut> action) where TService : IService, new()
        {
            return Transaction.Service(action);
        }
    }
}