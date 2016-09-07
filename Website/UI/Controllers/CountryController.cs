using BL.Dto;
using BL.Services;
using Nelibur.ObjectMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UI.ViewModels;

namespace UI.Controllers
{
    public class CountryController : BaseController
    {
        private CountryDto MapViewModelToDto(CountryViewModel model)
        {
            return TinyMapper.Map<CountryDto>(model);
        }

        private CountryViewModel MapDtoToViewModel(CountryDto dto)
        {
            return TinyMapper.Map<CountryViewModel>(dto);
        }

        [Route("")]
        public ActionResult Index()
        {
            return Service<CountryService, ActionResult>(service =>
            {
                int take = 10;
                return View(service.Get(take, Skip(take)).Select(c => MapDtoToViewModel(c)).ToList());
            });
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CountryViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ValidateCountryViewModel model)
        {
            var error = ViewData.ModelState.Where(o => o.Value.Errors.Count > 0).ToList();

            if (ModelState.IsValid)
            {
                Service<CountryService>(service => service.Add(MapViewModelToDto(model)));
                return RedirectToAction("Index");
            }
            //return JsonFormError(ModelState);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return Service<CountryService, ActionResult>(service =>
            {
                return View(MapDtoToViewModel(service.Get(id)));
            });
               
        }

        [HttpPost]
        public ActionResult Edit(ValidateCountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Service<CountryService>(service => service.Edit(MapViewModelToDto(model)));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            Service<CountryService>(service => service.Delete(id));
            return JsonResultSuccess(new[] { "Record has been successfully deleted!" });
        }
    }

    public class TextValueItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
    }
}