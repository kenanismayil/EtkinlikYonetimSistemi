using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ActivityImageValidator : AbstractValidator<ActivityImage>
    {
        public ActivityImageValidator()
        {
            RuleFor(a => a.ImagePath).NotNull().WithMessage("Resim yolu boş bırakılamaz");
            RuleFor(a => a.Date).NotNull().WithMessage("Tarih boş bırakılamaz");
        }

    }
}
