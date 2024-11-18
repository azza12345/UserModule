using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class GroupPermission
    {
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
