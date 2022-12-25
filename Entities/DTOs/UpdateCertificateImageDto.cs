using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UpdateCertificateImageDto : IDto
    {
        public int UserId { get; set; }
        public int ActivityId { get; set; }
        public string CertificateImage { get; set; }
    }
}
