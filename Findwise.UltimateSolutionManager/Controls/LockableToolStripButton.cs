using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Findwise.SolutionManager.Controls
{
    public class LockableToolStripButton : ToolStripButton
    {
        public LockingBehavior LockingBehavior { get; set; }

        public void SetLocked(bool state)
        {
            switch (LockingBehavior)
            {
                case LockingBehavior.Normal:
                    Enabled = state;
                    break;
                case LockingBehavior.AlwaysEnabled:
                    Enabled = true;
                    break;
                case LockingBehavior.AlwaysDisabled:
                    Enabled = false;
                    break;
                case LockingBehavior.Invert:
                    Enabled = !state;
                    break;
                default:
                    break;
            }
        }

        [Editor(typeof(OperationTrait.TypeEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(OperationTrait.NameTypeConverter))]
        public OperationTrait OperationTrait { get; set; }
    }

    public enum LockingBehavior
    {
        Normal,
        AlwaysEnabled,
        AlwaysDisabled,
        Invert
    }
}
