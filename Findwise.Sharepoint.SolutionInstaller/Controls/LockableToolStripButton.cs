﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Findwise.Sharepoint.SolutionInstaller.Controls
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
    }

    public enum LockingBehavior
    {
        Normal,
        AlwaysEnabled,
        AlwaysDisabled,
        Invert
    }
}