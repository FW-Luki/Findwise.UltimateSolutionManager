using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.Sharepoint.SolutionInstaller.Models
{
    public class BindingItem
    {
        [Browsable(false)]
        public Guid Id { get; } = Guid.NewGuid();

        public string Name { get; set; }
        public Type Type { get; set; }
        public object Value { get; set; }

    }
}
