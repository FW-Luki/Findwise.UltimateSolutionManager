using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PowershellScriptExecutor
{
    internal class PowershellScriptFilenameEditor : FileNameEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var filename = base.EditValue(context, provider, value);
            return filename;
        }

        protected override void InitializeDialog(OpenFileDialog openFileDialog)
        {
            openFileDialog.Filter = "PowerShell script files|*.ps1|All files|*.*";
        }
    }
}
