// -----------------------------------------------------------------------
// <copyright file="NormalAgingItemHandler.cs" company="Gilded Rose">
// Copyright (c) Gilded Rose.  All rights reserved.
// </copyright>
// <summary>Defines an item handler that handles normal item aging.</summary>
// -----------------------------------------------------------------------
namespace GildedRose.ItemHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GildedRose.ItemApplication;
    using GildedRose.ItemHandlers.Properties;

    /// <summary>
    /// Defines an item handler that handles normal item aging.
    /// </summary>
    public class NormalAgingItemHandler : ItemHandlerBase
    {
        /// <summary>
        /// The amount to reduce the SellIn value by on each update.
        /// </summary>
        private int reduceSellInBy;

        /// <summary>
        /// The amount to increase the Quality by on each update.
        /// </summary>
        private int increaseQualityBy;

        /// <summary>
        /// The amount to increase the Quality by on each update when the item is beyond the SellIn days.
        /// </summary>
        private int increaseQualityBeyondSellInBy;

        /// <summary>
        /// The maximum Quality value allowed for the item.
        /// </summary>
        private int maxQuality;

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalAgingItemHandler"/> class.
        /// </summary>
        /// <param name="handlerProperties">The collection of handler properties.</param>
        public NormalAgingItemHandler(IDictionary<string, string> handlerProperties)
            : base(handlerProperties)
        {
            // Parse properties
            if (!this.HandlerProperties.ContainsKey("ReduceSellInBy"))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.HandlerPropertyMissing, "ReduceSellInBy"), "handlerProperties");
            }
            else
            {
                this.reduceSellInBy = int.Parse(this.HandlerProperties["ReduceSellInBy"], CultureInfo.InvariantCulture);
            }

            if (!this.HandlerProperties.ContainsKey("IncreaseQualityBy"))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.HandlerPropertyMissing, "IncreaseQualityBy"), "handlerProperties");
            }
            else
            {
                this.increaseQualityBy = int.Parse(this.HandlerProperties["IncreaseQualityBy"], CultureInfo.InvariantCulture);
            }

            if (!this.HandlerProperties.ContainsKey("IncreaseQualityBeyondSellInBy"))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.HandlerPropertyMissing, "IncreaseQualityBeyondSellInBy"), "handlerProperties");
            }
            else
            {
                this.increaseQualityBeyondSellInBy = int.Parse(this.HandlerProperties["IncreaseQualityBeyondSellInBy"], CultureInfo.InvariantCulture);
            }

            if (!this.HandlerProperties.ContainsKey("MaxQuality"))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.HandlerPropertyMissing, "MaxQuality"), "handlerProperties");
            }
            else
            {
                this.maxQuality = int.Parse(this.HandlerProperties["MaxQuality"], CultureInfo.InvariantCulture);
            }
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

            if (item.Quality < 0)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.QualityLessThanMinimum, item.Quality, 0), "item");
            }

            if (item.Quality > this.maxQuality)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.QualityGreaterThanMaximum, item.Quality, this.maxQuality), "item");
            }

            // Decrease sell in value
            item.SellIn -= this.reduceSellInBy;

            // Increase quality (only if we have not hit the maximum)
            if (item.Quality < this.maxQuality)
            {
                if (item.SellIn >= 0)
                {
                    item.Quality += this.increaseQualityBy;
                }
                else
                {
                    item.Quality += this.increaseQualityBeyondSellInBy;
                }

                // Reset to maximum if above
                if (item.Quality > this.maxQuality)
                {
                    item.Quality = this.maxQuality;
                }
            }
        }
    }
}
