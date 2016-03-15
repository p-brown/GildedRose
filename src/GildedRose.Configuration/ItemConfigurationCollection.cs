// -----------------------------------------------------------------------
// <copyright file="ItemConfigurationCollection.cs" company="Gilded Rose">
// Copyright (c) Gilded Rose.  All rights reserved.
// </copyright>
// <summary>Defines a class that represents a collection of items.</summary>
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
    /// Defines a class that represents a collection of items.
    /// </summary>
    public class ItemConfigurationCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Gets the type of the collection.
        /// </summary>
        /// <value>The type of the collection.</value>
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        /// <summary>
        /// Returns an object representing the item within the collection.
        /// </summary>
        /// <returns>An object representing the item in the collection.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ItemConfigurationElement();
        }

        /// <summary>
        /// Returns the key of the item.
        /// </summary>
        /// <param name="element">The configuration element.</param>
        /// <returns>The key of the supplied configuration element.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ItemConfigurationElement)element).Name;
        }
    }
}
