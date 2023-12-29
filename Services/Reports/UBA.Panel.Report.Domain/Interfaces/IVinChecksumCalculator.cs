namespace UBA.Panel.Report.Domain.Interfaces;

public interface IVinChecksumCalculator
{
    char Calculate(string vin);
}