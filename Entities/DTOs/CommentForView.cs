using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CommentForView 
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public UserInfoForComments User { get; set; }

    }
}
