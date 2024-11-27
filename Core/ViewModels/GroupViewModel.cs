using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class GroupViewModel
    {
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Guid? ParentGroupId { get; set; } 
        public Guid? SystemId { get; set; }
    }

}
