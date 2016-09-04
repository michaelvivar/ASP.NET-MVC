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
        public ActionResult Create(CountryViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                var dto = new CountryDto()
                {
                    Id = viewmodel.Id,
                    Name = viewmodel.Name,
                    Code = viewmodel.Code
                };
                Transaction.Service<CountryService>(o => o.Add(dto));
                //viewmodel.Id = dto.Id;
                return RedirectToAction("Index");
            }
            return View(viewmodel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CountryViewModel model = new CountryViewModel();
            Transaction.Service<CountryService>(o =>
            {
                var dto = o.Get(id);
                model.Id = dto.Id; model.Name = dto.Name; model.Code = dto.Code;
            });
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CountryViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                var dto = new CountryDto()
                {
                    Id = viewmodel.Id,
                    Name = viewmodel.Name,
                    Code = viewmodel.Code
                };
                Transaction.Service<CountryService>(o => o.Update(dto));
                return RedirectToAction("Index");
            }
            return View(viewmodel);
        }

        public JsonResult Delete(int id)
        {
            Transaction.Service<CountryService>(o => o.Delete(id));
            return Success("Record has been successfully deleted!");
        }
    }
}