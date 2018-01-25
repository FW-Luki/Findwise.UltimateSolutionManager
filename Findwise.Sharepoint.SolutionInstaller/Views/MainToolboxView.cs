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
using Findwise.Sharepoint.SolutionInstaller.Properties;

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

        public event EventHandler<ModuleAddedEventArgs> ModuleAdded;
        public class ModuleAddedEventArgs : EventArgs
        {
            public IInstallerModule Module { get; }
            public ModuleAddedEventArgs(IInstallerModule module)
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
            if (!designer.Panel.Controls.OfType<Button>().Any(b => b.Tag is Type t && typeof(IInstallerModule).IsAssignableFrom(t)))
            {
                Control.Invoke(new MethodInvoker(() =>
                {
                    var butt = GetToolboxButton("No modules found", Resources.if_emblem_unreadable_15398);
                    butt.Click -= ToolboxButton_Click;
                    butt.Enabled = false;
                    designer.Panel.Controls.Add(butt);
                }));
            }
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
                module.FriendlyName = $"{module.Name} {Controller.GetController<ProjectManager>(Controllers).Project.ModuleList.Count(m => m.GetType() == module.GetType()) + 1}";
                Controller.GetController<ProjectManager>(Controllers).AddModule(module);
                ModuleAdded?.Invoke(this, new ModuleAddedEventArgs(module));
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
