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
            //if this could be refactored out into another service that uses a DiscountProvider then this would be a poco which i think would be nice
            return new List<Discount>();
        }
    }
}
