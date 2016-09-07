using BL;
using BL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.ViewModels
{
    public class CountryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
    }

    public class ValidateCountryViewModel : CountryViewModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool[] result = Transaction.Service<CountryValidatorService, bool[]>(service =>
            {
                bool[] r = new bool[2];
                r[0] = service.CountryNameExists(Name);
                r[1] = service.CountryCodeExists(Code);
                return r;
            });
            if (result[0])
            {
                yield return new ValidationResult(string.Format("Name \"{0}\" is already exists!", Name), new[] { nameof(Name) });
            }
            if (result[1])
            {
                yield return new ValidationResult(string.Format("Code \"{0}\" is already exists!", Code), new[] { nameof(Code) });
            }
        }
    }
}