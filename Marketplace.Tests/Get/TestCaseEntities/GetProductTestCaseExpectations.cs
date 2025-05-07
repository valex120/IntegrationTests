using System.Net;
using Marketplace.Client.Contracts;

namespace Marketplace.Tests.Get.TestCaseEntities;

/// <summary>
///     Ожидаемые значения для тест кейса на получение товара
/// </summary>
public class GetProductTestCaseExpectations
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
    ///     Заведённый товар
    /// </summary>
    public ProductDto? Product { get; set; }
}