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
            RuleFor(c => c.GivenDate).NotEmpty().WithMessage("Sertifika verilme tarihi boş bırakılamaz");
            RuleFor(c => c.ExpiryDate).NotEmpty().WithMessage("Sertifika geçerlilik tarihi boş bırakılamaz");
            RuleFor(c => c.CertificateName).Length(2, 50);
        }
    }
}
