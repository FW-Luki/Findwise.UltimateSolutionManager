﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.Configuration;
using Findwise.SolutionManager;
using System.Runtime.CompilerServices;
using log4net;

namespace Findwise.InstallerModule
{
    [ProvideProperties(nameof(Configuration))]
    public abstract class InstallerModuleBase : IInstallerModule
    {
        protected readonly ILog Logger;
        public InstallerModuleBase()
        {
            Logger = LogManager.GetLogger(GetType());
        }

        public abstract string Name { get; }
        public virtual string FriendlyName { get; set; }
        public abstract Image Icon { get; }

        private InstallerModuleStatus __status = InstallerModuleStatus.Unknown;
        public virtual InstallerModuleStatus Status
        {
            get { return __status; }
            protected set
            {
                __status = value;
                OnStatusChanged();
            }
        }

        public abstract ConfigurationBase Configuration { get; set; }

        public event EventHandler StatusChanged;
        protected virtual void OnStatusChanged()
        {
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }

        public abstract void CheckStatus();
        public abstract void Install();
        public abstract void Uninstall();

        public virtual void PrepareInstall()
        {
            Status = InstallerModuleStatus.InstallationPending;
        }
        public virtual void PrepareUninstall()
        {
            Status = InstallerModuleStatus.UninstallationPending;
        }

        protected void LogError(Exception ex, [CallerMemberName] string methodName = "")
        {
            Logger.Error($"{GetType().Name} {methodName} error: ", ex);
        }
    }
}
