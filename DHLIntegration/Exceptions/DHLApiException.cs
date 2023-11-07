namespace DHLIntegration.Exceptions;

public class DHLApiException : Exception
{
    public DHLApiException(string message, Exception innerException) : base(message, innerException) { }
}