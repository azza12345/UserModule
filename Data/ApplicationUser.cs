using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    [Table("ApplicationUser")]
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public bool IsMobileVerified { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsActive { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Guid? UpdatedByUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Guid? SystemId { get; set; }
        public SystemModule SystemModule { get; set; }

        

        public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    }
}
