// -----------------------------------------------------------------------
// <copyright file="ItemPropertyConfigurationElement.cs" company="Gilded Rose">
// Copyright (c) Gilded Rose.  All rights reserved.
// </copyright>
// <summary>Represents an item property configuration item.</summary>
// -----------------------------------------------------------------------
namespace GildedRose.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an item property configuration item.
    /// </summary>
    public class ItemPropertyConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        /// <value>The value of the property.</value>
        [ConfigurationProperty("value", IsKey = false, IsRequired = true)]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
    }
}
