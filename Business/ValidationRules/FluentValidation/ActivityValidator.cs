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
            RuleFor(a => a.Title).NotEmpty().WithMessage("Aktivite ismi boş bırakılamaz");
            RuleFor(a => a.Title).Length(2, 50);  
            //RuleFor(a => a.ActivityTypeId).NotNull().WithMessage("AktiviteTypeId boş olamaz");
            RuleFor(a => a.ActivityDate).NotEmpty().WithMessage("Aktivite tarihi boş bırakılamaz");
            RuleFor(a => a.CreatedTime).NotEmpty().WithMessage("Aktivite oluşturulma tarihi boş bırakılamaz");
            RuleFor(a => a.AppDeadLine).NotEmpty().WithMessage("Aktivite son bulma tarihi boş bırakılamaz");
        }
    }
}
