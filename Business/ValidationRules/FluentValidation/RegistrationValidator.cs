using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class RegistrationValidator : AbstractValidator<Registration>
    {
        public RegistrationValidator()
        {
            RuleFor(r => r.Date).NotNull().WithMessage("Kayit tatihi boş bırakılamaz");
        }
    }
}
