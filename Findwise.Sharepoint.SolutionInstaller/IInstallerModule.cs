﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.Configuration;

namespace Findwise.Sharepoint.SolutionInstaller
{
    public interface IInstallerModule
    {
        string Name { get; }
        string FriendlyName { get; set; }
        Image Icon { get; }

        InstallerModuleStatus Status { get; }

        ConfigurationBase Configuration { get; set; }

        event EventHandler StatusChanged;

        void CheckStatus();
        void Install();
        void Uninstall();
        void PrepareInstall();
        void PrepareUninstall();
    }
}
