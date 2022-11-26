using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class LocationValidator : AbstractValidator<Location>
    {
        public LocationValidator()
        {
            RuleFor(c => c.Name).NotNull().WithMessage("Lokasyon ismi boş bırakılamaz");
            RuleFor(c => c.Name).MinimumLength(3).WithMessage("Lokasyon isminin minimum uzunluğu 3 olmalıdır");
        }
    }
}
