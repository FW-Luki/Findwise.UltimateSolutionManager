using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Findwise.Configuration;

namespace DatabaseCreator
{
    public class Configuration : ConfigurationBase, IBindableComponent
    {
        [Bindable(true)]
        public string ServerName { get; set; }
        private string _databaseName;
        public string DatabaseName { get; set; }
        public string Tablename { get; set; }
        public DbColumn[] Columns { get; set; }

        public Configuration()
        {
            DataBindings = new ControlBindingsCollection(this);
        }

        #region IBindableComponent Support
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlBindingsCollection DataBindings { get; }

        //[System.Xml.Serialization.XmlIgnore, System.Runtime.Serialization.IgnoreDataMember]
        [Browsable(false)]
        public BindingContext BindingContext { get; set; } = new BindingContext();

        #region IComponent Support
        [Browsable(false)]
        public ISite Site { get; set; } = null;

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
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
                Disposed?.Invoke(this, EventArgs.Empty);
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Configuration() {
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
        #endregion

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class DbColumn
        {
            public string Name { get; set; }
            public string TypeName { get; set; }
            public bool IsIdentity { get; set; }
            public bool IsNullable { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
    }

}
