// -----------------------------------------------------------------------
// <copyright file="ItemCategory.cs" company="Gilded Rose">
// Copyright (c) Gilded Rose.  All rights reserved.
// </copyright>
// <summary>Defines a class that represents an item with a category.</summary>
// -----------------------------------------------------------------------
namespace GildedRose.ItemApplication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a class that represents an item with a category.
    /// </summary>
    public class ItemCategory : Item
    {
        /// <summary>
        /// Gets or sets the name of the item category.
        /// </summary>
        /// <value>The name of the item category.</value>
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the item handler used to update the quality of the item.
        /// </summary>
        /// <value>The item handler used to update the quality of the item.</value>
        public IItemHandler ItemHandler { get; set; }
    }
}
