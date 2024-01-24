
using UBA.Panel.Report.Infrastructure.Services;

namespace UBA.Panel.Report.Infrastructure.Tests.Services
{
    public class VinChecksumCalculatorTests
    {
        private readonly VinChecksumCalculator _calculator = new();

        [Theory]
        [InlineData("5GAEV2388J273707", '7')]
        [InlineData("1G6ET1201B146065", '9')]
        [InlineData("2C4RC1CGFR521418", '3')]
        [InlineData("2G1WX12XS9131791", '9')]
        public void Calculate_ShouldCalculateCorrectVinNumber(string vin, char checksum)
        {
            // Act
            var result = _calculator.Calculate(vin);

            // Assert
            Assert.Equal(checksum, result);
        }
    }
}
