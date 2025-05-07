using System.Net;

namespace Marketplace.Tests.Create.TestCaseEntities;

/// <summary>
///     Ожидаемые значения для тест кейса на удаление товара
/// </summary>
public class DeleteProductTestCaseExpectations
{
    /// <summary>
    ///     Код ответа сервера
    /// </summary>
    public HttpStatusCode HttpStatusCode { get; set; }

    /// <summary>
    ///     Текст ошибки
    /// </summary>
    public string? Error { get; set; }
}