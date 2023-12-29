using System.Text.RegularExpressions;
using UBA.Panel.Report.Domain.Interfaces;

namespace UBA.Panel.Report.Infrastructure.Services;

public class VinChecksumCalculator : IVinChecksumCalculator
{
    private readonly Dictionary<char, byte> _transliterationValues = new()
    {
        { 'A', 1 }, { 'B', 2 }, { 'C', 3 }, { 'D', 4 }, { 'E', 5 }, { 'F', 6 }, { 'G', 7 }, { 'H', 8 },
        { 'J', 1 }, { 'K', 2 }, { 'L', 3 }, { 'M', 4 }, { 'N', 5 }, { 'P', 7 }, { 'R', 9 },
        { 'S', 2 }, { 'T', 3 }, { 'U', 4 }, { 'V', 5 }, { 'W', 6 }, { 'X', 7 }, { 'Y', 8 }, { 'Z', 9 }
    };

    private readonly Dictionary<byte, byte> _positionedWeights = new()
    {
        { 0, 8 }, { 1, 7 }, { 2, 6 }, { 3, 5 }, { 4, 4 }, { 5, 3 }, { 6, 2 }, { 7, 10 },
        { 8, 9 }, { 9, 8 }, { 10, 7 }, { 11, 6 }, { 12, 5 }, { 13, 4 }, { 14, 3 }, { 15, 2 }
    };

    public char Calculate(string vin)
    {
        var checksum = 0;
        vin = vin.ToUpper();
        for (byte index = 0; index < vin.Length; index++)
        {
            var @char = vin[index];
            byte charValue;
            if (char.IsNumber(@char))
            {
                charValue = byte.Parse(@char.ToString());
            }
            else
            {
                charValue = _transliterationValues[@char];
            }

            checksum += _positionedWeights[index] * charValue;
        }

        Console.WriteLine($"Checksum: {checksum}");
        var checkDigit = checksum % 11;

        return checkDigit == 10 ? 'X' : checkDigit.ToString()[0];
    }
}