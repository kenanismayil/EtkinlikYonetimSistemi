using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CommentValidator : AbstractValidator<Comment>
    {
        public CommentValidator()
        {
            RuleFor(c => c.CommentText).MaximumLength(100).WithMessage("Yorumun maksimum uzunluğu 100 olmalıdır");
            RuleFor(c => c.ActivityId).NotNull().WithMessage("Aktivite ismi boş olamaz");
            RuleFor(c => c.UserId).NotNull().WithMessage("UserId boş olamaz");
        }
    }
}
