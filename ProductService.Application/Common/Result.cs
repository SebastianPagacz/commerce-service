namespace ProductService.Application.Common;

public record Result<T>
{
    public T? Value { get; }
    public string? Message { get; }
    public bool IsSuccess { get; }
    public bool IsFail => !IsSuccess;
    
    private Result(T value, bool isSuccess)
    {
        Value = value;
        IsSuccess = isSuccess;
    }
    private Result(string message, bool isSuccess)
    {
        Message = message;
        IsSuccess = isSuccess;
    }

    public static Result<T> Success(T value) => new Result<T>(value, true);
    public static Result<T> Fail(string message) => new Result<T>(message, false);
}