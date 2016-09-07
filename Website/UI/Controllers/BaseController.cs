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
        #region Json Results
        protected JsonResult JsonFormError(ModelStateDictionary model)
        {
            object[] keys = new object[model.Keys.Count];
            List<string> errors = new List<string>();
            List<object> list = new List<object>();

            int count = 0;
            foreach (var key in model.Keys)
            {
                keys[count] = key;
                count++;
            }
            count = 0;
            foreach (var value in model.Values)
            {
                List<string> str = new List<string>();
                if (value.Errors.Count > 0)
                {
                    foreach (var err in value.Errors)
                    {
                        str.Add(err.ErrorMessage);
                        errors.Add(err.ErrorMessage);
                    }
                }
                list.Add(new { Field = keys[count], Errors = str });
                count++;
            }
            return JsonResultError(errors, list);
        }
        protected JsonResult JsonResultError(IEnumerable<string> messages)
        {
            return Json(new { Status = ActionResultStatus.Error, Messages = messages }, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult JsonResultError(IEnumerable<string> messages, object data)
        {
            return Json(new { Status = ActionResultStatus.Error, Messages = messages, Data = data }, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult JsonResultWarning(IEnumerable<string> messages)
        {
            return Json(new { Status = ActionResultStatus.Warning, Messages = messages }, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult JsonResultWarning(IEnumerable<string> messages, object data)
        {
            return Json(new { Status = ActionResultStatus.Error, Messages = messages, Data = data }, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult JsonResultSuccess(IEnumerable<string> messages)
        {
            return Json(new { Status = ActionResultStatus.Success, Messages = messages }, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult JsonResultSuccess(IEnumerable<string> messages, object data)
        {
            return Json(new { Status = ActionResultStatus.Success, Messages = messages, Data = data }, JsonRequestBehavior.AllowGet);
        } 
        #endregion

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