using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SystemModule
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<User>? Users { get; set; }
        public ICollection<Group>? Groups { get; set; }
      
        public ICollection<View>? Views { get; set; }
        public ICollection<Action>? Actions { get; set; }
    }

}
