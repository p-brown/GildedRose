// -----------------------------------------------------------------------
// <copyright file="ItemHandlerTests.cs" company="Gilded Rose">
// Copyright (c) Gilded Rose.  All rights reserved.
// </copyright>
// <summary>Defines a set of tests for item handlers.</summary>
// -----------------------------------------------------------------------
namespace GildedRose.Tests
{
    using System.Collections.Generic;
    using System.Globalization;
    using GildedRose.ItemApplication;
    using GildedRose.ItemHandlers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines a set of tests for item handlers.
    /// </summary>
    [TestClass]
    public class ItemHandlerTests
    {
        /// <summary>
        /// Tests the fixed quality no sell item handler.
        /// </summary>
        [TestMethod]
        public void ItemHandler_GivenALegendaryItem_WhenUpdateQualityCalledMoreThanOnce_ThenQualityShouldNotChange()
        {
            // Item properties
            int sellIn = 0;
            int quality = 80;

            // Create handler and item
            IItemHandler itemHandler = new FixedQualityNoSellItemHandler(new Dictionary<string, string>());
            ItemCategory item = new ItemCategory() { Name = "Sulfuras", CategoryName = "Legendary", SellIn = sellIn, Quality = quality, ItemHandler = itemHandler };

            // Test
            Assert.AreEqual<int>(quality, item.Quality, "The Quality should not have changed before updating the quality.");
            Assert.AreEqual<int>(sellIn, item.SellIn, "The SellIn value should not have changed before updating the quality.");

            itemHandler.UpdateQuality(item);

            Assert.AreEqual<int>(quality, item.Quality, "The Quality should not have changed for a Legendary item after updating the quality.");
            Assert.AreEqual<int>(sellIn, item.SellIn, "The SellIn value should not have changed for a Legendary item after updating the quality.");
        }

        /// <summary>
        /// Tests the normal aging item handler.
        /// </summary>
        [TestMethod]
        public void ItemHandler_GivenAnItemThatAgesNormallyTheCloserTheSellByDateApproaches_WhenUpdateQualityCalledMoreThanOnce_ThenQualityShouldIncrementLinearlyByOne()
        {
            // Item properties
            int sellIn = 2;
            int quality = 0;
            int reduceSellInBy = 1;
            int increaseQualityBy = 1;
            int increaseQualityBeyondSellInBy = 1;
            int maxQuality = 50;

            // Test item handler
            ItemHandlerTests.TestNormalAgingItemHandler(quality, sellIn, reduceSellInBy, increaseQualityBy, increaseQualityBeyondSellInBy, maxQuality);
        }

        /// <summary>
        /// Tests the normal aging item handler.
        /// </summary>
        [TestMethod]
        public void ItemHandler_GivenAnItemThatAgesNormallyTheCloserTheSellByDateApproaches_WhenUpdateQualityCalledMoreThanOnce_ThenQualityShouldIncrementLinearlyByTwo()
        {
            // Item properties
            int sellIn = 2;
            int quality = 0;
            int reduceSellInBy = 1;
            int increaseQualityBy = 2;
            int increaseQualityBeyondSellInBy = 1;
            int maxQuality = 50;

            // Test item handler
            ItemHandlerTests.TestNormalAgingItemHandler(quality, sellIn, reduceSellInBy, increaseQualityBy, increaseQualityBeyondSellInBy, maxQuality);
        }

        /// <summary>
        /// Tests the normal degrading item handler.
        /// </summary>
        [TestMethod]
        public void ItemHandler_GivenAnItemThatDegradesNormallyTheCloserTheSellByDateApproaches_WhenUpdateQualityCalledMoreThanOnce_ThenQualityShouldDecrementLinearlyByOneUntilSellByDatePassedThenTwiceAsFast()
        {
            // Item properties
            int sellIn = 10;
            int quality = 20;
            int reduceSellInBy = 1;
            int reduceQualityBy = 1;
            int reduceQualityBeyondSellInBy = 2;
            int maxQuality = 50;

            // Test item handler
            ItemHandlerTests.TestNormalDegradingItemHandler(quality, sellIn, reduceSellInBy, reduceQualityBy, reduceQualityBeyondSellInBy, maxQuality);
        }

        /// <summary>
        /// Tests the normal degrading item handler.
        /// </summary>
        [TestMethod]
        public void ItemHandler_GivenAnItemThatDegradesNormallyTheCloserTheSellByDateApproaches_WhenUpdateQualityCalledMoreThanOnce_ThenQualityShouldDecrementLinearlyByTwoUntilSellByDatePassedThenTwiceAsFast()
        {
            // Item properties
            int sellIn = 10;
            int quality = 20;
            int reduceSellInBy = 1;
            int reduceQualityBy = 2;
            int reduceQualityBeyondSellInBy = 4;
            int maxQuality = 50;

            // Test item handler
            ItemHandlerTests.TestNormalDegradingItemHandler(quality, sellIn, reduceSellInBy, reduceQualityBy, reduceQualityBeyondSellInBy, maxQuality);
        }

        /// <summary>
        /// Tests the incrementing aging item handler.
        /// </summary>
        [TestMethod]
        public void ItemHandler_GivenAnItemThatAgesBetterTheCloserTheSellByDateApproaches_WhenUpdateQualityCalledMoreThanOnce_ThenQualityShouldIncrementIncreasinglyTheCloserTheSellByDateApproaches()
        {
            // Item properties
            int sellIn = 15;
            int quality = 20;
            int reduceSellInBy = 1;
            int[] increaseQualitySellInThresholds = { 15, 10, 5 };
            int[] increaseQualitySellInThresholdsBy = { 1, 2, 3 };
            int maxQuality = 50;

            // Create handler properties
            Dictionary<string, string> handlerProperties = new Dictionary<string, string>();
            handlerProperties.Add("ReduceSellInBy", reduceSellInBy.ToString(CultureInfo.InvariantCulture));
            handlerProperties.Add("IncreaseQualitySellInThresholds", string.Join(";", increaseQualitySellInThresholds));
            handlerProperties.Add("IncreaseQualitySellInThresholdsBy", string.Join(";", increaseQualitySellInThresholdsBy));
            handlerProperties.Add("MaxQuality", maxQuality.ToString(CultureInfo.InvariantCulture));

            // Create handler and item
            IItemHandler itemHandler = new IncrementingAgingItemHandler(handlerProperties);
            ItemCategory item = new ItemCategory() { Name = "Backstage passes", CategoryName = "IncrementingAging", SellIn = sellIn, Quality = quality, ItemHandler = itemHandler };

            // Test
            Assert.AreEqual<int>(quality, item.Quality, "The Quality should not have changed before updating the quality.");
            Assert.AreEqual<int>(sellIn, item.SellIn, "The SellIn value should not have changed before updating the quality.");

            bool done = false;
            while (!done)
            {
                int currentQuality = item.Quality;
                int currentSellIn = item.SellIn;

                // Update the quality using the handler
                itemHandler.UpdateQuality(item);

                // Find right threshold
                int thresholdLevel = 0;
                for (int i = 0; i < increaseQualitySellInThresholds.Length; i++)
                {
                    if (item.SellIn <= increaseQualitySellInThresholds[i])
                    {
                        thresholdLevel = i;
                    }
                    else
                    {
                        break;
                    }
                }

                // Test that sell in decreased
                Assert.AreEqual<int>(currentSellIn - 1, item.SellIn, "The SellIn value should have decreased after updating the quality.");

                // Test that quality increased dependent with the sell in value
                if (item.SellIn >= 0)
                {
                    if ((currentQuality + increaseQualitySellInThresholdsBy[thresholdLevel]) > maxQuality)
                    {
                        Assert.IsTrue(item.Quality == maxQuality, $"The Quality should have increased to the maximum quality {maxQuality}.");
                    }
                    else
                    {
                        Assert.AreEqual<int>(currentQuality + increaseQualitySellInThresholdsBy[thresholdLevel], item.Quality, $"The Quality should have increased by {increaseQualitySellInThresholdsBy[thresholdLevel]} after updating the quality.");
                    }
                }
                else
                {
                    Assert.IsTrue(item.Quality == 0, $"The Quality should have dropped to zero once the sell by date had passed.");
                    done = true;
                }
            }
        }

        /// <summary>
        /// Tests a normal aging item handler.
        /// </summary>
        /// <param name="quality">The initial quality of the item.</param>
        /// <param name="sellIn">The initial sell in value of the item.</param>
        /// <param name="reduceSellInBy">The decrement value for reducing sell in.</param>
        /// <param name="increaseQualityBy">The increment value for increasing quality.</param>
        /// <param name="increaseQualityBeyondSellInBy">The increment value for increasing quality when the sell in has passed.</param>
        /// <param name="maxQuality">The maximum allowed quality.</param>
        private static void TestNormalAgingItemHandler(int quality, int sellIn, int reduceSellInBy, int increaseQualityBy, int increaseQualityBeyondSellInBy, int maxQuality)
        {
            // Create handler properties
            Dictionary<string, string> handlerProperties = new Dictionary<string, string>();
            handlerProperties.Add("ReduceSellInBy", reduceSellInBy.ToString(CultureInfo.InvariantCulture));
            handlerProperties.Add("IncreaseQualityBy", increaseQualityBy.ToString(CultureInfo.InvariantCulture));
            handlerProperties.Add("IncreaseQualityBeyondSellInBy", increaseQualityBeyondSellInBy.ToString(CultureInfo.InvariantCulture));
            handlerProperties.Add("MaxQuality", maxQuality.ToString(CultureInfo.InvariantCulture));

            // Create handler
            IItemHandler itemHandler = new NormalAgingItemHandler(handlerProperties);

            // Create item
            ItemCategory item = new ItemCategory() { Name = "Aged Brie", CategoryName = "NormalAging", SellIn = sellIn, Quality = quality, ItemHandler = itemHandler };

            // Test
            Assert.AreEqual<int>(quality, item.Quality, "The Quality should not have changed before updating the quality.");
            Assert.AreEqual<int>(sellIn, item.SellIn, "The SellIn value should not have changed before updating the quality.");

            bool done = false;
            while (!done)
            {
                int currentQuality = item.Quality;
                int currentSellIn = item.SellIn;

                // Update the quality using the handler
                itemHandler.UpdateQuality(item);

                // Test that sell in decreased
                Assert.AreEqual<int>(currentSellIn - reduceSellInBy, item.SellIn, "The SellIn value should have decreased after updating the quality.");

                // Test that max quality was not exceeded
                if (currentQuality == maxQuality)
                {
                    Assert.AreEqual<int>(maxQuality, item.Quality, $"The Quality should not have exceeded the maximum quality of {maxQuality}.");
                    done = true;
                }
                else
                {
                    // Test that quality increased (but not beyond max quality)
                    if (item.SellIn >= 0)
                    {
                        if ((currentQuality + increaseQualityBy) > maxQuality)
                        {
                            Assert.IsTrue(item.Quality == maxQuality, $"The Quality should have increased to the maximum quality {maxQuality}.");
                        }
                        else
                        {
                            Assert.AreEqual<int>(currentQuality + increaseQualityBy, item.Quality, $"The Quality should have increased by {increaseQualityBy} after updating the quality.");
                        }
                    }
                    else
                    {
                        if ((currentQuality + increaseQualityBeyondSellInBy) > maxQuality)
                        {
                            Assert.IsTrue(item.Quality == maxQuality, $"The Quality should have increased to the maximum quality {maxQuality}.");
                        }
                        else
                        {
                            Assert.AreEqual<int>(currentQuality + increaseQualityBeyondSellInBy, item.Quality, $"The Quality should have increased by {increaseQualityBeyondSellInBy} after updating the quality.");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Tests a normal aging item handler.
        /// </summary>
        /// <param name="quality">The initial quality of the item.</param>
        /// <param name="sellIn">The initial sell in value of the item.</param>
        /// <param name="reduceSellInBy">The decrement value for reducing sell in.</param>
        /// <param name="reduceQualityBy">The decrement value for reducing quality.</param>
        /// <param name="reduceQualityBeyondSellInBy">The decrement value for reducing quality when the sell in has passed.</param>
        /// <param name="maxQuality">The maximum allowed quality.</param>
        private static void TestNormalDegradingItemHandler(int quality, int sellIn, int reduceSellInBy, int reduceQualityBy, int reduceQualityBeyondSellInBy, int maxQuality)
        {
            // Create handler properties
            Dictionary<string, string> handlerProperties = new Dictionary<string, string>();
            handlerProperties.Add("ReduceSellInBy", reduceSellInBy.ToString(CultureInfo.InvariantCulture));
            handlerProperties.Add("ReduceQualityBy", reduceQualityBy.ToString(CultureInfo.InvariantCulture));
            handlerProperties.Add("ReduceQualityBeyondSellInBy", reduceQualityBeyondSellInBy.ToString(CultureInfo.InvariantCulture));
            handlerProperties.Add("MaxQuality", maxQuality.ToString(CultureInfo.InvariantCulture));

            // Create handler
            IItemHandler itemHandler = new NormalDegradingItemHandler(handlerProperties);

            // Create item
            ItemCategory item = new ItemCategory() { Name = "+5 Dexterity Vest", CategoryName = "NormalDegrading", SellIn = sellIn, Quality = quality, ItemHandler = itemHandler };

            // Test
            Assert.AreEqual<int>(quality, item.Quality, "The Quality should not have changed before updating the quality.");
            Assert.AreEqual<int>(sellIn, item.SellIn, "The SellIn value should not have changed before updating the quality.");

            bool done = false;
            while (!done)
            {
                int currentQuality = item.Quality;
                int currentSellIn = item.SellIn;

                // Update the quality using the handler
                itemHandler.UpdateQuality(item);

                // Test that sell in decreased
                Assert.AreEqual<int>(currentSellIn - reduceSellInBy, item.SellIn, "The SellIn value should have decreased after updating the quality.");

                // Test that quality decreased
                if (item.SellIn >= 0)
                {
                    if ((currentQuality - reduceQualityBy) < 0)
                    {
                        Assert.IsTrue(item.Quality == 0, $"The Quality should have decreased to the minimum quality 0.");
                    }
                    else
                    {
                        Assert.AreEqual<int>(currentQuality - reduceQualityBy, item.Quality, $"The Quality should have decreased by {reduceQualityBy} after updating the quality.");
                    }
                }
                else
                {
                    if ((currentQuality - reduceQualityBeyondSellInBy) < 0)
                    {
                        Assert.IsTrue(item.Quality == 0, $"The Quality should have decreased to the minimum quality 0.");
                    }
                    else
                    {
                        Assert.AreEqual<int>(currentQuality - reduceQualityBeyondSellInBy, item.Quality, $"The Quality should have decreased by {reduceQualityBeyondSellInBy} after updating the quality.");
                    }

                    done = true;
                }
            }
        }
    }
}