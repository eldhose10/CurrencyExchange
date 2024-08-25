using Xunit;
using Moq;
using CurrencyExchange;

namespace CurrencyConverterTests
{
    public class CurrencyConverterTests
    {
        private readonly ICurrencyConverter _currencyConverter;
        private readonly Mock<IExchangeRateService> _exchangeRateServiceMock;

        public CurrencyConverterTests()
        {
            _exchangeRateServiceMock = new Mock<IExchangeRateService>();
            _currencyConverter = new CurrencyConverter(_exchangeRateServiceMock.Object);
        }

        [Fact]
        public void Convert_SameCurrency_ReturnsSameAmount()
        {
            // Arrange
            var currencyPair = "USD/USD";
            var amount = 100m;

            // Act
            var result = _currencyConverter.Convert(currencyPair, amount);

            // Assert
            Assert.Equal(amount, result);
        }

        [Fact]
        public void Convert_DifferentCurrencies_ReturnsConvertedAmount()
        {
            // Arrange
            var currencyPair = "EUR/USD";
            var amount = 100m;

            // Mock
            _exchangeRateServiceMock.Setup(x => x.GetRate("EUR")).Returns(7.2145m);
            _exchangeRateServiceMock.Setup(x => x.GetRate("USD")).Returns(6.3625m);

            // Act
            var result = _currencyConverter.Convert(currencyPair, amount);
             
            var expected = (amount * 7.2145m) / 6.3625m;

            // Assert
            Assert.Equal(expected, result, 4);
        }
    }
}