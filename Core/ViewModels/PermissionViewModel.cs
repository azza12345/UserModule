using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = Data.Action;

namespace Core.ViewModels
{
  public  class PermissionViewModel
    {
        public Guid Id { get; set; }

        public Guid ViewId { get; set; }
        public View View { get; set; }
        public Guid ActionId { get; set; }
        public Action Action { get; set; }

    }
}
