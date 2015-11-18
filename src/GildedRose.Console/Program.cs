using System.Collections.Generic;

namespace GildedRose.Console
{
    public class Program
    {
        public static IList<Item> Items;
        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");
            System.Console.WriteLine();

            Items = new List<Item>
                                        {
                                            new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                            new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                            new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                            new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                            new Item
                                                {
                                                    Name = "Backstage passes to a TAFKAL80ETC concert",
                                                    SellIn = 15,
                                                    Quality = 20
                                                },
                                            new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                        };
            for (int i = 0; i < Items.Count; i++)
            {
                System.Console.WriteLine(string.Format("Item : {0}, Quality : {1}, SellIn : {2}", Items[i].Name, Items[i].Quality, Items[i].SellIn));
            }

            Program.UpdateQuality();
            System.Console.WriteLine();
            System.Console.WriteLine("Updated...");
            System.Console.WriteLine();

            for (int i = 0; i < Items.Count; i++)
            {
                System.Console.WriteLine(string.Format("Item : {0}, Quality : {1}, SellIn : {2}", Items[i].Name, Items[i].Quality, Items[i].SellIn));
            }

            System.Console.ReadKey();

        }

        /// <summary>
        /// Increments the original value by the increment value. Return value cannot be greater than 50
        /// or less than 0.
        /// </summary>
        /// <param name="originalValue">Quality value to increment</param>
        /// <param name="increment">Number to increment by, can be negative</param>
        /// <returns>New quality value</returns>
        private static int IncrementItemQuality(int originalValue, int increment)
        {
            if (increment > 0)
            {
                if (originalValue + increment <= 50) originalValue += increment;
                else originalValue = 50;
            }
            else
            {
                if (originalValue + increment > 0) originalValue += increment;
                else originalValue = 0;
            }
            return originalValue;
        }

        static public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                switch (Items[i].Name)
                {
                    case "Sulfuras, Hand of Ragnaros":
                        // Never has to be sold, never degenerates.
                        break;

                    case "Backstage passes to a TAFKAL80ETC concert":
                        // Backstage passes increase in quality with age.                                               
                        if (Items[i].SellIn < 6)
                        {
                            // 5 days or less, increase by 3.
                            Items[i].Quality = IncrementItemQuality(Items[i].Quality, 3);
                        }
                        else if (Items[i].SellIn < 11)
                        {
                            // 10 days or less, increase by 2.
                            Items[i].Quality = IncrementItemQuality(Items[i].Quality, 2);
                        }
                        else
                        {
                            // Above 10 days increase by 1.
                            Items[i].Quality = IncrementItemQuality(Items[i].Quality, 1);
                        }
                        // Sell in days decreased by 1.
                        Items[i].SellIn--;
                        // When beyond sell in days, backstage passes have 0 quality.
                        if (Items[i].SellIn < 0)
                        {
                            Items[i].Quality = 0;
                        }
                        break;

                    case "Aged Brie":
                        // Aged brie gets better with age, but never better than 50.
                        Items[i].Quality = IncrementItemQuality(Items[i].Quality, 1);
                        // Sell in days decreased by 1.
                        Items[i].SellIn--;
                        // Aged brie increases in quality twice as fast when past sell in date.
                        if (Items[i].SellIn < 0)
                        {
                            Items[i].Quality = IncrementItemQuality(Items[i].Quality, 1);
                        }
                        break;

                    case "Conjured Mana Cake":
                        // Conjured items degenerate twice as fast as normal items to a minimum of 0.
                        Items[i].Quality = IncrementItemQuality(Items[i].Quality, -2);
                        // Sell in days decreased by 1.
                        Items[i].SellIn--;
                        // Conjured items degenerate twice as quickly when past sell in days.
                        if (Items[i].SellIn < 0)
                        {
                            Items[i].Quality = IncrementItemQuality(Items[i].Quality, -2);
                        }
                        break;

                    default:
                        // Normal items degenerate to a minimum of 0.
                        Items[i].Quality = IncrementItemQuality(Items[i].Quality, -1);
                        // Sell in days decreased by 1.
                        Items[i].SellIn--;
                        // Normal items degenerate twice as quickly when past sell in days.
                        if (Items[i].SellIn < 0)
                        {
                            Items[i].Quality = IncrementItemQuality(Items[i].Quality, -1);
                        }
                        break;
                }

            }
        }


    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
