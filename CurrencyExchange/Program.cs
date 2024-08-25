using CurrencyExchange;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: Exchange <currency pair> <amount to exchange>");
            return;
        }

        var currencyPair = args[0]; 
        decimal amount;
        bool isValidAmount = decimal.TryParse(args[1], out amount);

        if (!isValidAmount)
        {
            Console.WriteLine("Invalid amount.");
            return;
        }

        try
        {
            IExchangeRateService exchangeRateService = new ExchangeRateService();
            ICurrencyConverter converter = new CurrencyConverter(exchangeRateService);
            var result = converter.Convert(currencyPair, amount);
            Console.WriteLine(result.ToString("0.0000"));
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}