using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenShoppingCalculator
{
    public class BasketCalculator
    {
     
        private IPriceProvider _priceProvider;


        public BasketCalculator(IPriceProvider priceProvider)
        {
            
            _priceProvider = priceProvider;
        }
        public double CalculateBasketPrice(Basket _basket)
        {

            var discounts = _basket.Items.SelectMany(x => x.GetDiscounts()).ToList();

            return _basket.Items.Select(x =>
            {
                var discount = discounts.Where(y => y.ItemName == x.ItemName).ToList();

                if (discount != null && discount.Count > 0)
                {
                    return (_priceProvider.GetPrice(x.ItemName) * (x.ItemQty - discount.Count))
                      + (_priceProvider.GetPrice(x.ItemName) * discount.First().ReductionRate * discount.Count());
                }
                else
                {
                    return _priceProvider.GetPrice(x.ItemName) * x.ItemQty;
                }
            }).Sum() / 100;

        }


    }
}
