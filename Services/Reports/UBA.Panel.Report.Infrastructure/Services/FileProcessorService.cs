using System.Globalization;
using Azure.Storage.Blobs;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using UBA.Panel.Report.Domain.Data.ValueObjects;
using UBA.Panel.Report.Domain.Interfaces;
using UBA.Panel.Report.Infrastructure.ClassMaps;

namespace UBA.Panel.Report.Infrastructure.Services;

public class FileProcessorService : IFileProcessorService
{
    private readonly IReportsRepository _repository;
    private readonly BlobContainerClient _sourceContainerClient;
    private readonly BlobContainerClient _targetContainerClient;
    private readonly ILogger<FileProcessorService> _logger;
    private readonly IVinChecksumCalculator _vinChecksumCalculator;

    public FileProcessorService(
        BlobContainerClient sourceContainerClient,
        BlobContainerClient targetContainerClient,
        IReportsRepository repository,
        ILogger<FileProcessorService> logger,
        IVinChecksumCalculator vinChecksumCalculator)
    {
        _sourceContainerClient = sourceContainerClient;
        _targetContainerClient = targetContainerClient;
        _repository = repository;
        _logger = logger;
        _vinChecksumCalculator = vinChecksumCalculator;
    }
    
    public async Task Process(Guid reportId, string fileName, Stream file)
    {
        using var streamReader = new StreamReader(file);
        using var csvReader = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ","
        });
        csvReader.Context.RegisterClassMap(typeof(ReportItemClassMap));
        
        await ParseFile(reportId, fileName, csvReader);
        var position = streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
        streamReader.BaseStream.Position = position;
        await MoveFile(reportId, fileName, streamReader);
    }

    private async Task ParseFile(Guid reportId, string fileName, CsvReader csvReader)
    {
        var report = await _repository.GetReportAsync((r) => r.Id == reportId);

        if (report == null)
        {
            throw new NullReferenceException(nameof(report));
        }

        await foreach (var record in csvReader.GetRecordsAsync<ReportItem>())
        {
            record.CreatedAt = DateTime.UtcNow;
            record.Source = fileName;
            report.AddReportItem(record, _vinChecksumCalculator);
        }

        await _repository.UpdateReportAsync(report);
    }

    private async Task MoveFile(Guid reportId, string fileName, StreamReader streamReader)
    {
        var sourceBlobClient = _sourceContainerClient.GetBlobClient($"{reportId}/{fileName}");
        var targetBlobClient = _targetContainerClient.GetBlobClient($"{reportId}/{fileName}");
        
        await targetBlobClient.UploadAsync(streamReader.BaseStream);
        await sourceBlobClient.DeleteAsync();
    }

    public Task Process(Guid reportId, string fileName, MemoryStream memoryStream)
    {
        throw new NotImplementedException();
    }
}