using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenShoppingCalculator
{

    public class Butter : IBasketItem
    {
        const double breadReductionRateWhen2Butter = 0.5;
        const int minButterCountForBreadDiscount = 2;

        public EnumProduct ItemName => EnumProduct.Butter;

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
                discounts.Add(new Discount(EnumProduct.Bread, breadReductionRateWhen2Butter));
            }
            return discounts;
        }
    }
}
