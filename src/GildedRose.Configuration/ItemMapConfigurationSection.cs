// -----------------------------------------------------------------------
// <copyright file="ItemMapConfigurationSection.cs" company="Gilded Rose">
// Copyright (c) Gilded Rose.  All rights reserved.
// </copyright>
// <summary>Defines a class that represents the configuration section for the Gilded Rose system.</summary>
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
    /// Defines a class that represents the configuration section for the Gilded Rose system.
    /// </summary>
    public class ItemMapConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// This constant defines the name of the section.
        /// </summary>
        public const string SectionName = "itemMap";

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemMapConfigurationSection"/> class.
        /// </summary>
        public ItemMapConfigurationSection()
        {
            // Initialize members
            this["handlers"] = new ItemHandlerConfigurationCollection();
            this["categories"] = new ItemCategoryConfigurationCollection();
            this["items"] = new ItemConfigurationCollection();
        }

        /// <summary>
        /// Gets the item handlers.
        /// </summary>
        /// <value>An item handler collection.</value>
        [ConfigurationProperty("handlers", IsDefaultCollection = false, IsRequired = true)]
        public ItemHandlerConfigurationCollection Handlers
        {
            get
            {
                return (ItemHandlerConfigurationCollection)this["handlers"];
            }
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <value>An item category collection.</value>
        [ConfigurationProperty("categories", IsDefaultCollection = false, IsRequired = true)]
        public ItemCategoryConfigurationCollection Categories
        {
            get
            {
                return (ItemCategoryConfigurationCollection)this["categories"];
            }
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>An item collection.</value>
        [ConfigurationProperty("items", IsDefaultCollection = false, IsRequired = true)]
        public ItemConfigurationCollection Items
        {
            get
            {
                return (ItemConfigurationCollection)this["items"];
            }
        }
    }
}
