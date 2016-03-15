// -----------------------------------------------------------------------
// <copyright file="ItemCategoryConfigurationElement.cs" company="Gilded Rose">
// Copyright (c) Gilded Rose.  All rights reserved.
// </copyright>
// <summary>Represents an item category configuration item.</summary>
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
    /// Represents an item category configuration item.
    /// </summary>
    public class ItemCategoryConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>The name of the category.</value>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Gets or sets the handler for the category.
        /// </summary>
        /// <value>The handler for the category.</value>
        [ConfigurationProperty("handler", IsKey = false, IsRequired = true)]
        public string Handler
        {
            get { return (string)this["handler"]; }
            set { this["handler"] = value; }
        }
    }
}
