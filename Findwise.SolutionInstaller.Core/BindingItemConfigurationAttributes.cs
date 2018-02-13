﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.SolutionInstaller.Core
{
    /// <summary>
    /// Specifies default <!--<see cref="BindingItem"/>--> BindingItem name instead of this property name.
    /// </summary>
    public class DefaultBindingItemNameAttribute : Attribute
    {
        public string BindingItemDefaultName { get; }
        public DefaultBindingItemNameAttribute(string name)
        {
            BindingItemDefaultName = name;
        }
    }

    /// <summary>
    /// Indicates a name of the property holding value used for default BindingItem name.
    /// </summary>
    public class DefaultBindingItemNameProviderAttribute : Attribute
    {
        public string BindingItemDefaultNameProviderPropertyName { get; }
        public DefaultBindingItemNameProviderAttribute(string propertyName)
        {
            BindingItemDefaultNameProviderPropertyName = propertyName;
        }
    }


    /// <summary>
    /// Specifies default BindingItem type instead of this property type.
    /// </summary>
    public class DefaultBindingItemTypeAttribute : Attribute
    {
        public Type BindingItemDefaultType { get; }
        public DefaultBindingItemTypeAttribute(Type type)
        {
            BindingItemDefaultType = type;
        }
    }

    /// <summary>
    /// Indicates a name of the property holding value used for default BindingItem type.
    /// </summary>
    public class DefaultBindingItemTypeProviderAttribute : Attribute
    {
        public string BindingItemDefaultTypeProviderPropertyName { get; }
        public DefaultBindingItemTypeProviderAttribute(string propertyName)
        {
            BindingItemDefaultTypeProviderPropertyName = propertyName;
        }
    }
}