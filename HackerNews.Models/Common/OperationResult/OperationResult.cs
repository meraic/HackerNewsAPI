namespace HackerNews.Models.Common.OperationResult;

public class OperationResult
{
    public bool IsSuccess { get; init; }

    public string FailureMessage { get; init; }

    public FailReason? FailureReason { get; init; }

    protected OperationResult()
    {
        IsSuccess = true;
    }

    protected OperationResult(FailReason reason, string message)
    {
        IsSuccess = false;
        FailureReason = reason;
        FailureMessage = message;
    }
    public static OperationResult Success() => new();

    public static OperationResult ValidatonFailed(string message) => new(FailReason.ValidationFailed, message);
    public static OperationResult NotFound(string message) => new(FailReason.NotFound, message);
    public static OperationResult Conflict(string message) => new(FailReason.Conflict, message);
    public static OperationResult InternalServerError(string message = null) => new(FailReason.InternalServerError, message);
    public static OperationResult OperationCancelled(string message = null) => new(FailReason.OperationCancelled, message);

    public static OperationResult NxptServiceUnavailable(string message = null) => new(FailReason.HackerNewServiceUnavailable, message);
    public static OperationResult NxptServiceResponseError(string message = null) => new(FailReason.HackerNewServiceResponseError, message);
}

public class OperationResult<T> : OperationResult
{
    public T Result { get; init; }

    protected OperationResult(T result)
    {
        IsSuccess = true;
        Result = result;
    }

    public OperationResult(FailReason reason, string message) : base(reason, message)
    {
    }

    public OperationResult(OperationResult failedResult) : this(failedResult.FailureReason.Value, failedResult.FailureMessage)
    {
    }

    public static OperationResult<T> Success(T result) => new(result);
    public static OperationResult<T> ValidationFailed(string message) => new(FailReason.ValidationFailed, message);

    public new static OperationResult<T> NotFound(string message) => new(FailReason.NotFound, message);
    public new static OperationResult<T> Conflict(string message) => new(FailReason.Conflict, message);
    public new static OperationResult<T> InternalServerError(string message = null) => new(FailReason.InternalServerError, message);
    public new static OperationResult<T> OperationCancelled(string message = null) => new(FailReason.OperationCancelled, message);

    public new static OperationResult<T> NxptServiceUnavailable(string message = null) => new(FailReason.HackerNewServiceUnavailable, message);
    public new static OperationResult<T> NxptServiceResponseError(string message = null) => new(FailReason.HackerNewServiceResponseError, message);
}
