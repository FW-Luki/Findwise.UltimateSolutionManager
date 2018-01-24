using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Findwise.Sharepoint.SolutionInstaller.Controls;
using Findwise.Sharepoint.SolutionInstaller.Controllers;

namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    [ToolboxItem(false)]
    public partial class MainToolboxViewDesigner : UserControl
    {
        public MainToolboxViewDesigner()
        {
            InitializeComponent();
        }

        internal SizeablePanel Panel => ToolboxPanel;

    }


    public class MainToolboxView : IComponentView
    {
        private MainToolboxViewDesigner designer = new MainToolboxViewDesigner();

        public Control Control => designer.Panel;
        public Controller[] Controllers { get; set; }
        public TableLayout Layout { get; set; } = new TableLayout();

        public event EventHandler<ModuleCreatedEventArgs> ModuleCreated;
        public class ModuleCreatedEventArgs : EventArgs
        {
            public IInstallerModule Module { get; }
            public ModuleCreatedEventArgs(IInstallerModule module)
            {
                Module = module;
            }
        }

        public void AddModule(Type moduleType)
        {
            var module = (IInstallerModule)Activator.CreateInstance(moduleType);
            var button = GetToolboxButton(module.Name, module.Icon, moduleType);
            (module as IDisposable)?.Dispose();

            Control.Invoke(new MethodInvoker(() =>
            {
                designer.Panel.Controls.Add(button);
                designer.Panel.Controls.SetChildIndex(button, 0);
            }));
        }

        public void EndAddingModules()
        {
        }

        private Button GetToolboxButton(string text, Image image, object tag = null)
        {
            var button = new NoFocusButton()
            {
                AutoSize = true,
                AutoEllipsis = true,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleLeft,
                ImageAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                Text = text,
                Image = image,
                Tag = tag
            };
            //toolTip1.SetToolTip(button, text);
            button.Click += ToolboxButton_Click;
            return button;
        }

        private void ToolboxButton_Click(object sender, EventArgs e)
        {
            if ((sender as Button)?.Tag is Type moduleType)
            {
                var module = (IInstallerModule)Activator.CreateInstance(moduleType);
                ModuleCreated?.Invoke(this, new ModuleCreatedEventArgs(module));
                //module.FriendlyName = $"{module.Name} {_projectManager.InstallerModules.Count(m => m.GetType() == module.GetType()) + 1}";
                //module.StatusChanged += Module_StatusChanged;
                //_projectManager.InstallerModules.Add(module);
            }
        }


        #region IComponent Support
        public ISite Site { get; set; }

        public event EventHandler Disposed;

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    designer.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
                Disposed?.Invoke(this, EventArgs.Empty);
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MainToolboxView() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
        #endregion
    }

}
