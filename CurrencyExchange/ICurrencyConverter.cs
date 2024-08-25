using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange
{
    public interface ICurrencyConverter
    {
        decimal Convert(string currencyPair, decimal amount);
    }
}