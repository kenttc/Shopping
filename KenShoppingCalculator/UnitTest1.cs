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

    

   


    
   



   

    


 

  




}


