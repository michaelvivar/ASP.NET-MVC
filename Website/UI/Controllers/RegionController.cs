using BL;
using BL.Dto;
using BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.ViewModels;

namespace UI.Controllers
{
    public class RegionController : BaseController
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}