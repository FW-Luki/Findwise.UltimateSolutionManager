using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Findwise.Configuration.TypeEditors;

namespace Findwise.SolutionManager.Views
{
    /// <summary>
    /// Defines an <see cref="IView"/> which implements <see cref="IComponent"/> and thus can be added to the form or control component list as an icon below the visual designer.
    /// </summary>
    /// <example>
    /// Below is shown an example of implementing <see cref="IComponentView"/> interface using two classes:
    ///     1. A <see cref="System.Windows.Forms.UserControl"/> containing the control for your <see cref="IView"/> providing the Windows Designer for your convenience.
    ///     2. A <see cref="IComponentView"/> implementation class itself supporting disposing of the designer <see cref="System.Windows.Forms.UserControl"/>.
    /// <![CDATA[
    ///     [ToolboxItem(false)]
    ///     public partial class {Name}ViewDesigner : UserControl
    ///     {
    ///         public {Name}ViewDesigner()
    ///         {
    ///             InitializeComponent();
    ///         }
    ///     }
    /// 
    ///     public class {Name}View : IComponentView
    ///     {
    ///         private readonly {Name}ViewDesigner designer = new {Name}ViewDesigner();
    /// 
    ///         public Control Control => designer;
    ///         public TableLayout Layout { get; set; } = new TableLayout();
    /// 
    ///         #region IComponent Support
    ///         public ISite Site { get; set; }
    /// 
    ///         public event EventHandler Disposed;
    /// 
    ///         #region IDisposable Support
    ///         private bool disposedValue = false; // To detect redundant calls
    /// 
    ///         protected virtual void Dispose(bool disposing)
    ///         {
    ///             if (!disposedValue)
    ///             {
    ///                 if (disposing)
    ///                 {
    ///                     // TODO: dispose managed state (managed objects).
    ///                     designer.Dispose();
    ///                 }
    /// 
    ///                 // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
    ///                 // TODO: set large fields to null.
    /// 
    ///                 disposedValue = true;
    ///                 Disposed?.Invoke(this, EventArgs.Empty);
    ///             }
    ///         }
    /// 
    ///         // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    ///         // ~MainToolboxView() {
    ///         //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    ///         //   Dispose(false);
    ///         // }
    /// 
    ///         // This code added to correctly implement the disposable pattern.
    ///         public void Dispose()
    ///         {
    ///             // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    ///             Dispose(true);
    ///             // TODO: uncomment the following line if the finalizer is overridden above.
    ///             // GC.SuppressFinalize(this);
    ///         }
    ///         #endregion
    ///         #endregion
    ///     }
    /// ]]>
    /// </example>
    interface IComponentView : IComponent, IView
    {
        TableLayout Layout { get; }
    }


    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TableLayout
    {
        public int Column { get; set; }

        [DefaultValue(1)]
        public int ColumnSpan { get; set; } = 1;


        public int Row { get; set; }

        [DefaultValue(1)]
        public int RowSpan { get; set; } = 1;


        [DefaultValue(null)]
        [Editor(typeof(CreateInstanceEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ColumnStyle ColumnStyle { get; set; } //= new ColumnStyle();

        [DefaultValue(null)]
        [Editor(typeof(CreateInstanceEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public RowStyle RowStyle { get; set; } //= new RowStyle();
    }
}
