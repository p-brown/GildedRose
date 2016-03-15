// -----------------------------------------------------------------------
// <copyright file="Item.cs" company="Gilded Rose">
// Copyright (c) Gilded Rose.  All rights reserved.
// </copyright>
// <summary>Defines an item for sale.</summary>
// -----------------------------------------------------------------------
namespace GildedRose.ItemApplication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines an item for sale.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        /// <value>The name of the item.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the number of days available to sell the item.
        /// </summary>
        /// <value>The number of days available to sell the item.</value>
        public int SellIn { get; set; }

        /// <summary>
        /// Gets or sets the quality of the item.
        /// </summary>
        /// <value>The quality of the item.</value>
        public int Quality { get; set; }
    }
}
