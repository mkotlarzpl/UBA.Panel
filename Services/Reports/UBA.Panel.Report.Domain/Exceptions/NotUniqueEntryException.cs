namespace UBA.Panel.Report.Domain.Exceptions;

public class NotUniqueEntryException : Exception
{
    public NotUniqueEntryException(string message) 
        : base($"Not unique entry exception: {message}")
    {
    }
}