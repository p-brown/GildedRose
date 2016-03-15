// -----------------------------------------------------------------------
// <copyright file="ItemHandlerBase.cs" company="Gilded Rose">
// Copyright (c) Gilded Rose.  All rights reserved.
// </copyright>
// <summary>Defines an item handler that defines a fixed quality and no sell in period.</summary>
// -----------------------------------------------------------------------
namespace GildedRose.ItemHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GildedRose.ItemApplication;

    /// <summary>
    /// Defines a base item handler.
    /// </summary>
    public abstract class ItemHandlerBase : IItemHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemHandlerBase"/> class.
        /// </summary>
        /// <param name="handlerProperties">The collection of handler properties.</param>
        public ItemHandlerBase(IDictionary<string, string> handlerProperties)
        {
            // Validate
            if (handlerProperties == null)
            {
                throw new ArgumentNullException("handlerProperties");
            }

            // Assign arguments
            this.HandlerProperties = handlerProperties;
        }

        /// <summary>
        /// Gets a collection of handler properties.
        /// </summary>
        /// <value>A collection of handler properties.</value>
        public IDictionary<string, string> HandlerProperties { get; private set; }

        /// <summary>
        /// Implemented by derived classes to update the quality of an item.
        /// </summary>
        /// <param name="item">The item to update.</param>
        public abstract void UpdateQuality(Item item);
    }
}
