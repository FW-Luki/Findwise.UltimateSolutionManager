using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Findwise.Configuration.TypeEditors;

namespace Findwise.Sharepoint.SolutionInstaller
{
    public interface IReportProgress
    {
        event EventHandler<ReportProgressEventArgs_OLD> ReportProgress;
    }


    public class ReportProgressEventArgs_OLD
    {
        public int Percentage { get; }
        public string Message { get; }
        public OperationTrait[] OperationTraits { get; }
        public ReportProgressEventArgs_OLD(int percentage, string message, params OperationTrait[] traits)
        {
            Percentage = percentage;
            Message = message;
            OperationTraits = traits;
        }
        public ReportProgressEventArgs_OLD(int num, int qty, string message, params OperationTrait[] traits)
            : this(Math.Max(Math.Min((int)(((double)num / qty) * 100), 100), 0), message, traits)
        {
        }
    }

    /// <summary>
    /// Contains operation traits/attributes (not to be confused with System.Attribute).
    /// </summary>
    public class OperationTrait
    {
        public static OperationTrait NoSetProjectAllowed => new OperationTrait();
        public static OperationTrait NoGetProjectAllowed => new OperationTrait();
        public static OperationTrait NoProjectItemsChangeAllowed => new OperationTrait();
        public static OperationTrait NoModuleOperationsAllowed => new OperationTrait();
        public static OperationTrait Cancellable => new OperationTrait();

        public static OperationTrait NoToolBoxAllowed => new OperationTrait();
        public static OperationTrait NoMainViewsAllowed => new OperationTrait();
        public static OperationTrait NoPropertiesAllowed => new OperationTrait();

        public static OperationTrait Active => new OperationTrait();
        public static OperationTrait Inactive => new OperationTrait();

        public static OperationTrait[] All => typeof(OperationTrait).GetProperties().Where(p => p.PropertyType == typeof(OperationTrait)).Select(p => (OperationTrait)p.GetValue(null)).ToArray();


        public /*readonly*/ string Name { get; }
        private OperationTrait([CallerMemberName] string name = null)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            Name = name;
        }

        public static bool operator ==(OperationTrait x, OperationTrait y)
        {
            return x.Name == y.Name;
        }
        public static bool operator !=(OperationTrait x, OperationTrait y)
        {
            return x.Name != y.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is OperationTrait that)
                return this == that;
            else
                return false;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }


        public class TypeEditor : ListBoxEditor
        {
            protected override object GetCurrentItem(object value)
            {
                return (OperationTrait)value;
            }

            protected override string GetDisplayMember()
            {
                return nameof(Name);
            }

            protected override object[] GetItems(ITypeDescriptorContext context)
            {
                return OperationTrait.All;
            }

            protected override object GetPreviousItem(object value)
            {
                return null;
            }

            protected override string GetValueMember()
            {
                return "";
            }
        }
        public class NameTypeConverter : TypeConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return (value as OperationTrait)?.Name;
                else
                    return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }

}
