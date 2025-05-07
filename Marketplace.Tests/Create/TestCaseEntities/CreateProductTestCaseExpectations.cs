using System.Net;
using Marketplace.Client.Contracts;

namespace Marketplace.Tests.Create.TestCaseEntities;

/// <summary>
///     Ожидаемые значения для тест кейса на создание товара
/// </summary>
public class CreateProductTestCaseExpectations
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
    ///     Значение хедера Location в ответе сервера
    /// </summary>
    public string? LocationHeader { get; set; }

    /// <summary>
    ///     Заведённый товар
    /// </summary>
    public ProductDto? Product { get; set; }
}