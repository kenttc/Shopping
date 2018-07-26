using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenShoppingCalculator
{
    public class Discount
    {
        public Discount(EnumProduct itemName, double reductionRate)
        {
            ItemName = itemName;
            ReductionRate = reductionRate;
        }

        public EnumProduct ItemName { get; private set; }
        public double ReductionRate { get; private set; }


    }
}
