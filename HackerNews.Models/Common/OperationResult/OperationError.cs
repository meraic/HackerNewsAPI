namespace HackerNews.Models.Common.OperationResult;

public record OperationError(FailReason FailReason, string Message);
