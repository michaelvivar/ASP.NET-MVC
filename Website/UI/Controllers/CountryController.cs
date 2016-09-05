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
    public class CountryController : BaseController
    {
        private CountryDto ViewModelToDto(CountryViewModel model)
        {
            return new CountryDto { Id = model.Id, Name = model.Name, Code = model.Code };
        }

        private CountryViewModel DtoToViewModel(CountryDto dto)
        {
            return new CountryViewModel { Id = dto.Id, Name = dto.Name, Code = dto.Code };
        }

        [Route("")]
        public ActionResult Index()
        {
            int perpage = 10; int skip = 0;
            object page = Request.QueryString["page"];
            if (page != null)
            {
                int num = Convert.ToInt32(page);
                if (num > 1)
                {
                    skip = (num - 1) * perpage;
                }
            }
            return View(Transaction.Service<CountryService, List<CountryViewModel>>(o => o.Get(perpage, skip).Select(c => DtoToViewModel(c)).ToList()));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CountryViewModel());
        }

        [HttpPost]
        public ActionResult Create(CountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Transaction.Service<CountryService>(o => o.Add(ViewModelToDto(model)));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(Transaction.Service<CountryService, CountryViewModel>(o => DtoToViewModel(o.Get(id))));
        }

        [HttpPost]
        public ActionResult Edit(CountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Transaction.Service<CountryService>(o => o.Update(ViewModelToDto(model)));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            Transaction.Service<CountryService>(o => o.Delete(id));
            return Success("Record has been successfully deleted!");
        }
    }
}