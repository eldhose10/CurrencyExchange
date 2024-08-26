using Xunit;
using Moq;
using CurrencyExchange;

namespace CurrencyConverterTests
{
    public class CurrencyConverterTests
    {
        private readonly ICurrencyConverter _currencyConverter;
        private readonly Mock<IAmountFormatter> _amountFormatterMock;
        private readonly Mock<IExchangeRateService> _exchangeRateServiceMock;

        public CurrencyConverterTests()
        {
            _exchangeRateServiceMock = new Mock<IExchangeRateService>();
            _amountFormatterMock = new Mock<IAmountFormatter>();
            _currencyConverter = new CurrencyConverter(_exchangeRateServiceMock.Object, _amountFormatterMock.Object);
        }

        [Fact]
        public void Convert_SameCurrency_ReturnsSameAmount()
        {
            // Arrange
            var currencyPair = "USD/USD";
            var amount = 100m;

            //Mock
             _amountFormatterMock.Setup(x => x.Format(amount)).Returns("100,0000");

            // Act
            var result = _currencyConverter.Convert(currencyPair, amount); 

            // Assert
            Assert.Equal("100,0000", result);
        }

        [Fact]
        public void Convert_DifferentCurrencies_ReturnsConvertedAmount()
        {
            // Arrange
            var currencyPair = "EUR/USD";
            var amount = 100m;

            // Mock
            var expected = (amount * 7.2145m) / 6.3625m;
            _exchangeRateServiceMock.Setup(x => x.GetRate("EUR")).Returns(7.2145m);
            _exchangeRateServiceMock.Setup(x => x.GetRate("USD")).Returns(6.3625m);
            _amountFormatterMock.Setup(x => x.Format(expected)).Returns("113,3909");

            // Act
            var result = _currencyConverter.Convert(currencyPair, amount); 

            // Assert
            Assert.Equal("113,3909", result);
        }
    }
}