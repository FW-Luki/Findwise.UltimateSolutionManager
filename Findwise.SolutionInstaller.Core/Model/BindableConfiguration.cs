﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Findwise.Configuration;

namespace Findwise.SolutionInstaller.Core.Model
{
    public class BindableConfiguration : ConfigurationBase, IBindableComponent
    {
        protected virtual PropertyHelper Prop { get; } = new PropertyHelper();

        public BindableConfiguration()
        {
            DataBindings = new ControlBindingsCollection(this);
            Site = new BindableConfigurationSite(this);
        }


        #region IBindableComponent Support
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlBindingsCollection DataBindings { get; }
        [Browsable(false)]
        public Binding[] Bindings
        {
            get => DataBindings.Cast<System.Windows.Forms.Binding>().Select(b => (Binding)b).ToArray();
            set
            {
                DataBindings.Clear();
                foreach (var item in value ?? Enumerable.Empty<Binding>())
                {
                    DataBindings.Add(item.PropertyName, item.DataSource, item.DataMember);
                }
            }
        }
        public class Binding
        {
            public string PropertyName { get; /*private*/ set; }
            public object DataSource { get; /*private*/ set; }
            public string DataMember { get; /*private*/ set; }

            public static explicit operator Binding(System.Windows.Forms.Binding binding)
            {
                return new Binding()
                {
                    PropertyName = binding.PropertyName,
                    DataSource = binding.DataSource,
                    DataMember = binding.BindingMemberInfo.BindingField
                };
            }
        }

        [XmlIgnore, IgnoreDataMember]
        [Browsable(false)]
        public BindingContext BindingContext { get; set; } = new BindingContext();

        #region IComponent Support
        [Browsable(false)]
        [XmlIgnore, IgnoreDataMember]
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

    }
}
