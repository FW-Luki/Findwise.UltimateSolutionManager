using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.SolutionInstaller.Core.Model;

namespace Findwise.Sharepoint.SolutionInstaller.Models
{
    public class MasterConfig : ObservableObject
    {
        [Browsable(false)]
        public Guid Id { get => Property(() => Guid.NewGuid()); }

        public string Name
        {
            get => Property<string>();
            set => Property(value);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
