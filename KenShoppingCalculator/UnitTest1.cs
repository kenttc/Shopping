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
            basket.Items.Add(new BasketItem(Product.Milk, 1));
            basket.Items.Add(new BasketItem(Product.Bread, 1));
            basket.Items.Add(new BasketItem(Product.Butter, 1));
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
            
            basket.Items.Add(new BasketItem(Product.Bread, 2));
            basket.Items.Add(new BasketItem(Product.Butter, 2));
            var pricelistProvider = new PriceProvider();
            var sut = new BasketCalculator(basket, pricelistProvider);
            //act
            var result = sut.CalculateBasketPrice();
            //assert
            Assert.AreEqual(3.1, result);

        }

        [TestMethod]
        public void When_4_milk_in_basket_total_should_be_345()
        {
            //arrange
            var basket = new Basket();

            basket.Items.Add(new BasketItem(Product.Milk, 4));
           
            var pricelistProvider = new PriceProvider();
            var sut = new BasketCalculator(basket, pricelistProvider);
            //act
            var result = sut.CalculateBasketPrice();
            //assert
            Assert.AreEqual(3.45, result);

        }

        [TestMethod]
        public void When_2_butter_1_bread_8_milk_in_basket_total_should_be_900()
        {
            //arrange
            var basket = new Basket();

            basket.Items.Add(new BasketItem(Product.Milk, 8));
            basket.Items.Add(new BasketItem(Product.Butter, 2));
            basket.Items.Add(new BasketItem(Product.Bread, 1));

            var pricelistProvider = new PriceProvider();
            var sut = new BasketCalculator(basket, pricelistProvider);
            //act
            var result = sut.CalculateBasketPrice();
            //assert
            Assert.AreEqual(9, result);

        }
    }

    public class PriceProvider
    {
        private  Dictionary<Product, int> _priceList = new Dictionary<Product, int>();

        public PriceProvider()
        {
            _priceList.Add(Product.Milk, 115);
            _priceList.Add(Product.Butter, 80);
            _priceList.Add(Product.Bread, 100);
        }

        public int GetPrice (Product name)
        {
            int amount = 0;
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
        const double breadReductionRateWhen2Butter = 0.5;

        public BasketCalculator(Basket basket, PriceProvider priceProvider)
        {
            _basket = basket;   
            _priceProvider = priceProvider;
        }
        public double CalculateBasketPrice()
        {
            
            var discounts = GetDiscounts(_basket);
            
       
            return  _basket.Items.Select(x =>
            {
                var discount = discounts.Where(y => y.ItemName == x.ItemName).ToList();
                if(discount  != null && discount.Count > 0)
                {
                  return  (_priceProvider.GetPrice(x.ItemName) * (x.ItemQty- discount.Count)) 
                    + (_priceProvider.GetPrice(x.ItemName) *  discount.First().ReductionRate * discount.Count());
                }
                else
                {
                    return _priceProvider.GetPrice(x.ItemName) * x.ItemQty;
                }
            }).Sum() /100;
             
        }

        public List<Discount> GetDiscounts(Basket basket)
        {
            var discounts = new List<Discount>();
            var butterdiscount =
                basket.Items.Where(x => x.ItemName == Product.Butter 
                && x.ItemQty == 2).Select(x=> new Discount(Product.Bread, breadReductionRateWhen2Butter));

            discounts.AddRange(butterdiscount);

            var numDiscountToAdd = basket.Items.Where(x => x.ItemName == Product.Milk)
             .Select(x => Math.Floor(Convert.ToDouble(x.ItemQty / 4))).FirstOrDefault();
          //  var discounts = new List<Discount>();
            for (var i = 0; i < numDiscountToAdd; i++)
            {
                discounts.Add(new Discount(Product.Milk, 0));
            }
            return discounts;

        }

    }

    
    public class Discount
    {
        public Discount(Product itemName, double reductionRate)
        {
            ItemName = itemName;
            ReductionRate = reductionRate;
        }

        public Product ItemName { get; private set; }
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
        public BasketItem(Product name, int qty)
        {
            ItemQty = qty;
            ItemName = name;
        }

        public Product ItemName { get; private set; }

        public int ItemQty { get; private set; }
    }
}


