// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Gilded Rose">
// Copyright (c) Gilded Rose.  All rights reserved.
// </copyright>
// <summary>Defines the main program of the application.</summary>
// -----------------------------------------------------------------------
namespace GildedRose.Console
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using GildedRose.Configuration;
    using GildedRose.Console.Properties;
    using GildedRose.ItemApplication;

    /// <summary>
    /// Defines the main program of the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines a collection of item handlers loaded from configuration.
        /// </summary>
        private Dictionary<string, IItemHandler> itemHandlers = new Dictionary<string, IItemHandler>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Program"/> class.
        /// </summary>
        public Program()
        {
            this.Items = new List<Item>();
        }

        /// <summary>
        /// Gets the collection of items offered for sale.
        /// </summary>
        /// <value>The collection of items offered for sale.</value>
        public IList<Item> Items
        {
            get;
            private set;
        }

        /// <summary>
        /// Defines the entry point for the application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("OMGHAI!");

            // Start program and update quality of items
            var app = new Program();
            app.LoadConfiguration();
            app.UpdateQuality();

            Console.ReadKey();
        }

        /// <summary>
        /// Loads the configuration of items.
        /// </summary>
        public void LoadConfiguration()
        {
            // Load item handlers
            ItemMapConfigurationSection configurationSection = (ItemMapConfigurationSection)ConfigurationManager.GetSection("itemMap");
            foreach (ItemHandlerConfigurationElement itemHandler in configurationSection.Handlers)
            {
                // Get type
                Type itemHandlerType = Type.GetType(itemHandler.Type);
                if (itemHandlerType == null)
                {
                    throw new TypeLoadException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.FailedToLoadItemHandlerType, itemHandler.Type));
                }

                // Get properties
                Dictionary<string, string> itemHandlerProperties = itemHandler.HandlerProperties.Cast<ItemPropertyConfigurationElement>().ToDictionary(p => p.Name, p => p.Value);

                // Create item handler
                IItemHandler itemHandlerInstance = (IItemHandler)Activator.CreateInstance(itemHandlerType, itemHandlerProperties);
                this.itemHandlers.Add(itemHandler.Name, itemHandlerInstance);
            }

            // Load items
            foreach (ItemConfigurationElement item in configurationSection.Items)
            {
                ItemCategoryConfigurationElement itemCategory = configurationSection.Categories.Cast<ItemCategoryConfigurationElement>().SingleOrDefault(c => c.Name == item.Category);
                if (itemCategory == null)
                {
                    throw new ConfigurationErrorsException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.CategoryNotSpecifiedInConfiguration, item.Category, item.Name));
                }

                this.Items.Add(new ItemCategory() { Name = item.Name, CategoryName = item.Category, SellIn = item.SellIn, Quality = item.Quality, ItemHandler = itemCategory.Handler });
            }
        }

        /// <summary>
        /// Updates the quality of the items.
        /// </summary>
        public void UpdateQuality()
        {
            // Update the quality on each item using the associated item handler
            for (int i = 0; i < this.Items.Count; i++)
            {
                ItemCategory item = (ItemCategory)this.Items[i];

                Console.Write($"Item: {item.Name}, Sell In: {item.SellIn}, Current Quality: {item.Quality}, ");

                // Find item handler
                IItemHandler handler = this.itemHandlers.Where(kvp => kvp.Key == item.ItemHandler).Select(kvp => kvp.Value).SingleOrDefault();
                if (handler == null)
                {
                    throw new ConfigurationErrorsException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.ItemHandlerNotSpecifiedInConfiguration, item.ItemHandler, item.CategoryName));
                }

                // Update quality
                handler.UpdateQuality(item);

                Console.WriteLine($"Updated Quality: {item.Quality}");
            }
        }
    }
}