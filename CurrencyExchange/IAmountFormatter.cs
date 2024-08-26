using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange
{
    public interface IAmountFormatter
    {
        string Format(decimal amount);
    }
}
