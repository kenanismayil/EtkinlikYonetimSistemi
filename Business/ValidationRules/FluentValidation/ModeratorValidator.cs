using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ModeratorValidator : AbstractValidator<Moderator>
    {
        public ModeratorValidator()
        {
            RuleFor(c => c.UserId).NotNull().WithMessage("UserId boş olamaz");
        }
    }
}
