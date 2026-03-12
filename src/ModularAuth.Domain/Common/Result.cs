using System;

namespace ModularAuth.Domain.Common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public string? Error { get; }
    public string? ErrorCode { get; }

    public Result(bool isSuccess, T? value, string? error, string? errorCode)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        ErrorCode = errorCode;
    }

    // Creates a successful result.
    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, null, null);
    }
    
    // Creates a failure result.
    public static Result<T> Failure(string error, string? errorCode = null)
    {
        return new Result<T>(false, default, error, errorCode);
    }
}
