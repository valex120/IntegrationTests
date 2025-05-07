using System.Net;
using Marketplace.Client.Contracts;

namespace Marketplace.Tests.List.TestCaseEntities;

/// <summary>
///     Ожидаемые значения для тест кейса на поиск товаров
/// </summary>
public class ListProductTestCaseExpectations
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
    ///     Товары, вошедшие в поиск
    /// </summary>
    public ProductDto[] IncludedProducts { get; set; }

    /// <summary>
    ///     Товары, невошедшие в поиск
    /// </summary>
    public ProductDto[] ExcludedProducts { get; set; }
}