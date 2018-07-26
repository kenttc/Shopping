using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenShoppingCalculator
{
    public class PriceProvider : IPriceProvider
    {
        private Dictionary<EnumProduct, int> _priceList = new Dictionary<EnumProduct, int>();

        public PriceProvider()
        {
            _priceList.Add(EnumProduct.Milk, 115);
            _priceList.Add(EnumProduct.Butter, 80);
            _priceList.Add(EnumProduct.Bread, 100);
        }

        public int GetPrice(EnumProduct name)
        {
            int amount = 0;
            _priceList.TryGetValue(name, out amount);
            return amount;
        }
    }

    public interface IPriceProvider
    {
        int GetPrice(EnumProduct name);
    }
}
