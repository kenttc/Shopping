using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using System.Linq;
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
            var pricelistProvider = new PriceProvider();
            var sut = new BasketCalculator(basket, pricelistProvider);
            //act
            var result = sut.CalculateBasketPrice();
            //assert
            Assert.AreEqual(2.95, result);

        }

        [TestMethod]
        public void When_2_bread_2_butter_in_basket_total_should_be_310()
        {
            //arrange
            var basket = new Basket();
            
            basket.Items.Add(new BasketItem("Bread", 2));
            basket.Items.Add(new BasketItem("Butter", 2));
            var pricelistProvider = new PriceProvider();
            var sut = new BasketCalculator(basket, pricelistProvider);
            //act
            var result = sut.CalculateBasketPrice();
            //assert
            Assert.AreEqual(3.1, result);

        }
    }

    public class PriceProvider
    {
        private  Dictionary<string, double> _priceList = new Dictionary<string, double>();

        public PriceProvider()
        {
            _priceList.Add("Milk", 1.15);
            _priceList.Add("Butter", 0.8);
            _priceList.Add("Bread", 1);
        }

        public double GetPrice (string name)
        {
            double amount = 0;
            _priceList.TryGetValue(name, out amount);
            return amount;
        }
    }

    public enum Product
    {
        Butter, 
        Milk, 
        Bread
    }
    public class BasketCalculator
    {
        private Basket _basket;
        private PriceProvider _priceProvider;

        public BasketCalculator(Basket basket, PriceProvider priceProvider)
        {
            _basket = basket;   
            _priceProvider = priceProvider;
        }
        public double CalculateBasketPrice()
        {
            //  var discount = _basket.Items
            var discounts = GetDiscounts(_basket);

            return  _basket.Items.Select(x =>
            {
                return _priceProvider.GetPrice(x.ItemName) * x.ItemQty;
            }).Sum();
             
        }

        public List<Discount> GetDiscounts(Basket basket)
        {
            var butterdiscount =
                basket.Items.Count(x => x.ItemName == Product.Butter.ToString() 
                && x.ItemQty == 2);

            //    .Select(x=> { return x.ItemQty >= 2 ? x.ItemQty % 2 : 0; }).FirstOrDefault();
            
            return new List<Discount>();

        }

    }
    public class Discount
    {
        public Discount(string itemName, double reductionRate)
        {
            ItemName = itemName;
            ReductionRate = reductionRate;
        }

        public string ItemName { get; private set; }
        public double ReductionRate { get; private set; }


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


