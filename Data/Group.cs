using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public Guid? CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? ParentGroupId { get; set; } 
        public Group ParentGroup { get; set; }

        public Guid? SystemId { get; set; }
        public SystemModule? System { get; set; }

        public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
        public ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();

    }

}
