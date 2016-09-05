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
            List<CountryViewModel> list = new List<CountryViewModel>();
            Transaction.Service<CountryService>(o => list = o.GetAll().Select(c => new CountryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Code = c.Code
            }).ToList());
            return View(list);
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
                var dto = new CountryDto();
                Transaction.Service<CountryService>(o => o.Add(ViewModelToDto(model)));
                //viewmodel.Id = dto.Id;
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CountryViewModel model = null;
            Transaction.Service<CountryService>(o => model = DtoToViewModel(o.Get(id)));
            return View(model);
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