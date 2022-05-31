using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CityValidator : AbstractValidator<City>
    {
        public CityValidator()
        {
            RuleFor(c => c.CityName).NotNull().WithMessage("Şehir ismi boş bırakılamaz");
            RuleFor(c => c.CityName).MinimumLength(3).WithMessage("Şehir isminin minimum uzunluğu 3 olmalıdır");
            RuleFor(c => c.Country).NotNull().WithMessage("Ülke ismi boş bırakılamaz");
            RuleFor(c => c.CountryId).NotNull().WithMessage("ÜlkeId boş olamaz");
        }
    }
}
