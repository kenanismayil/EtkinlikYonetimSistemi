using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class UserOperationClaim : IEntity
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int OperationClaimId { get; set; }

        //Foreign Key
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("OperationClaimId")]
        public OperationClaim OperationClaim { get; set; }
    }
}
