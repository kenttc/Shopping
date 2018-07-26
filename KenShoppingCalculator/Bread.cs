using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenShoppingCalculator
{
    public class Bread : IBasketItem
    {

        public EnumProduct ItemName => EnumProduct.Bread;

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
}
