// -----------------------------------------------------------------------
// <copyright file="IncrementingAgingItemHandler.cs" company="Gilded Rose">
// Copyright (c) Gilded Rose.  All rights reserved.
// </copyright>
// <summary>Defines an item handler that handles incrementing item aging.</summary>
// -----------------------------------------------------------------------
namespace GildedRose.ItemHandlers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GildedRose.ItemApplication;
    using GildedRose.ItemHandlers.Properties;

    /// <summary>
    /// Defines an item handler that handles incrementing item aging.
    /// </summary>
    public class IncrementingAgingItemHandler : ItemHandlerBase
    {
        /// <summary>
        /// The amount to reduce the SellIn value by on each update.
        /// </summary>
        private int reduceSellInBy;

        /// <summary>
        /// The thresholds at which Quality increases at different rates.
        /// </summary>
        private int[] increaseQualitySellInThresholds;

        /// <summary>
        /// The amount at which Quality increases at different rates, related to the thresholds.
        /// </summary>
        private int[] increaseQualitySellInThresholdsBy;

        /// <summary>
        /// The maximum Quality value allowed for the item.
        /// </summary>
        private int maxQuality;

        /// <summary>
        /// Initializes a new instance of the <see cref="IncrementingAgingItemHandler"/> class.
        /// </summary>
        /// <param name="handlerProperties">The collection of handler properties.</param>
        public IncrementingAgingItemHandler(IDictionary<string, string> handlerProperties)
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

            if (!this.HandlerProperties.ContainsKey("IncreaseQualitySellInThresholds"))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.HandlerPropertyMissing, "IncreaseQualitySellInThresholds"), "handlerProperties");
            }
            else
            {
                this.increaseQualitySellInThresholds = this.HandlerProperties["IncreaseQualitySellInThresholds"].Split(';').Select(s => int.Parse(s, CultureInfo.InvariantCulture)).ToArray();
            }

            if (!this.HandlerProperties.ContainsKey("IncreaseQualitySellInThresholdsBy"))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.HandlerPropertyMissing, "IncreaseQualitySellInThresholdsBy"), "handlerProperties");
            }
            else
            {
                this.increaseQualitySellInThresholdsBy = this.HandlerProperties["IncreaseQualitySellInThresholdsBy"].Split(';').Select(s => int.Parse(s, CultureInfo.InvariantCulture)).ToArray();
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

            // Check property values
            if (this.increaseQualitySellInThresholds.Length != this.increaseQualitySellInThresholdsBy.Length)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.NumberOfThresholdsAndThresholdIncrementsNotTheSame, this.increaseQualitySellInThresholds.Length, this.increaseQualitySellInThresholdsBy.Length), "handlerProperties");
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

            if (item.SellIn >= 0)
            {
                // Increase quality (only if we have not hit the maximum)
                if (item.Quality < this.maxQuality)
                {
                    // Find correct increment for quality
                    int increment = 1;
                    for (int i = 0; i < this.increaseQualitySellInThresholds.Length; i++)
                    {
                        if (item.SellIn <= this.increaseQualitySellInThresholds[i])
                        {
                            increment = this.increaseQualitySellInThresholdsBy[i];
                        }
                        else
                        {
                            break;
                        }
                    }

                    item.Quality += increment;

                    // Reset to maximum if above
                    if (item.Quality > this.maxQuality)
                    {
                        item.Quality = this.maxQuality;
                    }
                }
            }
            else
            {
                // Quality drops to zero beyond sell by
                item.Quality = 0;
            }
        }
    }
}
