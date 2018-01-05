using System;
using System.Collections.Generic;
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
        public Form1()
        {
            InitializeComponent();
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
                        return GetToolboxButton(module.Name, module.Icon, type);
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
            return button;
        }
    }
}
