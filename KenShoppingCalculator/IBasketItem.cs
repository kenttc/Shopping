using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenShoppingCalculator
{
    public interface IBasketItem
    {

        EnumProduct ItemName { get; }

        int ItemQty { get; }
        List<Discount> GetDiscounts();

    }
}
