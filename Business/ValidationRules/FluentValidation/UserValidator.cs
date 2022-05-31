using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName).NotNull().WithMessage("İsim boş bırakılamaz");
            RuleFor(u => u.FirstName).Length(2, 50);
            RuleFor(u => u.LastName).NotNull().WithMessage("Soyadı boş bırakılamaz");
            RuleFor(u => u.LastName).Length(2, 50);
            RuleFor(u => u.Email).NotNull().WithMessage("Email boş bırakılamaz");
            RuleFor(u => u.Email).Length(8, 50);
        }
    }
}
