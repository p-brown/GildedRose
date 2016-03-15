// -----------------------------------------------------------------------
// <copyright file="ItemConfigurationElement.cs" company="Gilded Rose">
// Copyright (c) Gilded Rose.  All rights reserved.
// </copyright>
// <summary>Represents an item configuration item.</summary>
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
    /// Represents an item configuration item.
    /// </summary>
    public class ItemConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        /// <value>The name of the item.</value>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Gets or sets the number of days to sell the item.
        /// </summary>
        /// <value>The number of days to sell the item.</value>
        [ConfigurationProperty("sellIn", IsKey = false, IsRequired = true)]
        public int SellIn
        {
            get { return (int)this["sellIn"]; }
            set { this["sellIn"] = value; }
        }

        /// <summary>
        /// Gets or sets the quality of the item.
        /// </summary>
        /// <value>The number of days to sell the item.</value>
        [ConfigurationProperty("quality", IsKey = false, IsRequired = true)]
        public int Quality
        {
            get { return (int)this["quality"]; }
            set { this["quality"] = value; }
        }

        /// <summary>
        /// Gets or sets the category of the item.
        /// </summary>
        /// <value>The number of days to sell the item.</value>
        [ConfigurationProperty("category", IsKey = false, IsRequired = true)]
        public string Category
        {
            get { return (string)this["category"]; }
            set { this["category"] = value; }
        }
    }
}
