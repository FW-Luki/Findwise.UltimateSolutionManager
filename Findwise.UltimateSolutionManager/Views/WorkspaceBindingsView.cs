using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using Findwise.SolutionManager.Controllers;
using Findwise.SolutionManager.Properties;
using Findwise.SolutionManager.Models;

namespace Findwise.SolutionManager.Views
{
    [ToolboxItem(false)]
    public partial class WorkspaceBindingsViewDesigner : UserControl
    {
        public WorkspaceBindingsViewDesigner()
        {
            InitializeComponent();
            DataGridView1.AutoGenerateColumns = true;
            ToolStrip.Visible = false;
        }
    }

    public class WorkspaceBindingsView : WorkspaceViewBase
    {
        private const string AddingItemErrorMessage = "Error adding item";
        private const string DeletingItemErrorMessage = "Error deleting item";

        private readonly Image defaultImage;

        private readonly WorkspaceBindingsViewDesigner designer = new WorkspaceBindingsViewDesigner();
        protected override IDisposable DisposableDesigner => designer;


        [Category(ToolStripButtonEventsCategoryName)]
        public event EventHandler AddRequested;

        [Category(ToolStripButtonEventsCategoryName)]
        public event EventHandler DeleteRequested;


        public WorkspaceBindingsView()
        {
            Title = "Data Bindings";
            ToolBoxAvailable = false;
            Order = 1;

            defaultImage = new Bitmap(Resources.if_text_x_generic_15420);
            using (var g = Graphics.FromImage(defaultImage))
            {
                var size = new Size(16, 16);
                g.DrawImage(Resources.if_Lock_65762, new Rectangle(defaultImage.Width - size.Width, defaultImage.Height / 2 - size.Height / 2, size.Width, size.Height));
            }

            designer.AddToolStripButton.Click += (s_, e_) => ProtectedInvoke(AddRequested, errorTitle: AddingItemErrorMessage);
            designer.DeleteToolStripButton.Click += (s_, e_) => ProtectedInvoke(DeleteRequested, errorTitle: DeletingItemErrorMessage);

            designer.DataGridView1.SelectionChanged += DataGridView_SelectionChanged;
        }


        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            OnSelectionChanged();
        }

        private void ProtectedInvoke(EventHandler handler, string errorTitle = null, string errorMessage = null)
        {
            try
            {
                handler?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(errorMessage ?? ex.Message, errorTitle ?? handler.Method.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region IMainView Support
        private IEnumerable<object> _selectedObjects => designer.DataGridView1.SelectedRows.Cast<DataGridViewRow>().Select(r => r.DataBoundItem);
        private IEnumerable<BindingItem> _selectedModules => _selectedObjects.OfType<BindingItem>().Where(m => m != null);

        public override string SelectedObjectTitle
        {
            get
            {
                var count = _selectedModules.Count();
                return
                    count > 0 ?
                        count > 1 ?
                            $"{typeof(BindingItem).Name}[{_selectedModules.Count()}]" :
                            _selectedModules.First().Name ?? $"{typeof(BindingItem).Name}({_selectedModules.First().Id})" :
                        string.Empty;
            }
        }

        public override object[] SelectedObjects
        {
            get
            {
                return _selectedObjects.ToArray();
            }
            set
            {
                designer.DataGridView1.ClearSelection();
                designer.DataGridView1.Rows.Cast<DataGridViewRow>().Where(r => value.Contains(r.DataBoundItem)).ToList().ForEach(r => r.Selected = true);
            }
        }
        #endregion

        #region IComponentView Support
        public override Control Control => designer.TableLayoutPanel;
        #endregion

        #region IMainView Support
        public override Image Icon
        {
            get { return UseDefaultIcon ? defaultImage : base.Icon; }
            set { base.Icon = value; }
        }
        public bool UseDefaultIcon { get; set; }

        public override object DataSource
        {
            get { return base.DataSource; }
            set
            {
                base.DataSource = value;
                designer.DataGridView1.DataSource = value;
            }
        }

        public override ToolStrip ToolStrip => designer.ToolStrip;


        public override void RefreshView()
        {
            RefreshDataGridView(designer.DataGridView1);
        }
        #endregion
    }
}
