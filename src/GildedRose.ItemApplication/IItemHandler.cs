// -----------------------------------------------------------------------
// <copyright file="IItemHandler.cs" company="Gilded Rose">
// Copyright (c) Gilded Rose.  All rights reserved.
// </copyright>
// <summary>Defines an interface that represents an item handler.</summary>
// -----------------------------------------------------------------------
namespace GildedRose.ItemApplication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines an interface that represents an item handler.
    /// </summary>
    public interface IItemHandler
    {
        /// <summary>
        /// Updates the quality of the supplied item.
        /// </summary>
        /// <param name="item">The item whose quality is updated.</param>
        void UpdateQuality(Item item);
    }
}
