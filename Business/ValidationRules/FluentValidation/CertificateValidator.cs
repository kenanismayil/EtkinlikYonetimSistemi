using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CertificateValidator : AbstractValidator<Certificate>
    {
        public CertificateValidator()
        {
            RuleFor(c => c.CertificateName).NotEmpty().WithMessage("Sertifika ismi boş bırakılamaz");
            RuleFor(c => c.CertificateName).Length(2, 50);
            RuleFor(c => c.GivenDate).NotEmpty().WithMessage("Aktivite tipi boş bırakılamaz");
            RuleFor(c => c.ExpiryDate).NotEmpty().WithMessage("Aktivite tarihi boş bırakılamaz");
            RuleFor(c => c.UserId).NotNull().WithMessage("UserId boş bırakılamaz");
            RuleFor(c => c.ActivityId).NotNull().WithMessage("AktiviteId boş bırakılamaz");
        }
    }
}
