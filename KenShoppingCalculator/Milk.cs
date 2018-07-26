using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenShoppingCalculator
{
    public class Milk : IBasketItem
    {
        const int minMilkFor1ForFree = 4;
        const int zeroValue = 0;
        public EnumProduct ItemName => EnumProduct.Milk;
        public int ItemQty { get; private set; }

        public Milk(int qty)
        {
            ItemQty = qty;

        }

        public List<Discount> GetDiscounts()
        {
            //if this could be refactored out into another service that uses a DiscountProvider then this would be a poco which i think would be nice
            var discounts = new List<Discount>();
            for (var i = 0; i < Math.Floor(Convert.ToDouble(this.ItemQty / minMilkFor1ForFree)); i++)
            {
                discounts.Add(new Discount(EnumProduct.Milk, zeroValue));
            }
            return discounts;
        }
    }
}
