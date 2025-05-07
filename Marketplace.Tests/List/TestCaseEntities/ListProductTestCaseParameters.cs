using Marketplace.Client.Contracts;

namespace Marketplace.Tests.List.TestCaseEntities;

/// <summary>
///     Входные параметры для тест кейса на поиск товаров
/// </summary>
public class ListProductTestCaseParameters
{
    /// <summary>
    ///     Запрос поиска товаров
    /// </summary>
    public ListProductsRequest Request { get; set; } = default!;
}