using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange
{
    public class AmountFormatter : IAmountFormatter
    { 
        public string Format(decimal amount)
        {
            return string.Format(new CultureInfo("fr-FR"), "{0:#,##0.0000}", amount);
        }
    }
}
