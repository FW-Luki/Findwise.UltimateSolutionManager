using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design;
using System.Windows.Forms;
using Findwise.SolutionInstaller.Core;

namespace Findwise.Sharepoint.SolutionInstaller.Controls
{
    public class PropertyGridEx : PropertyGrid
    {
        private ToolStrip MyToolStrip;
        private ToolStripButton SortCategorizedButton;
        private ToolStripButton SortAlphabeticallyButton;
        private Bitmap dataBitmap;

        public new PropertySort PropertySort
        {
            get { return base.PropertySort; }
            set
            {
                base.PropertySort = value;
                SetSortButtonsState();
            }
        }


        public PropertyGridEx()
        {
            MyToolStrip = Controls.OfType<ToolStrip>().First();
            MyToolStrip.GripStyle = ToolStripGripStyle.Visible;
            MyToolStrip.RenderMode = ToolStripRenderMode.ManagerRenderMode;

            SetupSortButton(ref SortCategorizedButton, MyToolStrip, 0, PropertySort.Categorized);
            SetupSortButton(ref SortAlphabeticallyButton, MyToolStrip, 1, PropertySort.Alphabetical);
            //MyToolStrip.Items[4].Visible = false;

            dataBitmap = new Bitmap(typeof(ControlDesigner).Assembly.GetManifestResourceStream("System.Windows.Forms.Design.BoundProperty.bmp"));
            dataBitmap.MakeTransparent();

        }

        public void RefreshBindingGlyph()
        {
            this.BeginInvoke(new Action(() => { ShowGlyph(); }));
        }

        protected override void OnSelectedObjectsChanged(EventArgs e)
        {
            base.OnSelectedObjectsChanged(e);
            if (IsHandleCreated) BeginInvoke(new Action(() => { ShowGlyph(); }));
        }
        protected override void OnPropertySortChanged(EventArgs e)
        {
            base.OnPropertySortChanged(e);
            this.BeginInvoke(new Action(() => { ShowGlyph(); }));
        }
        private void ShowGlyph()
        {
            var grid = this.Controls[2];
            var field = grid.GetType().GetField("allGridEntries", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            var value = field.GetValue(grid);
            if (value == null)
                return;
            var entries = (value as IEnumerable).Cast<GridItem>().SelectMany(g => new[] { g }.Concat(g.GridItems.Cast<GridItem>()));
            foreach (var entry in entries)
            {
                if (entry.IsBound(out var component))
                {
                    var pvSvcField = entry.GetType().GetField("pvSvc", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                    IPropertyValueUIService pvSvc = new PropertyValueUIService();
                    pvSvc.AddPropertyValueUIHandler((context, propDesc, valueUIItemList) =>
                    {
                        valueUIItemList.Add(new PropertyValueUIItem(dataBitmap, (ctx, desc, invokedItem) => { }, GetToolTip(component.DataBindings.Cast<Binding>().FirstOrDefault(b => b.PropertyName == entry.PropertyDescriptor.Name) ?? component.DataBindings.Cast<Binding>().FirstOrDefault())));
                    });
                    pvSvcField.SetValue(entry, pvSvc);
                }
            }
        }
        private static string GetToolTip(Binding binding)
        {
            if (binding?.DataSource?.GetType().GetProperty("Name") is PropertyInfo nameProperty)
            {
                return $"Bound to: {nameProperty.GetValue(binding.DataSource).ToString()}";
            }
            else
            {
                return $"Datasource: {binding?.DataSource?.ToString() ?? "(null)"}";
            }
        }

        private void SetupSortButton(ref ToolStripButton button, ToolStrip owner, int index, PropertySort sort)
        {
            button = owner.Items[index] as ToolStripButton;
            if (button != null)
            {
                var events = button.GetType().GetProperty(nameof(Events), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(button) as EventHandlerList;
                var head = events.GetType().GetField("head", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(events);
                var key = head.GetType().GetField("key", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(head);
                var handler = head.GetType().GetField("handler", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(head) as Delegate;
                events.RemoveHandler(key, handler);

                button.Tag = sort;
                button.Click += (s_, e_) =>
                {
                    var b = s_ as ToolStripButton;
                    if (b.Tag is PropertySort s)
                    {
                        b.Checked = !b.Checked;
                        if (b.Checked)
                        {
                            PropertySort |= s;
                        }
                        else
                        {
                            PropertySort &= ~s;
                        }
                        RefreshDatasouce();
                    }
                    SetSortButtonsState();
                };
            }   
        }
        private void SetSortButtonsState()
        {
            foreach (var button in new[] { SortCategorizedButton, SortAlphabeticallyButton })
            {
                if (button.Tag is PropertySort s)
                {
                    button.Checked = PropertySort.HasFlag(s);
                }
            }
        }


        public void RefreshDatasouce()
        {
            var tmp = SelectedObjects;
            SelectedObjects = null;
            SelectedObjects = tmp;
        }

        public void MergeToolStrip(ToolStrip toolStrip)
        {
            if (MyToolStrip != null && toolStrip != null)
            {
                MyToolStrip.Items.Add(new FancyToolStripSeparator());
                //myToolStrip.AllowMerge = true;
                //toolStrip.AllowMerge = true;
                //ToolStripManager.Merge(myToolStrip, toolStrip);
                //ToolStripManager.Merge(toolStrip, myToolStrip);
                MyToolStrip.Items.AddRange(toolStrip.Items.OfType<ToolStripItem>().ToArray());
            }
        }
    }


    internal class PropertyValueUIService : IPropertyValueUIService
    {
        private PropertyValueUIHandler handler;
        private ArrayList items;

        public event EventHandler PropertyUIValueItemsChanged;
        public void NotifyPropertyValueUIItemsChanged()
        {
            PropertyUIValueItemsChanged?.Invoke(this, EventArgs.Empty);
        }
        public void AddPropertyValueUIHandler(PropertyValueUIHandler newHandler)
        {
            handler += newHandler ?? throw new ArgumentNullException("newHandler");
        }
        public PropertyValueUIItem[] GetPropertyUIValueItems(ITypeDescriptorContext context, PropertyDescriptor propDesc)
        {
            if (propDesc == null)
                throw new ArgumentNullException("propDesc");
            if (this.handler == null)
                return new PropertyValueUIItem[0];
            lock (this)
            {
                if (this.items == null)
                    this.items = new ArrayList();
                this.handler(context, propDesc, this.items);
                int count = this.items.Count;
                if (count > 0)
                {
                    PropertyValueUIItem[] propertyValueUiItemArray = new PropertyValueUIItem[count];
                    this.items.CopyTo((Array)propertyValueUiItemArray, 0);
                    this.items.Clear();
                    return propertyValueUiItemArray;
                }
            }
            return null;
        }
        public void RemovePropertyValueUIHandler(PropertyValueUIHandler newHandler)
        {
            handler -= newHandler ?? throw new ArgumentNullException("newHandler");

        }
    }
}
