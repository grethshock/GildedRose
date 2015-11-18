using Xunit;
using GildedRose.Console;
using System.Collections.Generic;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        /// <summary>
        /// Taken out of the original console app and test the results still match.
        /// </summary>
        [Fact]
        public void TestOriginalConsoleApp()
        {
            // Setup items.
            Program.Items = new List<GildedRose.Console.Item>
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
            // Update quality.
            Program.UpdateQuality();

            // Assert each item for expected values.
            foreach(GildedRose.Console.Item it in Program.Items)
            {
                switch(it.Name)
                {
                    case "+5 Dexterity Vest":
                        Assert.True(it.Quality == 19, string.Format("{0} Quality failed", it.Name));
                        Assert.True(it.SellIn == 9, string.Format("{0} SellIn failed", it.Name));
                        break;
                    case "Aged Brie":
                        Assert.True(it.Quality == 1, string.Format("{0} Quality failed", it.Name));
                        Assert.True(it.SellIn == 1, string.Format("{0} SellIn failed", it.Name));
                        break;
                    case "Elixir of the Mongoose":
                        Assert.True(it.Quality == 6, string.Format("{0} Quality failed", it.Name));
                        Assert.True(it.SellIn == 4, string.Format("{0} SellIn failed", it.Name));
                        break;
                    case "Sulfuras, Hand of Ragnaros":
                        Assert.True(it.Quality == 80, string.Format("{0} Quality failed", it.Name));
                        Assert.True(it.SellIn == 0, string.Format("{0} SellIn failed", it.Name));
                        break;
                    case "Backstage passes to a TAFKAL80ETC concert":
                        Assert.True(it.Quality == 21, string.Format("{0} Quality failed", it.Name));
                        Assert.True(it.SellIn == 14, string.Format("{0} SellIn failed", it.Name));
                        break;
                    case "Conjured Mana Cake":
                        // Change after refactor.
                        //Assert.True(it.Quality == 5, string.Format("{0} Quality failed", it.Name));
                        //Assert.True(it.SellIn == 2, string.Format("{0} SellIn failed", it.Name));
                        Assert.True(it.Quality == 4, string.Format("{0} Quality failed", it.Name));
                        Assert.True(it.SellIn == 2, string.Format("{0} SellIn failed", it.Name));
                        break;
                    default:
                        break;
                }
            }

        }

        /// <summary>
        /// Tests standard items, as this is hard coded, name test to show
        /// it is testing the vest.
        /// </summary>
        [Fact]
        public void TestDexterityVest()
        {
            // Test most possible scenario's!
            for (int quality = 50; quality > -1; quality--)
            {
                for (int sellIn = 20; sellIn > -10; sellIn--)
                {
                    // Workout what we expect.
                    int expectedSellIn = sellIn;
                    int expectedQuality = quality;
                    if (expectedQuality > 0)
                    {
                        expectedQuality -= 1;
                    }
                    expectedSellIn -= 1;
                    if (expectedSellIn < 0)
                        if (expectedQuality > 0) expectedQuality -= 1;
                    // Setup items.
                    Program.Items = new List<GildedRose.Console.Item>
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = sellIn, Quality = quality}
                                          };
                    // Update quality.
                    Program.UpdateQuality();
                    // Only 1 item being tested.
                    GildedRose.Console.Item it = Program.Items[0];
                    // Assert each value.
                    Assert.True(it.Quality == expectedQuality, string.Format("{0} Quality failed. Testing sellIn : {1}. quality : {2}", it.Name, sellIn, quality));
                    Assert.True(it.SellIn == expectedSellIn, string.Format("{0} SellIn failed. Testing sellIn : {1}. quality : {2}", it.Name, sellIn, quality));
                }
            }
        }

        /// <summary>
        /// Test method for aged brie. Includes calculation to work out what is expected
        /// after the UpdateQuality method is called.
        /// </summary>
        [Fact]
        public void TestAgedBrie()
        {
            // Test most possible scenario's!
            for (int quality = 50; quality > -1; quality--)
            {
                for (int sellIn = 20; sellIn > -10; sellIn--)
                {
                    // Workout what we expect.
                    int expectedSellIn = sellIn;
                    int expectedQuality = quality;
                    if (expectedQuality < 50)
                    {
                        expectedQuality += 1;
                    }
                    expectedSellIn -= 1;
                    if (expectedSellIn < 0)
                        if (expectedQuality < 50) expectedQuality += 1;
                    // Setup items.
                    Program.Items = new List<GildedRose.Console.Item>
                                          {
                                              new Item {Name = "Aged Brie", SellIn = sellIn, Quality = quality}
                                          };
                    // Update quality.
                    Program.UpdateQuality();
                    // Only 1 item being tested.
                    GildedRose.Console.Item it = Program.Items[0];
                    // Assert each value.
                    Assert.True(it.Quality == expectedQuality, string.Format("{0} Quality failed. Testing sellIn : {1}. quality : {2}", it.Name, sellIn, quality));
                    Assert.True(it.SellIn == expectedSellIn, string.Format("{0} SellIn failed. Testing sellIn : {1}. quality : {2}", it.Name, sellIn, quality));
                }
            }
        }

        /// <summary>
        /// Test method for Backstage passes to a concert. Includes calculation to work out what is expected
        /// after the UpdateQuality method is called.
        /// </summary>
        [Fact]
        public void TestBackstagePasses()
        {
            // Test most possible scenario's!
            for (int quality = 50; quality > -1; quality--)
            {
                for (int sellIn = 20; sellIn > -10; sellIn--)
                {
                    // Workout what we expect.
                    int expectedSellIn = sellIn;
                    int expectedQuality = quality;

                    if (expectedQuality < 50)
                    {
                        expectedQuality += 1;
                    }
                    if (expectedSellIn < 11)
                        if (expectedQuality < 50) expectedQuality += 1;
                    if (expectedSellIn < 6)
                        if (expectedQuality < 50) expectedQuality += 1;
                    expectedSellIn -= 1;
                    if (expectedSellIn < 0)
                    {
                        expectedQuality = 0;
                    }
                    // Setup items.
                    Program.Items = new List<GildedRose.Console.Item>
                                          {
                                              new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = quality}
                                          };
                    // Update quality.
                    Program.UpdateQuality();
                    // Only 1 item being tested.
                    GildedRose.Console.Item it = Program.Items[0];
                    // Assert each value.
                    Assert.True(it.Quality == expectedQuality, string.Format("{0} Quality failed. Testing sellIn : {1}. quality : {2}", it.Name, sellIn, quality));
                    Assert.True(it.SellIn == expectedSellIn, string.Format("{0} SellIn failed. Testing sellIn : {1}. quality : {2}", it.Name, sellIn, quality));
                    
                }
            }

        }

        /// <summary>
        /// Test method for Sulfuras, Hand of Ragnaros. Expect values to never change.
        /// </summary>
        /// <param name="sellIn">Sell in days</param>
        /// <param name="quality">Quality value</param>
        [Theory]
        [InlineData(0, 80)]
        public void TestSulfurasHandofRagnaros(int sellIn, int quality)
        {
            // Workout what we expect.
            int expectedSellIn = sellIn;
            int expectedQuality = quality;
            // Setup items.
            Program.Items = new List<GildedRose.Console.Item>
                                          {
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = sellIn, Quality = quality}
                                          };
            // Update quality.
            Program.UpdateQuality();
            // Only 1 item being tested.
            GildedRose.Console.Item it = Program.Items[0];
            // Assert each value.
            Assert.True(it.Quality == expectedQuality, string.Format("{0} Quality failed. Testing sellIn : {1}. quality : {2}", it.Name, sellIn, quality));
            Assert.True(it.SellIn == expectedSellIn, string.Format("{0} SellIn failed. Testing sellIn : {1}. quality : {2}", it.Name, sellIn, quality));
        }

        #region Conjured Items

        #region Old Method Test
        ///// <summary>
        ///// Tests conjured items. This test should fail after refactor.
        ///// </summary>
        //[Fact]
        //public void TestOriginalConjuredItem()
        //{
        //    // Test most possible scenario's!
        //    for (int quality = 50; quality > -1; quality--)
        //    {
        //        for (int sellIn = 20; sellIn > -10; sellIn--)
        //        {
        //            // Workout what we expect.
        //            int expectedSellIn = sellIn;
        //            int expectedQuality = quality;
        //            if (expectedQuality > 0)
        //            {
        //                expectedQuality -= 1;
        //            }
        //            expectedSellIn -= 1;
        //            if (expectedSellIn < 0)
        //                if (expectedQuality > 0) expectedQuality -= 1;
        //            // Setup items.
        //            Program.Items = new List<GildedRose.Console.Item>
        //                                  {
        //                                      new Item {Name = "Conjured Mana Cake", SellIn = sellIn, Quality = quality}
        //                                  };
        //            // Update quality.
        //            Program.UpdateQuality();
        //            // Only 1 item being tested.
        //            GildedRose.Console.Item it = Program.Items[0];
        //            // Assert each value.
        //            Assert.True(it.Quality == expectedQuality, string.Format("{0} Quality failed. Testing sellIn : {1}. quality : {2}", it.Name, sellIn, quality));
        //            Assert.True(it.SellIn == expectedSellIn, string.Format("{0} SellIn failed. Testing sellIn : {1}. quality : {2}", it.Name, sellIn, quality));
        //        }
        //    }
        //}
        #endregion
        /// <summary>
        /// Tests conjured items after refactoring.
        /// </summary>
        [Fact]
        public void TestRefactoredConjuredItem()
        {
            // Test most possible scenario's!
            for (int quality = 50; quality > -1; quality--)
            {
                for (int sellIn = 20; sellIn > -10; sellIn--)
                {
                    // Workout what we expect.
                    int expectedSellIn = sellIn;
                    int expectedQuality = quality;
                    // Set initial degradation of quality (-2) minimum 0.
                    if (expectedQuality > 1) expectedQuality -= 2;
                    else expectedQuality = 0;
                    // Reduce sell in days.
                    expectedSellIn -= 1;
                    // If negative then reduce in quality again (-2) minimum 0.
                    if (expectedSellIn < 0)
                    {
                        if (expectedQuality > 1) expectedQuality -= 2;
                        else expectedQuality = 0;
                    }
                    // Setup items.
                    Program.Items = new List<GildedRose.Console.Item>
                                          {
                                              new Item {Name = "Conjured Mana Cake", SellIn = sellIn, Quality = quality}
                                          };
                    // Update quality.
                    Program.UpdateQuality();
                    // Only 1 item being tested.
                    GildedRose.Console.Item it = Program.Items[0];
                    // Assert each value.
                    Assert.True(it.Quality == expectedQuality, string.Format("{0} Quality failed. Testing sellIn : {1}. quality : {2}", it.Name, sellIn, quality));
                    Assert.True(it.SellIn == expectedSellIn, string.Format("{0} SellIn failed. Testing sellIn : {1}. quality : {2}", it.Name, sellIn, quality));
                }
            }
        }

        /// <summary>
        /// Tests conjured items. Basic test with hard coded values we know should be correct.
        /// </summary>
        [Fact]
        public void TestConjuredItemOne()
        {
            // Setup items.
            Program.Items = new List<GildedRose.Console.Item>
                                          {
                                              new Item {Name = "Conjured Mana Cake", SellIn = 5, Quality = 10}
                                          };
            // Update quality.
            Program.UpdateQuality();
            // Only 1 item being tested.
            GildedRose.Console.Item it = Program.Items[0];
            // Assert each value (quality degrades twice as fast).
            Assert.True(it.Quality == 8, string.Format("{0} Quality failed. Testing sellIn : {1}. quality : {2}", it.Name, 5, 10));
            Assert.True(it.SellIn == 4, string.Format("{0} SellIn failed. Testing sellIn : {1}. quality : {2}", it.Name, 5, 10));
        }

        /// <summary>
        /// Tests conjured items. Basic test with hard coded values we know should be correct.
        /// </summary>
        [Fact]
        public void TestConjuredItemTwo()
        {
            // Setup items.
            Program.Items = new List<GildedRose.Console.Item>
                                          {
                                              new Item {Name = "Conjured Mana Cake", SellIn = 0, Quality = 10}
                                          };
            // Update quality.
            Program.UpdateQuality();
            // Only 1 item being tested.
            GildedRose.Console.Item it = Program.Items[0];
            // Assert each value (quality degrades twice as fast).
            Assert.True(it.Quality == 6, string.Format("{0} Quality failed. Testing sellIn : {1}. quality : {2}", it.Name, 0, 10));
            Assert.True(it.SellIn == -1, string.Format("{0} SellIn failed. Testing sellIn : {1}. quality : {2}", it.Name, 0, 10));
        }

        /// <summary>
        /// Tests conjured items. Basic test with hard coded values we know should be correct.
        /// </summary>
        [Fact]
        public void TestConjuredItemThree()
        {
            // Setup items.
            Program.Items = new List<GildedRose.Console.Item>
                                          {
                                              new Item {Name = "Conjured Mana Cake", SellIn = 5, Quality = 1}
                                          };
            // Update quality.
            Program.UpdateQuality();
            // Only 1 item being tested.
            GildedRose.Console.Item it = Program.Items[0];
            // Assert each value (quality degrades twice as fast).
            Assert.True(it.Quality == 0, string.Format("{0} Quality failed. Testing sellIn : {1}. quality : {2}", it.Name, 5, 1));
            Assert.True(it.SellIn == 4, string.Format("{0} SellIn failed. Testing sellIn : {1}. quality : {2}", it.Name, 5, 1));
        }

        #endregion

    }
}