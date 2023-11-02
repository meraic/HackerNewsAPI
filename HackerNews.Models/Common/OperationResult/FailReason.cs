using System.Text.Json.Serialization;

namespace HackerNews.Models.Common.OperationResult;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FailReason
{
    ValidationFailed,
    NotFound,
    Conflict,
    InternalServerError,
    OperationCancelled,

    HackerNewServiceUnavailable,
    HackerNewServiceResponseError
}

