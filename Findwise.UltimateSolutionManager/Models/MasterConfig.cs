using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.SolutionManager.Core.Model;

namespace Findwise.SolutionManager.Models
{
    public class MasterConfig : ObservableObject, IComparable
    {
        [Browsable(false)]
        public Guid Id { get => Property(() => Guid.NewGuid()); }

        public string Name
        {
            get => Property<string>();
            set => Property(value);
        }

        public int CompareTo(object obj)
        {
            if (obj is MasterConfig mc)
                return Name.CompareTo(mc.Name);
            else
                return 0;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
