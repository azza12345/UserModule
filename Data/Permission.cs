using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Permission
    {
        public Guid Id { get; set; }
     
        public Guid ViewId { get; set; }
        public View View { get; set; }
        public Guid ActionId { get; set; }
        public Action Action { get; set; }

        public ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();
    
}
}
