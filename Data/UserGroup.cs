using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UserGroup
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid GroupId { get; set; }
        public Group Group { get; set; }
    }
}
