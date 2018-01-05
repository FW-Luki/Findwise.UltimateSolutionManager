using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Findwise.PluginManager;
using Findwise.Sharepoint.SolutionInstaller.Controls;

namespace Findwise.Sharepoint.SolutionInstaller
{
    public partial class Form1 : Form
    {
        [Obsolete("Do not use this field. Please use corresponding property.")]
        private BindingList<IInstallerModule> __installerModules;
        protected internal BindingList<IInstallerModule> InstallerModules
        {
#pragma warning disable CS0618 // Type or member is obsolete
            get { return __installerModules; }
            set
            {
                __installerModules = value;
                //var bindingList = new BindingList<IInstallerModule>(__installerModules);
                //bindingList.ListChanged += (s_, e_) => dataGridView1.Update();
                //var bindingSource = new BindingSource { DataSource = __installerModules };
                //__installerModules.CollectionChanged +=(s_,e_)=>dataGridView1.data
                dataGridView1.DataSource = __installerModules;
            }
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var buttons = System.IO.Directory.GetFiles("Modules", "*.dll", System.IO.SearchOption.TopDirectoryOnly)
                    .SelectMany(filename => AssemblyLoader.LoadClassesFromFile<IInstallerModule>(System.IO.Path.GetFullPath(filename)))
                    .Select(type =>
                    {
                        var module = (IInstallerModule)Activator.CreateInstance(type);
                        try
                        {
                            return GetToolboxButton(module.Name, module.Icon, type);
                        }
                        finally
                        {
                            (module as IDisposable)?.Dispose();
                        }
                    });
                if (buttons.Any())
                {
                    foreach (var button in buttons)
                    {
                        sizeablePanel1.Controls.Add(button);
                        sizeablePanel1.Controls.SetChildIndex(button, 0);
                    }
                }
                else
                {
                    var b = GetToolboxButton("No modules found", null);
                    b.Enabled = false;
                    sizeablePanel1.Controls.Add(b);
                }
            }
            catch (Exception ex)
            {
                var b = GetToolboxButton(ex.Message, null);
                b.Enabled = false;
                b.ForeColor = Color.Red;
                sizeablePanel1.Controls.Add(b);
            }

            NewProject();
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
                //Margin = new Padding(3, 3, 6, 3),
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
                InstallerModules.Add((IInstallerModule)Activator.CreateInstance(moduleType));
            }
        }

        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            NewProject();
        }
        private void NewProject()
        {
            InstallerModules = new BindingList<IInstallerModule>(); ;
        }

        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SaveProject(saveFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error saving project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void SaveProject(string filename)
        {
            System.IO.File.WriteAllText(filename, Project.Create(InstallerModules).Serialize());
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == NumberColumn.Index && e.RowIndex >= 0)
            {
                var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var value = e.RowIndex + 1;
                if (!(cell.Value is int cellValue) || cellValue != value)
                {
                    cell.Value = value;
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObjects = dataGridView1.SelectedRows.Cast<DataGridViewRow>().Select(r => (r.DataBoundItem as IInstallerModule)?.Configuration).Where(m => m != null).ToArray();
        }

    }
}
