using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ActivityTypeValidator : AbstractValidator<ActivityType>
    {
        public ActivityTypeValidator()
        {
            RuleFor(t => t.ActivityTypeName).NotNull().WithMessage("Aktivitenin tip ismi boş bırakılamaz");
            RuleFor(t => t.ActivityTypeName).MinimumLength(2);
        }
    }
}
