namespace MyApp.Modules.Shared.Exceptions;

public class NotValidException : Exception
{
    public NotValidException(string? message) : base(message)
    {
    }
}
