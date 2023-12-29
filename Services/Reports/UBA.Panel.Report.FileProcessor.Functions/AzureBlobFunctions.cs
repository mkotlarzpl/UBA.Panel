using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using UBA.Panel.Report.Domain.Interfaces;

namespace UBA.Panel.Report.FileProcessor.Functions;

public class AzureBlobFunctions
{
    private readonly IFileProcessorService _fileProcessor;

    public AzureBlobFunctions(IFileProcessorService fileProcessor)
    {
        _fileProcessor = fileProcessor;
    }

    [FunctionName("ProcessFileFunction")]
    public async Task ProcessFileFunction([BlobTrigger("to-process/{reportId}/{fileName}",
            Connection = "FileStore:ConnectionString")]
        Stream file, string reportId, string fileName, ILogger log)
    {
        await _fileProcessor.Process(Guid.Parse(reportId), fileName, file);
    }
}