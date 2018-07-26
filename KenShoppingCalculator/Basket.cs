using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenShoppingCalculator
{
    public class Basket
    {

        public Basket()
        {
            Items = new List<IBasketItem>();
        }
        public List<IBasketItem> Items { get; set; }
    }
}
