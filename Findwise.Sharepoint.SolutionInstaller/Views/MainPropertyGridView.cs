using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Findwise.Sharepoint.SolutionInstaller.Controllers;
using Findwise.Configuration;

namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    [ToolboxItem(false)]
    public partial class MainPropertyGridViewDesigner : UserControl
    {
        private readonly string defaultHelpButtonText;

        public MainPropertyGridViewDesigner()
        {
            InitializeComponent();

            propertyGrid1.MergeToolStrip(PropertyGridMergeToolStrip);
            PropertyGridMergeToolStrip.Visible = false;

            defaultHelpButtonText = HelpToolStripButton.Text;
            propertyGrid1.SelectedGridItemChanged += PropertyGrid1_SelectedGridItemChanged;
        }


        internal Panel Panel => sizeablePanel1;

        internal string Title { get => SelectedObjectToolStrip.Text; set => SelectedObjectToolStrip.Text = value; }

        internal object[] SelectedObjects
        {
            get { return propertyGrid1.SelectedObjects; }
            set
            {
                PropertyValueChangedEventHandler h = (s_, e_) => PropertyGridSelectedValueChanged?.Invoke(this, EventArgs.Empty);
                propertyGrid1.PropertyValueChanged -= h;
                propertyGrid1.SelectedObjects = value;
                propertyGrid1.PropertyValueChanged += h;
            }
        }


        internal event EventHandler PropertyGridSelectedValueChanged;
        //internal event SelectedGridItemChangedEventHandler PropertyGridSelectedGridItemChanged;
        internal event EventHandler HelpButtonClicked;


        private void RestoreDefaultToolStripButton_Click(object sender, EventArgs e)
        {
            propertyGrid1.ResetSelectedProperty();
        }

        private void MasterConfigSelectToolStrip_SizeChanged(object sender, EventArgs e)
        {
            MasterConfigSelectComboBox.Width = MasterConfigSelectToolStrip.DisplayRectangle.Width - MasterConfigSelectToolStrip.Padding.Horizontal;
        }

        private void PropertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            var helpLinkAttribs = e.NewSelection.PropertyDescriptor?.Attributes.OfType<HelpLinkAttribute>();
            if (helpLinkAttribs?.Any()??false)
            {
                var helpLinkAttrib = helpLinkAttribs.First();
                HelpToolStripButton.Tag = helpLinkAttrib.Url;
                HelpToolStripButton.Text = helpLinkAttrib.Text ?? defaultHelpButtonText;
                HelpToolStripButton.ToolTipText = helpLinkAttrib.Description ?? defaultHelpButtonText;
                HelpToolStripButton.Enabled = true;
            }
            else
            {
                HelpToolStripButton.Enabled = false;
            }
        }

        private void HelpToolStripButton_Click(object sender, EventArgs e)
        {
            HelpButtonClicked?.Invoke(sender, e);
        }
    }

    public class MainPropertyGridView : IComponentView
    {
        private MainPropertyGridViewDesigner designer = new MainPropertyGridViewDesigner();

        public Control Control => designer.Panel;
        public Controller[] Controllers { get; set; }
        public TableLayout Layout { get; set; } = new TableLayout();


        [Browsable(false)]
        public string SelectedObjectName { get => designer.Title; set => designer.Title = value; }

        private object[] _selectedObjects;
        [Browsable(false)]
        public object[] SelectedObjects
        {
            get { return _selectedObjects; }
            set
            {
                _selectedObjects = value;
                designer.SelectedObjects = value?.Select(obj =>
                {
                    var propertiesMemberName = obj.GetType().GetCustomAttributes(true).OfType<ProvidePropertiesAttribute>().FirstOrDefault()?.PropertiesMemberName;
                    if (!string.IsNullOrEmpty(propertiesMemberName) && obj?.GetType().GetProperty(propertiesMemberName).GetValue(obj) is object propertiesObject)
                        return propertiesObject;
                    else
                        return obj;
                }).ToArray();
            }
        }


        public MainPropertyGridView()
        {
            designer.PropertyGridSelectedValueChanged += (s_, e_) => PropertyGridSelectedValueChanged?.Invoke(this, EventArgs.Empty);
            designer.HelpButtonClicked += Designer_HelpButtonClicked;
        }


        public event EventHandler PropertyGridSelectedValueChanged;


        private void Designer_HelpButtonClicked(object sender, EventArgs e)
        {
            var button = sender as ToolStripItem;
            if (button?.Tag is string url)
            {
                var owner = Control.FindForm();
                var point = button.Owner.PointToScreen(button.Owner.Location);
                var leftMargin = SystemInformation.CaptionHeight;
                var helpWindow = new Form()
                {
                    StartPosition = FormStartPosition.Manual,
                    Location = new Point(owner.Left + leftMargin, point.Y + button.Owner.Height),
                    Size = new Size(owner.Width - leftMargin - (owner.Right - point.X) + button.Bounds.Right, owner.Height - (point.Y - owner.Top) - 2 * leftMargin)
                };
                var helpBrowser = new WebBrowser()
                {
                    Parent = helpWindow,
                    Dock = DockStyle.Fill,
                    Url = new Uri(url)
                };
                helpWindow.Show(owner);
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
        // ~MainToolStripView() {
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
