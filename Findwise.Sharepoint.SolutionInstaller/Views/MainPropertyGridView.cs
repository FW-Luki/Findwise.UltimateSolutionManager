﻿using System;
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
using Findwise.Sharepoint.SolutionInstaller.Models;
using Findwise.SolutionInstaller.Core;

namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    [ToolboxItem(false)]
    public partial class MainPropertyGridViewDesigner : UserControl
    {
        internal readonly string DefaultHelpButtonText;

        public MainPropertyGridViewDesigner()
        {
            InitializeComponent();

            propertyGrid1.MergeToolStrip(PropertyGridMergeToolStrip);
            PropertyGridMergeToolStrip.Visible = false;

            DefaultHelpButtonText = HelpToolStripButton.Text;
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
        internal event SelectedGridItemChangedEventHandler PropertyGridSelectedGridItemChanged;


        internal void RefreshPropertyGrid()
        {
            propertyGrid1.RefreshDatasouce();
        }

        internal void FocusPropertyGrid()
        {
            if (!propertyGrid1.ContainsFocus) propertyGrid1.Focus();
        }


        private void RestoreDefaultToolStripButton_Click(object sender, EventArgs e)
        {
            propertyGrid1.ResetSelectedProperty();
        }

        private void MasterConfigSelectToolStrip_SizeChanged(object sender, EventArgs e)
        {
            MasterConfigSelectComboBox.Width = MasterConfigSelectToolStrip.DisplayRectangle.Width - MasterConfigSelectToolStrip.Padding.Horizontal - MasterConfigSelectComboBox.Margin.Horizontal - toolStripLabel1.Width - toolStripLabel1.Margin.Horizontal;
        }

        private void PropertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            PropertyGridSelectedGridItemChanged?.Invoke(sender, e);
        }

        private void MasterConfigSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid1.RefreshBindingGlyph();
        }
    }

    public class MainPropertyGridView : IComponentView
    {
        private MainPropertyGridViewDesigner designer = new MainPropertyGridViewDesigner();
        private List<ToolStripItem> propertyBindingItems = new List<ToolStripItem>();

        public Control Control => designer.Panel;
        public Controller[] Controllers { get; set; }
        public TableLayout Layout { get; set; } = new TableLayout();


        private ProjectManager _projectManager;
        public ProjectManager ProjectManager
        {
            get { return _projectManager; }
            set
            {
                _projectManager = value;
                BindDataSources();
                _projectManager.PropertyChanged += (s_, e_) => BindDataSources(); ;
            }
        }

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
                if (value == null || !value.Any()) Designer_PropertyGridSelectedGridItemChanged(null, new SelectedGridItemChangedEventArgs(null, null));
            }
        }

        public MasterConfig MasterConfig
        {
            get { return designer.MasterConfigSelectComboBox.SelectedItem as MasterConfig; }
        }


        public event EventHandler PropertyGridSelectedValueChanged;

        public event EventHandler SelectedMasterConfigurationChanged;


        public MainPropertyGridView()
        {
            designer.PropertyGridSelectedValueChanged += (s_, e_) => PropertyGridSelectedValueChanged?.Invoke(this, EventArgs.Empty);
            designer.PropertyGridSelectedGridItemChanged += Designer_PropertyGridSelectedGridItemChanged;
            designer.HelpToolStripButton.Click += Designer_HelpButtonClicked;
            designer.MasterConfigSelectComboBox.SelectedIndexChanged += (s_, e_) => SelectedMasterConfigurationChanged?.Invoke(this, EventArgs.Empty);
            designer.BindPropertyToolStripDropDownButton.DropDownOpening += BindPropertyToolStripDropDownButton_DropDownOpening;
            designer.BindPropertyToolStripDropDownButton.DropDownClosed += BindPropertyToolStripDropDownButton_DropDownClosed;
            designer.UnbindPropertyToolStripButton.Click += UnbindPropertyToolStripButton_Click;
            designer.NewBindingSourceToolStripMenuItem.Click += NewBindingSourceToolStripMenuItem_Click;
        }


        public void PreviewKeyDown(PreviewKeyDownEventArgs pkdevent)
        {
            if (pkdevent.KeyData == Keys.F4) designer.FocusPropertyGrid();
        }


        private void Designer_PropertyGridSelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            var helpLinkAttribs = e.NewSelection?.PropertyDescriptor?.Attributes.OfType<HelpLinkAttribute>();
            if (helpLinkAttribs?.Any() ?? false)
            {
                var helpLinkAttrib = helpLinkAttribs.First();
                designer.HelpToolStripButton.Tag = helpLinkAttrib.Url;
                designer.HelpToolStripButton.Text = helpLinkAttrib.Text ?? designer.DefaultHelpButtonText;
                designer.HelpToolStripButton.ToolTipText = helpLinkAttrib.Description ?? designer.DefaultHelpButtonText;
                designer.HelpToolStripButton.Enabled = true;
            }
            else
            {
                designer.HelpToolStripButton.Enabled = false;
            }

            if (sender is PropertyGrid propertyGrid && propertyGrid.SelectedObjects.Count() == 1 && GetBindableObject(propertyGrid, e.NewSelection) is IBindableComponent bindableComponent
            && (e.NewSelection?.PropertyDescriptor?.Attributes.OfType<BindableAttribute>().FirstOrDefault() ?? BindableAttribute.Default).Bindable)
            {   //Selected property grid item is bindable
                designer.NonBindableToolStripButton.Visible = false;
                if (bindableComponent.DataBindings.Cast<Binding>().Any(b => b.PropertyName == GetBindablePropertyDescriptor(e.NewSelection.PropertyDescriptor).Name))
                {   //Selected property grid item is bound
                    designer.BindPropertyToolStripDropDownButton.Visible = false;
                    designer.UnbindPropertyToolStripButton.Visible = true;

                    var bindingDef = new BindingDefinition(bindableComponent, GetBindablePropertyDescriptor(e.NewSelection.PropertyDescriptor));
                    designer.UnbindPropertyToolStripButton.Tag = bindingDef;
                }
                else
                {   //Selected property grid item is unbound
                    designer.BindPropertyToolStripDropDownButton.Visible = true;
                    designer.UnbindPropertyToolStripButton.Visible = false;

                    var bindingDef = new BindingDefinition(bindableComponent, GetBindablePropertyDescriptor(e.NewSelection.PropertyDescriptor));
                    designer.BindPropertyToolStripDropDownButton.Tag = bindingDef;
                    designer.NewBindingSourceToolStripMenuItem.Tag = bindingDef;
                }
            }
            else
            {   //Selected property grid item is non-bindable
                designer.BindPropertyToolStripDropDownButton.Visible = false;
                designer.UnbindPropertyToolStripButton.Visible = false;
                designer.NonBindableToolStripButton.Visible = true;
            }
        }
        private static PropertyDescriptor GetBindablePropertyDescriptor(PropertyDescriptor descriptor)
        {
            return descriptor.PropertyType.GetCustomAttributes(false).OfType<BindingSurrogateAttribute>().FirstOrDefault() is BindingSurrogateAttribute bsa ?
                TypeDescriptor.GetProperties(descriptor.PropertyType)[bsa.BindablePropertyName] : descriptor;
        }
        private static object GetBindableObject(PropertyGrid propertyGrid, GridItem gridItem)
        {
            if (gridItem != null)
            {
                if (gridItem.Value?.GetType().GetCustomAttributes(false).OfType<BindingSurrogateAttribute>().Any()??false)
                {
                    return gridItem.Value;
                }
                if (gridItem.Parent?.Value != null)
                {
                    return gridItem.Parent.Value;
                }
            }
            return propertyGrid.SelectedObject;
        }

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

        private void BindPropertyToolStripDropDownButton_DropDownOpening(object sender, EventArgs e)
        {
            if (designer.BindPropertyToolStripDropDownButton.HasDropDownItems) designer.BindPropertyToolStripDropDownButton.DropDownItems.Clear();
            designer.BindPropertyToolStripDropDownButton.DropDownItems.Add(designer.NewBindingSourceToolStripMenuItem);
            if ((sender as ToolStripItem)?.Tag is BindingDefinition bindingDef)
            {
                var bindingSources = _projectManager.Project.BindingSourceList.Where(b => b.Type != null && b.Type.IsAssignableFrom(GetDefaultBindingSourceType(bindingDef.PropertyDescriptor, bindingDef.Component)));
                if (bindingSources.Any())
                {
                    designer.BindPropertyToolStripDropDownButton.DropDownItems.Add(designer.BindingSourcesToolStripSeparator);
                    foreach (var bindingSource in bindingSources)
                    {
                        var newBindingDef = (BindingDefinition)bindingDef.Clone();
                        newBindingDef.DataSource = bindingSource;
                        var item = new ToolStripMenuItem(bindingSource.Name, null, BindProperty)
                        {
                            Tag = newBindingDef
                        };
                        propertyBindingItems.Add(item);
                        designer.BindPropertyToolStripDropDownButton.DropDownItems.Add(item);
                    }
                }
            }
        }
        private void BindPropertyToolStripDropDownButton_DropDownClosed(object sender, EventArgs e)
        {
            designer.BindPropertyToolStripDropDownButton.DropDownItems.Clear();
            propertyBindingItems.ForEach(item => item.Dispose());
            propertyBindingItems.Clear();
        }
        private void NewBindingSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripItem)?.Tag is BindingDefinition bindingDef)
            {
                _projectManager.AddDataBindingSource(GetDefaultBindingSourceName(bindingDef.PropertyDescriptor, bindingDef.Component), GetDefaultBindingSourceType(bindingDef.PropertyDescriptor, bindingDef.Component));
                bindingDef.DataSource = _projectManager.Project.BindingSourceList.Last();
                ((ToolStripItem)sender).Tag = bindingDef; //AddDataBindingSource causes PropertyGrid selected objects change which results in changing the sender's Tag, so we have to assign our value back.
                BindProperty(sender, e);
            }
        }
        private string GetDefaultBindingSourceName(PropertyDescriptor prop, object component)
        {
            string name = null;
            if (prop.Attributes.OfType<DefaultBindingItemNameProviderAttribute>().FirstOrDefault() is DefaultBindingItemNameProviderAttribute npa)
            {
                name = component.GetType().GetProperty(npa.BindingItemDefaultNameProviderPropertyName)?.GetValue(component)?.ToString();
            }
            if (prop.Attributes.OfType<DefaultBindingItemNameAttribute>().FirstOrDefault() is DefaultBindingItemNameAttribute na)
            {
                name = na.BindingItemDefaultName;
            }
            return string.IsNullOrEmpty(name) ? prop.Name : name;
        }
        private Type GetDefaultBindingSourceType(PropertyDescriptor prop, object component)
        {
            Type type = null;
            if (prop.Attributes.OfType<DefaultBindingItemTypeProviderAttribute>().FirstOrDefault() is DefaultBindingItemTypeProviderAttribute tpa)
            {
                type = component.GetType().GetProperty(tpa.BindingItemDefaultTypeProviderPropertyName)?.GetValue(component) as Type;
            }
            if (prop.Attributes.OfType<DefaultBindingItemTypeAttribute>().FirstOrDefault() is DefaultBindingItemTypeAttribute ta)
            {
                type = ta.BindingItemDefaultType;
            }
            return type ?? prop.PropertyType;
        }
        private void BindProperty(object sender, EventArgs e)
        {
            if ((sender as ToolStripItem)?.Tag is BindingDefinition bindingDef)
            {
                bindingDef.Component.DataBindings.Add(bindingDef.PropertyDescriptor.Name, bindingDef.DataSource, nameof(BindingItem.Value));
                designer.RefreshPropertyGrid();
            }
        }
        private void UnbindPropertyToolStripButton_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripItem)?.Tag is BindingDefinition bindingDef)
            {
                var binding = bindingDef.Component.DataBindings.Cast<Binding>().FirstOrDefault(b => b.PropertyName == bindingDef.PropertyDescriptor.Name);
                if (binding != null)
                {
                    bindingDef.Component.DataBindings.Remove(binding);
                    designer.RefreshPropertyGrid();
                }
            }
        }

        private void BindDataSources()
        {
            designer.MasterConfigSelectComboBox.ComboBox.DataSource = _projectManager.Project.MasterConfigurationList;
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


        private class BindingDefinition : ICloneable
        {
            public IBindableComponent Component { get; }
            public PropertyDescriptor PropertyDescriptor { get; }
            public BindingItem DataSource { get; set; }
            public BindingDefinition(IBindableComponent component, PropertyDescriptor propertyDescriptor)
            {
                Component = component;
                PropertyDescriptor = propertyDescriptor;
            }

            public object Clone()
            {
                return new BindingDefinition(Component, PropertyDescriptor);
            }
        }
    }
}
