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
            basket.Items.Add(new Milk(1));
            basket.Items.Add(new Bread(1));
            basket.Items.Add(new Butter(1));
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
            
            basket.Items.Add(new Bread(2));
            basket.Items.Add(new Butter(2));
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

            basket.Items.Add(new Milk(4));
           
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

            basket.Items.Add(new Milk(8));
            basket.Items.Add(new Butter(2));
            basket.Items.Add(new Bread(1));

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
        private  Dictionary<enumProduct, int> _priceList = new Dictionary<enumProduct, int>();

        public PriceProvider()
        {
            _priceList.Add(enumProduct.Milk, 115);
            _priceList.Add(enumProduct.Butter, 80);
            _priceList.Add(enumProduct.Bread, 100);
        }

        public int GetPrice (enumProduct name)
        {
            int amount = 0;
            _priceList.TryGetValue(name, out amount);
            return amount;
        }
    }

    public enum enumProduct
    {
        Butter, 
        Milk, 
        Bread
    }


    public class Milk :  IBasketItem
    {
        const int minMilkFor1ForFree = 4;
        const int zeroValue = 0;
        public enumProduct ItemName => enumProduct.Milk;
        public int ItemQty { get; private set; }

        public  Milk(int qty)
        {
            ItemQty = qty;
            
        }
        
        public List<Discount> GetDiscounts()
        {
                    
            var discounts = new List<Discount>();
            for (var i = 0; i < Math.Floor(Convert.ToDouble(this.ItemQty / minMilkFor1ForFree)); i++)
            {
                discounts.Add(new Discount(enumProduct.Milk, zeroValue));
            }
            return discounts;
        }
    }
    public class Bread : IBasketItem
    {
      
        public enumProduct ItemName => enumProduct.Bread;

        public int ItemQty { get; private set; }

        public Bread(int qty)
        {
            ItemQty = qty;

        }

        public List<Discount> GetDiscounts()
        {

         return new List<Discount>();
        }
    }

    public class Butter : IBasketItem
    {
        const double breadReductionRateWhen2Butter = 0.5;
       const int minButterCountForBreadDiscount = 2;

        public enumProduct ItemName => enumProduct.Butter;

        public int ItemQty { get; private set; }

        public Butter(int qty)
        {
            ItemQty = qty;

        }

        public List<Discount> GetDiscounts()
        {
            var discounts = new List<Discount>();
            for (var i = 0; i < Math.Floor(Convert.ToDouble(this.ItemQty / minButterCountForBreadDiscount)); i++)
            {
                discounts.Add(new Discount(enumProduct.Bread, breadReductionRateWhen2Butter));
            }
            return discounts;
        }
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

            var discounts = _basket.Items.SelectMany(x => x.GetDiscounts()).ToList();

      

            return  _basket.Items.Select(x =>
            {
                var discount = discounts.Where(y => y.ItemName == x.ItemName).ToList();
               
                if (discount  != null && discount.Count > 0)
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

     
    }

    public static class DiscountExtensions
    {
        const double breadReductionRateWhen2Butter = 0.5;
        const int zeroValue = 0;
        const int minMilkFor1ForFree = 4;
        const int minButterCountForBreadDiscount = 2;

        public static List<Discount> GetMilkDiscounts(this Basket basket)
        {

            var numDiscountToAdd = basket.Items.Where(x => x.ItemName == enumProduct.Milk)
                .Select(x => Math.Floor(Convert.ToDouble(x.ItemQty / minMilkFor1ForFree))).FirstOrDefault();
            var discounts = new List<Discount>();
            for (var i = 0; i < numDiscountToAdd; i++)
            {
                discounts.Add(new Discount(enumProduct.Milk, zeroValue));
            }
            return discounts;
        }


        public static List<Discount> GetButterDiscount(this Basket basket)
        {
            return basket.Items.Where(x => x.ItemName == enumProduct.Butter
                         && x.ItemQty == minButterCountForBreadDiscount)
                         .Select(x => new Discount(enumProduct.Bread, breadReductionRateWhen2Butter)).ToList();
            
        }
    }
    public class Discount
    {
        public Discount(enumProduct itemName, double reductionRate)
        {
            ItemName = itemName;
            ReductionRate = reductionRate;
        }

        public enumProduct ItemName { get; private set; }
        public double ReductionRate { get; private set; }


    }

    public class Basket
    {

        public Basket()
        {
            Items = new List<IBasketItem>();
        }
        public List<IBasketItem> Items { get; set; }
    }

    public interface IBasketItem 
    {

        enumProduct ItemName { get; }

        int ItemQty { get; }
        List<Discount> GetDiscounts();

    }




}


