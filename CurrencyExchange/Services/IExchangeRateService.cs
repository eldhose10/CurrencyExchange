using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Services
{
    public interface IExchangeRateService
    {
        decimal GetRate(string currency);
    }
}