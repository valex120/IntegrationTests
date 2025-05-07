using System.Net;

namespace Marketplace.Client.Exceptions;

/// <summary>
///     Ошибка запроса к API
/// </summary>
public sealed class ApiException : Exception
{
    /// <summary>
    ///     Код ответа
    /// </summary>
    public HttpStatusCode Code { get; }

    public ApiException(HttpStatusCode code, string message) : base(message)
    {
        Code = code;
    }
}