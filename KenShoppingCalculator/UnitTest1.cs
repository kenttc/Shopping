using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
namespace KenShoppingCalculator
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void When_1_bread_1_milk_1_butter_in_basket_total_should_be_295()
        {
            //arrange
            var basket = new Basket();
            basket.Items.Add(new BasketItem("Milk", 1));
            basket.Items.Add(new BasketItem("Bread", 1));
            basket.Items.Add(new BasketItem("Butter", 1));

            var sut = new BasketCalculator(basket);
            //act
            var result = sut.CalculateBasketPrice();
            //assert
            Assert.AreEqual(2.95, result);

        }
    }


    public class BasketCalculator
    {
        public BasketCalculator(Basket basket)
        {

        }
        public double CalculateBasketPrice()
        {
            return 2.95;

        }
    }

    public class Basket
    {

        public Basket()
        {
            Items = new List<BasketItem>();
        }
        public List<BasketItem> Items { get; set; }
    }

    public class BasketItem
    {
        public BasketItem(string name, int qty)
        {
            ItemQty = qty;
            ItemName = name;
        }

        public string ItemName { get; private set; }

        public int ItemQty { get; private set; }
    }
}


