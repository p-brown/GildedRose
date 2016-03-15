// -----------------------------------------------------------------------
// <copyright file="ItemHandlerConfigurationElement.cs" company="Gilded Rose">
// Copyright (c) Gilded Rose.  All rights reserved.
// </copyright>
// <summary>Represents an item handler configuration item.</summary>
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
    /// Represents an item handler configuration item.
    /// </summary>
    public class ItemHandlerConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemHandlerConfigurationElement"/> class.
        /// </summary>
        public ItemHandlerConfigurationElement()
        {
            // Initialize members
            this["properties"] = new ItemPropertyConfigurationCollection();
        }

        /// <summary>
        /// Gets or sets the name of the item handler.
        /// </summary>
        /// <value>The name of the item handler.</value>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Gets or sets the type of the item handler.
        /// </summary>
        /// <value>The type of the item handler.</value>
        [ConfigurationProperty("type", IsKey = false, IsRequired = true)]
        public string Type
        {
            get { return (string)this["type"]; }
            set { this["type"] = value; }
        }

        /// <summary>
        /// Gets the item handler properties.
        /// </summary>
        /// <value>An item handler property collection.</value>
        [ConfigurationProperty("properties", IsDefaultCollection = false, IsRequired = false)]
        public ItemPropertyConfigurationCollection HandlerProperties
        {
            get
            {
                return (ItemPropertyConfigurationCollection)this["properties"];
            }
        }
    }
}
