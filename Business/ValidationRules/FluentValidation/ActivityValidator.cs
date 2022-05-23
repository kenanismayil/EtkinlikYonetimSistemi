using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ActivityValidator : AbstractValidator<Activity>
    {
        public ActivityValidator()
        {
            RuleFor(p => p.ActivityName).NotEmpty();
            RuleFor(p => p.ActivityName).MinimumLength(2);
            RuleFor(p => p.ActivityType).NotEmpty();
            RuleFor(p => p.ActivityDate).NotEmpty();
            RuleFor(p => p.CreatedTime).NotEmpty();
            RuleFor(p => p.AppDeadLine).NotEmpty();
        }
    }
}
