using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly Dictionary<string, decimal> rates = new Dictionary<string, decimal>
        {
            { "EUR", 743.94m },
            { "USD", 663.11m },
            { "GBP", 852.85m },
            { "SEK", 76.10m },
            { "NOK", 78.40m },
            { "CHF", 683.58m },
            { "JPY", 5.9740m },
            { "DKK", 100m }//Base Currency
        };

        public decimal GetRate(string currency)
        {
            //Get exchange rate for specified currency
            if (rates.TryGetValue(currency, out var rate))
            {
                return rate;
            }
            throw new ArgumentException("Unknown currency pair.");
        }
    }
}