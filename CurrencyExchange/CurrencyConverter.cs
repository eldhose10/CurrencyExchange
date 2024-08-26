using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange
{
    public class CurrencyConverter : ICurrencyConverter
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IAmountFormatter _amountFormatter;

        public CurrencyConverter(IExchangeRateService exchangeRateService, IAmountFormatter amountFormatter)
        {
            _exchangeRateService = exchangeRateService;
            _amountFormatter = amountFormatter;
        }

        public string Convert(string currencyPair, decimal amount)
        {
            try
            {
                var currencies = currencyPair.ToUpper().Split('/');
                if (!IsCurrencyPairValid(currencyPair))
                {
                    throw new ArgumentException("Invalid currency pair format.");
                }
                string sourceCurrency = currencies[0];
                string targetCurrency = currencies[1];

                // Return the same amount if both currencies are the same
                if (sourceCurrency == targetCurrency)
                {
                    return _amountFormatter.Format(amount);
                }

                decimal sourceCurrencyRate = _exchangeRateService.GetRate(sourceCurrency);
                decimal targetCurrencyRate = _exchangeRateService.GetRate(targetCurrency);

                decimal amountInBaseCurrency = amount * sourceCurrencyRate;
                decimal convertedAmount = amountInBaseCurrency / targetCurrencyRate;

                return _amountFormatter.Format(convertedAmount);

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message.ToString());
            }
        }
        private bool IsCurrencyPairValid(string currencyPair)
        {
            var currencies = currencyPair.Split('/');

            if (currencies.Length != 2)
            {
                return false;
            }
            return true;
        }
    }
}