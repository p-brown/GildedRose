﻿// -----------------------------------------------------------------------
// <copyright file="NormalDegradingItemHandler.cs" company="Gilded Rose">
// Copyright (c) Gilded Rose.  All rights reserved.
// </copyright>
// <summary>Defines an item handler that handles normal item degradation.</summary>
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
    /// Defines an item handler that handles normal item degradation.
    /// </summary>
    public class NormalDegradingItemHandler : ItemHandlerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NormalDegradingItemHandler"/> class.
        /// </summary>
        /// <param name="handlerProperties">The collection of handler properties.</param>
        public NormalDegradingItemHandler(IDictionary<string, string> handlerProperties)
            : base(handlerProperties)
        {
        }

        /// <summary>
        /// Updates the quality of the supplied item.
        /// </summary>
        /// <param name="item">The item whose quality is updated.</param>
        public override void UpdateQuality(Item item)
        {
            // Validate
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
        }
    }
}
