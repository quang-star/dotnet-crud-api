namespace Exceptions;

public class ConflictException : BaseException
{
    public ConflictException(string message)
        : base(message, StatusCodes.Status409Conflict)
    {
    }
}