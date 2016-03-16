// -----------------------------------------------------------------------
// <copyright file="NormalDegradingItemHandler.cs" company="Gilded Rose">
// Copyright (c) Gilded Rose.  All rights reserved.
// </copyright>
// <summary>Defines an item handler that handles normal item degradation.</summary>
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
    /// Defines an item handler that handles normal item degradation.
    /// </summary>
    public class NormalDegradingItemHandler : ItemHandlerBase
    {
        /// <summary>
        /// The amount to reduce the SellIn value by on each update.
        /// </summary>
        private int reduceSellInBy;

        /// <summary>
        /// The amount to reduce the Quality by on each update.
        /// </summary>
        private int reduceQualityBy;

        /// <summary>
        /// The amount to reduce the Quality by on each update when the item is beyond the SellIn days.
        /// </summary>
        private int reduceQualityBeyondSellInBy;

        /// <summary>
        /// The maximum Quality value allowed for the item.
        /// </summary>
        private int maxQuality;

        /// <summary>
        /// The minimum Quality value allowed for the item.
        /// </summary>
        private int minQuality;

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalDegradingItemHandler"/> class.
        /// </summary>
        /// <param name="handlerProperties">The collection of handler properties.</param>
        public NormalDegradingItemHandler(IDictionary<string, string> handlerProperties)
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

            if (!this.HandlerProperties.ContainsKey("ReduceQualityBy"))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.HandlerPropertyMissing, "ReduceQualityBy"), "handlerProperties");
            }
            else
            {
                this.reduceQualityBy = int.Parse(this.HandlerProperties["ReduceQualityBy"], CultureInfo.InvariantCulture);
            }

            if (!this.HandlerProperties.ContainsKey("ReduceQualityBeyondSellInBy"))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.HandlerPropertyMissing, "ReduceQualityBeyondSellInBy"), "handlerProperties");
            }
            else
            {
                this.reduceQualityBeyondSellInBy = int.Parse(this.HandlerProperties["ReduceQualityBeyondSellInBy"], CultureInfo.InvariantCulture);
            }

            if (!this.HandlerProperties.ContainsKey("MaxQuality"))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.HandlerPropertyMissing, "MaxQuality"), "handlerProperties");
            }
            else
            {
                this.maxQuality = int.Parse(this.HandlerProperties["MaxQuality"], CultureInfo.InvariantCulture);
            }

            if (!this.HandlerProperties.ContainsKey("MaxQuality"))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.HandlerPropertyMissing, "MaxQuality"), "handlerProperties");
            }
            else
            {
                this.maxQuality = int.Parse(this.HandlerProperties["MaxQuality"], CultureInfo.InvariantCulture);
            }

            if (!this.HandlerProperties.ContainsKey("MinQuality"))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.HandlerPropertyMissing, "MinQuality"), "handlerProperties");
            }
            else
            {
                this.minQuality = int.Parse(this.HandlerProperties["MinQuality"], CultureInfo.InvariantCulture);
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

            if (item.Quality < this.minQuality)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.QualityLessThanMinimum, item.Quality, this.minQuality), "item");
            }

            if (item.Quality > this.maxQuality)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.QualityGreaterThanMaximum, item.Quality, this.maxQuality), "item");
            }

            // Decrease sell in value
            item.SellIn -= this.reduceSellInBy;

            // Decrease quality (only if we have not hit the minimum)
            if (item.Quality > this.minQuality)
            {
                if (item.SellIn >= 0)
                {
                    item.Quality -= this.reduceQualityBy;
                }
                else
                {
                    item.Quality -= this.reduceQualityBeyondSellInBy;
                }
            }
        }
    }
}
