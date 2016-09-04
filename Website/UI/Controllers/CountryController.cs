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
                viewmodel.Id = dto.Id;
            }
            return View(viewmodel);
        }
    }
}