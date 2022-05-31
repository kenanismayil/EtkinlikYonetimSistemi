using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CountryValidator : AbstractValidator<Country>
    {
        public CountryValidator()
        {
            RuleFor(c => c.CountryName).NotEmpty().WithMessage("Ülke ismi boş bırakılamaz");
            RuleFor(c => c.CountryName).MinimumLength(3).WithMessage("Ülke ismi minimum uzunluğu 100 olmalıdır");
        }
    }
}
