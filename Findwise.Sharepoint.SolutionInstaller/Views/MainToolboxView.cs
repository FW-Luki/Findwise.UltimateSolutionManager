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

            using (var res = new FunnyPropertyGrid())
            {
                var bmp1 = new Bitmap(res.SortByCategoryImage);
                bmp1.MakeTransparent();
                SortByCategoryImage = bmp1;

                var bmp2 = new Bitmap(res.SortByPropertyImage);
                bmp2.MakeTransparent();
                SortAlphabeticallyImage = bmp2;
            }
            sortButton.Image = SortByCategoryImage;
        }

        internal SizeablePanel Panel => ToolboxPanel;
        internal NoFocusButton SortButton => sortButton;
        internal Image SortByCategoryImage { get; }
        internal Image SortAlphabeticallyImage { get; }


        private class FunnyPropertyGrid : PropertyGrid
        {
            public new Image SortByCategoryImage => base.SortByCategoryImage;
            public new Image SortByPropertyImage => base.SortByPropertyImage;
        }
    }


    public class MainToolboxView : IComponentView
    {
        private readonly MainToolboxViewDesigner designer = new MainToolboxViewDesigner();
        private readonly List<Button> moduleButtons = new List<Button>();
        private readonly List<GroupBox> categoryGroupboxes = new List<GroupBox>();

        public Control Control => designer.Panel;
        public Controller[] Controllers { get; set; }
        public TableLayout Layout { get; set; } = new TableLayout();
        public string MiscCategoryName { get; set; } = "Misc";

        private PropertySort _moduleSort = PropertySort.Categorized;
        public PropertySort ModuleSort
        {
            get { return _moduleSort; }
            set
            {
                _moduleSort = value;

                moduleButtons.ForEach(mb => designer.Panel.Controls.Remove(mb));
                categoryGroupboxes.ForEach(g => { g.Controls.Clear(); g.Dispose(); });
                categoryGroupboxes.Clear();
                IEnumerable<Control> controls;
                switch (_moduleSort)
                {
                    case PropertySort.Alphabetical:
                        controls = moduleButtons.OrderByDescending(mb => mb.Text);
                        break;
                    case PropertySort.Categorized:
                    case PropertySort.CategorizedAlphabetical:
                        var categories = moduleButtons.Select(mb => (mb.Tag as Type)?.GetCustomAttributes(false).OfType<CategoryAttribute>().FirstOrDefault()?.Category ?? MiscCategoryName).Distinct();
                        controls = categories.Select(cat =>
                        {
                            var groupbox = GetToolboxGroupbox(cat);
                            groupbox.Controls.AddRange(moduleButtons.Where(mb => ((mb.Tag as Type)?.GetCustomAttributes(false).OfType<CategoryAttribute>().FirstOrDefault()?.Category ?? MiscCategoryName) == cat).OrderByDescending(mb => mb.Text).ToArray());
                            categoryGroupboxes.Add(groupbox);
                            return groupbox;
                        });
                        break;
                    default:
                        controls = moduleButtons;
                        break;
                }
                foreach (var control in controls)
                {
                    designer.Panel.Controls.Add(control);
                }
            }
        }

        public event EventHandler<ModuleAddedEventArgs> ModuleAdded;
        public class ModuleAddedEventArgs : EventArgs
        {
            public IInstallerModule Module { get; }
            public ModuleAddedEventArgs(IInstallerModule module)
            {
                Module = module;
            }
        }


        public MainToolboxView()
        {
            ((Panel)designer.Panel).Controls.Add(designer.SortButton);
            designer.Panel.Layout += (s_, e_) => designer.SortButton.BringToFront();
            designer.SortButton.Click += SortButton_Click;
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
            moduleButtons.Add(button);
        }

        public void EndAddingModules()
        {
            if (!moduleButtons.Any(b => b.Tag is Type t && typeof(IInstallerModule).IsAssignableFrom(t)))
            {
                Control.Invoke(new MethodInvoker(() =>
                {
                    var butt = GetToolboxButton("No modules found", Resources.if_emblem_unreadable_15398);
                    butt.Click -= ToolboxButton_Click;
                    butt.Enabled = false;
                    designer.Panel.Controls.Add(butt);
                }));
            }
            else
            {
                Control.Invoke(new MethodInvoker(() =>
                {
                    ModuleSort = PropertySort.Categorized;
                }));
            }
        }

        public void PreviewKeyDown(PreviewKeyDownEventArgs pkdevent)
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

        private GroupBox GetToolboxGroupbox(string text)
        {
            return new GroupBox()
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Top,
                Text = text
            };
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

        private void SortButton_Click(object sender, EventArgs e)
        {
            if (ModuleSort == PropertySort.Categorized)
            {
                ModuleSort = PropertySort.Alphabetical;
                designer.SortButton.Image = designer.SortAlphabeticallyImage;
            }
            else
            {
                ModuleSort = PropertySort.Categorized;
                designer.SortButton.Image = designer.SortByCategoryImage;
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
