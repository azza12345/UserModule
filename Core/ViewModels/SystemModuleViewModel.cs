using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class SystemModuleViewModel
    {
        public Guid Id { get; set; }
        [MaxLength(50)] 
        
        public string Name { get; set; } 
        [MaxLength(100)]
        public string Description { get; set; } 

        
       // public IEnumerable<GroupViewModel> Groups { get; set; } = new List<GroupViewModel>();
        //public IEnumerable<ViewViewModel> Views { get; set; } = new List<ViewViewModel>();
        //public IEnumerable<ActionViewModel> Actions { get; set; } = new List<ActionViewModel>();
    }
}

