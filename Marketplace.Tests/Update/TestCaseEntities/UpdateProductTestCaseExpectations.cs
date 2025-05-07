using System.Net;
using Marketplace.Client.Contracts;

namespace Marketplace.Tests.Update.TestCaseEntities;

/// <summary>
///     Ожидаемые значения для тест кейса на обновление данных товара
/// </summary>
public class UpdateProductTestCaseExpectations
{
    /// <summary>
    ///     Код ответа сервера
    /// </summary>
    public HttpStatusCode HttpStatusCode { get; set; }

    /// <summary>
    ///     Текст ошибки
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    ///     Обновлённый товар
    /// </summary>
    public ProductDto? Product { get; set; }
}