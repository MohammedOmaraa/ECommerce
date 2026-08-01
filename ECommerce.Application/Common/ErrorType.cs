
using System.Text.Json.Serialization;

namespace ECommerce.Application.Common
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ErrorType
    {
        Failure = 0,
        Validation = 1,
        NotFound = 2,
        Unauthorized = 3,
        Forbidden = 4,
        Conflict = 5,
        InternalServerError = 6,
        BadRequest = 7,
        InvalidCredentials = 8,
    }
}
